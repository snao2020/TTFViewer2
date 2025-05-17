using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Win32;

namespace WindowsHooks
{
    public abstract class WindowsHook
    {
        readonly Int32 HookType;
        IntPtr HHook;

        protected Window Window { get; private set; }


        protected WindowsHook(Int32 hookType)
        {
            HookType = hookType;
        }


        public void Hook(Window window)
        {
            Window = window;
            DoHook();
        }


        public void Unhook()
        {
            DoUnhook();
            Window = null;
        }


        protected abstract User32.HookProc GetHookProc();


        protected virtual void DoHook()
        {
            if (HHook == IntPtr.Zero)
            {
                uint threadId = Kernel32.GetCurrentThreadId();
                HHook = User32.SetWindowsHookExW(HookType, GetHookProc(), IntPtr.Zero, threadId);
            }
        }


        protected virtual void DoUnhook()
        {
            if (HHook != IntPtr.Zero)
            {
                User32.UnhookWindowsHookEx(HHook);
                HHook = IntPtr.Zero;
            }
        }


        protected bool IsInWindow(IntPtr hwnd)
        {
            return Window != null && new WindowInteropHelper(Window).Handle == hwnd;
        }


        protected IntPtr CallNextHook(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            return User32.CallNextHookEx(HHook, nCode, wParam, lParam);
        }
    }


    //----------------------------------------------------------------------------------
    //
    //  CallWndProc
    //


    public delegate void CallWndProcHandler(IntPtr hwnd, UIntPtr wParam, IntPtr lParam);


    public class CallWndProcHook : WindowsHook
    {
        Dictionary<UInt32, (CallWndProcHandler, bool)> MessageHandlers;

        User32.HookProc HookProc;
                              

        public CallWndProcHook()
            : base(User32.WH_CALLWNDPROC)
        {
            MessageHandlers = new Dictionary<uint, (CallWndProcHandler, bool)>();
            HookProc = MessageHookProc;
        }


        public void AddHandler(UInt32 message, CallWndProcHandler handler, bool isInWindow)
        {
            MessageHandlers.Add(message, (handler, isInWindow));
        }


        public void RemoveHandler(UInt32 message)
        {
            MessageHandlers.Remove(message);
        }


        protected override User32.HookProc GetHookProc()
        {
            return HookProc;
        }


        IntPtr MessageHookProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                User32.CWPSTRUCT cwp = (User32.CWPSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32.CWPSTRUCT));
                if (MessageHandlers.TryGetValue(cwp.message, out var value))
                {
                    if(!value.Item2 || IsInWindow(cwp.hwnd))
                        value.Item1(cwp.hwnd, cwp.wParam, cwp.lParam);
                }
            }
            return CallNextHook(nCode, wParam, lParam);
        }
    }


    //----------------------------------------------------------------------------------
    //
    //  MsgFilterProc
    //

    // return cancel
    public delegate bool MsgFilterProcHandler(IntPtr hwnd, UIntPtr wParam, IntPtr lParam);


    public class MsgFilterProcHook : WindowsHook
    {
        Dictionary<UInt32, (MsgFilterProcHandler, bool)> MessageHandlers;

        User32.HookProc HookProc;


        public MsgFilterProcHook()
            : base(User32.WH_MSGFILTER)
        {
            MessageHandlers = new Dictionary<uint, (MsgFilterProcHandler, bool)>();
            HookProc = MessageHookProc;
        }


        public void AddHandler(UInt32 message, MsgFilterProcHandler handler, bool isInWindow)
        {
            MessageHandlers.Add(message, (handler, isInWindow));
        }


        public void RemoveHandler(UInt32 message)
        {
            MessageHandlers.Remove(message);
        }


        protected override User32.HookProc GetHookProc()
        {
            return HookProc;
        }


        IntPtr MessageHookProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;
            if (lParam != IntPtr.Zero)
            {
                User32.MSG msg = (User32.MSG)Marshal.PtrToStructure(lParam, typeof(User32.MSG));
                if(MessageHandlers.TryGetValue(msg.message, out var value))
                {
                    if (!value.Item2 || IsInWindow(msg.hwnd))
                        cancel = value.Item1(msg.hwnd, msg.wParam, msg.lParam);
                }
            }
            return cancel ? (IntPtr)1 : CallNextHook(nCode, wParam, lParam);
        }
    }
}
