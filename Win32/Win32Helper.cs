using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
    public static class Win32Helper
    {
        public static WinDef.POINT TopLeft(WinDef.RECT rc)
        {
            return new WinDef.POINT { x = rc.left, y = rc.top };
        }

        public static WinDef.POINT BottomRight(WinDef.RECT rc)
        {
            return new WinDef.POINT { x = rc.right, y = rc.bottom };
        }

        public static WinDef.SIZE Size(WinDef.RECT rc)
        {
            return new WinDef.SIZE { cx = rc.right - rc.left, cy = rc.bottom - rc.top };
        }

        public static WinDef.POINT LParamToPoint(IntPtr lParam)
        {
            int x, y;
            unchecked
            {
                x = (short)((ulong)lParam & 0xFFFF);
                y = (short)(((ulong)lParam >> 16) & 0xFFFF);
            }
            return new WinDef.POINT { x = x, y = y };
        }
    }


    public class Win32Exception : Exception
    {
        public Win32Exception()
        {
        }

        public Win32Exception(string message)
            : base(message)
        {
        }
    }


    public class StringSafeHandle : SafeHandle
    {
        public override bool IsInvalid => handle == IntPtr.Zero;

        bool IsUnicode;


        public StringSafeHandle(Int32 length, bool isUnicode)
            : base(IntPtr.Zero, true)
        {
            if(length > 0)
                handle = Marshal.AllocHGlobal(length * (isUnicode ? 2 : 1));

            IsUnicode = isUnicode;
        }


        public StringSafeHandle(string text, bool isUnicode)
            : base(IntPtr.Zero, true)
        {
            if (text != null)
            {
                if (isUnicode)
                    handle = Marshal.StringToHGlobalUni(text);
                else
                    handle = Marshal.StringToHGlobalAnsi(text);
            }

            IsUnicode = isUnicode;
        }


        public static implicit operator IntPtr(StringSafeHandle ssh)
        {
            return ssh.handle;
        }


        public override string ToString()
        {
            if (IsInvalid)
                return null;
            else if (IsUnicode)
                return Marshal.PtrToStringUni(handle);
            else
                return Marshal.PtrToStringAnsi(handle);
        }


        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }


    public class Option<T> : SafeHandle where T : struct
    {
        public override bool IsInvalid => handle == IntPtr.Zero; // (IntPtr)(-1);


        public Option(T t)
            : base(IntPtr.Zero, true)
        {
            handle = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
            Marshal.StructureToPtr(t, handle, false);
        }


        public static implicit operator IntPtr(Option<T> optionT)
        {
            return optionT.handle;
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
