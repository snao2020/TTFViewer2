using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
#pragma warning disable IDE1006

    public static class WinDef
    {
        // #define MAKEWORD(a, b)      ((WORD)(((BYTE)(((DWORD_PTR)(a)) & 0xff)) | ((WORD)((BYTE)(((DWORD_PTR)(b)) & 0xff))) << 8))
        public static UInt16 MAKEWORD(IntPtr a, IntPtr b)
        {
            return ((UInt16)(((Byte)(((UInt32)a) & 0xff)) | ((UInt16)((Byte)(((UInt32)b) & 0xff))) << 8));
        }

        // #define MAKEWORD(a, b)      ((WORD)(((BYTE)(((DWORD_PTR)(a)) & 0xff)) | ((WORD)((BYTE)(((DWORD_PTR)(b)) & 0xff))) << 8))
        public static Int32 MAKELONG(IntPtr a, IntPtr b)
        {
            return (Int32)(((UInt16)(((UInt32)(a)) & 0xffff)) | ((UInt32)((UInt16)(((UInt32)(b)) & 0xffff))) << 16);
        }

        // #define LOWORD(l)           ((WORD)(((DWORD_PTR)(l)) & 0xffff))
        public static UInt16 LOWORD(IntPtr l)
        {
            return ((UInt16)(((UInt32)(l)) & 0xffff));
        }
        public static UInt16 LOWORD(UIntPtr l)
        {
            return ((UInt16)(((UInt32)(l)) & 0xffff));
        }

        // #define HIWORD(l)           ((WORD)((((DWORD_PTR)(l)) >> 16) & 0xffff))
        public static UInt16 HIWORD(IntPtr l)
        {
            return ((UInt16)((((UInt32)(l)) >> 16) & 0xffff));
        }

        public static UInt16 HIWORD(UIntPtr l)
        {
            return ((UInt16)((((UInt32)(l)) >> 16) & 0xffff));
        }

        // #define LOBYTE(w)           ((BYTE)(((DWORD_PTR)(w)) & 0xff))
        public static Byte LOBYTE(IntPtr w)
        {
            return ((Byte)(((UInt32)(w)) & 0xff));
        }

        //#define HIBYTE(w)           ((BYTE)((((DWORD_PTR)(w)) >> 8) & 0xff))
        public static Byte HIBYTE(IntPtr w)
        {
            return ((Byte)((((UInt32)(w)) >> 8) & 0xff));
        }


        public struct RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }


        public struct RECTL       /* rcl */
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }


        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }


        public struct POINTL      /* ptl  */
        {
            public Int32 x;
            public Int32 y;
        }


        public struct SIZE
        {
            public Int32 cx;
            public Int32 cy;
        }


        public struct POINTS
        {
            public Int16 y;
            public Int16 x;
        }
    }
#pragma warning restore IDE1006
}
