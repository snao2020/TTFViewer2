// source = 10.0.15063.0

//#define WIN32_WINNT_WIN10_LATER
#define OEMRESOURCE
#define _WINGDI_


#if WIN32_WINNT_WIN10_LATER         // 0x0A00
#define WIN32_WINNT_WINBLUE_LATER
#endif
#if WIN32_WINNT_WINBLUE_LATER       // 0x0603
#define WIN32_WINNT_WIN8_LATER
#endif
#if WIN32_WINNT_WIN8_LATER	        // 0x0602
#define WIN32_WINNT_WIN7_LATER
#endif
#if WIN32_WINNT_WIN7_LATER			// 0x0601
#define WIN32_WINNT_VISTA_LATER
#endif
#if WIN32_WINNT_VISTA_LATER		    // 0x0600
#define WIN32_WINNT_WS03_LATER
#endif
#if WIN32_WINNT_WS03_LATER			// 0x0502
#define WIN32_WINNT_WINXP_LATER
#endif
#if WIN32_WINNT_WINXP_LATER		    // 0x0501
#define WIN32_WINNT_WIN2K_LATER
#endif
#if WIN32_WINNT_WIN2K_LATER		    // 0x0500
#define WIN32_WINNT_NT4_LATER       // 0x0400
#endif
#if !WIN32_WINNT_NT4_LATER
#define INVALIDWINDOWSVERSION
#endif

using System;
using System.Runtime.InteropServices;

namespace Win32
{
#pragma warning disable IDE1006

    public static partial class User32
    {
                   
        //typedef LRESULT(CALLBACK* WNDPROC)(HWND, UINT, WPARAM, LPARAM);
        public delegate IntPtr WNDPROC(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam);

        //typedef INT_PTR(CALLBACK* DLGPROC)(HWND, UINT, WPARAM, LPARAM);
        public delegate IntPtr DLGPROC(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam);


		//typedef VOID(CALLBACK* TIMERPROC)(HWND, UINT, UINT_PTR, DWORD);
		public delegate void TIMERPROC(IntPtr hwnd, UInt32 message, UIntPtr idEvent, UInt32 dwTime);
        //typedef BOOL(CALLBACK* GRAYSTRINGPROC)(HDC, LPARAM, int);

        //typedef BOOL(CALLBACK* WNDENUMPROC)(HWND, LPARAM);
        //typedef LRESULT(CALLBACK* HOOKPROC)(int code, WPARAM wParam, LPARAM lParam);
        public delegate IntPtr HookProc(Int32 nCode, UIntPtr wParam, IntPtr lParam);
        //typedef VOID(CALLBACK* SENDASYNCPROC)(HWND, UINT, ULONG_PTR, LRESULT);

        //typedef BOOL(CALLBACK* PROPENUMPROCA)(HWND, LPCSTR, HANDLE);
        //typedef BOOL(CALLBACK* PROPENUMPROCW)(HWND, LPCWSTR, HANDLE);

        //typedef BOOL(CALLBACK* PROPENUMPROCEXA)(HWND, LPSTR, HANDLE, ULONG_PTR);
        //typedef BOOL(CALLBACK* PROPENUMPROCEXW)(HWND, LPWSTR, HANDLE, ULONG_PTR);

        //typedef int (CALLBACK* EDITWORDBREAKPROCA) (LPSTR lpch, int ichCurrent, int cch, int code);
        //typedef int (CALLBACK* EDITWORDBREAKPROCW) (LPWSTR lpch, int ichCurrent, int cch, int code);

#if WIN32_WINNT_NT4_LATER
        //typedef BOOL(CALLBACK* DRAWSTATEPROC)(HDC hdc, LPARAM lData, WPARAM wData, int cx, int cy);
#endif // WIN32_WINNT_NT4_LATER


        //typedef BOOL(CALLBACK* NAMEENUMPROCA)(LPSTR, LPARAM);
        //typedef BOOL(CALLBACK* NAMEENUMPROCW)(LPWSTR, LPARAM);

        //typedef NAMEENUMPROCA   WINSTAENUMPROCA;
        //typedef NAMEENUMPROCA   DESKTOPENUMPROCA;
        //typedef NAMEENUMPROCW   WINSTAENUMPROCW;
        //typedef NAMEENUMPROCW   DESKTOPENUMPROCW;
        

        //#define IS_INTRESOURCE(_r) ((((ULONG_PTR)(_r)) >> 16) == 0)
        //#define MAKEINTRESOURCEA(i) ((LPSTR)((ULONG_PTR)((WORD)(i))))
        //#define MAKEINTRESOURCEW(i) ((LPWSTR)((ULONG_PTR)((WORD)(i))))

        public static IntPtr MAKEINTRESOURCE(Int32 i)
		{
			return (IntPtr)((UInt16)i);
		}


        #region RESOURCE

        public static readonly IntPtr RT_CURSOR			= MAKEINTRESOURCE(1);
        public static readonly IntPtr RT_BITMAP			= MAKEINTRESOURCE(2);
        public static readonly IntPtr RT_ICON			= MAKEINTRESOURCE(3);
        public static readonly IntPtr RT_MENU			= MAKEINTRESOURCE(4);
        public static readonly IntPtr RT_DIALOG			= MAKEINTRESOURCE(5);
        public static readonly IntPtr RT_STRING			= MAKEINTRESOURCE(6);
        public static readonly IntPtr RT_FONTDIR		= MAKEINTRESOURCE(7);
        public static readonly IntPtr RT_FONT			= MAKEINTRESOURCE(8);
        public static readonly IntPtr RT_ACCELERATOR	= MAKEINTRESOURCE(9);
        public static readonly IntPtr RT_RCDATA			= MAKEINTRESOURCE(10);
        public static readonly IntPtr RT_MESSAGETABLE	= MAKEINTRESOURCE(11);

        public const Int32 DIFFERENCE = 11;

        public static readonly IntPtr RT_GROUP_CURSOR	= MAKEINTRESOURCE(RT_CURSOR.ToInt32() + DIFFERENCE);
        public static readonly IntPtr RT_GROUP_ICON		= MAKEINTRESOURCE(RT_ICON.ToInt32() + DIFFERENCE);
        public static readonly IntPtr RT_VERSION		= MAKEINTRESOURCE(16);
        public static readonly IntPtr RT_DLGINCLUDE		= MAKEINTRESOURCE(17);
#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr RT_PLUGPLAY		= MAKEINTRESOURCE(19);
        public static readonly IntPtr RT_VXD			= MAKEINTRESOURCE(20);
        public static readonly IntPtr RT_ANICURSOR		= MAKEINTRESOURCE(21);
        public static readonly IntPtr RT_ANIICON		= MAKEINTRESOURCE(22);
#endif // WIN32_WINNT_NT4_LATER
        public static readonly IntPtr RT_HTML			= MAKEINTRESOURCE(23);

        public static readonly IntPtr RT_MANIFEST		= MAKEINTRESOURCE(24);
        public static readonly IntPtr CREATEPROCESS_MANIFEST_RESOURCE_ID	= MAKEINTRESOURCE( 1);
        public static readonly IntPtr ISOLATIONAWARE_MANIFEST_RESOURCE_ID	= MAKEINTRESOURCE(2);
        public static readonly IntPtr ISOLATIONAWARE_NOSTATICIMPORT_MANIFEST_RESOURCE_ID = MAKEINTRESOURCE(3);
        public static readonly IntPtr MINIMUM_RESERVED_MANIFEST_RESOURCE_ID	= MAKEINTRESOURCE( 1 /*inclusive*/);
        public static readonly IntPtr MAXIMUM_RESERVED_MANIFEST_RESOURCE_ID	= MAKEINTRESOURCE(16 /*inclusive*/);

        #endregion RESOURCE


        /*
		WINUSERAPI int WINAPI wvsprintfA(
			_Out_ LPSTR,
			_In_ _Printf_format_string_ LPCSTR,
			_In_ va_list arglist);

		WINUSERAPI int WINAPI wvsprintfW(
			_Out_ LPWSTR,
			_In_ _Printf_format_string_ LPCWSTR,
			_In_ va_list arglist);


		WINUSERAPI int WINAPIV wsprintfA(
			_Out_ LPSTR,
			_In_ _Printf_format_string_ LPCSTR,
			...);

		WINUSERAPI int WINAPIV wsprintfW(
			_Out_ LPWSTR,
			_In_ _Printf_format_string_ LPCWSTR,
			...);
        */


        //#define SETWALLPAPER_DEFAULT    ((LPWSTR)-1)


        #region SCROLL

        public const Int32 SB_HORZ = 0;
		public const Int32 SB_VERT = 1;
		public const Int32 SB_CTL  = 2;
		public const Int32 SB_BOTH = 3;

		public const UInt16 SB_LINEUP           = 0;
		public const UInt16 SB_LINELEFT         = 0;
		public const UInt16 SB_LINEDOWN         = 1;
		public const UInt16 SB_LINERIGHT        = 1;
		public const UInt16 SB_PAGEUP           = 2;
		public const UInt16 SB_PAGELEFT         = 2;
		public const UInt16 SB_PAGEDOWN         = 3;
		public const UInt16 SB_PAGERIGHT        = 3;
		public const UInt16 SB_THUMBPOSITION    = 4;
		public const UInt16 SB_THUMBTRACK       = 5;
		public const UInt16 SB_TOP              = 6;
		public const UInt16 SB_LEFT             = 6;
		public const UInt16 SB_BOTTOM           = 7;
		public const UInt16 SB_RIGHT            = 7;
		public const UInt16 SB_ENDSCROLL        = 8;

        #endregion SCROLL


        #region SHWOWWINDOW

        public const Int32 SW_HIDE             = 0;
		public const Int32 SW_SHOWNORMAL       = 1;
		public const Int32 SW_NORMAL           = 1;
		public const Int32 SW_SHOWMINIMIZED    = 2;
		public const Int32 SW_SHOWMAXIMIZED    = 3;
		public const Int32 SW_MAXIMIZE         = 3;
		public const Int32 SW_SHOWNOACTIVATE   = 4;
		public const Int32 SW_SHOW             = 5;
		public const Int32 SW_MINIMIZE         = 6;
		public const Int32 SW_SHOWMINNOACTIVE  = 7;
		public const Int32 SW_SHOWNA           = 8;
		public const Int32 SW_RESTORE          = 9;
		public const Int32 SW_SHOWDEFAULT      = 10;
		public const Int32 SW_FORCEMINIMIZE    = 11;
		public const Int32 SW_MAX              = 11;

		public const Int32 HIDE_WINDOW         = 0;
		public const Int32 SHOW_OPENWINDOW     = 1;
		public const Int32 SHOW_ICONWINDOW     = 2;
		public const Int32 SHOW_FULLSCREEN     = 3;
		public const Int32 SHOW_OPENNOACTIVATE = 4;

		public const Int32 SW_PARENTCLOSING    = 1;
		public const Int32 SW_OTHERZOOM        = 2;
		public const Int32 SW_PARENTOPENING    = 3;
		public const Int32 SW_OTHERUNZOOM      = 4;

        #endregion SHOWWINDOW


#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 AW_HOR_POSITIVE             = 0x00000001;
		public const UInt32 AW_HOR_NEGATIVE             = 0x00000002;
		public const UInt32 AW_VER_POSITIVE             = 0x00000004;
		public const UInt32 AW_VER_NEGATIVE             = 0x00000008;
		public const UInt32 AW_CENTER                   = 0x00000010;
		public const UInt32 AW_HIDE                     = 0x00010000;
		public const UInt32 AW_ACTIVATE                 = 0x00020000;
		public const UInt32 AW_SLIDE                    = 0x00040000;
		public const UInt32 AW_BLEND                    = 0x00080000;
#endif // WIN32_WINNT_WIN2K_LATER

        public const UInt16 KF_EXTENDED       = 0x0100;
		public const UInt16 KF_DLGMODE        = 0x0800;
		public const UInt16 KF_MENUMODE       = 0x1000;
		public const UInt16 KF_ALTDOWN        = 0x2000;
		public const UInt16 KF_REPEAT         = 0x4000;
		public const UInt16 KF_UP             = 0x8000;


        #region VIRTUALKEYCODES

        public const UInt16 VK_LBUTTON        = 0x01;
		public const UInt16 VK_RBUTTON        = 0x02;
		public const UInt16 VK_CANCEL         = 0x03;
		public const UInt16 VK_MBUTTON        = 0x04;

#if WIN32_WINNT_WIN2K_LATER
		public const UInt16 VK_XBUTTON1       = 0x05;
		public const UInt16 VK_XBUTTON2       = 0x06;
#endif // WIN32_WINNT_WIN2K_LATER

        /*
        * 0x07 : reserved
        */

		public const UInt16 VK_BACK           = 0x08;
		public const UInt16 VK_TAB            = 0x09;

        /*
        * 0x0A - 0x0B : reserved
        */

		public const UInt16 VK_CLEAR          = 0x0C;
		public const UInt16 VK_RETURN         = 0x0D;

        /*
        * 0x0E - 0x0F : unassigned
        */

		public const UInt16 VK_SHIFT          = 0x10;
		public const UInt16 VK_CONTROL        = 0x11;
		public const UInt16 VK_MENU           = 0x12;
		public const UInt16 VK_PAUSE          = 0x13;
		public const UInt16 VK_CAPITAL        = 0x14;

		public const UInt16 VK_KANA           = 0x15;
		public const UInt16 VK_HANGEUL        = 0x15;
		public const UInt16 VK_HANGUL         = 0x15;

        /*
        * 0x16 : unassigned
        */

        public const UInt16 VK_JUNJA          = 0x17;
        public const UInt16 VK_FINAL          = 0x18;
        public const UInt16 VK_HANJA          = 0x19;
        public const UInt16 VK_KANJI          = 0x19;

        /*
        * 0x1A : unassigned
        */

        public const UInt16 VK_ESCAPE         = 0x1B;

        public const UInt16 VK_CONVERT        = 0x1C;
        public const UInt16 VK_NONCONVERT     = 0x1D;
        public const UInt16 VK_ACCEPT         = 0x1E;
        public const UInt16 VK_MODECHANGE     = 0x1F;

        public const UInt16 VK_SPACE          = 0x20;
        public const UInt16 VK_PRIOR          = 0x21;
        public const UInt16 VK_NEXT           = 0x22;
        public const UInt16 VK_END            = 0x23;
        public const UInt16 VK_HOME           = 0x24;
        public const UInt16 VK_LEFT           = 0x25;
        public const UInt16 VK_UP             = 0x26;
        public const UInt16 VK_RIGHT          = 0x27;
        public const UInt16 VK_DOWN           = 0x28;
        public const UInt16 VK_SELECT         = 0x29;
        public const UInt16 VK_PRINT          = 0x2A;
        public const UInt16 VK_EXECUTE        = 0x2B;
        public const UInt16 VK_SNAPSHOT       = 0x2C;
        public const UInt16 VK_INSERT         = 0x2D;
        public const UInt16 VK_DELETE         = 0x2E;
        public const UInt16 VK_HELP           = 0x2F;

        /*
        * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
        * 0x3A - 0x40 : unassigned
        * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
        */

        public const UInt16 VK_LWIN           = 0x5B;
        public const UInt16 VK_RWIN           = 0x5C;
        public const UInt16 VK_APPS           = 0x5D;

        /*
        * 0x5E : reserved
        */

        public const UInt16 VK_SLEEP          = 0x5F;

        public const UInt16 VK_NUMPAD0        = 0x60;
        public const UInt16 VK_NUMPAD1        = 0x61;
        public const UInt16 VK_NUMPAD2        = 0x62;
        public const UInt16 VK_NUMPAD3        = 0x63;
        public const UInt16 VK_NUMPAD4        = 0x64;
        public const UInt16 VK_NUMPAD5        = 0x65;
        public const UInt16 VK_NUMPAD6        = 0x66;
        public const UInt16 VK_NUMPAD7        = 0x67;
        public const UInt16 VK_NUMPAD8        = 0x68;
        public const UInt16 VK_NUMPAD9        = 0x69;
        public const UInt16 VK_MULTIPLY       = 0x6A;
        public const UInt16 VK_ADD            = 0x6B;
        public const UInt16 VK_SEPARATOR      = 0x6C;
        public const UInt16 VK_SUBTRACT       = 0x6D;
        public const UInt16 VK_DECIMAL        = 0x6E;
        public const UInt16 VK_DIVIDE         = 0x6F;
        public const UInt16 VK_F1             = 0x70;
        public const UInt16 VK_F2             = 0x71;
        public const UInt16 VK_F3             = 0x72;
        public const UInt16 VK_F4             = 0x73;
        public const UInt16 VK_F5             = 0x74;
        public const UInt16 VK_F6             = 0x75;
        public const UInt16 VK_F7             = 0x76;
        public const UInt16 VK_F8             = 0x77;
        public const UInt16 VK_F9             = 0x78;
        public const UInt16 VK_F10            = 0x79;
        public const UInt16 VK_F11            = 0x7A;
        public const UInt16 VK_F12            = 0x7B;
        public const UInt16 VK_F13            = 0x7C;
        public const UInt16 VK_F14            = 0x7D;
        public const UInt16 VK_F15            = 0x7E;
        public const UInt16 VK_F16            = 0x7F;
        public const UInt16 VK_F17            = 0x80;
        public const UInt16 VK_F18            = 0x81;
        public const UInt16 VK_F19            = 0x82;
        public const UInt16 VK_F20            = 0x83;
        public const UInt16 VK_F21            = 0x84;
        public const UInt16 VK_F22            = 0x85;
        public const UInt16 VK_F23            = 0x86;
        public const UInt16 VK_F24            = 0x87;

#if WIN32_WINNT_WIN10_LATER //(_WIN32_WINNT >= 0x0604)
        /*
        * 0x88 - 0x8F : UI navigation
        */

        public const UInt16 VK_NAVIGATION_VIEW     = 0x88; // reserved
        public const UInt16 VK_NAVIGATION_MENU     = 0x89; // reserved
        public const UInt16 VK_NAVIGATION_UP       = 0x8A; // reserved
        public const UInt16 VK_NAVIGATION_DOWN     = 0x8B; // reserved
        public const UInt16 VK_NAVIGATION_LEFT     = 0x8C; // reserved
        public const UInt16 VK_NAVIGATION_RIGHT    = 0x8D; // reserved
        public const UInt16 VK_NAVIGATION_ACCEPT   = 0x8E; // reserved
        public const UInt16 VK_NAVIGATION_CANCEL   = 0x8F; // reserved
#endif // WIN32_WINNT_WIN10_LATER (_WIN32_WINNT >= 0x0604) 

        public const UInt16 VK_NUMLOCK        = 0x90;
        public const UInt16 VK_SCROLL         = 0x91;

        /*
        * NEC PC-9800 kbd definitions
        */
        public const UInt16 VK_OEM_NEC_EQUAL  = 0x92;   // '=' key on numpad

        /*
        * Fujitsu/OASYS kbd definitions
        */
        public const UInt16 VK_OEM_FJ_JISHO   = 0x92;   // 'Dictionary' key
        public const UInt16 VK_OEM_FJ_MASSHOU = 0x93;   // 'Unregister word' key
        public const UInt16 VK_OEM_FJ_TOUROKU = 0x94;   // 'Register word' key
        public const UInt16 VK_OEM_FJ_LOYA    = 0x95;   // 'Left OYAYUBI' key
        public const UInt16 VK_OEM_FJ_ROYA    = 0x96;   // 'Right OYAYUBI' key

        /*
        * 0x97 - 0x9F : unassigned
        */

        public const UInt16 VK_LSHIFT         = 0xA0;
        public const UInt16 VK_RSHIFT         = 0xA1;
        public const UInt16 VK_LCONTROL       = 0xA2;
        public const UInt16 VK_RCONTROL       = 0xA3;
        public const UInt16 VK_LMENU          = 0xA4;
        public const UInt16 VK_RMENU          = 0xA5;

#if WIN32_WINNT_WIN2K_LATER
        public const UInt16 VK_BROWSER_BACK        = 0xA6;
        public const UInt16 VK_BROWSER_FORWARD     = 0xA7;
        public const UInt16 VK_BROWSER_REFRESH     = 0xA8;
        public const UInt16 VK_BROWSER_STOP        = 0xA9;
        public const UInt16 VK_BROWSER_SEARCH      = 0xAA;
        public const UInt16 VK_BROWSER_FAVORITES   = 0xAB;
        public const UInt16 VK_BROWSER_HOME        = 0xAC;
        public const UInt16 VK_VOLUME_MUTE         = 0xAD;
        public const UInt16 VK_VOLUME_DOWN         = 0xAE;
        public const UInt16 VK_VOLUME_UP           = 0xAF;
        public const UInt16 VK_MEDIA_NEXT_TRACK    = 0xB0;
        public const UInt16 VK_MEDIA_PREV_TRACK    = 0xB1;
        public const UInt16 VK_MEDIA_STOP          = 0xB2;
        public const UInt16 VK_MEDIA_PLAY_PAUSE    = 0xB3;
        public const UInt16 VK_LAUNCH_MAIL         = 0xB4;
        public const UInt16 VK_LAUNCH_MEDIA_SELECT = 0xB5;
        public const UInt16 VK_LAUNCH_APP1         = 0xB6;
        public const UInt16 VK_LAUNCH_APP2         = 0xB7;
#endif // WIN32_WINNT_WIN2K_LATER

        /*
        * 0xB8 - 0xB9 : reserved
        */

        public const UInt16 VK_OEM_1          = 0xBA;   // ';:' for US
        public const UInt16 VK_OEM_PLUS       = 0xBB;   // '+' any country
        public const UInt16 VK_OEM_COMMA      = 0xBC;   // ',' any country
        public const UInt16 VK_OEM_MINUS      = 0xBD;   // '-' any country
        public const UInt16 VK_OEM_PERIOD     = 0xBE;   // '.' any country
        public const UInt16 VK_OEM_2          = 0xBF;   // '/?' for US
        public const UInt16 VK_OEM_3          = 0xC0;   // '`~' for US

        /*
        * 0xC1 - 0xC2 : reserved
        */

#if WIN32_WINNT_WIN10_LATER //(_WIN32_WINNT >= 0x0604)
        /*
        * 0xC3 - 0xDA : Gamepad input
        */

        public const UInt16 VK_GAMEPAD_A                         = 0xC3; // reserved
        public const UInt16 VK_GAMEPAD_B                         = 0xC4; // reserved
        public const UInt16 VK_GAMEPAD_X                         = 0xC5; // reserved
        public const UInt16 VK_GAMEPAD_Y                         = 0xC6; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_SHOULDER            = 0xC7; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_SHOULDER             = 0xC8; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_TRIGGER              = 0xC9; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_TRIGGER             = 0xCA; // reserved
        public const UInt16 VK_GAMEPAD_DPAD_UP                   = 0xCB; // reserved
        public const UInt16 VK_GAMEPAD_DPAD_DOWN                 = 0xCC; // reserved
        public const UInt16 VK_GAMEPAD_DPAD_LEFT                 = 0xCD; // reserved
        public const UInt16 VK_GAMEPAD_DPAD_RIGHT                = 0xCE; // reserved
        public const UInt16 VK_GAMEPAD_MENU                      = 0xCF; // reserved
        public const UInt16 VK_GAMEPAD_VIEW                      = 0xD0; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_THUMBSTICK_BUTTON    = 0xD1; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_THUMBSTICK_BUTTON   = 0xD2; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_THUMBSTICK_UP        = 0xD3; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_THUMBSTICK_DOWN      = 0xD4; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_THUMBSTICK_RIGHT     = 0xD5; // reserved
        public const UInt16 VK_GAMEPAD_LEFT_THUMBSTICK_LEFT      = 0xD6; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_THUMBSTICK_UP       = 0xD7; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_THUMBSTICK_DOWN     = 0xD8; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_THUMBSTICK_RIGHT    = 0xD9; // reserved
        public const UInt16 VK_GAMEPAD_RIGHT_THUMBSTICK_LEFT     = 0xDA; // reserved
#endif // WIN32_WINNT_WIN10_LATER (_WIN32_WINNT >= 0x0604)

        public const UInt16 VK_OEM_4          = 0xDB;  //  '[{' for US
        public const UInt16 VK_OEM_5          = 0xDC;  //  '\|' for US
        public const UInt16 VK_OEM_6          = 0xDD;  //  ']}' for US
        public const UInt16 VK_OEM_7          = 0xDE;  //  ''"' for US
        public const UInt16 VK_OEM_8          = 0xDF;

        /*
        * 0xE0 : reserved
        */

        /*
        * Various extended or enhanced keyboards
        */
        public const UInt16 VK_OEM_AX         = 0xE1;  //  'AX' key on Japanese AX kbd
        public const UInt16 VK_OEM_102        = 0xE2;  //  "<>" or "\|" on RT 102-key kbd.
        public const UInt16 VK_ICO_HELP       = 0xE3;  //  Help key on ICO
        public const UInt16 VK_ICO_00         = 0xE4;  //  00 key on ICO

#if WIN32_WINNT_NT4_LATER
        public const UInt16 VK_PROCESSKEY     = 0xE5;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt16 VK_ICO_CLEAR      = 0xE6;

#if WIN32_WINNT_WIN2K_LATER
        public const UInt16 VK_PACKET         = 0xE7;
#endif // WIN32_WINNT_WIN2K_LATER

        /*
        * 0xE8 : unassigned
        */

        /*
        * Nokia/Ericsson definitions
        */
        public const UInt16 VK_OEM_RESET      = 0xE9;
        public const UInt16 VK_OEM_JUMP       = 0xEA;
        public const UInt16 VK_OEM_PA1        = 0xEB;
        public const UInt16 VK_OEM_PA2        = 0xEC;
        public const UInt16 VK_OEM_PA3        = 0xED;
        public const UInt16 VK_OEM_WSCTRL     = 0xEE;
        public const UInt16 VK_OEM_CUSEL      = 0xEF;
        public const UInt16 VK_OEM_ATTN       = 0xF0;
        public const UInt16 VK_OEM_FINISH     = 0xF1;
        public const UInt16 VK_OEM_COPY       = 0xF2;
        public const UInt16 VK_OEM_AUTO       = 0xF3;
        public const UInt16 VK_OEM_ENLW       = 0xF4;
        public const UInt16 VK_OEM_BACKTAB    = 0xF5;

        public const UInt16 VK_ATTN           = 0xF6;
        public const UInt16 VK_CRSEL          = 0xF7;
        public const UInt16 VK_EXSEL          = 0xF8;
        public const UInt16 VK_EREOF          = 0xF9;
        public const UInt16 VK_PLAY           = 0xFA;
        public const UInt16 VK_ZOOM           = 0xFB;
        public const UInt16 VK_NONAME         = 0xFC;
        public const UInt16 VK_PA1            = 0xFD;
        public const UInt16 VK_OEM_CLEAR      = 0xFE;

        /*
        * 0xFF : reserved
        */

        #endregion VIRTUALKEYCODES


        #region WH

        public const Int32 WH_MIN              = (-1);
		public const Int32 WH_MSGFILTER        = (-1);
		public const Int32 WH_JOURNALRECORD    = 0;
		public const Int32 WH_JOURNALPLAYBACK  = 1;
		public const Int32 WH_KEYBOARD         = 2;
		public const Int32 WH_GETMESSAGE       = 3;
		public const Int32 WH_CALLWNDPROC      = 4;
		public const Int32 WH_CBT              = 5;
		public const Int32 WH_SYSMSGFILTER     = 6;
		public const Int32 WH_MOUSE            = 7;
#if INVALIDWINDOWSVERSION // defined(_WIN32_WINDOWS)
		public const Int32 WH_HARDWARE         = 8;
#endif
		public const Int32 WH_DEBUG            = 9;
		public const Int32 WH_SHELL            = 10;
		public const Int32 WH_FOREGROUNDIDLE   = 11;
#if WIN32_WINNT_NT4_LATER
		public const Int32 WH_CALLWNDPROCRET   = 12;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER
		public const Int32 WH_KEYBOARD_LL      = 13;
		public const Int32 WH_MOUSE_LL         = 14;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
		public const Int32 WH_MAX              = 14;
#else
		public const Int32 WH_MAX              = 12;
#endif // WIN32_WINNT_NT4_LATER
#else
		public const Int32 WH_MAX              = 11;
#endif

		public const Int32 WH_MINHOOK         = WH_MIN;
		public const Int32 WH_MAXHOOK         = WH_MAX;

        public const Int32 HC_ACTION           = 0;
		public const Int32 HC_GETNEXT          = 1;
		public const Int32 HC_SKIP             = 2;
		public const Int32 HC_NOREMOVE         = 3;
		public const Int32 HC_NOREM            = HC_NOREMOVE;
		public const Int32 HC_SYSMODALON       = 4;
		public const Int32 HC_SYSMODALOFF      = 5;

        public const Int32 HCBT_MOVESIZE       = 0;
		public const Int32 HCBT_MINMAX         = 1;
		public const Int32 HCBT_QS             = 2;
		public const Int32 HCBT_CREATEWND      = 3;
		public const Int32 HCBT_DESTROYWND     = 4;
		public const Int32 HCBT_ACTIVATE       = 5;
		public const Int32 HCBT_CLICKSKIPPED   = 6;
		public const Int32 HCBT_KEYSKIPPED     = 7;
		public const Int32 HCBT_SYSCOMMAND     = 8;
		public const Int32 HCBT_SETFOCUS       = 9;


        public struct CBT_CREATEWNDA
        {
            public IntPtr lpcs; // struct tagCREATESTRUCTA *lpcs;
			public IntPtr hwndInsertAfter;
        }

		public struct CBT_CREATEWNDW
        {
            public IntPtr lpcs; //struct tagCREATESTRUCTW *lpcs;
			public IntPtr hwndInsertAfter;
        }

		public struct CBTACTIVATESTRUCT
        {
            public bool fMouse;
            public IntPtr hWndActive;
        }


#if WIN32_WINNT_WINXP_LATER

		public struct WTSSESSION_NOTIFICATION
        {
            public UInt32 cbSize;
            public UInt32 dwSessionId;

        }

        public const UInt32 WTS_CONSOLE_CONNECT                = 0x1;
		public const UInt32 WTS_CONSOLE_DISCONNECT             = 0x2;
		public const UInt32 WTS_REMOTE_CONNECT                 = 0x3;
		public const UInt32 WTS_REMOTE_DISCONNECT              = 0x4;
		public const UInt32 WTS_SESSION_LOGON                  = 0x5;
		public const UInt32 WTS_SESSION_LOGOFF                 = 0x6;
		public const UInt32 WTS_SESSION_LOCK                   = 0x7;
		public const UInt32 WTS_SESSION_UNLOCK                 = 0x8;
		public const UInt32 WTS_SESSION_REMOTE_CONTROL         = 0x9;
		public const UInt32 WTS_SESSION_CREATE                 = 0xa;
		public const UInt32 WTS_SESSION_TERMINATE              = 0xb;

#endif // WIN32_WINNT_WINXP_LATER

		public const Int32 MSGF_DIALOGBOX      = 0;
		public const Int32 MSGF_MESSAGEBOX     = 1;
		public const Int32 MSGF_MENU           = 2;
		public const Int32 MSGF_SCROLLBAR      = 5;
		public const Int32 MSGF_NEXTWINDOW     = 6;
		public const Int32 MSGF_MAX            = 8;                       // unused
		public const Int32 MSGF_USER           = 4096;

		public const Int32 HSHELL_WINDOWCREATED        = 1;
		public const Int32 HSHELL_WINDOWDESTROYED      = 2;
		public const Int32 HSHELL_ACTIVATESHELLWINDOW  = 3;
#if WIN32_WINNT_NT4_LATER
		public const Int32 HSHELL_WINDOWACTIVATED      = 4;
		public const Int32 HSHELL_GETMINRECT           = 5;
		public const Int32 HSHELL_REDRAW               = 6;
		public const Int32 HSHELL_TASKMAN              = 7;
		public const Int32 HSHELL_LANGUAGE             = 8;
		public const Int32 HSHELL_SYSMENU              = 9;
		public const Int32 HSHELL_ENDTASK              = 10;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WIN2K_LATER
		public const Int32 HSHELL_ACCESSIBILITYSTATE   = 11;
		public const Int32 HSHELL_APPCOMMAND           = 12;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WINXP_LATER
		public const Int32 HSHELL_WINDOWREPLACED       = 13;
		public const Int32 HSHELL_WINDOWREPLACING      = 14;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WIN8_LATER
		public const Int32 HSHELL_MONITORCHANGED       = 16;
#endif // WIN32_WINNT_WIN8_LATER

		public const Int32 HSHELL_HIGHBIT              = 0x8000;
		public const Int32 HSHELL_FLASH                = (HSHELL_REDRAW|HSHELL_HIGHBIT);
		public const Int32 HSHELL_RUDEAPPACTIVATED     = (HSHELL_WINDOWACTIVATED|HSHELL_HIGHBIT);


#if WIN32_WINNT_WIN2K_LATER
		public const Int16 APPCOMMAND_BROWSER_BACKWARD       = 1;
		public const Int16 APPCOMMAND_BROWSER_FORWARD        = 2;
		public const Int16 APPCOMMAND_BROWSER_REFRESH        = 3;
		public const Int16 APPCOMMAND_BROWSER_STOP           = 4;
		public const Int16 APPCOMMAND_BROWSER_SEARCH         = 5;
		public const Int16 APPCOMMAND_BROWSER_FAVORITES      = 6;
		public const Int16 APPCOMMAND_BROWSER_HOME           = 7;
		public const Int16 APPCOMMAND_VOLUME_MUTE            = 8;
		public const Int16 APPCOMMAND_VOLUME_DOWN            = 9;
		public const Int16 APPCOMMAND_VOLUME_UP              = 10;
		public const Int16 APPCOMMAND_MEDIA_NEXTTRACK        = 11;
		public const Int16 APPCOMMAND_MEDIA_PREVIOUSTRACK    = 12;
		public const Int16 APPCOMMAND_MEDIA_STOP             = 13;
		public const Int16 APPCOMMAND_MEDIA_PLAY_PAUSE       = 14;
		public const Int16 APPCOMMAND_LAUNCH_MAIL            = 15;
		public const Int16 APPCOMMAND_LAUNCH_MEDIA_SELECT    = 16;
		public const Int16 APPCOMMAND_LAUNCH_APP1            = 17;
		public const Int16 APPCOMMAND_LAUNCH_APP2            = 18;
		public const Int16 APPCOMMAND_BASS_DOWN              = 19;
		public const Int16 APPCOMMAND_BASS_BOOST             = 20;
		public const Int16 APPCOMMAND_BASS_UP                = 21;
		public const Int16 APPCOMMAND_TREBLE_DOWN            = 22;
		public const Int16 APPCOMMAND_TREBLE_UP              = 23;
#if WIN32_WINNT_WINXP_LATER
		public const Int16 APPCOMMAND_MICROPHONE_VOLUME_MUTE = 24;
		public const Int16 APPCOMMAND_MICROPHONE_VOLUME_DOWN = 25;
		public const Int16 APPCOMMAND_MICROPHONE_VOLUME_UP   = 26;
		public const Int16 APPCOMMAND_HELP                   = 27;
		public const Int16 APPCOMMAND_FIND                   = 28;
		public const Int16 APPCOMMAND_NEW                    = 29;
		public const Int16 APPCOMMAND_OPEN                   = 30;
		public const Int16 APPCOMMAND_CLOSE                  = 31;
		public const Int16 APPCOMMAND_SAVE                   = 32;
		public const Int16 APPCOMMAND_PRINT                  = 33;
		public const Int16 APPCOMMAND_UNDO                   = 34;
		public const Int16 APPCOMMAND_REDO                   = 35;
		public const Int16 APPCOMMAND_COPY                   = 36;
		public const Int16 APPCOMMAND_CUT                    = 37;
		public const Int16 APPCOMMAND_PASTE                  = 38;
		public const Int16 APPCOMMAND_REPLY_TO_MAIL          = 39;
		public const Int16 APPCOMMAND_FORWARD_MAIL           = 40;
		public const Int16 APPCOMMAND_SEND_MAIL              = 41;
		public const Int16 APPCOMMAND_SPELL_CHECK            = 42;
		public const Int16 APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE = 43;
		public const Int16 APPCOMMAND_MIC_ON_OFF_TOGGLE      = 44;
		public const Int16 APPCOMMAND_CORRECTION_LIST        = 45;
		public const Int16 APPCOMMAND_MEDIA_PLAY             = 46;
		public const Int16 APPCOMMAND_MEDIA_PAUSE            = 47;
		public const Int16 APPCOMMAND_MEDIA_RECORD           = 48;
		public const Int16 APPCOMMAND_MEDIA_FAST_FORWARD     = 49;
		public const Int16 APPCOMMAND_MEDIA_REWIND           = 50;
		public const Int16 APPCOMMAND_MEDIA_CHANNEL_UP       = 51;
		public const Int16 APPCOMMAND_MEDIA_CHANNEL_DOWN     = 52;
#endif // WIN32_WINNT_WINXP_LATER
#if WIN32_WINNT_VISTA_LATER
		public const Int16 APPCOMMAND_DELETE                 = 53;
		public const Int16 APPCOMMAND_DWM_FLIP3D             = 54;
#endif // WIN32_WINNT_VISTA_LATER

		public const UInt16 FAPPCOMMAND_MOUSE = 0x8000;
		public const UInt16 FAPPCOMMAND_KEY   = 0;
		public const UInt16 FAPPCOMMAND_OEM   = 0x1000;
		public const UInt16 FAPPCOMMAND_MASK  = 0xF000;

		//#define GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
		public static Int16 GET_APPCOMMAND_LPARAM(IntPtr lParam)
		{
			return ((short)(WinDef.HIWORD(lParam) & ~FAPPCOMMAND_MASK));
		}

		//#define GET_DEVICE_LPARAM(lParam)     ((WORD)(WinDef.HIWORD(lParam) & FAPPCOMMAND_MASK))
		public static UInt16 GET_DEVICE_LPARAM(IntPtr lParam)
		{
			return ((UInt16)(WinDef.HIWORD(lParam) & FAPPCOMMAND_MASK));
		}

		//#define GET_MOUSEORKEY_LPARAM         GET_DEVICE_LPARAM

		//#define GET_FLAGS_LPARAM(lParam)      (LOWORD(lParam))
		public static UInt16 GET_FLAGS_LPARAM(IntPtr lParam)
		{
			return (WinDef.LOWORD(lParam));
		}

        //#define GET_KEYSTATE_LPARAM(lParam)   GET_FLAGS_LPARAM(lParam)
        public static UInt16 GET_KEYSTATE_LPARAM(IntPtr lParam)
        {
            return (WinDef.LOWORD(lParam));
        }

#endif // WIN32_WINNT_WIN2K_LATER


        public struct SHELLHOOKINFO
		{
		    public IntPtr hwnd;
			public WinDef.RECT rc;
		}

		public struct EVENTMSG
		{
			public UInt32 message;
			public UInt32 paramL;
			public UInt32 paramH;
			public UInt32 time;
			public IntPtr hwnd;
		}

		public struct CWPSTRUCT
		{
			public IntPtr lParam;
	        public UIntPtr wParam;
		    public UInt32 message;
			public IntPtr hwnd;
		}

#if WIN32_WINNT_NT4_LATER

        public struct CWPRETSTRUCT
		{
			public IntPtr lResult;
			public IntPtr lParam;
			public UIntPtr wParam;
			public UInt32 message;
			public IntPtr hwnd;
        }

#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_NT4_LATER

		public const UInt32 LLKHF_EXTENDED       = (KF_EXTENDED >> 8); /* 0x00000001 */
		public const UInt32 LLKHF_INJECTED       = 0x00000010;
		public const UInt32 LLKHF_ALTDOWN        = (KF_ALTDOWN >> 8); /* 0x00000020 */
		public const UInt32 LLKHF_UP             = (KF_UP >> 8);      /* 0x00000080 */
		public const UInt32 LLKHF_LOWER_IL_INJECTED        = 0x00000002;

		public const UInt32 LLMHF_INJECTED       = 0x00000001;
		public const UInt32 LLMHF_LOWER_IL_INJECTED        = 0x00000002;


		public struct KBDLLHOOKSTRUCT
		{
	        public UInt32 vkCode;
		    public UInt32 scanCode;
			public UInt32 flags;
	        public UInt32 time;
		    public UIntPtr dwExtraInfo;
		}

		public struct MSLLHOOKSTRUCT
		{
	        public WinDef.POINT pt;
		    public UInt32 mouseData;
			public UInt32 flags;
	        public UInt32 time;
		    public UIntPtr dwExtraInfo;
		}

#endif // WIN32_WINNT_NT4_LATER

		public struct DEBUGHOOKINFO
		{
	        public UInt32 idThread;
	        public UInt32 idThreadInstaller;
		    public Int32 lParam;
			public UInt32 wParam;
			public Int32 code;
		}

		public struct MOUSEHOOKSTRUCT
	    {
		    public WinDef.POINT pt;
			public IntPtr hwnd;
	        public UInt32 wHitTestCode;
		    public UIntPtr dwExtraInfo;
		}

#if WIN32_WINNT_WIN2K_LATER

        public struct MOUSEHOOKSTRUCTEX
		{
			public MOUSEHOOKSTRUCT DUMMYSTRUCTNAME;
			public UInt32   mouseData;
		}

#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_NT4_LATER

        public struct HARDWAREHOOKSTRUCT
		{
			public IntPtr hwnd;
		    public UInt32 message;
			public UIntPtr wParam;
			public IntPtr lParam;
		}

#endif // WIN32_WINNT_NT4_LATER

        #endregion WH


        public static readonly IntPtr HKL_PREV = (IntPtr)0;
		public static readonly IntPtr HKL_NEXT = (IntPtr)1;
        public static readonly IntPtr KLF_ACTIVATE        = (IntPtr)0x00000001;
        public static readonly IntPtr KLF_SUBSTITUTE_OK   = (IntPtr)0x00000002;
        public static readonly IntPtr KLF_REORDER         = (IntPtr)0x00000008;
#if WIN32_WINNT_NT4_LATER
		public static readonly IntPtr KLF_REPLACELANG     = (IntPtr)0x00000010;
		public static readonly IntPtr KLF_NOTELLSHELL     = (IntPtr)0x00000080;
#endif // WIN32_WINNT_NT4_LATER
		public static readonly IntPtr KLF_SETFORPROCESS   = (IntPtr)0x00000100;
#if WIN32_WINNT_WIN2K_LATER
		public static readonly IntPtr KLF_SHIFTLOCK       = (IntPtr)0x00010000;
		public static readonly IntPtr KLF_RESET           = (IntPtr)0x40000000;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 INPUTLANGCHANGE_SYSCHARSET = 0x0001;
		public const UInt32 INPUTLANGCHANGE_FORWARD    = 0x0002;
		public const UInt32 INPUTLANGCHANGE_BACKWARD   = 0x0004;
#endif // WIN32_WINNT_WIN2K_LATER

		public const UInt32 KL_NAMELENGTH = 9;


		// WINUSERAPI HKL WINAPI LoadKeyboardLayoutA(_In_ LPCSTR pwszKLID, _In_ UINT Flags);

		// WINUSERAPI HKL WINAPI LoadKeyboardLayoutW(_In_ LPCWSTR pwszKLID, _In_ UINT Flags);


#if WIN32_WINNT_NT4_LATER
		// WINUSERAPI HKL WINAPI ActivateKeyboardLayout(_In_ HKL hkl, _In_ UINT Flags);
#else
		// WINUSERAPI BOOL WINAPI ActivateKeyboardLayout(_In_ HKL hkl, _In_ UINT Flags);
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
		/*
		WINUSERAPI int WINAPI ToUnicodeEx(
			_In_ UINT wVirtKey,
			_In_ UINT wScanCode,
			_In_reads_bytes_(256) CONST BYTE * lpKeyState,
			_Out_writes_(cchBuff) LPWSTR pwszBuff,
			_In_ int cchBuff,
			_In_ UINT wFlags,
			_In_opt_ HKL dwhkl);
		*/
#endif // WIN32_WINNT_NT4_LATER

		// WINUSERAPI BOOL WINAPI UnloadKeyboardLayout(_In_ HKL hkl);

		// WINUSERAPI BOOL WINAPI GetKeyboardLayoutNameA(_Out_writes_(KL_NAMELENGTH) LPSTR pwszKLID);
		
		// WINUSERAPI BOOL WINAPI GetKeyboardLayoutNameW(_Out_writes_(KL_NAMELENGTH) LPWSTR pwszKLID);

#if WIN32_WINNT_NT4_LATER

		// WINUSERAPI int WINAPI GetKeyboardLayoutList(_In_ int nBuff, _Out_writes_to_opt_(nBuff, return) HKL FAR * lpList);

		// WINUSERAPI HKL WINAPI GetKeyboardLayout(_In_ DWORD idThread);

#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_WIN2K_LATER

		public  struct MOUSEMOVEPOINT
		{
			public Int32 x;
			public Int32 y;
			public UInt32 time;
			public UIntPtr dwExtraInfo;
		}


		public const UInt32 GMMP_USE_DISPLAY_POINTS          = 1;
		public const UInt32 GMMP_USE_HIGH_RESOLUTION_POINTS  = 2;

        /*
		WINUSERAPI int WINAPI GetMouseMovePointsEx(
			_In_ UINT cbSize,
			_In_ LPMOUSEMOVEPOINT lppt,
			_Out_writes_(nBufPoints) LPMOUSEMOVEPOINT lpptBuf,
			_In_ int nBufPoints,
			_In_ DWORD resolution);
        */

#endif // WIN32_WINNT_WIN2K_LATER


        #region DESKTOP

        public const UInt32 DESKTOP_READOBJECTS         = 0x0001;
		public const UInt32 DESKTOP_CREATEWINDOW        = 0x0002;
		public const UInt32 DESKTOP_CREATEMENU          = 0x0004;
        public const UInt32 DESKTOP_HOOKCONTROL         = 0x0008;
        public const UInt32 DESKTOP_JOURNALRECORD       = 0x0010;
		public const UInt32 DESKTOP_JOURNALPLAYBACK     = 0x0020;
		public const UInt32 DESKTOP_ENUMERATE           = 0x0040;
		public const UInt32 DESKTOP_WRITEOBJECTS        = 0x0080;
		public const UInt32 DESKTOP_SWITCHDESKTOP       = 0x0100;

		public const UInt32 DF_ALLOWOTHERACCOUNTHOOK    = 0x0001;

#if _WINGDI_
#if !NOGDI

        /*
		WINUSERAPI HDESK WINAPI CreateDesktopA(
			_In_ LPCSTR lpszDesktop,
			_Reserved_ LPCSTR lpszDevice,
			_Reserved_ DEVMODEA* pDevmode,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa);
		
		WINUSERAPI HDESK WINAPI CreateDesktopW(
			_In_ LPCWSTR lpszDesktop,
			_Reserved_ LPCWSTR lpszDevice,
			_Reserved_ DEVMODEW* pDevmode,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa);
        */
        /*
		WINUSERAPI HDESK WINAPI CreateDesktopExA(
			_In_ LPCSTR lpszDesktop,
			_Reserved_ LPCSTR lpszDevice,
			_Reserved_ DEVMODEA* pDevmode,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa,
			_In_ ULONG ulHeapSize,
			_Reserved_ PVOID pvoid);
		WINUSERAPI HDESK WINAPI CreateDesktopExW(
			_In_ LPCWSTR lpszDesktop,
			_Reserved_ LPCWSTR lpszDevice,
			_Reserved_ DEVMODEW* pDevmode,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa,
			_In_ ULONG ulHeapSize,
			_Reserved_ PVOID pvoid);
        */

#endif // NOGDI
#endif // _WINGDI_ 

        /*
		WINUSERAPI HDESK WINAPI OpenDesktopA(
			_In_ LPCSTR lpszDesktop,
			_In_ DWORD dwFlags,
			_In_ BOOL fInherit,
			_In_ ACCESS_MASK dwDesiredAccess);
		WINUSERAPI HDESK WINAPI OpenDesktopW(
			_In_ LPCWSTR lpszDesktop,
			_In_ DWORD dwFlags,
			_In_ BOOL fInherit,
			_In_ ACCESS_MASK dwDesiredAccess);
        */
        /*
		WINUSERAPI HDESK WINAPI OpenInputDesktop(
			_In_ DWORD dwFlags,
			_In_ BOOL fInherit,
			_In_ ACCESS_MASK dwDesiredAccess);
        */
        /*
		WINUSERAPI BOOL WINAPI EnumDesktopsA(
			_In_opt_ HWINSTA hwinsta,
			_In_ DESKTOPENUMPROCA lpEnumFunc,
			_In_ LPARAM lParam);
		WINUSERAPI BOOL WINAPI EnumDesktopsW(
			_In_opt_ HWINSTA hwinsta,
			_In_ DESKTOPENUMPROCW lpEnumFunc,
			_In_ LPARAM lParam);
        */
        /*
		WINUSERAPI BOOL WINAPI EnumDesktopWindows(
			_In_opt_ HDESK hDesktop,
			_In_ WNDENUMPROC lpfn,
			_In_ LPARAM lParam);
        */

        // WINUSERAPI BOOL WINAPI SwitchDesktop(_In_ HDESK hDesktop);

        // WINUSERAPI BOOL WINAPI SetThreadDesktop(_In_ HDESK hDesktop);

        // WINUSERAPI BOOL WINAPI CloseDesktop(_In_ HDESK hDesktop);

        // WINUSERAPI HDESK WINAPI GetThreadDesktop(_In_ DWORD dwThreadId);

        #endregion DESKIOP


        #region WINDOWSTATION

        public const UInt32 WINSTA_ENUMDESKTOPS         = 0x0001;
		public const UInt32 WINSTA_READATTRIBUTES       = 0x0002;
		public const UInt32 WINSTA_ACCESSCLIPBOARD      = 0x0004;
		public const UInt32 WINSTA_CREATEDESKTOP        = 0x0008;
		public const UInt32 WINSTA_WRITEATTRIBUTES      = 0x0010;
		public const UInt32 WINSTA_ACCESSGLOBALATOMS    = 0x0020;
		public const UInt32 WINSTA_EXITWINDOWS          = 0x0040;
		public const UInt32 WINSTA_ENUMERATE            = 0x0100;
		public const UInt32 WINSTA_READSCREEN           = 0x0200;

		public const UInt32 WINSTA_ALL_ACCESS           = WINSTA_ENUMDESKTOPS  
														| WINSTA_READATTRIBUTES  
														| WINSTA_ACCESSCLIPBOARD 
														| WINSTA_CREATEDESKTOP 
														| WINSTA_WRITEATTRIBUTES 
														| WINSTA_ACCESSGLOBALATOMS 
														| WINSTA_EXITWINDOWS   
														| WINSTA_ENUMERATE       
														| WINSTA_READSCREEN;

		public const UInt32 CWF_CREATE_ONLY          = 0x00000001;

		public const Int16 WSF_VISIBLE                 = 0x0001;

        /*
		WINUSERAPI HWINSTA WINAPI CreateWindowStationA(
			_In_opt_ LPCSTR lpwinsta,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa);
			
		WINUSERAPI HWINSTA WINAPI CreateWindowStationW(
			_In_opt_ LPCWSTR lpwinsta,
			_In_ DWORD dwFlags,
			_In_ ACCESS_MASK dwDesiredAccess,
			_In_opt_ LPSECURITY_ATTRIBUTES lpsa);
		*/
        /*
		WINUSERAPI HWINSTA WINAPI OpenWindowStationA(
			_In_ LPCSTR lpszWinSta,
			_In_ BOOL fInherit,
			_In_ ACCESS_MASK dwDesiredAccess);
			
		WINUSERAPI HWINSTA WINAPI OpenWindowStationW(
			_In_ LPCWSTR lpszWinSta,
			_In_ BOOL fInherit,
			_In_ ACCESS_MASK dwDesiredAccess);
		*/
        /*
		WINUSERAPI BOOL WINAPI EnumWindowStationsA(_In_ WINSTAENUMPROCA lpEnumFunc, _In_ LPARAM lParam);

		WINUSERAPI BOOL WINAPI EnumWindowStationsW(_In_ WINSTAENUMPROCW lpEnumFunc, _In_ LPARAM lParam);
		*/
        // WINUSERAPI BOOL WINAPI CloseWindowStation(_In_ HWINSTA hWinSta);

        // WINUSERAPI BOOL WINAPI SetProcessWindowStation(_In_ HWINSTA hWinSta);

        // WINUSERAPI HWINSTA WINAPI GetProcessWindowStation(VOID);

        #endregion WINDOWSTATION


        #region SECURITY

        /*
		WINUSERAPI BOOL WINAPI SetUserObjectSecurity(
			_In_ HANDLE hObj,
			_In_ PSECURITY_INFORMATION pSIRequested,
			_In_ PSECURITY_DESCRIPTOR pSID);
		*/
        /*
		WINUSERAPI BOOL WINAPI GetUserObjectSecurity(
			_In_ HANDLE hObj,
			_In_ PSECURITY_INFORMATION pSIRequested,
			_Out_writes_bytes_opt_(nLength) PSECURITY_DESCRIPTOR pSID,
			_In_ DWORD nLength,
			_Out_ LPDWORD lpnLengthNeeded);
		*/

        public const Int32 UOI_FLAGS       = 1;
		public const Int32 UOI_NAME        = 2;
		public const Int32 UOI_TYPE        = 3;
		public const Int32 UOI_USER_SID    = 4;
#if WIN32_WINNT_VISTA_LATER
		public const Int32 UOI_HEAPSIZE    = 5;
		public const Int32 UOI_IO          = 6;
#endif // WIN32_WINNT_VISTA_LATER
		public const Int32 UOI_TIMERPROC_EXCEPTION_SUPPRESSION       = 7;

		public struct USEROBJECTFLAGS
		{
			public bool fInherit;
			public bool fReserved;
			public UInt32 dwFlags;
		}
        /*
		WINUSERAPI BOOL WINAPI GetUserObjectInformationA(
			_In_ HANDLE hObj,
			_In_ int nIndex,
			_Out_writes_bytes_opt_(nLength) PVOID pvInfo,
			_In_ DWORD nLength,
			_Out_opt_ LPDWORD lpnLengthNeeded);
			
		WINUSERAPI BOOL WINAPI GetUserObjectInformationW(
			_In_ HANDLE hObj,
			_In_ int nIndex,
			_Out_writes_bytes_opt_(nLength) PVOID pvInfo,
			_In_ DWORD nLength,
			_Out_opt_ LPDWORD lpnLengthNeeded);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetUserObjectInformationA(
			_In_ HANDLE hObj,
			_In_ int nIndex,
			_In_reads_bytes_(nLength) PVOID pvInfo,
			_In_ DWORD nLength);
			
		WINUSERAPI BOOL WINAPI SetUserObjectInformationW(
			_In_ HANDLE hObj,
			_In_ int nIndex,
			_In_reads_bytes_(nLength) PVOID pvInfo,
			_In_ DWORD nLength);
        */

        #endregion SECURITY



#if WIN32_WINNT_NT4_LATER

        public struct WNDCLASSEXA
		{
			public UInt32 cbSize;
			/* Win 3.x */
			public UInt32 style;
			public WNDPROC lpfnWndProc;
			public Int32 cbClsExtra;
			public Int32 cbWndExtra;
			public IntPtr hInstance;
			public IntPtr hIcon;
			public IntPtr hCursor;
			public IntPtr hbrBackground;
			[MarshalAs(UnmanagedType.LPStr)]
			public string lpszMenuName;
			[MarshalAs(UnmanagedType.LPStr)]
			public string lpszClassName;
			/* Win 4.0 */
			public IntPtr hIconSm;
		}

		public struct WNDCLASSEXW
		{
			public UInt32 cbSize;
			/* Win 3.x */
			public UInt32 style;
			public WNDPROC lpfnWndProc;
			public Int32 cbClsExtra;
			public Int32 cbWndExtra;
			public IntPtr hInstance;
			public IntPtr hIcon;
			public IntPtr hCursor;
			public IntPtr hbrBackground;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
			/* Win 4.0 */
			public IntPtr hIconSm;
		}

#endif // WIN32_WINNT_NT4_LATER
		
		public struct WNDCLASSA
		{
			public UInt32 style;
			public WNDPROC lpfnWndProc;
			public Int32 cbClsExtra;
			public Int32 cbWndExtra;
			public IntPtr hInstance;
			public IntPtr hIcon;
			public IntPtr hCursor;
			public IntPtr hbrBackground;
			public string lpszMenuName;
			public string lpszClassName;
		}

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WNDCLASSW
		{
			public UInt32 style;
			public WNDPROC lpfnWndProc;
			public Int32 cbClsExtra;
			public Int32 cbWndExtra;
			public IntPtr hInstance;
			public IntPtr hIcon;
			public IntPtr hCursor;
			public IntPtr hbrBackground;
			public string lpszMenuName;
			public string lpszClassName;
		}

        // WINUSERAPI BOOL WINAPI IsHungAppWindow(_In_ HWND hwnd);

#if WIN32_WINNT_WINXP_LATER
        // WINUSERAPI VOID WINAPI DisableProcessWindowsGhosting(VOID);
#endif // WIN32_WINNT_WINXP_LATER


        #region MSG

        public struct MSG
		{
			public IntPtr hwnd;
			public UInt32 message;
			public UIntPtr wParam;
			public IntPtr lParam;
			public UInt32 time;
			public WinDef.POINT pt;
		}


        //#define POINTSTOPOINT(pt, pts)                          \
        //        { (pt).x = (LONG) (SHORT) WinDef.LOWORD(*(LONG*)&pts);   \
        //          (pt).y = (LONG) (SHORT) WinDef.HIWORD(*(LONG*)&pts); }

        //#define POINTTOPOINTS(pt)      (MAKELONG((short)((pt).x), (short)((pt).y)))
        //#define MAKEWPARAM(l, h)      ((WPARAM)(DWORD)MAKELONG(l, h))
        //#define MAKELPARAM(l, h)      ((LPARAM)(DWORD)MAKELONG(l, h))
        //#define MAKELRESULT(l, h)     ((LRESULT)(DWORD)MAKELONG(l, h))

        #endregion MSG


        #region WINOFFSETS
//#if !_WIN64
//      public const Int32 GWL_WNDPROC         = (-4);
//		public const Int32 GWL_HINSTANCE       = (-6);
//		public const Int32 GWL_HWNDPARENT      = (-8);
//#endif
		public const Int32 GWL_STYLE           = (-16);
		public const Int32 GWL_EXSTYLE         = (-20);
//#if !_WIN64
//		public const Int32 GWL_USERDATA        = (-21);
//#endif
//		public const Int32 GWL_ID              = (-12);


		public const Int32 GWLP_WNDPROC        = (-4);
		public const Int32 GWLP_HINSTANCE      = (-6);
		public const Int32 GWLP_HWNDPARENT     = (-8);
		public const Int32 GWLP_USERDATA       = (-21);
		public const Int32 GWLP_ID             = (-12);

//#if !_WIN64
//		public const Int32 GCL_MENUNAME        = (-8);
//		public const Int32 GCL_HBRBACKGROUND   = (-10);
//		public const Int32 GCL_HCURSOR         = (-12);
//		public const Int32 GCL_HICON           = (-14);
//		public const Int32 GCL_HMODULE         = (-16);
//#endif
		public const Int32 GCL_CBWNDEXTRA      = (-18);
		public const Int32 GCL_CBCLSEXTRA      = (-20);
//#if !_WIN64
//		public const Int32 GCL_WNDPROC         = (-24);
//#endif
		public const Int32 GCL_STYLE           = (-26);
		public const Int32 GCW_ATOM            = (-32);
#if WIN32_WINNT_NT4_LATER
//#if !_WIN64
//		public const Int32 GCL_HICONSM         = (-34);
//#endif
#endif // WIN32_WINNT_NT4_LATER

		public const Int32 GCLP_MENUNAME       = (-8);
		public const Int32 GCLP_HBRBACKGROUND  = (-10);
		public const Int32 GCLP_HCURSOR        = (-12);
		public const Int32 GCLP_HICON          = (-14);
		public const Int32 GCLP_HMODULE        = (-16);
		public const Int32 GCLP_WNDPROC        = (-24);
		public const Int32 GCLP_HICONSM        = (-34);

        #endregion WINOFFSETS


        #region WINMESSAGES

        public const UInt32 WM_NULL                         = 0x0000;
		public const UInt32 WM_CREATE                       = 0x0001;
        public const UInt32 WM_DESTROY                      = 0x0002;
        public const UInt32 WM_MOVE                         = 0x0003;
        public const UInt32 WM_SIZE                         = 0x0005;

        public const UInt32 WM_ACTIVATE                     = 0x0006;

        public const UInt16 WA_INACTIVE     = 0;
		public const UInt16 WA_ACTIVE       = 1;
		public const UInt16 WA_CLICKACTIVE  = 2;

        public const UInt32 WM_SETFOCUS                     = 0x0007;
        public const UInt32 WM_KILLFOCUS                    = 0x0008;
        public const UInt32 WM_ENABLE                       = 0x000A;
        public const UInt32 WM_SETREDRAW                    = 0x000B;
        public const UInt32 WM_SETTEXT                      = 0x000C;
        public const UInt32 WM_GETTEXT                      = 0x000D;
        public const UInt32 WM_GETTEXTLENGTH                = 0x000E;
        public const UInt32 WM_PAINT                        = 0x000F;
        public const UInt32 WM_CLOSE                        = 0x0010;
//#if !_WIN32_WCE
        public const UInt32 WM_QUERYENDSESSION              = 0x0011;
        public const UInt32 WM_QUERYOPEN                    = 0x0013;
        public const UInt32 WM_ENDSESSION                   = 0x0016;
//#endif
        public const UInt32 WM_QUIT                         = 0x0012;
        public const UInt32 WM_ERASEBKGND                   = 0x0014;
        public const UInt32 WM_SYSCOLORCHANGE               = 0x0015;
        public const UInt32 WM_SHOWWINDOW                   = 0x0018;
        public const UInt32 WM_WININICHANGE                 = 0x001A;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_SETTINGCHANGE                = WM_WININICHANGE;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_DEVMODECHANGE                = 0x001B;
        public const UInt32 WM_ACTIVATEAPP                  = 0x001C;
        public const UInt32 WM_FONTCHANGE                   = 0x001D;
        public const UInt32 WM_TIMECHANGE                   = 0x001E;
        public const UInt32 WM_CANCELMODE                   = 0x001F;
        public const UInt32 WM_SETCURSOR                    = 0x0020;
        public const UInt32 WM_MOUSEACTIVATE                = 0x0021;
        public const UInt32 WM_CHILDACTIVATE                = 0x0022;
        public const UInt32 WM_QUEUESYNC                    = 0x0023;

        public const UInt32 WM_GETMINMAXINFO                = 0x0024;


        public struct MINMAXINFO
		{
			public WinDef.POINT ptReserved;
			public WinDef.POINT ptMaxSize;
			public WinDef.POINT ptMaxPosition;
			public WinDef.POINT ptMinTrackSize;
			public WinDef.POINT ptMaxTrackSize;
		}


        public const UInt32 WM_PAINTICON                    = 0x0026;
        public const UInt32 WM_ICONERASEBKGND               = 0x0027;
        public const UInt32 WM_NEXTDLGCTL                   = 0x0028;
        public const UInt32 WM_SPOOLERSTATUS                = 0x002A;
        public const UInt32 WM_DRAWITEM                     = 0x002B;
        public const UInt32 WM_MEASUREITEM                  = 0x002C;
        public const UInt32 WM_DELETEITEM                   = 0x002D;
        public const UInt32 WM_VKEYTOITEM                   = 0x002E;
        public const UInt32 WM_CHARTOITEM                   = 0x002F;
        public const UInt32 WM_SETFONT                      = 0x0030;
        public const UInt32 WM_GETFONT                      = 0x0031;
        public const UInt32 WM_SETHOTKEY                    = 0x0032;
        public const UInt32 WM_GETHOTKEY                    = 0x0033;
        public const UInt32 WM_QUERYDRAGICON                = 0x0037;
        public const UInt32 WM_COMPAREITEM                  = 0x0039;
#if WIN32_WINNT_WIN2K_LATER
//#if !_WIN32_WCE
        public const UInt32 WM_GETOBJECT                    = 0x003D;
//#endif
#endif // WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_COMPACTING                   = 0x0041;
        public const UInt32 WM_COMMNOTIFY                   = 0x0044;  /* no longer suported */
        public const UInt32 WM_WINDOWPOSCHANGING            = 0x0046;
        public const UInt32 WM_WINDOWPOSCHANGED             = 0x0047;

        public const UInt32 WM_POWER                        = 0x0048;

        public const UInt32 PWR_OK              = 1;
		public const UInt32 PWR_FAIL            = unchecked((UInt32)(-1));
		public const UInt32 PWR_SUSPENDREQUEST  = 1;
		public const UInt32 PWR_SUSPENDRESUME   = 2;
		public const UInt32 PWR_CRITICALRESUME  = 3;

        public const UInt32 WM_COPYDATA                     = 0x004A;
        public const UInt32 WM_CANCELJOURNAL                = 0x004B;


        public struct COPYDATASTRUCT
		{
			public UIntPtr dwData;
			public UInt32 cbData;
			//_Field_size_bytes_(cbData) PVOID lpData;
			public IntPtr lpData;
		}

#if WIN32_WINNT_NT4_LATER
		public struct MDINEXTMENU
		{
			public IntPtr hmenuIn;
			public IntPtr hmenuNext;
			public IntPtr hwndNext;
		}
#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_NOTIFY                       = 0x004E;
        public const UInt32 WM_INPUTLANGCHANGEREQUEST       = 0x0050;
        public const UInt32 WM_INPUTLANGCHANGE              = 0x0051;
        public const UInt32 WM_TCARD                        = 0x0052;
        public const UInt32 WM_HELP                         = 0x0053;
        public const UInt32 WM_USERCHANGED                  = 0x0054;
        public const UInt32 WM_NOTIFYFORMAT                 = 0x0055;

        public const Int32 NFR_ANSI                             = 1;
        public const Int32 NFR_UNICODE                          = 2;
        public const Int32 NF_QUERY                             = 3;
        public const Int32 NF_REQUERY                           = 4;

        public const UInt32 WM_CONTEXTMENU                  = 0x007B;
        public const UInt32 WM_STYLECHANGING                = 0x007C;
        public const UInt32 WM_STYLECHANGED                 = 0x007D;
        public const UInt32 WM_DISPLAYCHANGE                = 0x007E;
        public const UInt32 WM_GETICON                      = 0x007F;
        public const UInt32 WM_SETICON                      = 0x0080;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_NCCREATE                     = 0x0081;
        public const UInt32 WM_NCDESTROY                    = 0x0082;
        public const UInt32 WM_NCCALCSIZE                   = 0x0083;
        public const UInt32 WM_NCHITTEST                    = 0x0084;
        public const UInt32 WM_NCPAINT                      = 0x0085;
        public const UInt32 WM_NCACTIVATE                   = 0x0086;
        public const UInt32 WM_GETDLGCODE                   = 0x0087;
//#if !_WIN32_WCE
        public const UInt32 WM_SYNCPAINT                    = 0x0088;
//#endif
        public const UInt32 WM_NCMOUSEMOVE                  = 0x00A0;
        public const UInt32 WM_NCLBUTTONDOWN                = 0x00A1;
        public const UInt32 WM_NCLBUTTONUP                  = 0x00A2;
        public const UInt32 WM_NCLBUTTONDBLCLK              = 0x00A3;
        public const UInt32 WM_NCRBUTTONDOWN                = 0x00A4;
        public const UInt32 WM_NCRBUTTONUP                  = 0x00A5;
        public const UInt32 WM_NCRBUTTONDBLCLK              = 0x00A6;
        public const UInt32 WM_NCMBUTTONDOWN                = 0x00A7;
        public const UInt32 WM_NCMBUTTONUP                  = 0x00A8;
        public const UInt32 WM_NCMBUTTONDBLCLK              = 0x00A9;

#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_NCXBUTTONDOWN                = 0x00AB;
        public const UInt32 WM_NCXBUTTONUP                  = 0x00AC;
        public const UInt32 WM_NCXBUTTONDBLCLK              = 0x00AD;
#endif // WIN32_WINNT_WIN2K_LATER


#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_INPUT_DEVICE_CHANGE          = 0x00FE;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_INPUT                        = 0x00FF;
#endif // WIN32_WINNT_WINXP_LATER

        public const UInt32 WM_KEYFIRST                     = 0x0100;
        public const UInt32 WM_KEYDOWN                      = 0x0100;
        public const UInt32 WM_KEYUP                        = 0x0101;
        public const UInt32 WM_CHAR                         = 0x0102;
        public const UInt32 WM_DEADCHAR                     = 0x0103;
        public const UInt32 WM_SYSKEYDOWN                   = 0x0104;
        public const UInt32 WM_SYSKEYUP                     = 0x0105;
        public const UInt32 WM_SYSCHAR                      = 0x0106;
        public const UInt32 WM_SYSDEADCHAR                  = 0x0107;
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_UNICHAR                      = 0x0109;
        public const UInt32 WM_KEYLAST                      = 0x0109;
        public const UInt32 UNICODE_NOCHAR                  = 0xFFFF;
#else
        public const UInt32 WM_KEYLAST                      = 0x0108;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_IME_STARTCOMPOSITION         = 0x010D;
        public const UInt32 WM_IME_ENDCOMPOSITION           = 0x010E;
        public const UInt32 WM_IME_COMPOSITION              = 0x010F;
        public const UInt32 WM_IME_KEYLAST                  = 0x010F;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_INITDIALOG                   = 0x0110;
        public const UInt32 WM_COMMAND                      = 0x0111;
        public const UInt32 WM_SYSCOMMAND                   = 0x0112;
        public const UInt32 WM_TIMER                        = 0x0113;
        public const UInt32 WM_HSCROLL                      = 0x0114;
        public const UInt32 WM_VSCROLL                      = 0x0115;
        public const UInt32 WM_INITMENU                     = 0x0116;
        public const UInt32 WM_INITMENUPOPUP                = 0x0117;
#if WIN32_WINNT_WIN7_LATER
        public const UInt32 WM_GESTURE                      = 0x0119;
        public const UInt32 WM_GESTURENOTIFY                = 0x011A;
#endif // WIN32_WINNT_WIN7_LATER
        public const UInt32 WM_MENUSELECT                   = 0x011F;
        public const UInt32 WM_MENUCHAR                     = 0x0120;
        public const UInt32 WM_ENTERIDLE                    = 0x0121;
#if WIN32_WINNT_WIN2K_LATER
//#if !_WIN32_WCE
        public const UInt32 WM_MENURBUTTONUP                = 0x0122;
        public const UInt32 WM_MENUDRAG                     = 0x0123;
        public const UInt32 WM_MENUGETOBJECT                = 0x0124;
        public const UInt32 WM_UNINITMENUPOPUP              = 0x0125;
        public const UInt32 WM_MENUCOMMAND                  = 0x0126;

//#if !_WIN32_WCE
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_CHANGEUISTATE                = 0x0127;
        public const UInt32 WM_UPDATEUISTATE                = 0x0128;
        public const UInt32 WM_QUERYUISTATE                 = 0x0129;

        public const UInt16 UIS_SET								= 1;
		public const UInt16 UIS_CLEAR                       = 2;
		public const UInt16 UIS_INITIALIZE                  = 3;

		public const UInt16 UISF_HIDEFOCUS                  = 0x1;
		public const UInt16 UISF_HIDEACCEL                  = 0x2;
#if WIN32_WINNT_WINXP_LATER
		public const UInt16 UISF_ACTIVE                     = 0x4;
#endif // WIN32_WINNT_WINXP_LATER
#endif // WIN32_WINNT_WIN2K_LATER
//#endif

//#endif
#endif // WIN32_WINNT_WIN2K_LATER

        public const UInt32 WM_CTLCOLORMSGBOX               = 0x0132;
        public const UInt32 WM_CTLCOLOREDIT                 = 0x0133;
        public const UInt32 WM_CTLCOLORLISTBOX              = 0x0134;
        public const UInt32 WM_CTLCOLORBTN                  = 0x0135;
        public const UInt32 WM_CTLCOLORDLG                  = 0x0136;
        public const UInt32 WM_CTLCOLORSCROLLBAR            = 0x0137;
        public const UInt32 WM_CTLCOLORSTATIC               = 0x0138;
        public const UInt32 MN_GETHMENU                     = 0x01E1;

        public const UInt32 WM_MOUSEFIRST                   = 0x0200;
        public const UInt32 WM_MOUSEMOVE                    = 0x0200;
        public const UInt32 WM_LBUTTONDOWN                  = 0x0201;
        public const UInt32 WM_LBUTTONUP                    = 0x0202;
        public const UInt32 WM_LBUTTONDBLCLK                = 0x0203;
        public const UInt32 WM_RBUTTONDOWN                  = 0x0204;
        public const UInt32 WM_RBUTTONUP                    = 0x0205;
        public const UInt32 WM_RBUTTONDBLCLK                = 0x0206;
        public const UInt32 WM_MBUTTONDOWN                  = 0x0207;
        public const UInt32 WM_MBUTTONUP                    = 0x0208;
        public const UInt32 WM_MBUTTONDBLCLK                = 0x0209;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_MOUSEWHEEL                   = 0x020A;
#endif
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_XBUTTONDOWN                  = 0x020B;
        public const UInt32 WM_XBUTTONUP                    = 0x020C;
        public const UInt32 WM_XBUTTONDBLCLK                = 0x020D;
#endif
#if WIN32_WINNT_VISTA_LATER
        public const UInt32 WM_MOUSEHWHEEL                  = 0x020E;
#endif

#if WIN32_WINNT_VISTA_LATER
        public const UInt32 WM_MOUSELAST                    = 0x020E;
#elif WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_MOUSELAST                    = 0x020D;
#elif WIN32_WINNT_NT4_LATER
        public const UInt32 WM_MOUSELAST                    = 0x020A;
#else
        public const UInt32 WM_MOUSELAST                    = 0x0209;
#endif // WIN32_WINNT_VISTA_LATER


#if WIN32_WINNT_NT4_LATER
		public const Int16 WHEEL_DELTA						= 120;
		public static Int16 GET_WHEEL_DELTA_WPARAM(UIntPtr wParam)
		{
			return ((short)WinDef.HIWORD(wParam));
		}

        public const UInt32 WHEEL_PAGESCROLL                = UInt32.MaxValue; // (UINT_MAX)
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
		public static UInt16 GET_KEYSTATE_WPARAM(UIntPtr wParam)
		{
			return (WinDef.LOWORD(wParam));
		}

		public static Int16 GET_NCHITTEST_WPARAM(UIntPtr wParam)
		{
			return ((short)WinDef.LOWORD(wParam));
		}

		public static UInt16 GET_XBUTTON_WPARAM(UIntPtr wParam)
		{
			return (WinDef.HIWORD(wParam));
		}

		public const UInt16 XBUTTON1      = 0x0001;
		public const UInt16 XBUTTON2      = 0x0002;
#endif // WIN32_WINNT_WIN2K_LATER

        public const UInt32 WM_PARENTNOTIFY                 = 0x0210;
        public const UInt32 WM_ENTERMENULOOP                = 0x0211;
        public const UInt32 WM_EXITMENULOOP                 = 0x0212;

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_NEXTMENU                     = 0x0213;
        public const UInt32 WM_SIZING                       = 0x0214;
        public const UInt32 WM_CAPTURECHANGED               = 0x0215;
        public const UInt32 WM_MOVING                       = 0x0216;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER

        public const UInt32 WM_POWERBROADCAST               = 0x0218;
//#if !_WIN32_WCE
		public const UInt16 PBT_APMQUERYSUSPEND             = 0x0000;
		public const UInt16 PBT_APMQUERYSTANDBY             = 0x0001;

		public const UInt16 PBT_APMQUERYSUSPENDFAILED       = 0x0002;
		public const UInt16 PBT_APMQUERYSTANDBYFAILED       = 0x0003;

		public const UInt16 PBT_APMSUSPEND                  = 0x0004;
		public const UInt16 PBT_APMSTANDBY                  = 0x0005;

		public const UInt16 PBT_APMRESUMECRITICAL           = 0x0006;
		public const UInt16 PBT_APMRESUMESUSPEND            = 0x0007;
		public const UInt16 PBT_APMRESUMESTANDBY            = 0x0008;

		public const Int32 PBTF_APMRESUMEFROMFAILURE       = 0x00000001;

		public const UInt16 PBT_APMBATTERYLOW               = 0x0009;
		public const UInt16 PBT_APMPOWERSTATUSCHANGE        = 0x000A;

		public const UInt16 PBT_APMOEMEVENT                 = 0x000B;

		public const UInt16 PBT_APMRESUMEAUTOMATIC          = 0x0012;
#if WIN32_WINNT_WS03_LATER
#if !PBT_POWERSETTINGCHANGE
		public const UInt16 PBT_POWERSETTINGCHANGE          = 0x8013;


		public struct POWERBROADCAST_SETTING
		{
		    public Guid PowerSetting;
			public UInt32 DataLength;
			//UCHAR Data[1];
			public Byte[] Data;
		} 


#endif // PBT_POWERSETTINGCHANGE
#endif // (WIN32_WINNT_WS03_LATER)
//#endif

#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_DEVICECHANGE                 = 0x0219;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_MDICREATE                    = 0x0220;
        public const UInt32 WM_MDIDESTROY                   = 0x0221;
        public const UInt32 WM_MDIACTIVATE                  = 0x0222;
        public const UInt32 WM_MDIRESTORE                   = 0x0223;
        public const UInt32 WM_MDINEXT                      = 0x0224;
        public const UInt32 WM_MDIMAXIMIZE                  = 0x0225;
        public const UInt32 WM_MDITILE                      = 0x0226;
        public const UInt32 WM_MDICASCADE                   = 0x0227;
        public const UInt32 WM_MDIICONARRANGE               = 0x0228;
        public const UInt32 WM_MDIGETACTIVE                 = 0x0229;

        public const UInt32 WM_MDISETMENU                   = 0x0230;
        public const UInt32 WM_ENTERSIZEMOVE                = 0x0231;
        public const UInt32 WM_EXITSIZEMOVE                 = 0x0232;
        public const UInt32 WM_DROPFILES                    = 0x0233;
        public const UInt32 WM_MDIREFRESHMENU               = 0x0234;

#if WIN32_WINNT_WIN8_LATER
        public const UInt32 WM_POINTERDEVICECHANGE          = 0x238;
        public const UInt32 WM_POINTERDEVICEINRANGE         = 0x239;
        public const UInt32 WM_POINTERDEVICEOUTOFRANGE      = 0x23A;
#endif // WIN32_WINNT_WIN8_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 WM_TOUCH                        = 0x0240;
#endif // WIN32_WINNT_WIN7_LATER

#if WIN32_WINNT_WIN8_LATER
        public const UInt32 WM_NCPOINTERUPDATE              = 0x0241;
        public const UInt32 WM_NCPOINTERDOWN                = 0x0242;
        public const UInt32 WM_NCPOINTERUP                  = 0x0243;
        public const UInt32 WM_POINTERUPDATE                = 0x0245;
        public const UInt32 WM_POINTERDOWN                  = 0x0246;
        public const UInt32 WM_POINTERUP                    = 0x0247;
        public const UInt32 WM_POINTERENTER                 = 0x0249;
        public const UInt32 WM_POINTERLEAVE                 = 0x024A;
        public const UInt32 WM_POINTERACTIVATE              = 0x024B;
        public const UInt32 WM_POINTERCAPTURECHANGED        = 0x024C;
        public const UInt32 WM_TOUCHHITTESTING              = 0x024D;
        public const UInt32 WM_POINTERWHEEL                 = 0x024E;
        public const UInt32 WM_POINTERHWHEEL                = 0x024F;
        public const UInt32 DM_POINTERHITTEST               = 0x0250;
        public const UInt32 WM_POINTERROUTEDTO              = 0x0251;
        public const UInt32 WM_POINTERROUTEDAWAY            = 0x0252;
        public const UInt32 WM_POINTERROUTEDRELEASED        = 0x0253;
#endif // WIN32_WINNT_WIN8_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_IME_SETCONTEXT               = 0x0281;
        public const UInt32 WM_IME_NOTIFY                   = 0x0282;
        public const UInt32 WM_IME_CONTROL                  = 0x0283;
        public const UInt32 WM_IME_COMPOSITIONFULL          = 0x0284;
        public const UInt32 WM_IME_SELECT                   = 0x0285;
        public const UInt32 WM_IME_CHAR                     = 0x0286;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_IME_REQUEST                  = 0x0288;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_IME_KEYDOWN                  = 0x0290;
        public const UInt32 WM_IME_KEYUP                    = 0x0291;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_MOUSEHOVER                   = 0x02A1;
        public const UInt32 WM_MOUSELEAVE                   = 0x02A3;
#endif
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_NCMOUSEHOVER                 = 0x02A0;
        public const UInt32 WM_NCMOUSELEAVE                 = 0x02A2;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_WTSSESSION_CHANGE            = 0x02B1;

        public const UInt32 WM_TABLET_FIRST                 = 0x02c0;
        public const UInt32 WM_TABLET_LAST                  = 0x02df;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 WM_DPICHANGED                   = 0x02E0;
#endif // WIN32_WINNT_WIN7_LATER
#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0605)
        public const UInt32 WM_DPICHANGED_BEFOREPARENT      = 0x02E2;
        public const UInt32 WM_DPICHANGED_AFTERPARENT       = 0x02E3;
        public const UInt32 WM_GETDPISCALEDSIZE             = 0x02E4;
#endif // WINVER >= 0x0605

        public const UInt32 WM_CUT                          = 0x0300;
        public const UInt32 WM_COPY                         = 0x0301;
        public const UInt32 WM_PASTE                        = 0x0302;
        public const UInt32 WM_CLEAR                        = 0x0303;
        public const UInt32 WM_UNDO                         = 0x0304;
        public const UInt32 WM_RENDERFORMAT                 = 0x0305;
        public const UInt32 WM_RENDERALLFORMATS             = 0x0306;
        public const UInt32 WM_DESTROYCLIPBOARD             = 0x0307;
        public const UInt32 WM_DRAWCLIPBOARD                = 0x0308;
        public const UInt32 WM_PAINTCLIPBOARD               = 0x0309;
        public const UInt32 WM_VSCROLLCLIPBOARD             = 0x030A;
        public const UInt32 WM_SIZECLIPBOARD                = 0x030B;
        public const UInt32 WM_ASKCBFORMATNAME              = 0x030C;
        public const UInt32 WM_CHANGECBCHAIN                = 0x030D;
        public const UInt32 WM_HSCROLLCLIPBOARD             = 0x030E;
        public const UInt32 WM_QUERYNEWPALETTE              = 0x030F;
        public const UInt32 WM_PALETTEISCHANGING            = 0x0310;
        public const UInt32 WM_PALETTECHANGED               = 0x0311;
        public const UInt32 WM_HOTKEY                       = 0x0312;

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_PRINT                        = 0x0317;
        public const UInt32 WM_PRINTCLIENT                  = 0x0318;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 WM_APPCOMMAND                   = 0x0319;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_THEMECHANGED                 = 0x031A;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 WM_CLIPBOARDUPDATE              = 0x031D;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_VISTA_LATER
        public const UInt32 WM_DWMCOMPOSITIONCHANGED        = 0x031E;
        public const UInt32 WM_DWMNCRENDERINGCHANGED        = 0x031F;
        public const UInt32 WM_DWMCOLORIZATIONCOLORCHANGED  = 0x0320;
        public const UInt32 WM_DWMWINDOWMAXIMIZEDCHANGE     = 0x0321;
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 WM_DWMSENDICONICTHUMBNAIL           = 0x0323;
        public const UInt32 WM_DWMSENDICONICLIVEPREVIEWBITMAP   = 0x0326;
#endif // WIN32_WINNT_WIN7_LATER

#if WIN32_WINNT_VISTA_LATER
        public const UInt32 WM_GETTITLEBARINFOEX            = 0x033F;
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_NT4_LATER
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_HANDHELDFIRST                = 0x0358;
        public const UInt32 WM_HANDHELDLAST                 = 0x035F;

        public const UInt32 WM_AFXFIRST                     = 0x0360;
        public const UInt32 WM_AFXLAST                      = 0x037F;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_PENWINFIRST                  = 0x0380;
        public const UInt32 WM_PENWINLAST                   = 0x038F;

#if WIN32_WINNT_NT4_LATER
        public const UInt32 WM_APP                          = 0x8000;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 WM_USER                         = 0x0400;


#if WIN32_WINNT_NT4_LATER
		public const UInt16 WMSZ_LEFT           = 1;
		public const UInt16 WMSZ_RIGHT          = 2;
		public const UInt16 WMSZ_TOP            = 3;
		public const UInt16 WMSZ_TOPLEFT        = 4;
		public const UInt16 WMSZ_TOPRIGHT       = 5;
		public const UInt16 WMSZ_BOTTOM         = 6;
		public const UInt16 WMSZ_BOTTOMLEFT     = 7;
		public const UInt16 WMSZ_BOTTOMRIGHT    = 8;
#endif // WIN32_WINNT_NT4_LATER


        #region NCMESSAGES

        public const Int32 HTERROR             = (-2);
		public const Int32 HTTRANSPARENT       = (-1);
		public const Int32 HTNOWHERE           = 0;
		public const Int32 HTCLIENT            = 1;
		public const Int32 HTCAPTION           = 2;
		public const Int32 HTSYSMENU           = 3;
		public const Int32 HTGROWBOX           = 4;
		public const Int32 HTSIZE              = HTGROWBOX;
		public const Int32 HTMENU              = 5;
		public const Int32 HTHSCROLL           = 6;
		public const Int32 HTVSCROLL           = 7;
		public const Int32 HTMINBUTTON         = 8;
		public const Int32 HTMAXBUTTON         = 9;
		public const Int32 HTLEFT              = 10;
		public const Int32 HTRIGHT             = 11;
		public const Int32 HTTOP               = 12;
		public const Int32 HTTOPLEFT           = 13;
		public const Int32 HTTOPRIGHT          = 14;
		public const Int32 HTBOTTOM            = 15;
		public const Int32 HTBOTTOMLEFT        = 16;
		public const Int32 HTBOTTOMRIGHT       = 17;
		public const Int32 HTBORDER            = 18;
		public const Int32 HTREDUCE            = HTMINBUTTON;
		public const Int32 HTZOOM              = HTMAXBUTTON;
		public const Int32 HTSIZEFIRST         = HTLEFT;
		public const Int32 HTSIZELAST           =HTBOTTOMRIGHT;
#if WIN32_WINNT_NT4_LATER
		public const Int32 HTOBJECT            = 19;
		public const Int32 HTCLOSE             = 20;
		public const Int32 HTHELP              = 21;
#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 SMTO_NORMAL         = 0x0000;
		public const UInt32 SMTO_BLOCK          = 0x0001;
		public const UInt32 SMTO_ABORTIFHUNG    = 0x0002;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_VISTA_LATER
		public const UInt32 SMTO_ERRORONEXIT    = 0x0020;
#endif // WIN32_WINNT_VISTA_LATER
#if WIN32_WINNT_WIN8_LATER
#endif // WIN32_WINNT_WIN8_LATER
        #endregion NCMESSAGES


        public const Int32 MA_ACTIVATE         = 1;
		public const Int32 MA_ACTIVATEANDEAT   = 2;
		public const Int32 MA_NOACTIVATE       = 3;
		public const Int32 MA_NOACTIVATEANDEAT = 4;

		public const UInt32 ICON_SMALL          = 0;
		public const UInt32 ICON_BIG            = 1;
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 ICON_SMALL2         = 2;
#endif // WIN32_WINNT_WINXP_LATER


        // WINUSERAPI UINT WINAPI RegisterWindowMessageA(_In_ LPCSTR lpString);
        // WINUSERAPI UINT WINAPI RegisterWindowMessageW(_In_ LPCWSTR lpString);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern UInt32 RegisterWindowMessageA(string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern UInt32 RegisterWindowMessageW(string lpString);


        public const UInt32 SIZE_RESTORED       = 0;
		public const UInt32 SIZE_MINIMIZED      = 1;
		public const UInt32 SIZE_MAXIMIZED      = 2;
		public const UInt32 SIZE_MAXSHOW        = 3;
		public const UInt32 SIZE_MAXHIDE        = 4;

		public const UInt32 SIZENORMAL          = SIZE_RESTORED;
		public const UInt32 SIZEICONIC          = SIZE_MINIMIZED;
		public const UInt32 SIZEFULLSCREEN      = SIZE_MAXIMIZED;
		public const UInt32 SIZEZOOMSHOW        = SIZE_MAXSHOW;
		public const UInt32 SIZEZOOMHIDE        = SIZE_MAXHIDE;


        public struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndInsertAfter;
			public Int32 x;
			public Int32 y;
			public Int32 cx;
			public Int32 cy;
			public UInt32 flags;
		}

#if CSPROTING
        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
		{
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public WinDef.RECT[] rgrc; //[3];
            [MarshalAs(UnmanagedType.LPStruct)]
			public WINDOWPOS lppos;
		}
#endif

		public const Int32 WVR_ALIGNTOP        = 0x0010;
		public const Int32 WVR_ALIGNLEFT       = 0x0020;
		public const Int32 WVR_ALIGNBOTTOM     = 0x0040;
		public const Int32 WVR_ALIGNRIGHT      = 0x0080;
		public const Int32 WVR_HREDRAW         = 0x0100;
		public const Int32 WVR_VREDRAW         = 0x0200;
		public const Int32 WVR_REDRAW          = (WVR_HREDRAW | WVR_VREDRAW);
		public const Int32 WVR_VALIDRECTS      = 0x0400;


        #region KEYSTATES
        public const UInt32 MK_LBUTTON          = 0x0001;
		public const UInt32 MK_RBUTTON          = 0x0002;
		public const UInt32 MK_SHIFT            = 0x0004;
		public const UInt32 MK_CONTROL          = 0x0008;
		public const UInt32 MK_MBUTTON          = 0x0010;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 MK_XBUTTON1         = 0x0020;
		public const UInt32 MK_XBUTTON2         = 0x0040;
#endif // WIN32_WINNT_WIN2K_LATER
        #endregion KEYSTATES


        #region TRACKMOUSEEVENT

#if WIN32_WINNT_NT4_LATER
        public const UInt32 TME_HOVER       = 0x00000001;
		public const UInt32 TME_LEAVE       = 0x00000002;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 TME_NONCLIENT   = 0x00000010;
#endif // WIN32_WINNT_WIN2K_LATER
		public const UInt32 TME_QUERY       = 0x40000000;
		public const UInt32 TME_CANCEL      = 0x80000000;

		public const UInt32 HOVER_DEFAULT   = 0xFFFFFFFF;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER

        public struct TRACKMOUSEEVENT
		{
			public UInt32 cbSize;
			public UInt32 dwFlags;
			public IntPtr hwndTrack;
			public UInt32 dwHoverTime;
		}

        // WINUSERAPI BOOL WINAPI TrackMouseEvent(_Inout_ LPTRACKMOUSEEVENT lpEventTrack);


#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
#endif // WIN32_WINNT_NT4_LATER

        #endregion TRACKMOUSEEVENT


        #endregion WINMESSAGES


        #region WINSTYLES

        public const UInt32 WS_OVERLAPPED       = 0x00000000;
		public const UInt32 WS_POPUP            = 0x80000000;
		public const UInt32 WS_CHILD            = 0x40000000;
		public const UInt32 WS_MINIMIZE         = 0x20000000;
		public const UInt32 WS_VISIBLE          = 0x10000000;
		public const UInt32 WS_DISABLED         = 0x08000000;
		public const UInt32 WS_CLIPSIBLINGS     = 0x04000000;
		public const UInt32 WS_CLIPCHILDREN     = 0x02000000;
		public const UInt32 WS_MAXIMIZE         = 0x01000000;
		public const UInt32 WS_CAPTION          = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
		public const UInt32 WS_BORDER           = 0x00800000;
		public const UInt32 WS_DLGFRAME         = 0x00400000;
		public const UInt32 WS_VSCROLL          = 0x00200000;
		public const UInt32 WS_HSCROLL          = 0x00100000;
		public const UInt32 WS_SYSMENU          = 0x00080000;
		public const UInt32 WS_THICKFRAME       = 0x00040000;
		public const UInt32 WS_GROUP            = 0x00020000;
		public const UInt32 WS_TABSTOP          = 0x00010000;

		public const UInt32 WS_MINIMIZEBOX      = 0x00020000;
		public const UInt32 WS_MAXIMIZEBOX      = 0x00010000;

		public const UInt32 WS_TILED            = WS_OVERLAPPED;
		public const UInt32 WS_ICONIC           = WS_MINIMIZE;
		public const UInt32 WS_SIZEBOX          = WS_THICKFRAME;
		public const UInt32 WS_TILEDWINDOW      = WS_OVERLAPPEDWINDOW;

		public const UInt32 WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED     |
													WS_CAPTION        |
													WS_SYSMENU        |
													WS_THICKFRAME     | 
													WS_MINIMIZEBOX    | 
													WS_MAXIMIZEBOX);

		public const UInt32 WS_POPUPWINDOW      = (WS_POPUP         |
													WS_BORDER       |
													WS_SYSMENU);

		public const UInt32 WS_CHILDWINDOW      = (WS_CHILD);

		public const UInt32 WS_EX_DLGMODALFRAME     = 0x00000001;
		public const UInt32 WS_EX_NOPARENTNOTIFY    = 0x00000004;
		public const UInt32 WS_EX_TOPMOST           = 0x00000008;
		public const UInt32 WS_EX_ACCEPTFILES       = 0x00000010;
		public const UInt32 WS_EX_TRANSPARENT       = 0x00000020;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 WS_EX_MDICHILD          = 0x00000040;
		public const UInt32 WS_EX_TOOLWINDOW        = 0x00000080;
		public const UInt32 WS_EX_WINDOWEDGE        = 0x00000100;
		public const UInt32 WS_EX_CLIENTEDGE        = 0x00000200;
		public const UInt32 WS_EX_CONTEXTHELP       = 0x00000400;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER
		public const UInt32 WS_EX_RIGHT             = 0x00001000;
		public const UInt32 WS_EX_LEFT              = 0x00000000;
		public const UInt32 WS_EX_RTLREADING        = 0x00002000;
		public const UInt32 WS_EX_LTRREADING        = 0x00000000;
		public const UInt32 WS_EX_LEFTSCROLLBAR     = 0x00004000;
		public const UInt32 WS_EX_RIGHTSCROLLBAR    = 0x00000000;

		public const UInt32 WS_EX_CONTROLPARENT     = 0x00010000;
		public const UInt32 WS_EX_STATICEDGE        = 0x00020000;
		public const UInt32 WS_EX_APPWINDOW         = 0x00040000;


		public const UInt32 WS_EX_OVERLAPPEDWINDOW  = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
		public const UInt32 WS_EX_PALETTEWINDOW     = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 WS_EX_LAYERED           = 0x00080000;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 WS_EX_NOINHERITLAYOUT   = 0x00100000; // Disable inheritence of mirroring by children
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN8_LATER
		public const UInt32 WS_EX_NOREDIRECTIONBITMAP = 0x00200000;
#endif // WIN32_WINNT_WIN8_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WINXP_LATER
		public const UInt32 WS_EX_COMPOSITED        = 0x02000000;
#endif // WIN32_WINNT_WINXP_LATER
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 WS_EX_NOACTIVATE        = 0x08000000;
#endif // WIN32_WINNT_WIN2K_LATER

		public const UInt32 CS_VREDRAW          = 0x0001;
		public const UInt32 CS_HREDRAW          = 0x0002;
		public const UInt32 CS_DBLCLKS          = 0x0008;
		public const UInt32 CS_OWNDC            = 0x0020;
		public const UInt32 CS_CLASSDC          = 0x0040;
		public const UInt32 CS_PARENTDC         = 0x0080;
		public const UInt32 CS_NOCLOSE          = 0x0200;
		public const UInt32 CS_SAVEBITS         = 0x0800;
		public const UInt32 CS_BYTEALIGNCLIENT  = 0x1000;
		public const UInt32 CS_BYTEALIGNWINDOW  = 0x2000;
		public const UInt32 CS_GLOBALCLASS      = 0x4000;

		public const UInt32 CS_IME              = 0x00010000;
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 CS_DROPSHADOW       = 0x00020000;
#endif // WIN32_WINNT_WINXP_LATER

        #endregion WINSTYLES


#if WIN32_WINNT_NT4_LATER
        public const Int32 PRF_CHECKVISIBLE    = 0x00000001;
		public const Int32 PRF_NONCLIENT       = 0x00000002;
		public const Int32 PRF_CLIENT          = 0x00000004;
		public const Int32 PRF_ERASEBKGND      = 0x00000008;
		public const Int32 PRF_CHILDREN        = 0x00000010;
		public const Int32 PRF_OWNED           = 0x00000020;

		public const UInt32 BDR_RAISEDOUTER = 0x0001;
		public const UInt32 BDR_SUNKENOUTER = 0x0002;
		public const UInt32 BDR_RAISEDINNER = 0x0004;
		public const UInt32 BDR_SUNKENINNER = 0x0008;

		public const UInt32 BDR_OUTER       = (BDR_RAISEDOUTER | BDR_SUNKENOUTER);
		public const UInt32 BDR_INNER       = (BDR_RAISEDINNER | BDR_SUNKENINNER);
		public const UInt32 BDR_RAISED      = (BDR_RAISEDOUTER | BDR_RAISEDINNER);
		public const UInt32 BDR_SUNKEN      = (BDR_SUNKENOUTER | BDR_SUNKENINNER);


		public const UInt32 EDGE_RAISED     = (BDR_RAISEDOUTER | BDR_RAISEDINNER);
		public const UInt32 EDGE_SUNKEN     = (BDR_SUNKENOUTER | BDR_SUNKENINNER);
		public const UInt32 EDGE_ETCHED     = (BDR_SUNKENOUTER | BDR_RAISEDINNER);
		public const UInt32 EDGE_BUMP       = (BDR_RAISEDOUTER | BDR_SUNKENINNER);

		public const UInt32 BF_LEFT         = 0x0001;
		public const UInt32 BF_TOP          = 0x0002;
		public const UInt32 BF_RIGHT        = 0x0004;
		public const UInt32 BF_BOTTOM       = 0x0008;

		public const UInt32 BF_TOPLEFT      = (BF_TOP | BF_LEFT);
		public const UInt32 BF_TOPRIGHT     = (BF_TOP | BF_RIGHT);
		public const UInt32 BF_BOTTOMLEFT   = (BF_BOTTOM | BF_LEFT);
		public const UInt32 BF_BOTTOMRIGHT  = (BF_BOTTOM | BF_RIGHT);
		public const UInt32 BF_RECT         = (BF_LEFT | BF_TOP | BF_RIGHT | BF_BOTTOM);

		public const UInt32 BF_DIAGONAL     = 0x0010;

		public const UInt32 BF_DIAGONAL_ENDTOPRIGHT     = (BF_DIAGONAL | BF_TOP | BF_RIGHT);
		public const UInt32 BF_DIAGONAL_ENDTOPLEFT      = (BF_DIAGONAL | BF_TOP | BF_LEFT);
		public const UInt32 BF_DIAGONAL_ENDBOTTOMLEFT   = (BF_DIAGONAL | BF_BOTTOM | BF_LEFT);
		public const UInt32 BF_DIAGONAL_ENDBOTTOMRIGHT  = (BF_DIAGONAL | BF_BOTTOM | BF_RIGHT);

		public const UInt32 BF_MIDDLE       = 0x0800;  /* Fill in the middle */
		public const UInt32 BF_SOFT         = 0x1000;  /* For softer buttons */
		public const UInt32 BF_ADJUST       = 0x2000;  /* Calculate the space left over */
		public const UInt32 BF_FLAT         = 0x4000;  /* For flat rather than 3D borders */
		public const UInt32 BF_MONO         = 0x8000;  /* For monochrome borders */

		/*
		WINUSERAPI BOOL WINAPI DrawEdge(
			_In_ HDC hdc,
			_Inout_ LPRECT qrc,
			_In_ UINT edge,
			_In_ UINT grfFlags);
		*/

		public const UInt32 DFC_CAPTION             = 1;
		public const UInt32 DFC_MENU                = 2;
		public const UInt32 DFC_SCROLL              = 3;
		public const UInt32 DFC_BUTTON              = 4;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DFC_POPUPMENU           = 5;
#endif // WIN32_WINNT_WIN2K_LATER

		public const UInt32 DFCS_CAPTIONCLOSE       = 0x0000;
		public const UInt32 DFCS_CAPTIONMIN         = 0x0001;
		public const UInt32 DFCS_CAPTIONMAX         = 0x0002;
		public const UInt32 DFCS_CAPTIONRESTORE     = 0x0003;
		public const UInt32 DFCS_CAPTIONHELP        = 0x0004;

		public const UInt32 DFCS_MENUARROW          = 0x0000;
		public const UInt32 DFCS_MENUCHECK          = 0x0001;
		public const UInt32 DFCS_MENUBULLET         = 0x0002;
		public const UInt32 DFCS_MENUARROWRIGHT     = 0x0004;
		public const UInt32 DFCS_SCROLLUP           = 0x0000;
		public const UInt32 DFCS_SCROLLDOWN         = 0x0001;
		public const UInt32 DFCS_SCROLLLEFT         = 0x0002;
		public const UInt32 DFCS_SCROLLRIGHT        = 0x0003;
		public const UInt32 DFCS_SCROLLCOMBOBOX     = 0x0005;
		public const UInt32 DFCS_SCROLLSIZEGRIP     = 0x0008;
		public const UInt32 DFCS_SCROLLSIZEGRIPRIGHT = 0x0010;

		public const UInt32 DFCS_BUTTONCHECK        = 0x0000;
		public const UInt32 DFCS_BUTTONRADIOIMAGE   = 0x0001;
		public const UInt32 DFCS_BUTTONRADIOMASK    = 0x0002;
		public const UInt32 DFCS_BUTTONRADIO        = 0x0004;
		public const UInt32 DFCS_BUTTON3STATE       = 0x0008;
		public const UInt32 DFCS_BUTTONPUSH         = 0x0010;

		public const UInt32 DFCS_INACTIVE           = 0x0100;
		public const UInt32 DFCS_PUSHED             = 0x0200;
		public const UInt32 DFCS_CHECKED            = 0x0400;

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DFCS_TRANSPARENT        = 0x0800;
		public const UInt32 DFCS_HOT                = 0x1000;
#endif // WIN32_WINNT_WIN2K_LATER

		public const UInt32 DFCS_ADJUSTRECT         = 0x2000;
		public const UInt32 DFCS_FLAT               = 0x4000;
		public const UInt32 DFCS_MONO               = 0x8000;

    	/*
	    WINUSERAPI BOOL WINAPI DrawFrameControl(
            _In_ HDC,
		    _Inout_ LPRECT,
		    _In_ UINT,
		    _In_ UINT);
	    */

		public const UInt32 DC_ACTIVE           = 0x0001;
		public const UInt32 DC_SMALLCAP         = 0x0002;
		public const UInt32 DC_ICON             = 0x0004;
		public const UInt32 DC_TEXT             = 0x0008;
		public const UInt32 DC_INBUTTON         = 0x0010;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DC_GRADIENT         = 0x0020;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 DC_BUTTONS          = 0x1000;
#endif // WIN32_WINNT_WINXP_LATER

		/*
		WINUSERAPI BOOL WINAPI DrawCaption(
			_In_ IntPtr hwnd,
			_In_ IntPtr hdc,
			_In_ const IntPtr lprect,
			_In_ UInt32 flags);
		*/

		public const Int32 IDANI_OPEN          = 1;
		public const Int32 IDANI_CAPTION       = 3;

		/*
		WINUSERAPI BOOL WINAPI DrawAnimatedRects(
			_In_opt_ HWND hwnd,
			_In_ int idAni,
			_In_ IntPtr lprcFrom,
			_In_ const IntPtr lprcTo);
		*/

#endif // WIN32_WINNT_NT4_LATER


        #region CLIPBOARD

		public const UInt32 CF_TEXT             = 1;
		public const UInt32 CF_BITMAP           = 2;
		public const UInt32 CF_METAFILEPICT     = 3;
		public const UInt32 CF_SYLK             = 4;
		public const UInt32 CF_DIF              = 5;
		public const UInt32 CF_TIFF             = 6;
		public const UInt32 CF_OEMTEXT          = 7;
		public const UInt32 CF_DIB              = 8;
		public const UInt32 CF_PALETTE          = 9;
		public const UInt32 CF_PENDATA          = 10;
		public const UInt32 CF_RIFF             = 11;
		public const UInt32 CF_WAVE             = 12;
		public const UInt32 CF_UNICODETEXT      = 13;
		public const UInt32 CF_ENHMETAFILE      = 14;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 CF_HDROP            = 15;
		public const UInt32 CF_LOCALE           = 16;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 CF_DIBV5            = 17;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 CF_MAX              = 18;
#elif WIN32_WINNT_NT4_LATER
		public const UInt32 CF_MAX              = 17;
#else
		public const UInt32 CF_MAX              = 15;
#endif

		public const UInt32 CF_OWNERDISPLAY     = 0x0080;
		public const UInt32 CF_DSPTEXT          = 0x0081;
		public const UInt32 CF_DSPBITMAP        = 0x0082;
		public const UInt32 CF_DSPMETAFILEPICT  = 0x0083;
		public const UInt32 CF_DSPENHMETAFILE   = 0x008E;

		public const UInt32 CF_PRIVATEFIRST     = 0x0200;
		public const UInt32 CF_PRIVATELAST      = 0x02FF;

		public const UInt32 CF_GDIOBJFIRST      = 0x0300;
		public const UInt32 CF_GDIOBJLAST       = 0x03FF;

        #endregion CLIPBOARD


        public const Byte FVIRTKEY = 0x001; // TRUE;          /* Assumed to be == TRUE */
		public const Byte FNOINVERT = 0x02;
		public const Byte FSHIFT    = 0x04;
		public const Byte FCONTROL  = 0x08;
		public const Byte FALT      = 0x10;


		public struct ACCEL
		{
			public Byte   fVirt;               /* Also called the flags field */
			public UInt16 key;
			public UInt16 cmd;
		}


        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public WinDef.RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }


        public struct CREATESTRUCTA
		{
			public IntPtr lpCreateParams;
			public IntPtr hInstance;
			public IntPtr hMenu;
			public IntPtr hwndParent;
			public Int32 cy;
			public Int32 cx;
			public Int32 y;
			public Int32 x;
			public Int32 style;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpszName;
            public IntPtr lpszClass;    // string or atom
			public UInt32 dwExStyle;
		}


        public struct CREATESTRUCTW
		{
			public IntPtr lpCreateParams;
			public IntPtr hInstance;
			public IntPtr hMenu;
			public IntPtr hwndParent;
			public Int32 cy;
			public Int32 cx;
			public Int32 y;
			public Int32 x;
			public Int32 style;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszName;
            public IntPtr lpszClass;    // string or atom
			public UInt32 dwExStyle;
		}


		public struct WINDOWPLACEMENT
		{
			public UInt32 length;
			public UInt32 flags;
			public UInt32 showCmd;
			public WinDef.POINT ptMinPosition;
			public WinDef.POINT ptMaxPosition;
			public WinDef.RECT rcNormalPosition;
		}


		public const UInt32 WPF_SETMINPOSITION          = 0x0001;
		public const UInt32 WPF_RESTORETOMAXIMIZED      = 0x0002;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 WPF_ASYNCWINDOWPLACEMENT    = 0x0004;
#endif // WIN32_WINNT_WIN2K_LATER



#if WIN32_WINNT_NT4_LATER

		public struct NMHDR
		{
			public IntPtr hwndFrom;
			public UIntPtr idFrom;
			public UInt32 code;         // NM_ code
		}


		public struct STYLESTRUCT
		{
			public UInt32 styleOld;
			public UInt32 styleNew;
		}


#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 ODT_MENU        = 1;
		public const UInt32 ODT_LISTBOX     = 2;
		public const UInt32 ODT_COMBOBOX    = 3;
		public const UInt32 ODT_BUTTON      = 4;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 ODT_STATIC      = 5;
#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 ODA_DRAWENTIRE  = 0x0001;
		public const UInt32 ODA_SELECT      = 0x0002;
		public const UInt32 ODA_FOCUS       = 0x0004;

		public const UInt32 ODS_SELECTED    = 0x0001;
		public const UInt32 ODS_GRAYED      = 0x0002;
		public const UInt32 ODS_DISABLED    = 0x0004;
		public const UInt32 ODS_CHECKED     = 0x0008;
		public const UInt32 ODS_FOCUS       = 0x0010;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 ODS_DEFAULT         = 0x0020;
		public const UInt32 ODS_COMBOBOXEDIT    = 0x1000;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 ODS_HOTLIGHT        = 0x0040;
		public const UInt32 ODS_INACTIVE        = 0x0080;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 ODS_NOACCEL         = 0x0100;
		public const UInt32 ODS_NOFOCUSRECT     = 0x0200;
#endif // WIN32_WINNT_WIN2K_LATER
#endif // WIN32_WINNT_WIN2K_LATER


		public struct MEASUREITEMSTRUCT
		{
			public UInt32 CtlType;
			public UInt32 CtlID;
			public UInt32 itemID;
			public UInt32 itemWidth;
			public UInt32 itemHeight;
			public UIntPtr itemData;
		}

		public struct DRAWITEMSTRUCT
		{
			public UInt32 CtlType;
			public UInt32 CtlID;
			public UInt32 itemID;
			public UInt32 itemAction;
			public UInt32 itemState;
			public IntPtr hwndItem;
			public IntPtr hDC;
			public WinDef.RECT rcItem;
			public UIntPtr itemData;
		}

		public struct DELETEITEMSTRUCT
		{
			public UInt32 CtlType;
			public UInt32 CtlID;
			public UInt32 itemID;
			public IntPtr hwndItem;
			public UIntPtr itemData;
		}

		public struct COMPAREITEMSTRUCT
		{
			public UInt32 CtlType;
			public UInt32 CtlID;
			public IntPtr hwndItem;
			public UInt32 itemID1;
			public UIntPtr itemData1;
			public UInt32 itemID2;
			public UIntPtr itemData2;
			public UInt32 dwLocaleId;
		}


        #region MSG

        /*
		WINUSERAPI BOOL WINAPI GetMessageA(
			_Out_ LPMSG lpMsg,
			_In_opt_ HWND hWnd,
			_In_ UINT wMsgFilterMin,
			_In_ UINT wMsgFilterMax);
			
		WINUSERAPI BOOL WINAPI GetMessageW(
			_Out_ LPMSG lpMsg,
			_In_opt_ HWND hWnd,
			_In_ UINT wMsgFilterMin,
			_In_ UINT wMsgFilterMax);
        */


        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 GetMessageA(
            out MSG lpMsg,
            IntPtr hWnd,
            UInt32 wMsgFilterMin,
            UInt32 wMsgFilterMax);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 GetMessageW(
            out MSG lpMsg,
            IntPtr hWnd,
            UInt32 wMsgFilterMin,
            UInt32 wMsgFilterMax);


        // WINUSERAPI BOOL WINAPI TranslateMessage(_In_ CONST MSG* lpMsg); // no LastError
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);


        // WINUSERAPI LRESULT WINAPI DispatchMessageA(_In_ CONST MSG* lpMsg); // no LastError
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessageA([In] ref MSG lpMsg);

        // WINUSERAPI LRESULT WINAPI DispatchMessageW(_In_ CONST MSG* lpMsg); // no LastError
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessageW([In] ref MSG lpMsg);


        // WINUSERAPI BOOL WINAPI SetMessageQueue(_In_ int cMessagesMax);
        /*
		WINUSERAPI BOOL WINAPI PeekMessageA(
		    _Out_ LPMSG lpMsg,
			_In_opt_ HWND hWnd,
			_In_ UINT wMsgFilterMin,
			_In_ UINT wMsgFilterMax,
			_In_ UINT wRemoveMsg);
			
		WINUSERAPI BOOL WINAPI PeekMessageW(
			_Out_ LPMSG lpMsg,
			_In_opt_ HWND hWnd,
			_In_ UINT wMsgFilterMin,
			_In_ UINT wMsgFilterMax,
			_In_ UINT wRemoveMsg);
		*/
        
        public const UInt32 PM_NOREMOVE         = 0x0000;
		public const UInt32 PM_REMOVE           = 0x0001;
		public const UInt32 PM_NOYIELD          = 0x0002;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 PM_QS_INPUT         = (QS_INPUT << 16);
		public const UInt32 PM_QS_POSTMESSAGE   = ((QS_POSTMESSAGE | QS_HOTKEY | QS_TIMER) << 16);
		public const UInt32 PM_QS_PAINT         = (QS_PAINT << 16);
		public const UInt32 PM_QS_SENDMESSAGE   = (QS_SENDMESSAGE << 16);
#endif // WIN32_WINNT_WIN2K_LATER

        #endregion MSG


        /*
		WINUSERAPI BOOL WINAPI RegisterHotKey(
			_In_opt_ HWND hWnd,
			_In_ int id,
			_In_ UINT fsModifiers,
			_In_ UINT vk);
		*/

        //WINUSERAPI BOOL WINAPI UnregisterHotKey(_In_opt_ HWND hWnd, _In_ int id);


        public const UInt32 MOD_ALT             = 0x0001;
        public const UInt32 MOD_CONTROL         = 0x0002;
        public const UInt32 MOD_SHIFT           = 0x0004;
        public const UInt32 MOD_WIN             = 0x0008;
#if WIN32_WINNT_WIN7_LATER
        public const UInt32 MOD_NOREPEAT        = 0x4000;
#endif // WIN32_WINNT_WIN7_LATER


        public const UInt32 IDHOT_SNAPWINDOW    = unchecked((UInt32)(-1));    /* SHIFT-PRINTSCRN  */
        public const UInt32 IDHOT_SNAPDESKTOP   = unchecked((UInt32)(-2));    /* PRINTSCRN        */


//#if WIN_INTERNAL
//#if !LSTRING
//#define NOLSTRING
//#endif // LSTRING
//#if !LFILEIO
//#define NOLFILEIO
//#endif // LFILEIO
//#endif // WIN_INTERNAL


#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr ENDSESSION_CLOSEAPP = (IntPtr)0x00000001;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr ENDSESSION_CRITICAL = (IntPtr)0x40000000;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr ENDSESSION_LOGOFF   = (IntPtr)unchecked((Int32)0x80000000);
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 EWX_LOGOFF                  = 0x00000000;
        public const UInt32 EWX_SHUTDOWN                = 0x00000001;
        public const UInt32 EWX_REBOOT                  = 0x00000002;
        public const UInt32 EWX_FORCE                   = 0x00000004;
        public const UInt32 EWX_POWEROFF                = 0x00000008;
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 EWX_FORCEIFHUNG             = 0x00000010;
#endif // WIN32_WINNT_WIN2K_LATER
        public const UInt32 EWX_QUICKRESOLVE            = 0x00000020;
#if WIN32_WINNT_VISTA_LATER
        public const UInt32 EWX_RESTARTAPPS             = 0x00000040;
#endif // WIN32_WINNT_VISTA_LATER
        public const UInt32 EWX_HYBRID_SHUTDOWN         = 0x00400000;
        public const UInt32 EWX_BOOTOPTIONS             = 0x01000000;


        //#define ExitWindows(dwReserved, Code) ExitWindowsEx(EWX_LOGOFF, 0xFFFFFFFF)
        //_When_((uFlags&(EWX_POWEROFF | EWX_SHUTDOWN | EWX_FORCE)) != 0,
        //__drv_preferredFunction("InitiateSystemShutdownEx",
        //"Legacy API. Rearchitect to avoid Reboot"))

        // WINUSERAPI BOOL WINAPI ExitWindowsEx(_In_ UINT uFlags, _In_ DWORD dwReason);

        // WINUSERAPI BOOL WINAPI SwapMouseButton(_In_ BOOL fSwap);

        // WINUSERAPI DWORD WINAPI GetMessagePos(VOID); // no LastError
        [DllImport("user32.dll")]
        public static extern UInt32 GetMessagePos();

        // WINUSERAPI LONG WINAPI GetMessageTime(VOID);

        // WINUSERAPI LPARAM WINAPI GetMessageExtraInfo(VOID);

#if WIN32_WINNT_WIN8_LATER
        // WINUSERAPI DWORD WINAPI GetUnpredictedMessagePos(VOID);
#endif // WIN32_WINNT_WIN8_LATER

#if WIN32_WINNT_WINXP_LATER
        // WINUSERAPI BOOL WINAPI IsWow64Message(VOID);
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_NT4_LATER
        // WINUSERAPI LPARAM WINAPI SetMessageExtraInfo(_In_ LPARAM lParam);
#endif // WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI LRESULT WINAPI SendMessageA(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_Pre_maybenull_ _Post_valid_ WPARAM wParam,
			_Pre_maybenull_ _Post_valid_ LPARAM lParam);
		*/
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessageA(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam);
        /*
        WINUSERAPI LRESULT WINAPI SendMessageW(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_Pre_maybenull_ _Post_valid_ WPARAM wParam,
			_Pre_maybenull_ _Post_valid_ LPARAM lParam);
        */
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessageW(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam);
	    
        /*
		WINUSERAPI LRESULT WINAPI SendMessageTimeoutA(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_In_ UINT fuFlags,
			_In_ UINT uTimeout,
			_Out_opt_ PDWORD_PTR lpdwResult);
	
		WINUSERAPI LRESULT WINAPI SendMessageTimeoutW(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_In_ UINT fuFlags,
			_In_ UINT uTimeout,
			_Out_opt_ PDWORD_PTR lpdwResult);
        */			
        /*
		WINUSERAPI BOOL WINAPI SendNotifyMessageA(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
			
		WINUSERAPI BOOL WINAPI SendNotifyMessageW(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
		*/
        /*
		WINUSERAPI BOOL WINAPI SendMessageCallbackA(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_In_ SENDASYNCPROC lpResultCallBack,
			_In_ ULONG_PTR dwData);
			
		WINUSERAPI BOOL WINAPI SendMessageCallbackW(
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_In_ SENDASYNCPROC lpResultCallBack,
			_In_ ULONG_PTR dwData);
		*/

#if WIN32_WINNT_WINXP_LATER
        /*
		public struct BSMINFO
		{
			public UInt32 cbSize;
			public IntPtr hdesk;
			public IntPtr hwnd;
			public LUID luid;
		}
        */
        /*
		WINUSERAPI long WINAPI BroadcastSystemMessageExA(
			_In_ DWORD flags,
			_Inout_opt_ LPDWORD lpInfo,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_Out_opt_ PBSMINFO pbsmInfo);
			
		WINUSERAPI long WINAPI BroadcastSystemMessageExW(
			_In_ DWORD flags,
			_Inout_opt_ LPDWORD lpInfo,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam,
			_Out_opt_ PBSMINFO pbsmInfo);
		*/
#endif // WIN32_WINNT_WINXP_LATER


#if WIN32_WINNT_NT4_LATER


#if true //(_WIN32_WINNT)
        /*
		WINUSERAPI long WINAPI BroadcastSystemMessageA(
			_In_ DWORD flags,
			_Inout_opt_ LPDWORD lpInfo,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
				
		WINUSERAPI long WINAPI BroadcastSystemMessageW(
			_In_ DWORD flags,
			_Inout_opt_ LPDWORD lpInfo,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
        */
//#elif // defined(_WIN32_WINDOWS)
// The Win95 version isn't A/W decorated
		/*
		WINUSERAPI long WINAPI BroadcastSystemMessage(
			_In_ DWORD flags,
			_Inout_opt_ LPDWORD lpInfo,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
		*/
#endif


        public static readonly IntPtr BSM_ALLCOMPONENTS       = (IntPtr)0x00000000;
		public static readonly IntPtr BSM_VXDS                = (IntPtr)0x00000001;
        public static readonly IntPtr BSM_NETDRIVER           = (IntPtr)0x00000002;
        public static readonly IntPtr BSM_INSTALLABLEDRIVERS  = (IntPtr)0x00000004;
        public static readonly IntPtr BSM_APPLICATIONS        = (IntPtr)0x00000008;
        public static readonly IntPtr BSM_ALLDESKTOPS         = (IntPtr)0x00000010;

        public const UInt32 BSF_QUERY               = 0x00000001;
		public const UInt32 BSF_IGNORECURRENTTASK   = 0x00000002;
		public const UInt32 BSF_FLUSHDISK           = 0x00000004;
		public const UInt32 BSF_NOHANG              = 0x00000008;
		public const UInt32 BSF_POSTMESSAGE         = 0x00000010;
		public const UInt32 BSF_FORCEIFHUNG         = 0x00000020;
		public const UInt32 BSF_NOTIMEOUTIFNOTHUNG  = 0x00000040;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 BSF_ALLOWSFW            = 0x00000080;
		public const UInt32 BSF_SENDNOTIFYMESSAGE   = 0x00000100;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 BSF_RETURNHDESK         = 0x00000200;
		public const UInt32 BSF_LUID                = 0x00000400;
#endif // WIN32_WINNT_WINXP_LATER

        public static readonly IntPtr BROADCAST_QUERY_DENY  = (IntPtr)0x424D5144;  // Return this value to deny a query.
#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_WIN2K_LATER
        //typedef PVOID           HDEVNOTIFY;
        //typedef HDEVNOTIFY     * PHDEVNOTIFY;

        public const UInt32 DEVICE_NOTIFY_WINDOW_HANDLE          = 0x00000000;
        public const UInt32 DEVICE_NOTIFY_SERVICE_HANDLE         = 0x00000001;
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 DEVICE_NOTIFY_ALL_INTERFACE_CLASSES  = 0x00000004;
#endif // WIN32_WINNT_WINXP_LATER

        /*
		WINUSERAPI HDEVNOTIFY WINAPI RegisterDeviceNotificationA(
			_In_ HANDLE hRecipient,
			_In_ LPVOID NotificationFilter,
			_In_ DWORD Flags);
			
		WINUSERAPI HDEVNOTIFY WINAPI RegisterDeviceNotificationW(
			_In_ HANDLE hRecipient,
			_In_ LPVOID NotificationFilter,
			_In_ DWORD Flags);
		*/

        // WINUSERAPI BOOL WINAPI UnregisterDeviceNotification(_In_ HDEVNOTIFY Handle);

#if WIN32_WINNT_WS03_LATER

#if CSPORTING
//#if !_HPOWERNOTIFY_DEF_
//#define _HPOWERNOTIFY_DEF_
        //typedef PVOID           HPOWERNOTIFY;
        //typedef HPOWERNOTIFY   * PHPOWERNOTIFY;
//#endif
#endif

        /*
		WINUSERAPI HPOWERNOTIFY WINAPI RegisterPowerSettingNotification(
			IN HANDLE hRecipient,
			IN LPCGUID PowerSettingGuid,
			IN DWORD Flags);
	
		// WINUSERAPI BOOL WINAPI UnregisterPowerSettingNotification(IN HPOWERNOTIFY Handle);

		// WINUSERAPI HPOWERNOTIFY WINAPI RegisterSuspendResumeNotification(IN HANDLE hRecipient, IN DWORD Flags);

		// WINUSERAPI BOOL WINAPI UnregisterSuspendResumeNotification(IN HPOWERNOTIFY Handle);
        */

#endif // WIN32_WINNT_WS03_LATER
#endif // WIN32_WINNT_WIN2K_LATER


        /*
		WINUSERAPI BOOL WINAPI PostMessageA(
			_In_opt_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);

		WINUSERAPI BOOL WINAPI PostMessageW(
			_In_opt_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
        */
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessageA(IntPtr hwnd, uint message, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessageW(IntPtr hwnd, uint message, UIntPtr wParam, IntPtr lParam);

        /*
		WINUSERAPI BOOL WINAPI PostThreadMessageA(
			_In_ DWORD idThread,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
		    _In_ LPARAM lParam);
			
		WINUSERAPI BOOL WINAPI PostThreadMessageW(
		    _In_ DWORD idThread,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
        */


        //#define PostAppMessageA(idThread, wMsg, wParam, lParam)\
        //		PostThreadMessageA((DWORD) idThread, wMsg, wParam, lParam)
        //#define PostAppMessageW(idThread, wMsg, wParam, lParam)\
        //		PostThreadMessageW((DWORD) idThread, wMsg, wParam, lParam)

        public static readonly IntPtr HWND_BROADCAST    = ((IntPtr)0xffff);

#if WIN32_WINNT_WIN2K_LATER
        public static readonly IntPtr HWND_MESSAGE      = ((IntPtr)(-3));
#endif // WIN32_WINNT_WIN2K_LATER


        /*
		WINUSERAPI BOOL WINAPI AttachThreadInput(
			_In_ DWORD idAttach,
			_In_ DWORD idAttachTo,
			_In_ BOOL fAttach);
		*/
        // WINUSERAPI BOOL WINAPI ReplyMessage(_In_ LRESULT lResult);

        // WINUSERAPI BOOL WINAPI WaitMessage(VOID);


        // WINUSERAPI DWORD WINAPI WaitForInputIdle(_In_ HANDLE hProcess, _In_ DWORD dwMilliseconds);
        /*
		WINUSERAPI LRESULT WINAPI DefWindowProcA(
		    _In_ HWND hWnd,
		    _In_ UINT Msg,
		    _In_ WPARAM wParam,
		    _In_ LPARAM lParam);

		WINUSERAPI LRESULT WINAPI DefWindowProcW(
		    _In_ HWND hWnd,
		    _In_ UINT Msg,
		    _In_ WPARAM wParam,
		    _In_ LPARAM lParam);
		*/
        [DllImport("user32.dll")] // no LastError
        public static extern IntPtr DefWindowProcW(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam);


        // WINUSERAPI VOID WINAPI PostQuitMessage(_In_ int nExitCode);
        [DllImport("user32.dll")] // no LastError
        public static extern void PostQuitMessage(int nExitCode);

        /*
		WINUSERAPI LRESULT WINAPI CallWindowProcA(
			_In_ WNDPROC lpPrevWndFunc,
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
			
		WINUSERAPI LRESULT WINAPI CallWindowProcW(
			_In_ WNDPROC lpPrevWndFunc,
			_In_ HWND hWnd,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
		//#ifdef UNICODE
		//#define CallWindowProc  CallWindowProcW
		//#else
		//#define CallWindowProc  CallWindowProcA
		//#endif // !UNICODE
        */

        // WINUSERAPI BOOL WINAPI InSendMessage(VOID);


#if WIN32_WINNT_WIN2K_LATER


        // WINUSERAPI DWORD WINAPI InSendMessageEx(_Reserved_ LPVOID lpReserved);


        public const UInt32 ISMEX_NOSEND      = 0x00000000;
        public const UInt32 ISMEX_SEND        = 0x00000001;
        public const UInt32 ISMEX_NOTIFY      = 0x00000002;
        public const UInt32 ISMEX_CALLBACK    = 0x00000004;
        public const UInt32 ISMEX_REPLIED     = 0x00000008;

#endif // WIN32_WINNT_WIN2K_LATER


        // WINUSERAPI UINT WINAPI GetDoubleClickTime(VOID);

        // WINUSERAPI BOOL WINAPI SetDoubleClickTime(_In_ UINT);

        //WINUSERAPI ATOM WINAPI RegisterClassA(_In_ CONST WNDCLASSA* lpWndClass);
        //WINUSERAPI ATOM WINAPI RegisterClassW(_In_ CONST WNDCLASSW* lpWndClass);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt16 RegisterClassA([In] ref WNDCLASSA lpWndClass);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt16 RegisterClassW([In] ref WNDCLASSW lpWndClass);
        /*
		WINUSERAPI BOOL WINAPI UnregisterClassA(_In_ LPCSTR lpClassName, _In_opt_ HINSTANCE hInstance);
		
		WINUSERAPI BOOL WINAPI UnregisterClassW(_In_ LPCWSTR lpClassName, _In_opt_ HINSTANCE hInstance);
		*/
        /*
		_Success_(return) WINUSERAPI BOOL WINAPI GetClassInfoA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCSTR lpClassName,
			_Out_ LPWNDCLASSA lpWndClass);
			
		_Success_(return) WINUSERAPI BOOL WINAPI GetClassInfoW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCWSTR lpClassName,
			_Out_ LPWNDCLASSW lpWndClass);
		*/

#if WIN32_WINNT_NT4_LATER

        // WINUSERAPI ATOM WINAPI RegisterClassExA(_In_ CONST WNDCLASSEXA*);
        // WINUSERAPI ATOM WINAPI RegisterClassExW(_In_ CONST WNDCLASSEXW*);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt16 RegisterClassExA([In] ref WNDCLASSEXA lpWndClass);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt16 RegisterClassExW([In] ref WNDCLASSEXW lpWndClass);

        /*
		_Success_(return) WINUSERAPI BOOL WINAPI GetClassInfoExA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCSTR lpszClass,
			_Out_ LPWNDCLASSEXA lpwcx);
			
		_Success_(return) WINUSERAPI BOOL WINAPI GetClassInfoExW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCWSTR lpszClass,
			_Out_ LPWNDCLASSEXW lpwcx);
		*/
#endif // WIN32_WINNT_NT4_LATER


        public const Int32 CW_USEDEFAULT       = unchecked((Int32)0x80000000);

        public static IntPtr HWND_DESKTOP       = ((IntPtr)0);



#if WIN32_WINNT_WINXP_LATER
        //typedef BOOLEAN(WINAPI* PREGISTERCLASSNAMEW)(LPCWSTR);
#endif // WIN32_WINNT_WINXP_LATER

        /*
		WINUSERAPI HWND WINAPI CreateWindowExA(
			_In_ DWORD dwExStyle,
			_In_opt_ LPCSTR lpClassName,
			_In_opt_ LPCSTR lpWindowName,
			_In_ DWORD dwStyle,
			_In_ int X,
			_In_ int Y,
			_In_ int nWidth,
			_In_ int nHeight,
			_In_opt_ HWND hWndParent,
			_In_opt_ HMENU hMenu,
			_In_opt_ HINSTANCE hInstance,
			_In_opt_ LPVOID lpParam);
			
		WINUSERAPI HWND WINAPI CreateWindowExW(
			_In_ DWORD dwExStyle,
			_In_opt_ LPCWSTR lpClassName,
			_In_opt_ LPCWSTR lpWindowName,
			_In_ DWORD dwStyle,
			_In_ int X,
			_In_ int Y,
			_In_ int nWidth,
			_In_ int nHeight,
			_In_opt_ HWND hWndParent,
			_In_opt_ HMENU hMenu,
			_In_opt_ HINSTANCE hInstance,
			_In_opt_ LPVOID lpParam);
        */

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateWindowExA(
            UInt32 dwExStyle,
            IntPtr lpClassName, // text or atom
            string lpWindowName,
            UInt32 dwStyle,
            Int32 X,
            Int32 Y,
            Int32 nWidth,
            Int32 nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateWindowExW(
			UInt32 dwExStyle,
			IntPtr lpClassName, // text or atom
            string lpWindowName,
			UInt32 dwStyle,
			Int32 X,
			Int32 Y,
			Int32 nWidth,
			Int32 nHeight,
			IntPtr hWndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam);


        //#define CreateWindowA(lpClassName, lpWindowName, dwStyle, x, y,\
        //			nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam)\
        //			CreateWindowExA(0L, lpClassName, lpWindowName, dwStyle, x, y,\
        //				nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam)
        public static IntPtr CreateWindowA(
            IntPtr lpClassName, // text or atom
			string lpWindowName,
            UInt32 dwStyle,
			Int32 X,
            Int32 Y,
			Int32 nWidth,
            Int32 nHeight,
			IntPtr hWndParent,
            IntPtr hMenu,
			IntPtr hInstance,
            IntPtr lpParam)
        {
            return CreateWindowExA(0U, lpClassName, lpWindowName, dwStyle, X, Y,
                nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
        }

        //#define CreateWindowW(lpClassName, lpWindowName, dwStyle, x, y,\
        //			nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam)\
        //			CreateWindowExW(0L, lpClassName, lpWindowName, dwStyle, x, y,\
        //				nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam)


        // WINUSERAPI BOOL WINAPI IsWindow(_In_opt_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI IsMenu(_In_ HMENU hMenu);

        // WINUSERAPI BOOL WINAPI IsChild(_In_ HWND hWndParent, _In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI DestroyWindow(_In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI ShowWindow(_In_ HWND hWnd, _In_ int nCmdShow); // no LastError
        [DllImport("user32.dll")]        
        public static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);

#if WIN32_WINNT_WIN2K_LATER
        /*
		WINUSERAPI BOOL WINAPI AnimateWindow(
			_In_ HWND hWnd,
			_In_ DWORD dwTime,
			_In_ DWORD dwFlags);
		*/
#endif // WIN32_WINNT_WIN2K_LATER


#if WIN32_WINNT_WIN2K_LATER
#if _WINGDI_ && !NOGDI

        /*
		WINUSERAPI BOOL WINAPI UpdateLayeredWindow(
			_In_ HWND hWnd,
			_In_opt_ HDC hdcDst,
			_In_opt_ POINT* pptDst,
			_In_opt_ SIZE* psize,
			_In_opt_ HDC hdcSrc,
			_In_opt_ POINT* pptSrc,
			_In_ COLORREF crKey,
			_In_opt_ BLENDFUNCTION* pblend,
			_In_ DWORD dwFlags);
		*/

#if CSPROTING
        public struct UPDATELAYEREDWINDOWINFO
		{
			public UInt32 cbSize;
			public IntPtr hdcDst;
			public const WinDef.POINT* pptDst;
			public const WinDef.SIZE* psize;
            public IntPtr hdcSrc;
			public const WinDef.POINT* pptSrc;
			public UInt32 crKey;
			public const BLENDFUNCTION* pblend;
			public UInt32 dwFlags;
			public const WinDef.RECT* prcDirty;
		}
#endif // CSPORTING

#if !WIN32_WINNT_VISTA_LATER // (_WIN32_WINNT < 0x0502)
		//typedef
#endif // _WIN32_WINNT < 0x0502

        /*
		WINUSERAPI BOOL WINAPI UpdateLayeredWindowIndirect(
			_In_ HWND hWnd,
			_In_ const UPDATELAYEREDWINDOWINFO* pULWInfo);
		*/

#endif
#if WIN32_WINNT_WINXP_LATER

        /*
		WINUSERAPI BOOL WINAPI GetLayeredWindowAttributes(
			_In_ HWND hwnd,
			_Out_opt_ COLORREF* pcrKey,
			_Out_opt_ BYTE* pbAlpha,
			_Out_opt_ DWORD* pdwFlags);
        */
        public const UInt32 PW_CLIENTONLY           = 0x00000001;

#if WIN32_WINNT_WINBLUE_LATER
        public const UInt32 PW_RENDERFULLCONTENT    = 0x00000002;
#endif // WIN32_WINNT_WINBLUE_LATER

        /*
		WINUSERAPI BOOL WINAPI PrintWindow(
			_In_ HWND hwnd,
			_In_ HDC hdcBlt,
			_In_ UINT nFlags);
		*/


#endif // WIN32_WINNT_WINXP_LATER

        /*
		WINUSERAPI BOOL WINAPI SetLayeredWindowAttributes(
			_In_ HWND hwnd,
			_In_ COLORREF crKey,
			_In_ BYTE bAlpha,
			_In_ DWORD dwFlags);
		*/


        public const UInt32 LWA_COLORKEY            = 0x00000001;
		public const UInt32 LWA_ALPHA               = 0x00000002;


		public const UInt32 ULW_COLORKEY            = 0x00000001;
		public const UInt32 ULW_ALPHA               = 0x00000002;
		public const UInt32 ULW_OPAQUE              = 0x00000004;

		public const UInt32 ULW_EX_NORESIZE         = 0x00000008;

#endif // WIN32_WINNT_WIN2K_LATER


#if WIN32_WINNT_NT4_LATER
		// WINUSERAPI BOOL WINAPI ShowWindowAsync(_In_ HWND hWnd, _In_  int nCmdShow);
#endif // WIN32_WINNT_NT4_LATER

		// WINUSERAPI BOOL WINAPI FlashWindow(_In_ HWND hWnd, _In_ BOOL bInvert);

#if WIN32_WINNT_WIN2K_LATER
		public struct FLASHWINFO
		{
		    public UInt32 cbSize;
			public IntPtr hwnd;
			public UInt32 dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		// WINUSERAPI BOOL WINAPI FlashWindowEx(_In_ PFLASHWINFO pfwi);

		public const UInt32 FLASHW_STOP         = 0;
		public const UInt32 FLASHW_CAPTION      = 0x00000001;
		public const UInt32 FLASHW_TRAY         = 0x00000002;
		public const UInt32 FLASHW_ALL          = (FLASHW_CAPTION | FLASHW_TRAY);
		public const UInt32 FLASHW_TIMER        = 0x00000004;
		public const UInt32 FLASHW_TIMERNOFG    = 0x0000000C;

#endif // WIN32_WINNT_WIN2K_LATER

        // WINUSERAPI BOOL WINAPI ShowOwnedPopups(_In_ HWND hWnd, _In_ BOOL fShow);

        // WINUSERAPI BOOL WINAPI OpenIcon(_In_ HWND hWnd); 

        // WINUSERAPI BOOL WINAPI CloseWindow(_In_ HWND hWnd);
        /*
		WINUSERAPI BOOL WINAPI MoveWindow(
			_In_ HWND hWnd,
			_In_ int X,
			_In_ int Y,
			_In_ int nWidth,
			_In_ int nHeight,
			_In_ BOOL bRepaint);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetWindowPos(
			_In_ HWND hWnd,
			_In_opt_ HWND hWndInsertAfter,
			_In_ int X,
			_In_ int Y,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT uFlags);
		*/
        // WINUSERAPI BOOL WINAPI GetWindowPlacement(_In_ HWND hWnd, _Inout_ WINDOWPLACEMENT *lpwndpl);
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        // WINUSERAPI BOOL WINAPI SetWindowPlacement(_In_ HWND hWnd, _In_ CONST WINDOWPLACEMENT* lpwndpl);
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 WDA_NONE        = 0x00000000;
        public const UInt32 WDA_MONITOR     = 0x00000001;

        // WINUSERAPI BOOL WINAPI GetWindowDisplayAffinity(_In_ HWND hWnd, _Out_ DWORD* pdwAffinity);

        // WINUSERAPI BOOL WINAPI SetWindowDisplayAffinity(_In_ HWND hWnd, _In_ DWORD dwAffinity);

#endif // WIN32_WINNT_WIN7_LATER



        #region DEFERWINDOWPOS

        // WINUSERAPI HDWP WINAPI BeginDeferWindowPos(_In_ int nNumWindows);
        /*
		WINUSERAPI HDWP WINAPI DeferWindowPos(
			_In_ HDWP hWinPosInfo,
			_In_ HWND hWnd,
			_In_opt_ HWND hWndInsertAfter,
			_In_ int x,
			_In_ int y,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT uFlags);
		*/
        // WINUSERAPI BOOL WINAPI EndDeferWindowPos(_In_ HDWP hWinPosInfo);

        #endregion DEFERWINDOWPOS


        // WINUSERAPI BOOL WINAPI IsWindowVisible(_In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI IsIconic(_In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI AnyPopup(VOID);

        // WINUSERAPI BOOL WINAPI BringWindowToTop(_In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI IsZoomed(_In_ HWND hWnd);

        public const UInt32 SWP_NOSIZE          = 0x0001;
		public const UInt32 SWP_NOMOVE          = 0x0002;
		public const UInt32 SWP_NOZORDER        = 0x0004;
		public const UInt32 SWP_NOREDRAW        = 0x0008;
		public const UInt32 SWP_NOACTIVATE      = 0x0010;
		public const UInt32 SWP_FRAMECHANGED    = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
		public const UInt32 SWP_SHOWWINDOW      = 0x0040;
		public const UInt32 SWP_HIDEWINDOW      = 0x0080;
		public const UInt32 SWP_NOCOPYBITS      = 0x0100;
		public const UInt32 SWP_NOOWNERZORDER   = 0x0200;  /* Don't do owner Z ordering */
		public const UInt32 SWP_NOSENDCHANGING  = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

		public const UInt32 SWP_DRAWFRAME       = SWP_FRAMECHANGED;
		public const UInt32 SWP_NOREPOSITION    = SWP_NOOWNERZORDER;

#if WIN32_WINNT_NT4_LATER
		public const UInt32 SWP_DEFERERASE      = 0x2000;
		public const UInt32 SWP_ASYNCWINDOWPOS  = 0x4000;
#endif // WIN32_WINNT_NT4_LATER

        public static readonly IntPtr HWND_TOP        = (IntPtr)0;
        public static readonly IntPtr HWND_BOTTOM     = (IntPtr)1;
        public static readonly IntPtr HWND_TOPMOST    = (IntPtr)(-1);
        public static readonly IntPtr HWND_NOTOPMOST  = (IntPtr)(-2);


        #region CTLMGR

#if CSPORTING
//#include <pshpack2.h>
#endif

        public struct DLGTEMPLATE
		{
			public UInt32 style;
			public UInt32 dwExtendedStyle;
			public UInt16 cdit;
			public Int16 x;
			public Int16 y;
			public Int16 cx;
			public Int16 cy;
		} ;


        public struct  DLGITEMTEMPLATE
		{
			public UInt32 style;
			public UInt32 dwExtendedStyle;
			public Int16 x;
			public Int16 y;
			public Int16 cx;
			public Int16 cy;
			public UInt16 id;
		}


#if CSPORTING
//# include <poppack.h> /* Resume normal packing */
#endif

        /*
		WINUSERAPI HWND WINAPI CreateDialogParamA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCSTR lpTemplateName,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);

		WINUSERAPI HWND WINAPI CreateDialogParamW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCWSTR lpTemplateName,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
		*/
        /*
		WINUSERAPI HWND WINAPI CreateDialogIndirectParamA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCDLGTEMPLATEA lpTemplate,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
			
		WINUSERAPI HWND WINAPI CreateDialogIndirectParamW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCDLGTEMPLATEW lpTemplate,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
		*/

        //#define CreateDialogA(hInstance, lpName, hWndParent, lpDialogFunc) \
        //CreateDialogParamA(hInstance, lpName, hWndParent, lpDialogFunc, 0L)
        //#define CreateDialogW(hInstance, lpName, hWndParent, lpDialogFunc) \
        //CreateDialogParamW(hInstance, lpName, hWndParent, lpDialogFunc, 0L)

        //#define CreateDialogIndirectA(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
        //CreateDialogIndirectParamA(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)
        //#define CreateDialogIndirectW(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
        //CreateDialogIndirectParamW(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)
        /*
		WINUSERAPI INT_PTR WINAPI DialogBoxParamA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCSTR lpTemplateName,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
			
		WINUSERAPI INT_PTR WINAPI DialogBoxParamW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCWSTR lpTemplateName,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
        */
		/*
		WINUSERAPI INT_PTR WINAPI DialogBoxIndirectParamA(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCDLGTEMPLATEA hDialogTemplate,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
			
		WINUSERAPI INT_PTR WINAPI DialogBoxIndirectParamW(
			_In_opt_ HINSTANCE hInstance,
			_In_ LPCDLGTEMPLATEW hDialogTemplate,
			_In_opt_ HWND hWndParent,
			_In_opt_ DLGPROC lpDialogFunc,
			_In_ LPARAM dwInitParam);
        */
		//#define DialogBoxA(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
		//DialogBoxParamA(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)
		//#define DialogBoxW(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
		//DialogBoxParamW(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)

		//#define DialogBoxIndirectA(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
		//DialogBoxIndirectParamA(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)
		//#define DialogBoxIndirectW(hInstance, lpTemplate, hWndParent, lpDialogFunc) \
		//DialogBoxIndirectParamW(hInstance, lpTemplate, hWndParent, lpDialogFunc, 0L)

		// WINUSERAPI BOOL WINAPI EndDialog(_In_ HWND hDlg, _In_ INT_PTR nResult);

		// WINUSERAPI HWND WINAPI GetDlgItem(_In_opt_ HWND hDlg, _In_ int nIDDlgItem);
		/*
		WINUSERAPI BOOL WINAPI SetDlgItemInt(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_In_ UINT uValue,
			_In_ BOOL bSigned);
		*/
        /*
		WINUSERAPI UINT WINAPI GetDlgItemInt(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_Out_opt_ BOOL *lpTranslated,
			_In_ BOOL bSigned);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetDlgItemTextA(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_In_ LPCSTR lpString);
			
		WINUSERAPI BOOL WINAPI SetDlgItemTextW(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_In_ LPCWSTR lpString);
		*/
        /*
		_Ret_range_(0, cchMax) WINUSERAPI UINT WINAPI GetDlgItemTextA(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_Out_writes_(cchMax) LPSTR lpString,
			_In_ int cchMax);
			
		_Ret_range_(0, cchMax) WINUSERAPI UINT WINAPI GetDlgItemTextW(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_Out_writes_(cchMax) LPWSTR lpString,
			_In_ int cchMax);
		*/
        /*
		WINUSERAPI BOOL WINAPI CheckDlgButton(
			_In_ HWND hDlg,
			_In_ int nIDButton,
			_In_ UINT uCheck);
		*/
        /*
		WINUSERAPI BOOL WINAPI CheckRadioButton(
		    _In_ HWND hDlg,
			_In_ int nIDFirstButton,
			_In_ int nIDLastButton,
			_In_ int nIDCheckButton);
		*/

        // WINUSERAPI UINT WINAPI IsDlgButtonChecked(_In_ HWND hDlg, _In_ int nIDButton);
        /*
		WINUSERAPI LRESULT WINAPI SendDlgItemMessageA(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
			
		WINUSERAPI LRESULT WINAPI SendDlgItemMessageW(
			_In_ HWND hDlg,
			_In_ int nIDDlgItem,
			_In_ UINT Msg,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
		*/
        /*
		WINUSERAPI HWND WINAPI GetNextDlgGroupItem(
			_In_ HWND hDlg,
			_In_opt_ HWND hCtl,
			_In_ BOOL bPrevious);
		*/
        /*
		WINUSERAPI HWND WINAPI GetNextDlgTabItem(
			_In_ HWND hDlg,
			_In_opt_ HWND hCtl,
			_In_ BOOL bPrevious);
		*/

        // WINUSERAPI int WINAPI GetDlgCtrlID(_In_ HWND hWnd);

        //WINUSERAPI long WINAPI GetDialogBaseUnits(VOID);
        /*
        WINUSERAPI LRESULT WINAPI DefDlgProcA(
            _In_ HWND hDlg,
            _In_ UINT Msg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);

        WINUSERAPI LRESULT WINAPI DefDlgProcW(
            _In_ HWND hDlg,
            _In_ UINT Msg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);
        */

        public enum DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS
		{
			DCDC_DEFAULT = 0x0000,
			DCDC_DISABLE_FONT_UPDATE = 0x0001,
			DCDC_DISABLE_RELAYOUT = 0x0002,
		}


#if CSPORTING
#if !MIDL_PASS
        DEFINE_ENUM_FLAG_OPERATORS(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS);
#endif
#endif
		/*
		BOOL WINAPI SetDialogControlDpiChangeBehavior(
			_In_ HWND hWnd,
			_In_ DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS mask,
			_In_ DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS values);
		*/

		// DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS WINAPI GetDialogControlDpiChangeBehavior(_In_ HWND hWnd);

		public enum DIALOG_DPI_CHANGE_BEHAVIORS
		{
			DDC_DEFAULT = 0x0000,
			DDC_DISABLE_ALL = 0x0001,
			DDC_DISABLE_RESIZE = 0x0002,
			DDC_DISABLE_CONTROL_RELAYOUT = 0x0004,
		}
        //DIALOG_DPI_CHANGE_BEHAVIORS;

#if CSPORTING
#if !MIDL_PASS
        DEFINE_ENUM_FLAG_OPERATORS(DIALOG_DPI_CHANGE_BEHAVIORS);
#endif
#endif
		/*
		BOOL WINAPI SetDialogDpiChangeBehavior(
			_In_ HWND hDlg,
			_In_ DIALOG_DPI_CHANGE_BEHAVIORS mask,
			_In_ DIALOG_DPI_CHANGE_BEHAVIORS values);
		*/
		// DIALOG_DPI_CHANGE_BEHAVIORS WINAPI GetDialogDpiChangeBehavior(_In_ HWND hDlg);


		public const Int32 DLGWINDOWEXTRA = 30;

        #endregion CTLMGR


        #region MSG

        // WINUSERAPI BOOL WINAPI CallMsgFilterA(_In_ LPMSG lpMsg, _In_ int nCode);
        // WINUSERAPI BOOL WINAPI CallMsgFilterW(_In_ LPMSG lpMsg, _In_ int nCode);

        #endregion MSG


        #region CLIPBOARD

        // WINUSERAPI BOOL WINAPI OpenClipboard(_In_opt_ HWND hWndNewOwner);

        // WINUSERAPI BOOL WINAPI CloseClipboard(VOID);


#if WIN32_WINNT_WIN2K_LATER

        // WINUSERAPI DWORD WINAPI GetClipboardSequenceNumber(VOID);

#endif // WIN32_WINNT_WIN2K_LATER

        // WINUSERAPI HWND WINAPI GetClipboardOwner(VOID);

        // WINUSERAPI HWND WINAPI SetClipboardViewer(_In_ HWND hWndNewViewer);

        // WINUSERAPI HWND WINAPI GetClipboardViewer(VOID);

        // WINUSERAPI BOOL WINAPI ChangeClipboardChain(_In_ HWND hWndRemove, _In_ HWND hWndNewNext);

        // WINUSERAPI HANDLE WINAPI SetClipboardData(_In_ UINT uFormat, _In_opt_ HANDLE hMem);

        // WINUSERAPI HANDLE WINAPI GetClipboardData(_In_ UINT uFormat);

        // WINUSERAPI UINT WINAPI RegisterClipboardFormatA(_In_ LPCSTR lpszFormat);

        // WINUSERAPI UINT WINAPI RegisterClipboardFormatW(_In_ LPCWSTR lpszFormat);

        // WINUSERAPI int WINAPI CountClipboardFormats(VOID);

        // WINUSERAPI UINT WINAPI EnumClipboardFormats(_In_ UINT format);
        /*
		WINUSERAPI int WINAPI GetClipboardFormatNameA(
			_In_ UINT format,
			_Out_writes_(cchMaxCount) LPSTR lpszFormatName,
			_In_ int cchMaxCount);

		WINUSERAPI int WINAPI GetClipboardFormatNameW(
			_In_ UINT format,
			_Out_writes_(cchMaxCount) LPWSTR lpszFormatName,
			_In_ int cchMaxCount);
		*/

        // WINUSERAPI BOOL WINAPI EmptyClipboard(VOID);

        // WINUSERAPI BOOL WINAPI IsClipboardFormatAvailable(_In_ UINT format);

        // WINUSERAPI int WINAPI GetPriorityClipboardFormat(_In_reads_(cFormats) UINT *paFormatPriorityList, _In_ int cFormats);

        // WINUSERAPI HWND WINAPI GetOpenClipboardWindow(VOID);

#if WIN32_WINNT_VISTA_LATER
        // WINUSERAPI BOOL WINAPI AddClipboardFormatListener(_In_ HWND hwnd);

        // WINUSERAPI BOOL WINAPI RemoveClipboardFormatListener(_In_ HWND hwnd);
        /*
		WINUSERAPI BOOL WINAPI GetUpdatedClipboardFormats(
			_Out_writes_(cFormats) PUINT lpuiFormats,
			_In_ UINT cFormats,
			_Out_ PUINT pcFormatsOut);
		*/
#endif // WIN32_WINNT_VISTA_LATER

        #endregion CLIPBOARD

        /*
		WINUSERAPI BOOL WINAPI CharToOemA(_In_ LPCSTR pSrc, _Out_writes_(_Inexpressible_(strlen(pSrc) + 1)) LPSTR pDst);		
		WINUSERAPI BOOL WINAPI CharToOemW(_In_ LPCWSTR pSrc, _Out_writes_(_Inexpressible_(strlen(pSrc) + 1)) LPSTR pDst);
        */
        /*
		__drv_preferredFunction("OemToCharBuff", "Does not validate buffer size")
		WINUSERAPI BOOL WINAPI OemToCharA(
			_In_ LPCSTR pSrc,
			_Out_writes_(_Inexpressible_(strlen(pSrc) + 1)) LPSTR pDst);
			
		__drv_preferredFunction("OemToCharBuff", "Does not validate buffer size")
		WINUSERAPI BOOL WINAPI OemToCharW(
			_In_ LPCSTR pSrc,
			_Out_writes_(_Inexpressible_(strlen(pSrc) + 1)) LPWSTR pDst);
		*/
        /*
		WINUSERAPI BOOL WINAPI CharToOemBuffA(
			_In_ LPCSTR lpszSrc,
			_Out_writes_(cchDstLength) LPSTR lpszDst,
			_In_ DWORD cchDstLength);
			
		WINUSERAPI BOOL WINAPI CharToOemBuffW(
			_In_ LPCWSTR lpszSrc,
			_Out_writes_(cchDstLength) LPSTR lpszDst,
			_In_ DWORD cchDstLength);
		*/
        /*
		WINUSERAPI BOOL WINAPI OemToCharBuffA(
			_In_ LPCSTR lpszSrc,
			_Out_writes_(cchDstLength) LPSTR lpszDst,
			_In_ DWORD cchDstLength);
			
		WINUSERAPI BOOL WINAPI OemToCharBuffW(
			_In_ LPCSTR lpszSrc,
			_Out_writes_(cchDstLength) LPWSTR lpszDst,
			_In_ DWORD cchDstLength);
		*/


        /*
		WINUSERAPI LPSTR WINAPI CharUpperA(_Inout_ LPSTR lpsz);
		
		WINUSERAPI LPWSTR WINAPI CharUpperW(_Inout_ LPWSTR lpsz);
		*/
        /*
		WINUSERAPI DWORD WINAPI CharUpperBuffA(
			_Inout_updates_(cchLength) LPSTR lpsz,
			_In_ DWORD cchLength);
			
		WINUSERAPI DWORD WINAPI CharUpperBuffW(
			_Inout_updates_(cchLength) LPWSTR lpsz,
			_In_ DWORD cchLength);
		*/
        /*
		WINUSERAPI LPSTR WINAPI CharLowerA(_Inout_ LPSTR lpsz);		
		WINUSERAPI LPWSTR WINAPI CharLowerW(_Inout_ LPWSTR lpsz);
		*/
        /*
		WINUSERAPI DWORD WINAPI CharLowerBuffA(
			_Inout_updates_(cchLength) LPSTR lpsz,
			_In_ DWORD cchLength);
			
		WINUSERAPI DWORD WINAPI CharLowerBuffW(
			_Inout_updates_(cchLength) LPWSTR lpsz,
			_In_ DWORD cchLength);
		*/
        /*
		WINUSERAPI LPSTR WINAPI CharNextA(_In_ LPCSTR lpsz);		
		WINUSERAPI LPWSTR WINAPI CharNextW(_In_ LPCWSTR lpsz);
		*/
        /*
		WINUSERAPI LPSTR WINAPI CharPrevA(_In_ LPCSTR lpszStart, _In_ LPCSTR lpszCurrent);		
		WINUSERAPI LPWSTR WINAPI CharPrevW(_In_ LPCWSTR lpszStart, _In_ LPCWSTR lpszCurrent);
		*/

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI LPSTR WINAPI CharNextExA(
			_In_ WORD CodePage,
			_In_ LPCSTR lpCurrentChar,
			_In_ DWORD dwFlags);

		WINUSERAPI LPSTR WINAPI CharPrevExA(
			_In_ WORD CodePage,
			_In_ LPCSTR lpStart,
			_In_ LPCSTR lpCurrentChar,
			_In_ DWORD dwFlags);
		*/
#endif // WIN32_WINNT_NT4_LATER


        //#define AnsiToOem CharToOemA
        //#define OemToAnsi OemToCharA
        //#define AnsiToOemBuff CharToOemBuffA
        //#define OemToAnsiBuff OemToCharBuffA
        //#define AnsiUpper CharUpperA
        //#define AnsiUpperBuff CharUpperBuffA
        //#define AnsiLower CharLowerA
        //#define AnsiLowerBuff CharLowerBuffA
        //#define AnsiNext CharNextA
        //#define AnsiPrev CharPrevA


        #region LANGUAGE

        // WINUSERAPI BOOL WINAPI IsCharAlphaA(_In_ CHAR ch);
        // WINUSERAPI BOOL WINAPI IsCharAlphaW(_In_ WCHAR ch);

        // WINUSERAPI BOOL WINAPI IsCharAlphaNumericA(_In_ CHAR ch);
        // WINUSERAPI BOOL WINAPI IsCharAlphaNumericW(_In_ WCHAR ch);

        // WINUSERAPI BOOL WINAPI IsCharUpperA(_In_ CHAR ch);
        // WINUSERAPI BOOL WINAPI IsCharUpperW(_In_ WCHAR ch);

        // WINUSERAPI BOOL WINAPI IsCharLowerA(_In_ CHAR ch);
        // WINUSERAPI BOOL WINAPI IsCharLowerW(_In_ WCHAR ch);

        #endregion LANGUAGE



        // WINUSERAPI HWND WINAPI SetFocus(_In_opt_ HWND hWnd);

        // WINUSERAPI HWND WINAPI GetActiveWindow(VOID);

        // WINUSERAPI HWND WINAPI GetFocus(VOID);

        // WINUSERAPI UINT WINAPI GetKBCodePage(VOID);

        // WINUSERAPI SHORT WINAPI GetKeyState(_In_ int nVirtKey);

        // WINUSERAPI SHORT WINAPI GetAsyncKeyState(_In_ int vKey);

        // WINUSERAPI _Check_return_ BOOL WINAPI GetKeyboardState(_Out_writes_(256) PBYTE lpKeyState);

        // WINUSERAPI BOOL WINAPI SetKeyboardState(_In_reads_(256) LPBYTE lpKeyState);


        /*
		WINUSERAPI int WINAPI GetKeyNameTextA(
			_In_ LONG lParam,
			_Out_writes_(cchSize) LPSTR lpString,
			_In_ int cchSize);
			
		WINUSERAPI int WINAPI GetKeyNameTextW(
			_In_ LONG lParam,
			_Out_writes_(cchSize) LPWSTR lpString,
			_In_ int cchSize);
		*/



        // WINUSERAPI int WINAPI GetKeyboardType(_In_ int nTypeFlag);

        /*
		WINUSERAPI int WINAPI ToAscii(
			_In_ UINT uVirtKey,
			_In_ UINT uScanCode,
			_In_reads_opt_(256) CONST BYTE * lpKeyState,
			_Out_ LPWORD lpChar,
			_In_ UINT uFlags);
		*/

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI int WINAPI ToAsciiEx(
			_In_ UINT uVirtKey,
			_In_ UINT uScanCode,
			_In_reads_opt_(256) CONST BYTE * lpKeyState,
			_Out_ LPWORD lpChar,
			_In_ UINT uFlags,
			_In_opt_ HKL dwhkl);
		*/
#endif // WIN32_WINNT_NT4_LATER

        /*
		WINUSERAPI int WINAPI ToUnicode(
			_In_ UINT wVirtKey,
			_In_ UINT wScanCode,
			_In_reads_bytes_opt_(256) CONST BYTE * lpKeyState,
			_Out_writes_(cchBuff) LPWSTR pwszBuff,
			_In_ int cchBuff,
			_In_ UINT wFlags);
		*/

        // WINUSERAPI DWORD WINAPI OemKeyScan(_In_ WORD wOemChar);
        /*
		WINUSERAPI SHORT WINAPI VkKeyScanA(_In_ CHAR ch);		
		WINUSERAPI SHORT WINAPI VkKeyScanW(_In_ WCHAR ch);
		*/

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI SHORT WINAPI VkKeyScanExA(
			_In_ CHAR ch,
			_In_ HKL dwhkl);
			
		WINUSERAPI SHORT WINAPI VkKeyScanExW(
			_In_ WCHAR ch,
			_In_ HKL dwhkl);
		*/
#endif // WIN32_WINNT_NT4_LATER


        public const UInt32 KEYEVENTF_EXTENDEDKEY   = 0x0001;
        public const UInt32 KEYEVENTF_KEYUP         = 0x0002;
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 KEYEVENTF_UNICODE       = 0x0004;
		public const UInt32 KEYEVENTF_SCANCODE      = 0x0008;
#endif // WIN32_WINNT_WIN2K_LATER

		/*
		WINUSERAPI VOID WINAPI keybd_event(
			_In_ BYTE bVk,
			_In_ BYTE bScan,
			_In_ DWORD dwFlags,
			_In_ ULONG_PTR dwExtraInfo);
		*/


		public const UInt32 MOUSEEVENTF_MOVE        = 0x0001; /* mouse move */
		public const UInt32 MOUSEEVENTF_LEFTDOWN    = 0x0002; /* left button down */
		public const UInt32 MOUSEEVENTF_LEFTUP      = 0x0004; /* left button up */
		public const UInt32 MOUSEEVENTF_RIGHTDOWN   = 0x0008; /* right button down */
		public const UInt32 MOUSEEVENTF_RIGHTUP     = 0x0010; /* right button up */
		public const UInt32 MOUSEEVENTF_MIDDLEDOWN  = 0x0020; /* middle button down */
		public const UInt32 MOUSEEVENTF_MIDDLEUP    = 0x0040; /* middle button up */
		public const UInt32 MOUSEEVENTF_XDOWN       = 0x0080; /* x button down */
		public const UInt32 MOUSEEVENTF_XUP         = 0x0100; /* x button down */
		public const UInt32 MOUSEEVENTF_WHEEL                = 0x0800; /* wheel button rolled */
#if WIN32_WINNT_VISTA_LATER
		public const UInt32 MOUSEEVENTF_HWHEEL              = 0x01000; /* hwheel button rolled */
#endif
#if WIN32_WINNT_VISTA_LATER
		public const UInt32 MOUSEEVENTF_MOVE_NOCOALESCE      = 0x2000; /* do not coalesce mouse moves */
#endif // WIN32_WINNT_VISTA_LATER
		public const UInt32 MOUSEEVENTF_VIRTUALDESK          = 0x4000; /* map to entire virtual desktop */
		public const UInt32 MOUSEEVENTF_ABSOLUTE             = 0x8000; /* absolute move */


        /*
		WINUSERAPI VOID WINAPI mouse_event( // no LastError
			_In_ DWORD dwFlags,
			_In_ DWORD dx,
			_In_ DWORD dy,
			_In_ DWORD dwData,
			_In_ ULONG_PTR dwExtraInfo);
		*/
        [DllImport("user32.dll")]
        public static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, UIntPtr dwExtraInfo);


#if WIN32_WINNT_WIN2K_LATER

        public struct MOUSEINPUT
		{
			public Int32 dx;
			public Int32 dy;
			public UInt32 mouseData;
			public UInt32 dwFlags;
			public UInt32 time;
			public UIntPtr dwExtraInfo;
		}

		public struct KEYBDINPUT
		{
			public UInt16 wVk;
			public UInt16 wScan;
			public UInt32 dwFlags;
			public UInt32 time;
			public UIntPtr dwExtraInfo;
		}


		public struct HARDWAREINPUT
		{
			public UInt32 uMsg;
			public UInt16 wParamL;
			public UInt16 wParamH;
		}

		public const UInt32 INPUT_MOUSE     = 0;
		public const UInt32 INPUT_KEYBOARD  = 1;
		public const UInt32 INPUT_HARDWARE  = 2;

#if CSPORTING
        public struct INPUT
		{
			public UInt32 type;
			union DUMMYUNIONNAME
			{
				MOUSEINPUT mi;
				KEYBDINPUT ki;
				HARDWAREINPUT hi;
			}
		} 
#endif //CSPORTING

		/*
		WINUSERAPI UINT WINAPI SendInput(
			_In_ UINT cInputs,                     // number of input in the array
			_In_reads_(cInputs) LPINPUT pInputs,  // array of inputs
			_In_ int cbSize);                      // sizeof(INPUT)
		*/

#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN7_LATER


        //DECLARE_HANDLE(HTOUCHINPUT);

        public struct TOUCHINPUT
		{
			public Int32 x;
			public Int32 y;
			public IntPtr hSource;
			public UInt32 dwID;
			public UInt32 dwFlags;
			public UInt32 dwMask;
			public UInt32 dwTime;
			public UIntPtr dwExtraInfo;
			public UInt32 cxContact;
			public UInt32 cyContact;
		}


		//#define TOUCH_COORD_TO_PIXEL(l)         ((l) / 100)

		public const UInt32 TOUCHEVENTF_MOVE            = 0x0001;
		public const UInt32 TOUCHEVENTF_DOWN            = 0x0002;
		public const UInt32 TOUCHEVENTF_UP              = 0x0004;
		public const UInt32 TOUCHEVENTF_INRANGE         = 0x0008;
		public const UInt32 TOUCHEVENTF_PRIMARY         = 0x0010;
		public const UInt32 TOUCHEVENTF_NOCOALESCE      = 0x0020;
		public const UInt32 TOUCHEVENTF_PEN             = 0x0040;
		public const UInt32 TOUCHEVENTF_PALM            = 0x0080;

		public const UInt32 TOUCHINPUTMASKF_TIMEFROMSYSTEM  = 0x0001;  // the dwTime field contains a system generated value
		public const UInt32 TOUCHINPUTMASKF_EXTRAINFO       = 0x0002;  // the dwExtraInfo field is valid
		public const UInt32 TOUCHINPUTMASKF_CONTACTAREA     = 0x0004;  // the cxContact and cyContact fields are valid


		/*
		WINUSERAPI BOOL WINAPI GetTouchInputInfo(
			_In_ HTOUCHINPUT hTouchInput,               // input event handle; from touch message lParam
			_In_ UINT cInputs,                          // number of elements in the array
			_Out_writes_(cInputs) PTOUCHINPUT pInputs,  // array of touch inputs
			_In_ int cbSize);                           // sizeof(TOUCHINPUT)
		*/
		// WINUSERAPI BOOL WINAPI CloseTouchInputHandle(_In_ HTOUCHINPUT hTouchInput);                   // input event handle; from touch message lParam


		public const UInt32 TWF_FINETOUCH       = (0x00000001);
		public const UInt32 TWF_WANTPALM        = (0x00000002);


        // WINUSERAPI BOOL WINAPI RegisterTouchWindow(_In_ HWND hwnd, _In_ ULONG ulFlags);

        // WINUSERAPI BOOL WINAPI UnregisterTouchWindow(_In_ HWND hwnd);

        // WINUSERAPI BOOL WINAPI IsTouchWindow(_In_ HWND hwnd, _Out_opt_ PULONG pulFlags);


#endif // WIN32_WINNT_WIN7_LATER


#if WIN32_WINNT_WIN8_LATER


#if CSPORTING
        //#define POINTER_STRUCTURES
#endif
        public enum POINTER_INPUT_TYPE
		{
			PT_POINTER = 1,   // Generic pointer
			PT_TOUCH = 2,   // Touch
			PT_PEN = 3,   // Pen
			PT_MOUSE = 4,   // Mouse
#if WIN32_WINNT_WINBLUE_LATER
			PT_TOUCHPAD = 5,   // Touchpad
#endif // WIN32_WINNT_WINBLUE_LATER
		};

		//typedef DWORD POINTER_INPUT_TYPE;

		//typedef UINT32 POINTER_FLAGS;

		public const UInt32 POINTER_FLAG_NONE               = 0x00000000; // Default
		public const UInt32 POINTER_FLAG_NEW                = 0x00000001; // New pointer
		public const UInt32 POINTER_FLAG_INRANGE            = 0x00000002; // Pointer has not departed
		public const UInt32 POINTER_FLAG_INCONTACT          = 0x00000004; // Pointer is in contact
		public const UInt32 POINTER_FLAG_FIRSTBUTTON        = 0x00000010; // Primary action
		public const UInt32 POINTER_FLAG_SECONDBUTTON       = 0x00000020; // Secondary action
		public const UInt32 POINTER_FLAG_THIRDBUTTON        = 0x00000040; // Third button
		public const UInt32 POINTER_FLAG_FOURTHBUTTON       = 0x00000080; // Fourth button
		public const UInt32 POINTER_FLAG_FIFTHBUTTON        = 0x00000100; // Fifth button
		public const UInt32 POINTER_FLAG_PRIMARY            = 0x00002000; // Pointer is primary
		public const UInt32 POINTER_FLAG_CONFIDENCE         = 0x00004000; // Pointer is considered unlikely to be accidental
		public const UInt32 POINTER_FLAG_CANCELED           = 0x00008000; // Pointer is departing in an abnormal manner
		public const UInt32 POINTER_FLAG_DOWN               = 0x00010000; // Pointer transitioned to down state (made contact)
		public const UInt32 POINTER_FLAG_UPDATE             = 0x00020000; // Pointer update
		public const UInt32 POINTER_FLAG_UP                 = 0x00040000; // Pointer transitioned from down state (broke contact)
		public const UInt32 POINTER_FLAG_WHEEL              = 0x00080000; // Vertical wheel
		public const UInt32 POINTER_FLAG_HWHEEL             = 0x00100000; // Horizontal wheel
		public const UInt32 POINTER_FLAG_CAPTURECHANGED     = 0x00200000; // Lost capture
		public const UInt32 POINTER_FLAG_HASTRANSFORM       = 0x00400000; // Input has a transform associated with it

		public const UInt32 POINTER_MOD_SHIFT   = (0x0004);    // Shift key is held down.
		public const UInt32 POINTER_MOD_CTRL    = (0x0008);    // Ctrl key is held down.


		public enum POINTER_BUTTON_CHANGE_TYPE
		{
			POINTER_CHANGE_NONE,
			POINTER_CHANGE_FIRSTBUTTON_DOWN,
			POINTER_CHANGE_FIRSTBUTTON_UP,
			POINTER_CHANGE_SECONDBUTTON_DOWN,
			POINTER_CHANGE_SECONDBUTTON_UP,
		    POINTER_CHANGE_THIRDBUTTON_DOWN,
			POINTER_CHANGE_THIRDBUTTON_UP,
			POINTER_CHANGE_FOURTHBUTTON_DOWN,
			POINTER_CHANGE_FOURTHBUTTON_UP,
			POINTER_CHANGE_FIFTHBUTTON_DOWN,
			POINTER_CHANGE_FIFTHBUTTON_UP,
		}

		public struct POINTER_INFO
		{
			public UInt32 pointerType;
			public UInt32 pointerId;
			public UInt32 frameId;
			public UInt32 pointerFlags;
			public IntPtr sourceDevice;
			public IntPtr hwndTarget;
			public WinDef.POINT ptPixelLocation;
			public WinDef.POINT ptHimetricLocation;
			public WinDef.POINT ptPixelLocationRaw;
			public WinDef.POINT ptHimetricLocationRaw;
			public UInt32 dwTime;
			public UInt32 historyCount;
			public Int32 InputData;
			public UInt32 dwKeyStates;
			public UInt64 PerformanceCount;
			public POINTER_BUTTON_CHANGE_TYPE ButtonChangeType;
		}


		//typedef UINT32 TOUCH_FLAGS;
		public const UInt32 TOUCH_FLAG_NONE                 = 0x00000000; // Default

		//typedef UINT32 TOUCH_MASK;
		public const UInt32 TOUCH_MASK_NONE                 = 0x00000000; // Default - none of the optional fields are valid
		public const UInt32 TOUCH_MASK_CONTACTAREA          = 0x00000001; // The rcContact field is valid
		public const UInt32 TOUCH_MASK_ORIENTATION          = 0x00000002; // The orientation field is valid
		public const UInt32 TOUCH_MASK_PRESSURE             = 0x00000004; // The pressure field is valid

		public struct POINTER_TOUCH_INFO
		{
			public POINTER_INFO pointerInfo;
			public UInt32 touchFlags;
			public UInt32 touchMask;
			public WinDef.RECT rcContact;
			public WinDef.RECT rcContactRaw;
			public UInt32 orientation;
			public UInt32 pressure;
		}

		//typedef UINT32 PEN_FLAGS;
		public const UInt32 PEN_FLAG_NONE                   = 0x00000000; // Default
		public const UInt32 PEN_FLAG_BARREL                 = 0x00000001; // The barrel button is pressed
		public const UInt32 PEN_FLAG_INVERTED               = 0x00000002; // The pen is inverted
		public const UInt32 PEN_FLAG_ERASER                 = 0x00000004; // The eraser button is pressed

		//typedef UINT32 PEN_MASK;
		public const UInt32 PEN_MASK_NONE                   = 0x00000000; // Default - none of the optional fields are valid
		public const UInt32 PEN_MASK_PRESSURE               = 0x00000001; // The pressure field is valid
		public const UInt32 PEN_MASK_ROTATION               = 0x00000002; // The rotation field is valid
		public const UInt32 PEN_MASK_TILT_X                 = 0x00000004; // The tiltX field is valid
		public const UInt32 PEN_MASK_TILT_Y                 = 0x00000008; // The tiltY field is valid

		public struct POINTER_PEN_INFO
		{
			public POINTER_INFO pointerInfo;
			public UInt32 penFlags;
			public UInt32 penMask;
			public UInt32 pressure;
			public UInt32 rotation;
			public Int32 tiltX;
			public Int32 tiltY;
		}


		public const UInt32 POINTER_MESSAGE_FLAG_NEW                = 0x00000001; // New pointer
		public const UInt32 POINTER_MESSAGE_FLAG_INRANGE            = 0x00000002; // Pointer has not departed
		public const UInt32 POINTER_MESSAGE_FLAG_INCONTACT          = 0x00000004; // Pointer is in contact
		public const UInt32 POINTER_MESSAGE_FLAG_FIRSTBUTTON        = 0x00000010; // Primary action
		public const UInt32 POINTER_MESSAGE_FLAG_SECONDBUTTON       = 0x00000020; // Secondary action
		public const UInt32 POINTER_MESSAGE_FLAG_THIRDBUTTON        = 0x00000040; // Third button
		public const UInt32 POINTER_MESSAGE_FLAG_FOURTHBUTTON       = 0x00000080; // Fourth button
		public const UInt32 POINTER_MESSAGE_FLAG_FIFTHBUTTON        = 0x00000100; // Fifth button
		public const UInt32 POINTER_MESSAGE_FLAG_PRIMARY            = 0x00002000; // Pointer is primary
		public const UInt32 POINTER_MESSAGE_FLAG_CONFIDENCE         = 0x00004000; // Pointer is considered unlikely to be accidental
		public const UInt32 POINTER_MESSAGE_FLAG_CANCELED           = 0x00008000; // Pointer is departing in an abnormal manner

        /*
        //#define GET_POINTERID_WPARAM(wParam)                (LOWORD(wParam))
        //#define IS_POINTER_FLAG_SET_WPARAM(wParam, flag)    (((DWORD)HIWORD(wParam) & (flag)) == (flag))
        //#define IS_POINTER_NEW_WPARAM(wParam)               IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_NEW)
        //#define IS_POINTER_INRANGE_WPARAM(wParam)           IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_INRANGE)
        //#define IS_POINTER_INCONTACT_WPARAM(wParam)         IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_INCONTACT)
        //#define IS_POINTER_FIRSTBUTTON_WPARAM(wParam)       IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_FIRSTBUTTON)
        //#define IS_POINTER_SECONDBUTTON_WPARAM(wParam)      IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_SECONDBUTTON)
        //#define IS_POINTER_THIRDBUTTON_WPARAM(wParam)       IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_THIRDBUTTON)
        //#define IS_POINTER_FOURTHBUTTON_WPARAM(wParam)      IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_FOURTHBUTTON)
        //#define IS_POINTER_FIFTHBUTTON_WPARAM(wParam)       IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_FIFTHBUTTON)
        //#define IS_POINTER_PRIMARY_WPARAM(wParam)           IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_PRIMARY)
        //#define HAS_POINTER_CONFIDENCE_WPARAM(wParam)       IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_CONFIDENCE)
        //#define IS_POINTER_CANCELED_WPARAM(wParam)          IS_POINTER_FLAG_SET_WPARAM(wParam, POINTER_MESSAGE_FLAG_CANCELED)
		*/

        public const Int32 PA_ACTIVATE                     = MA_ACTIVATE;
		public const Int32 PA_NOACTIVATE                   = MA_NOACTIVATE;

		public const UInt32 MAX_TOUCH_COUNT = 256;

		public const UInt32 TOUCH_FEEDBACK_DEFAULT = 0x1;
		public const UInt32 TOUCH_FEEDBACK_INDIRECT = 0x2;
		public const UInt32 TOUCH_FEEDBACK_NONE = 0x3;


		// WINUSERAPI BOOL WINAPI InitializeTouchInjection(_In_ UINT32 maxCount, _In_ DWORD dwMode);

		// WINUSERAPI BOOL WINAPI InjectTouchInput(_In_ UINT32 count, _In_reads_(count) CONST POINTER_TOUCH_INFO* contacts);

		public  struct USAGE_PROPERTIES
		{
			public UInt16 level;
			public UInt16 page;
			public UInt16 usage;
			public Int32 logicalMinimum;
			public Int32 logicalMaximum;
			public UInt16 unit;
			public UInt16 exponent;
			public Byte count;
			public Int32 physicalMinimum;
			public Int32 physicalMaximum;
		}


#if CSPORTING
        public struct POINTER_TYPE_INFO
		{
			public POINTER_INPUT_TYPE type;
			
			union DUMMYUNIONNAME
			{
				POINTER_TOUCH_INFO touchInfo;
				POINTER_PEN_INFO penInfo;
			}
			
		}
#endif

		public struct INPUT_INJECTION_VALUE
		{
			public UInt16 page;
			public UInt16 usage;
			public Int32 value;
			public UInt16 index;
		}

		// WINUSERAPI BOOL WINAPI GetPointerType(_In_ UINT32 pointerId, _Out_ POINTER_INPUT_TYPE *pointerType);

		// WINUSERAPI BOOL WINAPI GetPointerCursorId(_In_ UINT32 pointerId, _Out_ UINT32 *cursorId);

		// WINUSERAPI BOOL WINAPI GetPointerInfo(_In_ UINT32 pointerId, _Out_writes_(1) POINTER_INFO* pointerInfo);
		/*
		WINUSERAPI BOOL WINAPI GetPointerInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Out_writes_opt_(* entriesCount) POINTER_INFO *pointerInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFrameInfo(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* pointerCount) POINTER_INFO *pointerInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFrameInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* entriesCount ** pointerCount) POINTER_INFO* pointerInfo);
		*/
		// WINUSERAPI BOOL WINAPI GetPointerTouchInfo(_In_ UINT32 pointerId, _Out_writes_(1) POINTER_TOUCH_INFO* touchInfo);
		/*
		WINUSERAPI BOOL WINAPI GetPointerTouchInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Out_writes_opt_(* entriesCount) POINTER_TOUCH_INFO *touchInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFrameTouchInfo(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* pointerCount) POINTER_TOUCH_INFO *touchInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFrameTouchInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* entriesCount ** pointerCount) POINTER_TOUCH_INFO* touchInfo);
		*/
		
		// WINUSERAPI BOOL WINAPI GetPointerPenInfo(_In_ UINT32 pointerId, _Out_writes_(1) POINTER_PEN_INFO* penInfo);
		/*
		WINUSERAPI BOOL WINAPI GetPointerPenInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Out_writes_opt_(* entriesCount) POINTER_PEN_INFO *penInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFramePenInfo(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* pointerCount) POINTER_PEN_INFO *penInfo);
		*/
		/*
		WINUSERAPI BOOL WINAPI GetPointerFramePenInfoHistory(
			_In_ UINT32 pointerId,
			_Inout_ UINT32 *entriesCount,
			_Inout_ UINT32 *pointerCount,
			_Out_writes_opt_(* entriesCount ** pointerCount) POINTER_PEN_INFO* penInfo);
		*/
		// WINUSERAPI BOOL WINAPI SkipPointerFrameMessages(_In_ UINT32 pointerId);

		// WINUSERAPI BOOL WINAPI RegisterPointerInputTarget(_In_ HWND hwnd, _In_ POINTER_INPUT_TYPE pointerType);

		// WINUSERAPI BOOL WINAPI UnregisterPointerInputTarget(_In_ HWND hwnd, _In_ POINTER_INPUT_TYPE pointerType);
		/*
		WINUSERAPI BOOL WINAPI RegisterPointerInputTargetEx(
			_In_ HWND hwnd,
			_In_ POINTER_INPUT_TYPE pointerType,
			_In_ BOOL fObserve);
		*/
		
		// WINUSERAPI BOOL WINAPI UnregisterPointerInputTargetEx(_In_ HWND hwnd, _In_ POINTER_INPUT_TYPE pointerType);

		// WINUSERAPI BOOL WINAPI EnableMouseInPointer(_In_ BOOL fEnable);

		// WINUSERAPI BOOL WINAPI IsMouseInPointerEnabled(VOID);


		public const UInt32 TOUCH_HIT_TESTING_DEFAULT = 0x0;
		public const UInt32 TOUCH_HIT_TESTING_CLIENT  = 0x1;
		public const UInt32 TOUCH_HIT_TESTING_NONE    = 0x2;

		// WINUSERAPI BOOL WINAPI RegisterTouchHitTestingWindow(_In_ HWND hwnd, _In_ ULONG value);

		public struct TOUCH_HIT_TESTING_PROXIMITY_EVALUATION
		{
			public UInt16 score;
			public WinDef.POINT adjustedPoint;
		}

		
		public struct TOUCH_HIT_TESTING_INPUT
		{
			public UInt32 pointerId;
			public WinDef.POINT point;
			public WinDef.RECT boundingBox;
			public WinDef.RECT nonOccludedBoundingBox;
		    public UInt32 orientation;
		}


		public const UInt16 TOUCH_HIT_TESTING_PROXIMITY_CLOSEST  = 0x0;
		public const UInt16 TOUCH_HIT_TESTING_PROXIMITY_FARTHEST = 0xFFF;
		/*
		WINUSERAPI BOOL WINAPI EvaluateProximityToRect(
			_In_ const RECT* controlBoundingBox,
			_In_ const TOUCH_HIT_TESTING_INPUT* pHitTestingInput,
			_Out_ TOUCH_HIT_TESTING_PROXIMITY_EVALUATION *pProximityEval);

		WINUSERAPI BOOL WINAPI EvaluateProximityToPolygon(
			UINT32 numVertices,
			_In_reads_(numVertices) const POINT* controlPolygon,
			_In_ const TOUCH_HIT_TESTING_INPUT* pHitTestingInput,
			_Out_ TOUCH_HIT_TESTING_PROXIMITY_EVALUATION *pProximityEval);

		WINUSERAPI LRESULT WINAPI PackTouchHitTestingProximityEvaluation(
			_In_ const TOUCH_HIT_TESTING_INPUT* pHitTestingInput,
			_In_ const TOUCH_HIT_TESTING_PROXIMITY_EVALUATION* pProximityEval);
		*/

		public enum FEEDBACK_TYPE
		{
			FEEDBACK_TOUCH_CONTACTVISUALIZATION = 1,
			FEEDBACK_PEN_BARRELVISUALIZATION = 2,
			FEEDBACK_PEN_TAP = 3,
			FEEDBACK_PEN_DOUBLETAP = 4,
			FEEDBACK_PEN_PRESSANDHOLD = 5,
			FEEDBACK_PEN_RIGHTTAP = 6,
			FEEDBACK_TOUCH_TAP = 7,
			FEEDBACK_TOUCH_DOUBLETAP = 8,
			FEEDBACK_TOUCH_PRESSANDHOLD = 9,
			FEEDBACK_TOUCH_RIGHTTAP = 10,
			FEEDBACK_GESTURE_PRESSANDTAP = 11,
			FEEDBACK_MAX = unchecked((Int32)0xFFFFFFFF),
		}


		public const UInt32 GWFS_INCLUDE_ANCESTORS           = 0x00000001;

        /*
		WINUSERAPI BOOL WINAPI GetWindowFeedbackSetting(
			_In_ HWND hwnd,
			_In_ FEEDBACK_TYPE feedback,
			_In_ DWORD dwFlags,
			_Inout_ UINT32* pSize,
			_Out_writes_bytes_opt_(* pSize) VOID* config);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetWindowFeedbackSetting(
			_In_ HWND hwnd,
			_In_ FEEDBACK_TYPE feedback,
			_In_ DWORD dwFlags,
			_In_ UINT32 size,
			_In_reads_bytes_opt_(size) CONST VOID* configuration);
		*/


#endif // WIN32_WINNT_WIN8_LATER


#if WIN32_WINNT_WINBLUE_LATER

        //Disable warning C4201:nameless struct/union
        //#if _MSC_VER >= 1200
        //#pragma warning(push)
        //#endif
        //#pragma warning(disable : 4201)

#if CSPORTING
        public struct INPUT_TRANSFORM
		{
			union DUMMYUNIONNAME
			{
				public struct DUMMYSTRUCTNAME
				{
		            float _11, _12, _13, _14;
					float _21, _22, _23, _24;
					float _31, _32, _33, _34;
					float _41, _42, _43, _44;
				}				
				public float m[4][4];
			}
		}
#endif       

        //#if _MSC_VER >= 1200
        //#pragma warning(pop)
        //#endif

		/*
		WINUSERAPI BOOL WINAPI GetPointerInputTransform(
			_In_ UINT32 pointerId,
			_In_ UINT32 historyCount,
			_Out_writes_(historyCount) INPUT_TRANSFORM *inputTransform);
		*/


#endif // WIN32_WINNT_WINBLUE_LATER


#if WIN32_WINNT_WIN2K_LATER

        public struct LASTINPUTINFO
		{
			public UInt32 cbSize;
			public UInt32 dwTime;
		}

		// WINUSERAPI BOOL WINAPI GetLastInputInfo(_Out_ PLASTINPUTINFO plii);
		
#endif // WIN32_WINNT_WIN2K_LATER

		/*
		WINUSERAPI UINT WINAPI MapVirtualKeyA(
			_In_ UINT uCode,
			_In_ UINT uMapType);

		WINUSERAPI UINT WINAPI MapVirtualKeyW(
			_In_ UINT uCode,
			_In_ UINT uMapType);
		*/
#if WIN32_WINNT_NT4_LATER
		/*
		WINUSERAPI UINT WINAPI MapVirtualKeyExA(
			_In_ UINT uCode,
			_In_ UINT uMapType,
			_In_opt_ HKL dwhkl);
			
		WINUSERAPI UINT WINAPI MapVirtualKeyExW(
			_In_ UINT uCode,
			_In_ UINT uMapType,
			_In_opt_ HKL dwhkl);
		*/
#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_NT4_LATER

		public const UInt32 MAPVK_VK_TO_VSC     = (0);
		public const UInt32 MAPVK_VSC_TO_VK     = (1);
		public const UInt32 MAPVK_VK_TO_CHAR    = (2);
		public const UInt32 MAPVK_VSC_TO_VK_EX  = (3);
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_VISTA_LATER
		public const UInt32 MAPVK_VK_TO_VSC_EX  = (4);
#endif // WIN32_WINNT_VISTA_LATER

		// WINUSERAPI BOOL WINAPI GetInputState(VOID);

		// WINUSERAPI DWORD WINAPI GetQueueStatus(_In_ UINT flags);

		// WINUSERAPI HWND WINAPI GetCapture(VOID);

		// WINUSERAPI HWND WINAPI SetCapture(_In_ HWND hWnd);

		// WINUSERAPI BOOL WINAPI ReleaseCapture(VOID);
		/*
		WINUSERAPI DWORD WINAPI MsgWaitForMultipleObjects(
			_In_ DWORD nCount,
			_In_reads_opt_(nCount) CONST HANDLE* pHandles,
			_In_ BOOL fWaitAll,
			_In_ DWORD dwMilliseconds,
			_In_ DWORD dwWakeMask);
		*/
		/*
		WINUSERAPI DWORD WINAPI MsgWaitForMultipleObjectsEx(
			_In_ DWORD nCount,
			_In_reads_opt_(nCount) CONST HANDLE* pHandles,
			_In_ DWORD dwMilliseconds,
			_In_ DWORD dwWakeMask,
			_In_ DWORD dwFlags);
		*/


		public const UInt32 MWMO_WAITALL        = 0x0001;
		public const UInt32 MWMO_ALERTABLE      = 0x0002;
		public const UInt32 MWMO_INPUTAVAILABLE = 0x0004;

		public const UInt32 QS_KEY              = 0x0001;
		public const UInt32 QS_MOUSEMOVE        = 0x0002;
		public const UInt32 QS_MOUSEBUTTON      = 0x0004;
		public const UInt32 QS_POSTMESSAGE      = 0x0008;
		public const UInt32 QS_TIMER            = 0x0010;
		public const UInt32 QS_PAINT            = 0x0020;
		public const UInt32 QS_SENDMESSAGE      = 0x0040;
		public const UInt32 QS_HOTKEY           = 0x0080;
		public const UInt32 QS_ALLPOSTMESSAGE   = 0x0100;
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 QS_RAWINPUT         = 0x0400;
#endif // WIN32_WINNT_WINXP_LATER
#if WIN32_WINNT_WIN8_LATER
		public const UInt32 QS_TOUCH            = 0x0800;
		public const UInt32 QS_POINTER          = 0x1000;
#endif // WIN32_WINNT_WIN8_LATER

		public const UInt32 QS_MOUSE            = (QS_MOUSEMOVE | QS_MOUSEBUTTON);

#if WIN32_WINNT_WIN8_LATER
		public const UInt32 QS_INPUT           = (QS_MOUSE | QS_KEY | QS_RAWINPUT | QS_TOUCH | QS_POINTER);
#else 
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 QS_INPUT           = (QS_MOUSE | QS_KEY | QS_RAWINPUT);
#else
		public const UInt32 QS_INPUT           = (QS_MOUSE | QS_KEY);
#endif // (WIN32_WINNT_WINXP_LATER)
#endif

		public const UInt32 QS_ALLEVENTS       = (QS_INPUT         |
												  QS_POSTMESSAGE   | 
												  QS_TIMER         | 
												  QS_PAINT         | 
												  QS_HOTKEY);

		public const UInt32 QS_ALLINPUT        = (QS_INPUT         |
												  QS_POSTMESSAGE   |
												  QS_TIMER         | 
												  QS_PAINT         |
												  QS_HOTKEY        |
												  QS_SENDMESSAGE);


		public const UInt32 USER_TIMER_MAXIMUM  = 0x7FFFFFFF;
		public const UInt32 USER_TIMER_MINIMUM  = 0x0000000A;


		/*
		WINUSERAPI UINT_PTR WINAPI SetTimer(
			_In_opt_ HWND hWnd,
			_In_ UINT_PTR nIDEvent,
			_In_ UINT uElapse,
			_In_opt_ TIMERPROC lpTimerFunc);
		*/
#if WIN32_WINNT_WIN7_LATER

		public const UInt32 TIMERV_DEFAULT_COALESCING   = (0);
		public const UInt32 TIMERV_NO_COALESCING        = (0xFFFFFFFF);

		public const UInt32 TIMERV_COALESCING_MIN       = (1);
		public const UInt32 TIMERV_COALESCING_MAX       = (0x7FFFFFF5);

        /*
		WINUSERAPI UINT_PTR WINAPI SetCoalescableTimer(
			_In_opt_ HWND hWnd,
			_In_ UINT_PTR nIDEvent,
			_In_ UINT uElapse,
			_In_opt_ TIMERPROC lpTimerFunc,
			_In_ ULONG uToleranceDelay);
		*/
#endif // WIN32_WINNT_WIN7_LATER

        // WINUSERAPI BOOL WINAPI KillTimer(_In_opt_ HWND hWnd, _In_ UINT_PTR uIDEvent);

        // WINUSERAPI BOOL WINAPI IsWindowUnicode(_In_ HWND hWnd); 

        // WINUSERAPI BOOL WINAPI EnableWindow(_In_ HWND hWnd, _In_ BOOL bEnable);

        // WINUSERAPI BOOL WINAPI IsWindowEnabled(_In_ HWND hWnd); 
        /*
		WINUSERAPI HACCEL WINAPI LoadAcceleratorsA(_In_opt_ HINSTANCE hInstance, _In_ LPCSTR lpTableName);
		WINUSERAPI HACCEL WINAPI LoadAcceleratorsW(_In_opt_ HINSTANCE hInstance, _In_ LPCWSTR lpTableName);
		*/
        /*
		WINUSERAPI HACCEL WINAPI CreateAcceleratorTableA(_In_reads_(cAccel) LPACCEL paccel, _In_ int cAccel);		
		WINUSERAPI HACCEL WINAPI CreateAcceleratorTableW(_In_reads_(cAccel) LPACCEL paccel, _In_ int cAccel);

		// WINUSERAPI BOOL WINAPI DestroyAcceleratorTable(_In_ HACCEL hAccel);
		/*
		WINUSERAPI int WINAPI CopyAcceleratorTableA(
			_In_ HACCEL hAccelSrc,
			_Out_writes_to_opt_(cAccelEntries, return) LPACCEL lpAccelDst,
			_In_ int cAccelEntries);
			
		WINUSERAPI int WINAPI CopyAcceleratorTableW(
		    _In_ HACCEL hAccelSrc,
			_Out_writes_to_opt_(cAccelEntries, return) LPACCEL lpAccelDst,
			_In_ int cAccelEntries);
		*/


        #region MSG
        /*
		WINUSERAPI int WINAPI TranslateAcceleratorA(
			_In_ HWND hWnd,
			_In_ HACCEL hAccTable,
			_In_ LPMSG lpMsg);
			
		WINUSERAPI int WINAPI TranslateAcceleratorW(
			_In_ HWND hWnd,
			_In_ HACCEL hAccTable,
			_In_ LPMSG lpMsg);
		*/

        #endregion MSG


        #region SYSTEMMETRICS

        public const Int32 SM_CXSCREEN             = 0;
		public const Int32 SM_CYSCREEN             = 1;
		public const Int32 SM_CXVSCROLL            = 2;
		public const Int32 SM_CYHSCROLL            = 3;
		public const Int32 SM_CYCAPTION            = 4;
		public const Int32 SM_CXBORDER             = 5;
		public const Int32 SM_CYBORDER             = 6;
		public const Int32 SM_CXDLGFRAME           = 7;
		public const Int32 SM_CYDLGFRAME           = 8;
		public const Int32 SM_CYVTHUMB             = 9;
		public const Int32 SM_CXHTHUMB             = 10;
		public const Int32 SM_CXICON               = 11;
		public const Int32 SM_CYICON               = 12;
		public const Int32 SM_CXCURSOR             = 13;
		public const Int32 SM_CYCURSOR             = 14;
		public const Int32 SM_CYMENU               = 15;
		public const Int32 SM_CXFULLSCREEN         = 16;
		public const Int32 SM_CYFULLSCREEN         = 17;
		public const Int32 SM_CYKANJIWINDOW        = 18;
		public const Int32 SM_MOUSEPRESENT         = 19;
		public const Int32 SM_CYVSCROLL            = 20;
		public const Int32 SM_CXHSCROLL            = 21;
		public const Int32 SM_DEBUG                = 22;
		public const Int32 SM_SWAPBUTTON           = 23;
		public const Int32 SM_RESERVED1            = 24;
		public const Int32 SM_RESERVED2            = 25;
		public const Int32 SM_RESERVED3            = 26;
		public const Int32 SM_RESERVED4            = 27;
		public const Int32 SM_CXMIN                = 28;
		public const Int32 SM_CYMIN                = 29;
		public const Int32 SM_CXSIZE               = 30;
		public const Int32 SM_CYSIZE               = 31;
		public const Int32 SM_CXFRAME              = 32;
		public const Int32 SM_CYFRAME              = 33;
		public const Int32 SM_CXMINTRACK           = 34;
		public const Int32 SM_CYMINTRACK           = 35;
		public const Int32 SM_CXDOUBLECLK          = 36;
		public const Int32 SM_CYDOUBLECLK          = 37;
		public const Int32 SM_CXICONSPACING        = 38;
		public const Int32 SM_CYICONSPACING        = 39;
		public const Int32 SM_MENUDROPALIGNMENT    = 40;
		public const Int32 SM_PENWINDOWS           = 41;
		public const Int32 SM_DBCSENABLED          = 42;
		public const Int32 SM_CMOUSEBUTTONS        = 43;
#if WIN32_WINNT_NT4_LATER
        public const Int32 SM_CXFIXEDFRAME           = SM_CXDLGFRAME;  /* ;win40 name change */
        public const Int32 SM_CYFIXEDFRAME           = SM_CYDLGFRAME;  /* ;win40 name change */
        public const Int32 SM_CXSIZEFRAME            = SM_CXFRAME;     /* ;win40 name change */
        public const Int32 SM_CYSIZEFRAME            = SM_CYFRAME;     /* ;win40 name change */

        public const Int32 SM_SECURE               = 44;
        public const Int32 SM_CXEDGE               = 45;
        public const Int32 SM_CYEDGE               = 46;
        public const Int32 SM_CXMINSPACING         = 47;
        public const Int32 SM_CYMINSPACING         = 48;
        public const Int32 SM_CXSMICON             = 49;
        public const Int32 SM_CYSMICON             = 50;
        public const Int32 SM_CYSMCAPTION          = 51;
        public const Int32 SM_CXSMSIZE             = 52;
        public const Int32 SM_CYSMSIZE             = 53;
        public const Int32 SM_CXMENUSIZE           = 54;
        public const Int32 SM_CYMENUSIZE           = 55;
        public const Int32 SM_ARRANGE              = 56;
        public const Int32 SM_CXMINIMIZED          = 57;
        public const Int32 SM_CYMINIMIZED          = 58;
        public const Int32 SM_CXMAXTRACK           = 59;
        public const Int32 SM_CYMAXTRACK           = 60;
        public const Int32 SM_CXMAXIMIZED          = 61;
        public const Int32 SM_CYMAXIMIZED          = 62;
        public const Int32 SM_NETWORK              = 63;
        public const Int32 SM_CLEANBOOT            = 67;
        public const Int32 SM_CXDRAG               = 68;
        public const Int32 SM_CYDRAG               = 69;
#endif // WIN32_WINNT_NT4_LATER
        public const Int32 SM_SHOWSOUNDS           = 70;
#if WIN32_WINNT_NT4_LATER
        public const Int32 SM_CXMENUCHECK          = 71;   /* Use instead of GetMenuCheckMarkDimensions()! */
        public const Int32 SM_CYMENUCHECK          = 72;
        public const Int32 SM_SLOWMACHINE          = 73;
        public const Int32 SM_MIDEASTENABLED       = 74;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
        public const Int32 SM_MOUSEWHEELPRESENT    = 75;
#endif
#if WIN32_WINNT_WIN2K_LATER
        public const Int32 SM_XVIRTUALSCREEN       = 76;
        public const Int32 SM_YVIRTUALSCREEN       = 77;
        public const Int32 SM_CXVIRTUALSCREEN      = 78;
        public const Int32 SM_CYVIRTUALSCREEN      = 79;
        public const Int32 SM_CMONITORS            = 80;
        public const Int32 SM_SAMEDISPLAYFORMAT    = 81;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WIN2K_LATER
        public const Int32 SM_IMMENABLED           = 82;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WINXP_LATER
        public const Int32 SM_CXFOCUSBORDER        = 83;
        public const Int32 SM_CYFOCUSBORDER        = 84;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const Int32 SM_TABLETPC             = 86;
        public const Int32 SM_MEDIACENTER          = 87;
        public const Int32 SM_STARTER              = 88;
        public const Int32 SM_SERVERR2             = 89;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_VISTA_LATER
        public const Int32 SM_MOUSEHORIZONTALWHEELPRESENT    = 91;
        public const Int32 SM_CXPADDEDBORDER       = 92;
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WIN7_LATER
        public const Int32 SM_DIGITIZER            = 94;
        public const Int32 SM_MAXIMUMTOUCHES       = 95;
#endif // WIN32_WINNT_WIN7_LATER

#if INVALIDWINDOWVERSION //(WINVER < 0x0500) && (!defined(_WIN32_WINNT) || (_WIN32_WINNT < 0x0400))
        public const Int32 SM_CMETRICS             = 76;
#elif !WIN32_WINNT_WINXP_LATER //WINVER == 0x500
        public const Int32 SM_CMETRICS             = 83;
#elif !WIN32_WINNT_WINVISTA //_WINVER == 0x501
        public const Int32 SM_CMETRICS             = 91;
#elif !WIN32_WINNT_WINBLUE_LATER //WINVER == 0x600
        public const Int32 SM_CMETRICS             = 93;
#else
        public const Int32 SM_CMETRICS             = 97;
#endif

#if WIN32_WINNT_WIN2K_LATER
        public const Int32 SM_REMOTESESSION        = 0x1000;

#if WIN32_WINNT_WINXP_LATER
        public const Int32 SM_SHUTTINGDOWN           = 0x2000;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const Int32 SM_REMOTECONTROL          = 0x2001;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const Int32 SM_CARETBLINKINGENABLED   = 0x2002;
#endif // WIn32_WINNT_WINXP

#if WIN32_WINNT_WIN8_LATER
        public const Int32 SM_CONVERTIBLESLATEMODE   = 0x2003;
        public const Int32 SM_SYSTEMDOCKED           = 0x2004;
#endif // WIN32_WINNT_WIN8_LATER

#endif // WIN32_WINNT_WIN2K_LATER


        // WINUSERAPI int WINAPI GetSystemMetrics(_In_ int nIndex);

#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0605)
        // WINUSERAPI int WINAPI GetSystemMetricsForDpi(_In_ int nIndex, _In_ UINT dpi);
#endif // WIN32_WINNT_WIN10_LATER WINVER >= 0x0605


        #endregion SYSMETRICS


        #region MENUS
        /*
		WINUSERAPI HMENU WINAPI LoadMenuA(_In_opt_ HINSTANCE hInstance, _In_ LPCSTR lpMenuName);		
		WINUSERAPI HMENU WINAPI LoadMenuW(_In_opt_ HINSTANCE hInstance, _In_ LPCWSTR lpMenuName);
		*/
        /*
		WINUSERAPI HMENU WINAPI LoadMenuIndirectA(_In_ CONST MENUTEMPLATEA* lpMenuTemplate);		
		WINUSERAPI HMENU WINAPI LoadMenuIndirectW(In_ CONST MENUTEMPLATEW* lpMenuTemplate);
		*/

        // WINUSERAPI HMENU WINAPI GetMenu(_In_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI SetMenu(_In_ HWND hWnd, _In_opt_ HMENU hMenu);
        /*
		WINUSERAPI BOOL WINAPI ChangeMenuA(
			_In_ HMENU hMenu,
			_In_ UINT cmd,
			_In_opt_ LPCSTR lpszNewItem,
			_In_ UINT cmdInsert,
			_In_ UINT flags);
			
		WINUSERAPI BOOL WINAPI ChangeMenuW(
			_In_ HMENU hMenu,
			_In_ UINT cmd,
			_In_opt_ LPCWSTR lpszNewItem,
			_In_ UINT cmdInsert,
			_In_ UINT flags);
		*/
        /*
		WINUSERAPI BOOL WINAPI HiliteMenuItem(
			_In_ HWND hWnd,
			_In_ HMENU hMenu,
			_In_ UINT uIDHiliteItem,
			_In_ UINT uHilite);
		*/
        /*
		WINUSERAPI int WINAPI GetMenuStringA(
			_In_ HMENU hMenu,
			_In_ UINT uIDItem,
			_Out_writes_opt_(cchMax) LPSTR lpString,
			_In_ int cchMax,
			_In_ UINT flags);
			
		WINUSERAPI int WINAPI GetMenuStringW(
			_In_ HMENU hMenu,
			_In_ UINT uIDItem,
			_Out_writes_opt_(cchMax) LPWSTR lpString,
			_In_ int cchMax,
			_In_ UINT flags);
        */		
		/*
		WINUSERAPI UINT WINAPI GetMenuState(    // no LastError
			_In_ HMENU hMenu,
			_In_ UINT uId,
			_In_ UINT uFlags);
		*/
        [DllImport("user32.dll")]
        static extern UInt32 GetMenuState(IntPtr hwnd, UInt32 uId, UInt32 uFlags);

        // WINUSERAPI BOOL WINAPI DrawMenuBar(_In_ HWND hWnd);

#if WIN32_WINNT_WINXP_LATER
        // #define PMB_ACTIVE      0x00000001
#endif // WIN32_WINNT_WINXP_LATER


        // WINUSERAPI HMENU WINAPI GetSystemMenu(_In_ HWND hWnd, _In_ BOOL bRevert); // no LastError
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        //WINUSERAPI HMENU WINAPI CreateMenu(VOID);

        // WINUSERAPI HMENU WINAPI CreatePopupMenu(VOID);

        // WINUSERAPI BOOL WINAPI DestroyMenu(_In_ HMENU hMenu); 
        /*
		WINUSERAPI DWORD WINAPI CheckMenuItem(
			_In_ HMENU hMenu,
			_In_ UINT uIDCheckItem,
			_In_ UINT uCheck);
		*/
        /*
		WINUSERAPI BOOL WINAPI EnableMenuItem(
			_In_ HMENU hMenu,
			_In_ UINT uIDEnableItem,
			_In_ UINT uEnable);
		*/
        // WINUSERAPI HMENU WINAPI GetSubMenu(_In_ HMENU hMenu, _In_ int nPos);

        // WINUSERAPI UINT WINAPI GetMenuItemID(_In_ HMENU hMenu, _In_ int nPos);

        // WINUSERAPI int WINAPI GetMenuItemCount(_In_opt_ HMENU hMenu);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 GetMenuItemCount(IntPtr hmenu);
        /*
		WINUSERAPI BOOL WINAPI InsertMenuA(
			_In_ HMENU hMenu,
			_In_ UINT uPosition,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCSTR lpNewItem);
			
		WINUSERAPI BOOL WINAPI InsertMenuW(
			_In_ HMENU hMenu,
			_In_ UINT uPosition,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCWSTR lpNewItem);
        */
		/*
		WINUSERAPI BOOL WINAPI AppendMenuA(
			_In_ HMENU hMenu,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCSTR lpNewItem);
			
		WINUSERAPI BOOL WINAPI AppendMenuW(
			_In_ HMENU hMenu,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCWSTR lpNewItem);
		*/
        /*
		WINUSERAPI BOOL WINAPI ModifyMenuA(
			_In_ HMENU hMnu,
			_In_ UINT uPosition,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCSTR lpNewItem);
			
		WINUSERAPI BOOL WINAPI ModifyMenuW(
			_In_ HMENU hMnu,
			_In_ UINT uPosition,
			_In_ UINT uFlags,
			_In_ UINT_PTR uIDNewItem,
			_In_opt_ LPCWSTR lpNewItem);
		*/
        /*
		WINUSERAPI BOOL WINAPI RemoveMenu(
			_In_ HMENU hMenu,
			_In_ UINT uPosition,
			_In_ UINT uFlags);
		*/
        /*
		WINUSERAPI BOOL WINAPI DeleteMenu(
			_In_ HMENU hMenu,
			_In_ UINT uPosition,
			_In_ UINT uFlags);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetMenuItemBitmaps(
			_In_ HMENU hMenu,
			_In_ UINT uPosition,
			_In_ UINT uFlags,
			_In_opt_ HBITMAP hBitmapUnchecked,
			_In_opt_ HBITMAP hBitmapChecked);
		*/

        // WINUSERAPI LONG WINAPI GetMenuCheckMarkDimensions(VOID);
        /*
		WINUSERAPI BOOL WINAPI TrackPopupMenu(
			_In_ HMENU hMenu,
			_In_ UINT uFlags,
			_In_ int x,
			_In_ int y,
			_Reserved_ int nReserved,
			_In_ HWND hWnd,
			_Reserved_ CONST RECT* prcRect);
		*/
#if WIN32_WINNT_NT4_LATER
        public const Int32 MNC_IGNORE  = 0;
		public const Int32 MNC_CLOSE   = 1;
		public const Int32 MNC_EXECUTE = 2;
		public const Int32 MNC_SELECT  = 3;

		public struct TPMPARAMS
		{
			public UInt32 cbSize;     /* Size of structure */
			public WinDef.RECT rcExclude;  /* Screen coordinates of rectangle to exclude when positioning */
		}
        /*
		WINUSERAPI BOOL WINAPI TrackPopupMenuEx(
			_In_ HMENU hMenu,
		    _In_ UINT uFlags,
			_In_ int x,
			_In_ int y,
			_In_ HWND hwnd,
			_In_opt_ LPTPMPARAMS lptpm);
		*/
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN7_LATER
        /* 
		WINUSERAPI BOOL WINAPI CalculatePopupWindowPosition(
			_In_ const POINT* anchorPoint,
			_In_ const SIZE* windowSize,
			_In_ UINT flags,
			_In_opt_ RECT *excludeRect,
			_Out_ RECT *popupWindowPosition);
		*/
        [DllImport("user32.dll")]
        public static extern bool CalculatePopupWindowPosition(
            [In] ref WinDef.POINT anchorPoint, 
            [In] ref WinDef.SIZE windowSize,
            UInt32 flags, 
            IntPtr excludeRect,
            out WinDef.RECT popupWindowPosition);

#endif // WIN32_WINNT_WIN7_LATER

#if WIN32_WINNT_WIN2K_LATER

		public const UInt32 MNS_NOCHECK         = 0x80000000;
		public const UInt32 MNS_MODELESS        = 0x40000000;
		public const UInt32 MNS_DRAGDROP        = 0x20000000;
		public const UInt32 MNS_AUTODISMISS     = 0x10000000;
		public const UInt32 MNS_NOTIFYBYPOS     = 0x08000000;
		public const UInt32 MNS_CHECKORBMP      = 0x04000000;

		public const UInt32 MIM_MAXHEIGHT               = 0x00000001;
		public const UInt32 MIM_BACKGROUND              = 0x00000002;
		public const UInt32 MIM_HELPID                  = 0x00000004;
		public const UInt32 MIM_MENUDATA                = 0x00000008;
		public const UInt32 MIM_STYLE                   = 0x00000010;
		public const UInt32 MIM_APPLYTOSUBMENUS         = 0x80000000;

		public struct MENUINFO
		{
			public UInt32 cbSize;
			public UInt32 fMask;
			public UInt32 dwStyle;
			public UInt32 cyMax;
			public IntPtr hbrBack;
			public UInt32 dwContextHelpID;
			public UIntPtr dwMenuData;
		}

        // WINUSERAPI BOOL WINAPI GetMenuInfo(_In_ HMENU, _Inout_ LPMENUINFO);

        // WINUSERAPI BOOL WINAPI SetMenuInfo(_In_ HMENU, _In_ LPCMENUINFO);

        // WINUSERAPI BOOL WINAPI EndMenu(VOID);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EndMenu();

        public const Int32 MND_CONTINUE       = 0;
		public const Int32 MND_ENDMENU        = 1;

		public struct MENUGETOBJECTINFO
		{
			public UInt32 dwFlags;
			public UInt32 uPos;
			public IntPtr hmenu;
			public IntPtr riid;
			public IntPtr pvObj;
		}

		public const UInt32 MNGOF_TOPGAP         = 0x00000001;
		public const UInt32 MNGOF_BOTTOMGAP      = 0x00000002;

		public const Int32 MNGO_NOINTERFACE     = 0x00000000;
		public const Int32 MNGO_NOERROR         = 0x00000001;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_NT4_LATER
		public const UInt32 MIIM_STATE       = 0x00000001;
		public const UInt32 MIIM_ID          = 0x00000002;
		public const UInt32 MIIM_SUBMENU     = 0x00000004;
		public const UInt32 MIIM_CHECKMARKS  = 0x00000008;
		public const UInt32 MIIM_TYPE        = 0x00000010;
		public const UInt32 MIIM_DATA        = 0x00000020;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 MIIM_STRING      = 0x00000040;
		public const UInt32 MIIM_BITMAP      = 0x00000080;
		public const UInt32 MIIM_FTYPE       = 0x00000100;

		public static readonly IntPtr HBMMENU_CALLBACK            = ((IntPtr)(-1));
		public static readonly IntPtr HBMMENU_SYSTEM              = ((IntPtr)  1);
		public static readonly IntPtr HBMMENU_MBAR_RESTORE        = ((IntPtr)  2);
		public static readonly IntPtr HBMMENU_MBAR_MINIMIZE       = ((IntPtr)  3);
		public static readonly IntPtr HBMMENU_MBAR_CLOSE          = ((IntPtr)  5);
		public static readonly IntPtr HBMMENU_MBAR_CLOSE_D        = ((IntPtr)  6);
		public static readonly IntPtr HBMMENU_MBAR_MINIMIZE_D     = ((IntPtr)  7);
		public static readonly IntPtr HBMMENU_POPUP_CLOSE         = ((IntPtr)  8);
		public static readonly IntPtr HBMMENU_POPUP_RESTORE       = ((IntPtr)  9);
		public static readonly IntPtr HBMMENU_POPUP_MAXIMIZE      = ((IntPtr) 10);
		public static readonly IntPtr HBMMENU_POPUP_MINIMIZE      = ((IntPtr) 11);
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_NT4_LATER

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MENUITEMINFOA
		{
			public UInt32 cbSize;
			public UInt32 fMask;
			public UInt32 fType;         // used if MIIM_TYPE (4.0) or MIIM_FTYPE (>4.0)
			public UInt32 fState;        // used if MIIM_STATE
			public UInt32 wID;           // used if MIIM_ID
			public IntPtr hSubMenu;      // used if MIIM_SUBMENU
			public IntPtr hbmpChecked;   // used if MIIM_CHECKMARKS
			public IntPtr hbmpUnchecked; // used if MIIM_CHECKMARKS
			public UIntPtr dwItemData;   // used if MIIM_DATA
			public IntPtr dwTypeData;    // not LPSTR //  used if MIIM_TYPE (4.0) or MIIM_STRING (>4.0)
			public UInt32 cch;           // used if MIIM_TYPE (4.0) or MIIM_STRING (>4.0)
#if WIN32_WINNT_WIN2K_LATER
			public IntPtr hbmpItem;      // used if MIIM_BITMAP
#endif // WIN32_WINNT_WIN2K_LATER
		}

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MENUITEMINFOW
		{
			public UInt32 cbSize;
			public UInt32 fMask;
			public UInt32 fType;         // used if MIIM_TYPE (4.0) or MIIM_FTYPE (>4.0)
			public UInt32 fState;        // used if MIIM_STATE
			public UInt32 wID;           // used if MIIM_ID
			public IntPtr hSubMenu;      // used if MIIM_SUBMENU
			public IntPtr hbmpChecked;   // used if MIIM_CHECKMARKS
			public IntPtr hbmpUnchecked; // used if MIIM_CHECKMARKS
			public UIntPtr dwItemData;   // used if MIIM_DATA
            public IntPtr dwTypeData;    // not LPWSTR // used if MIIM_TYPE (4.0) or MIIM_STRING (>4.0)
			public UInt32 cch;           // used if MIIM_TYPE (4.0) or MIIM_STRING (>4.0)
#if WIN32_WINNT_WIN2K_LATER
			public IntPtr hbmpItem;      // used if MIIM_BITMAP
#endif // WIN32_WINNT_WIN2K_LATER
		}
        

        /*
		WINUSERAPI BOOL WINAPI InsertMenuItemA(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPosition,
			_In_ LPCMENUITEMINFOA lpmi);

		WINUSERAPI BOOL WINAPI InsertMenuItemW(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPosition,
			_In_ LPCMENUITEMINFOW lpmi);
		*/
        /*
		WINUSERAPI BOOL WINAPI GetMenuItemInfoA(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPosition,
			_Inout_ LPMENUITEMINFOA lpmii);

        WINUSERAPI BOOL WINAPI GetMenuItemInfoW(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPosition,
			_Inout_ LPMENUITEMINFOW lpmii);
        */
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetMenuItemInfoA(IntPtr hMenu, UInt32 uItem, bool fByPosition, [In, Out] ref MENUITEMINFOA menuItemInfo);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetMenuItemInfoW(IntPtr hMenu, UInt32 uItem, bool fByPosition, [In, Out] ref MENUITEMINFOW menuItemInfo);
        /*
		WINUSERAPI BOOL WINAPI SetMenuItemInfoA(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPositon,
			_In_ LPCMENUITEMINFOA lpmii);
			
		WINUSERAPI BOOL WINAPI SetMenuItemInfoW(
			_In_ HMENU hmenu,
			_In_ UINT item,
			_In_ BOOL fByPositon,
			_In_ LPCMENUITEMINFOW lpmii);
		*/

        public const UInt32 GMDI_USEDISABLED    = 0x0001;
		public const UInt32 GMDI_GOINTOPOPUPS   = 0x0002;
        /*
		WINUSERAPI UINT WINAPI GetMenuDefaultItem(
			_In_ HMENU hMenu,
			_In_ UINT fByPos,
			_In_ UINT gmdiFlags);
        */
        /*
		WINUSERAPI BOOL WINAPI SetMenuDefaultItem(
			_In_ HMENU hMenu,
			_In_ UINT uItem,
			_In_ UINT fByPos);
		*/
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetMenuDefaultItem(IntPtr hMenu, UInt32 uItem, bool fByPos);
        /*
		WINUSERAPI BOOL WINAPI GetMenuItemRect(
			_In_opt_ HWND hWnd,
			_In_ HMENU hMenu,
			_In_ UINT uItem,
			_Out_ LPRECT lprcItem);
		*/
        /*
		WINUSERAPI int WINAPI MenuItemFromPoint( // no LastError
			_In_opt_ HWND hWnd,
			_In_ HMENU hMenu,
			_In_ POINT ptScreen);
		*/
        [DllImport("user32.dll")]
        public static extern Int32 MenuItemFromPoint(IntPtr hwnd, IntPtr hmenu, WinDef.POINT ptScreen);

#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 TPM_LEFTBUTTON  = 0x0000;
		public const UInt32 TPM_RIGHTBUTTON = 0x0002;
		public const UInt32 TPM_LEFTALIGN   = 0x0000;
		public const UInt32 TPM_CENTERALIGN = 0x0004;
		public const UInt32 TPM_RIGHTALIGN  = 0x0008;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 TPM_TOPALIGN        = 0x0000;
		public const UInt32 TPM_VCENTERALIGN    = 0x0010;
		public const UInt32 TPM_BOTTOMALIGN     = 0x0020;

		public const UInt32 TPM_HORIZONTAL      = 0x0000;     /* Horz alignment matters more */
		public const UInt32 TPM_VERTICAL        = 0x0040;     /* Vert alignment matters more */
		public const UInt32 TPM_NONOTIFY        = 0x0080;     /* Don't send any notification msgs */
		public const UInt32 TPM_RETURNCMD       = 0x00100;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 TPM_RECURSE         = 0x0001;
		public const UInt32 TPM_HORPOSANIMATION = 0x0400;
		public const UInt32 TPM_HORNEGANIMATION = 0x0800;
		public const UInt32 TPM_VERPOSANIMATION = 0x1000;
		public const UInt32 TPM_VERNEGANIMATION = 0x2000;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 TPM_NOANIMATION     = 0x4000;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WINXP_LATER
		public const UInt32 TPM_LAYOUTRTL       = 0x8000;
#endif // WIN32_WINNT_WINXP_LATER
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WIN7_LATER
		public const UInt32 TPM_WORKAREA        = 0x10000;
#endif // WIN32_WINNT_WIN7_LATER

        #endregion MENUS



#if WIN32_WINNT_NT4_LATER


        public struct DROPSTRUCT
		{
			public IntPtr hwndSource;
			public IntPtr hwndSink;
			public UInt32 wFmt;
			public UIntPtr dwData;
			public WinDef.POINT ptDrop;
			public UInt32 dwControlData;
		}


		public const UInt32 DOF_EXECUTABLE      = 0x8001;      // wFmt flags
		public const UInt32 DOF_DOCUMENT        = 0x8002;
		public const UInt32 DOF_DIRECTORY       = 0x8003;
		public const UInt32 DOF_MULTIPLE        = 0x8004;
		public const UInt32 DOF_PROGMAN         = 0x0001;
		public const UInt32 DOF_SHELLDATA       = 0x0002;

		public const UInt32 DO_DROPFILE         = 0x454C4946;
		public const UInt32 DO_PRINTFILE        = 0x544E5250;


        /*
		WINUSERAPI DWORD WINAPI DragObject(
			_In_ HWND hwndParent,
			_In_ HWND hwndFrom,
			_In_ UINT fmt,
			_In_ ULONG_PTR data,
			_In_opt_ HCURSOR hcur);
		*/
        // WINUSERAPI BOOL WINAPI DragDetect(_In_ HWND hwnd, _In_ POINT pt);


#endif // WIN32_WINNT_NT4_LATER

        /*
		WINUSERAPI BOOL WINAPI DrawIcon(
			_In_ HDC hDC,
			_In_ int X,
			_In_ int Y,
			_In_ HICON hIcon);
		*/


        #region DRAWTEXT

        public const UInt32 DT_TOP                      = 0x00000000;
		public const UInt32 DT_LEFT                     = 0x00000000;
		public const UInt32 DT_CENTER                   = 0x00000001;
		public const UInt32 DT_RIGHT                    = 0x00000002;
		public const UInt32 DT_VCENTER                  = 0x00000004;
		public const UInt32 DT_BOTTOM                   = 0x00000008;
		public const UInt32 DT_WORDBREAK                = 0x00000010;
		public const UInt32 DT_SINGLELINE               = 0x00000020;
		public const UInt32 DT_EXPANDTABS               = 0x00000040;
		public const UInt32 DT_TABSTOP                  = 0x00000080;
		public const UInt32 DT_NOCLIP                   = 0x00000100;
		public const UInt32 DT_EXTERNALLEADING          = 0x00000200;
		public const UInt32 DT_CALCRECT                 = 0x00000400;
		public const UInt32 DT_NOPREFIX                 = 0x00000800;
		public const UInt32 DT_INTERNAL                 = 0x00001000;

#if WIN32_WINNT_NT4_LATER
		public const UInt32 DT_EDITCONTROL              = 0x00002000;
		public const UInt32 DT_PATH_ELLIPSIS            = 0x00004000;
		public const UInt32 DT_END_ELLIPSIS             = 0x00008000;
		public const UInt32 DT_MODIFYSTRING             = 0x00010000;
		public const UInt32 DT_RTLREADING               = 0x00020000;
		public const UInt32 DT_WORD_ELLIPSIS            = 0x00040000;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DT_NOFULLWIDTHCHARBREAK     = 0x00080000;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DT_HIDEPREFIX               = 0x00100000;
		public const UInt32 DT_PREFIXONLY               = 0x00200000;
#endif // WIN32_WINNT_WIN2K_LATER
#endif // WIN32_WINNT_WIN2K_LATER


		public struct DRAWTEXTPARAMS
		{
			public UInt32 cbSize;
			public Int32 iTabLength;
			public Int32 iLeftMargin;
			public Int32 iRightMargin;
			public UInt32 uiLengthDrawn;
		}


#endif // WIN32_WINNT_NT4_LATER


        //#define _In_bypassable_reads_or_z_(size) \
        //			_When_(((size) == -1) || (_String_length_(_Curr_) <  (size)), _In_z_) \
        //			_When_(((size) != -1) && (_String_length_(_Curr_) >= (size)), _In_reads_(size))

        //#define _Inout_grows_updates_bypassable_or_z_(size, grows) \
        //			_When_(((size) == -1) || (_String_length_(_Curr_) <  (size)), _Pre_z_ _Pre_valid_ _Out_writes_z_(_String_length_(_Curr_) + (grows))) \
        //			_When_(((size) != -1) && (_String_length_(_Curr_) >= (size)), _Pre_count_(size) _Pre_valid_ _Out_writes_z_((size) + (grows)))

        /*
		WINUSERAPI _Success_(return) int WINAPI DrawTextA(
			_In_ HDC hdc,
			_When_((format & DT_MODIFYSTRING), _At_((LPSTR) lpchText, _Inout_grows_updates_bypassable_or_z_(cchText, 4)))

		    _When_((!(format & DT_MODIFYSTRING)), _In_bypassable_reads_or_z_(cchText))
			LPCSTR lpchText,
			_In_ int cchText,
			_Inout_ LPRECT lprc,
			_In_ UINT format);
			
		WINUSERAPI _Success_(return) int WINAPI DrawTextW(
			_In_ HDC hdc,
			_When_((format & DT_MODIFYSTRING), _At_((LPWSTR) lpchText, _Inout_grows_updates_bypassable_or_z_(cchText, 4)))

			_When_((!(format & DT_MODIFYSTRING)), _In_bypassable_reads_or_z_(cchText))
			LPCWSTR lpchText,
			_In_ int cchText,
			_Inout_ LPRECT lprc,
			_In_ UINT format);
		*/


#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI _Success_(return) int WINAPI DrawTextExA(
			_In_ HDC hdc,
			_When_((cchText) < -1, _Unreferenced_parameter_)

		    _When_((format & DT_MODIFYSTRING), _Inout_grows_updates_bypassable_or_z_(cchText, 4))

			_When_((!(format & DT_MODIFYSTRING)), _At_((LPCSTR) lpchText, _In_bypassable_reads_or_z_(cchText)))
			LPSTR lpchText,
			_In_ int cchText,
			_Inout_ LPRECT lprc,
			_In_ UINT format,
			_In_opt_ LPDRAWTEXTPARAMS lpdtp);

		WINUSERAPI _Success_(return) int WINAPI DrawTextExW(
			_In_ HDC hdc,
			_When_((cchText) < -1, _Unreferenced_parameter_)

			_When_((format & DT_MODIFYSTRING), _Inout_grows_updates_bypassable_or_z_(cchText, 4))

			_When_((!(format & DT_MODIFYSTRING)), _At_((LPCWSTR) lpchText, _In_bypassable_reads_or_z_(cchText)))
			LPWSTR lpchText,
			_In_ int cchText,
			_Inout_ LPRECT lprc,
			_In_ UINT format,
			_In_opt_ LPDRAWTEXTPARAMS lpdtp);
		*/
#endif // WIN32_WINNT_NT4_LATER


        #endregion DRAWTEXT


        /*
		WINUSERAPI BOOL WINAPI GrayStringA(
			_In_ HDC hDC,
			_In_opt_ HBRUSH hBrush,
			_In_opt_ GRAYSTRINGPROC lpOutputFunc,
			_In_ LPARAM lpData,
			_In_ int nCount,
			_In_ int X,
			_In_ int Y,
			_In_ int nWidth,
			_In_ int nHeight);

		WINUSERAPI BOOL WINAPI GrayStringW(
			_In_ HDC hDC,
			_In_opt_ HBRUSH hBrush,
			_In_opt_ GRAYSTRINGPROC lpOutputFunc,
			_In_ LPARAM lpData,
			_In_ int nCount,
			_In_ int X,
			_In_ int Y,
			_In_ int nWidth,
			_In_ int nHeight);
		*/


#if WIN32_WINNT_NT4_LATER
        public const UInt32 DST_COMPLEX     = 0x0000;
		public const UInt32 DST_TEXT        = 0x0001;
		public const UInt32 DST_PREFIXTEXT  = 0x0002;
		public const UInt32 DST_ICON        = 0x0003;
		public const UInt32 DST_BITMAP      = 0x0004;

		public const UInt32 DSS_NORMAL      = 0x0000;
		public const UInt32 DSS_UNION       = 0x0010;  /* Gray string appearance */
		public const UInt32 DSS_DISABLED    = 0x0020;
		public const UInt32 DSS_MONO        = 0x0080;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 DSS_HIDEPREFIX  = 0x0200;
		public const UInt32 DSS_PREFIXONLY  = 0x0400;
#endif // WIN32_WINNT_WIN2K_LATER
		public const UInt32 DSS_RIGHT       = 0x8000;


		/*
		WINUSERAPI BOOL WINAPI DrawStateA(
			_In_ HDC hdc,
			_In_opt_ HBRUSH hbrFore,
			_In_opt_ DRAWSTATEPROC qfnCallBack,
			_In_ LPARAM lData,
			_In_ WPARAM wData,
			_In_ int x,
			_In_ int y,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT uFlags);
			
		WINUSERAPI BOOL WINAPI DrawStateW( 
			_In_ HDC hdc,
			_In_opt_ HBRUSH hbrFore,
		    _In_opt_ DRAWSTATEPROC qfnCallBack,
			_In_ LPARAM lData,
			_In_ WPARAM wData,
			_In_ int x,
			_In_ int y,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT uFlags);
		*/


#endif // WIN32_WINNT_NT4_LATER


		/*
		WINUSERAPI LONG WINAPI TabbedTextOutA(
			_In_ HDC hdc,
			_In_ int x,
			_In_ int y,
			_In_reads_(chCount) LPCSTR lpString,
			_In_ int chCount,
			_In_ int nTabPositions,
			_In_reads_opt_(nTabPositions) CONST INT* lpnTabStopPositions,
			_In_ int nTabOrigin);

		WINUSERAPI LONG WINAPI TabbedTextOutW(
			_In_ HDC hdc,
			_In_ int x,
			_In_ int y,
			_In_reads_(chCount) LPCWSTR lpString,
			_In_ int chCount,
			_In_ int nTabPositions,
			_In_reads_opt_(nTabPositions) CONST INT* lpnTabStopPositions,
			_In_ int nTabOrigin);
		*/
		/*
		WINUSERAPI DWORD WINAPI GetTabbedTextExtentA(
			_In_ HDC hdc,
			_In_reads_(chCount) LPCSTR lpString,
			_In_ int chCount,
			_In_ int nTabPositions,
			_In_reads_opt_(nTabPositions) CONST INT* lpnTabStopPositions);
			
		WINUSERAPI DWORD WINAPI GetTabbedTextExtentW(
			_In_ HDC hdc,
			_In_reads_(chCount) LPCWSTR lpString,
			_In_ int chCount,
			_In_ int nTabPositions,
			_In_reads_opt_(nTabPositions) CONST INT* lpnTabStopPositions);
		*/

		// WINUSERAPI BOOL WINAPI UpdateWindow(_In_ HWND hWnd);

		// WINUSERAPI HWND WINAPI SetActiveWindow(_In_ HWND hWnd);

		// WINUSERAPI HWND WINAPI GetForegroundWindow(VOID);

#if WIN32_WINNT_NT4_LATER
		// WINUSERAPI BOOL WINAPI PaintDesktop(_In_ HDC hdc);

		// WINUSERAPI VOID WINAPI SwitchToThisWindow(_In_ HWND hwnd, _In_ BOOL fUnknown);
#endif // WIN32_WINNT_NT4_LATER


		//WINUSERAPI BOOL WINAPI SetForegroundWindow(_In_ HWND hWnd);

#if WIN32_WINNT_WIN2K_LATER
		// WINUSERAPI BOOL WINAPI AllowSetForegroundWindow(_In_ DWORD dwProcessId);

		public const UInt32 ASFW_ANY    = unchecked((UInt32)(-1));

		//WINUSERAPI BOOL WINAPI LockSetForegroundWindow(_In_ UINT uLockCode);

		public const UInt32 LSFW_LOCK       = 1;
		public const UInt32 LSFW_UNLOCK     = 2;

#endif // WIN32_WINNT_WIN2K_LATER

		//WINUSERAPI HWND WINAPI WindowFromDC(_In_ HDC hDC);

		//WINUSERAPI HDC WINAPI GetDC(_In_opt_ HWND hWnd);
		/*
		WINUSERAPI HDC WINAPI GetDCEx(
			_In_opt_ HWND hWnd,
			_In_opt_ HRGN hrgnClip,
			_In_ DWORD flags);
		*/


		public const UInt32 DCX_WINDOW           = 0x00000001;
		public const UInt32 DCX_CACHE            = 0x00000002;
		public const UInt32 DCX_NORESETATTRS     = 0x00000004;
		public const UInt32 DCX_CLIPCHILDREN     = 0x00000008;
		public const UInt32 DCX_CLIPSIBLINGS     = 0x00000010;
		public const UInt32 DCX_PARENTCLIP       = 0x00000020;
		public const UInt32 DCX_EXCLUDERGN       = 0x00000040;
		public const UInt32 DCX_INTERSECTRGN     = 0x00000080;
		public const UInt32 DCX_EXCLUDEUPDATE    = 0x00000100;
		public const UInt32 DCX_INTERSECTUPDATE  = 0x00000200;
		public const UInt32 DCX_LOCKWINDOWUPDATE = 0x00000400;

		public const UInt32 DCX_VALIDATE         = 0x00200000;


        // WINUSERAPI HDC WINAPI GetWindowDC(_In_opt_ HWND hWnd);

        // WINUSERAPI int WINAPI ReleaseDC(_In_opt_ HWND hWnd, _In_ HDC hDC);

        // WINUSERAPI HDC WINAPI BeginPaint(_In_ HWND hWnd, _Out_ LPPAINTSTRUCT lpPaint);
        [DllImport("user32.dll")]        
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        // WINUSERAPI BOOL WINAPI EndPaint(_In_ HWND hWnd, _In_ CONST PAINTSTRUCT* lpPaint); // no LastError
        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

        /*
		WINUSERAPI BOOL WINAPI GetUpdateRect(
		    _In_ HWND hWnd,
			_Out_opt_ LPRECT lpRect,
			_In_ BOOL bErase);
		*/
        /*
		WINUSERAPI int WINAPI GetUpdateRgn(
			_In_ HWND hWnd,
			_In_ HRGN hRgn,
			_In_ BOOL bErase);
		*/
        /*
		WINUSERAPI int WINAPI SetWindowRgn(
			_In_ HWND hWnd,
			_In_opt_ HRGN hRgn,
			_In_ BOOL bRedraw);
		*/


        // WINUSERAPI int WINAPI GetWindowRgn(_In_ HWND hWnd, _In_ HRGN hRgn);

#if WIN32_WINNT_WINXP_LATER

        // WINUSERAPI int WINAPI GetWindowRgnBox(_In_ HWND hWnd, _Out_ LPRECT lprc);

#endif // WIN32_WINNT_WINXP_LATER

        // WINUSERAPI int WINAPI ExcludeUpdateRgn(_In_ HDC hDC, _In_ HWND hWnd);
        /*
		WINUSERAPI BOOL WINAPI InvalidateRect(
			_In_opt_ HWND hWnd,
			_In_opt_ CONST RECT* lpRect,
			_In_ BOOL bErase);
		*/
        // WINUSERAPI BOOL WINAPI ValidateRect(_In_opt_ HWND hWnd, _In_opt_ CONST RECT* lpRect);
        /*
		WINUSERAPI BOOL WINAPI InvalidateRgn(
			_In_ HWND hWnd,
			_In_opt_ HRGN hRgn,
			_In_ BOOL bErase);
		*/
        // WINUSERAPI BOOL WINAPI ValidateRgn(_In_ HWND hWnd, _In_opt_ HRGN hRgn);
        /*
		WINUSERAPI BOOL WINAPI RedrawWindow(
			_In_opt_ HWND hWnd,
			_In_opt_ CONST RECT* lprcUpdate,
			_In_opt_ HRGN hrgnUpdate,
			_In_ UINT flags);
		*/


        public const UInt32 RDW_INVALIDATE          = 0x0001;
        public const UInt32 RDW_INTERNALPAINT       = 0x0002;
        public const UInt32 RDW_ERASE               = 0x0004;

        public const UInt32 RDW_VALIDATE            = 0x0008;
        public const UInt32 RDW_NOINTERNALPAINT     = 0x0010;
        public const UInt32 RDW_NOERASE             = 0x0020;

        public const UInt32 RDW_NOCHILDREN          = 0x0040;
        public const UInt32 RDW_ALLCHILDREN         = 0x0080;

        public const UInt32 RDW_UPDATENOW           = 0x0100;
        public const UInt32 RDW_ERASENOW            = 0x0200;

        public const UInt32 RDW_FRAME               = 0x0400;
        public const UInt32 RDW_NOFRAME             = 0x0800;


		// WINUSERAPI BOOL WINAPI 	LockWindowUpdate(_In_opt_ HWND hWndLock);
		/*
		WINUSERAPI BOOL WINAPI ScrollWindow(
			_In_ HWND hWnd,
			_In_ int XAmount,
			_In_ int YAmount,
			_In_opt_ CONST RECT* lpRect,
			_In_opt_ CONST RECT* lpClipRect);
		*/
		/*
		WINUSERAPI BOOL WINAPI ScrollDC(
			_In_ HDC hDC,
			_In_ int dx,
			_In_ int dy,
			_In_opt_ CONST RECT* lprcScroll,
			_In_opt_ CONST RECT* lprcClip,
			_In_opt_ HRGN hrgnUpdate,
			_Out_opt_ LPRECT lprcUpdate);
		*/
		/*
		WINUSERAPI int WINAPI ScrollWindowEx(
		    _In_ HWND hWnd,
			_In_ int dx,
			_In_ int dy,
			_In_opt_ CONST RECT* prcScroll,
			_In_opt_ CONST RECT* prcClip,
			_In_opt_ HRGN hrgnUpdate,
			_Out_opt_ LPRECT prcUpdate,
			_In_ UINT flags);
		*/


        public const UInt32 SW_SCROLLCHILDREN   = 0x0001;  /* Scroll children within *lprcScroll. */
        public const UInt32 SW_INVALIDATE       = 0x0002;  /* Invalidate after scrolling */
        public const UInt32 SW_ERASE            = 0x0004;  /* If SW_INVALIDATE, don't send WM_ERASEBACKGROUND */
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 SW_SMOOTHSCROLL     = 0x0010;  /* Use smooth scrolling */
#endif // WIN32_WINNT_WIN2K_LATER


        #region SCROLL
        /*
		WINUSERAPI int WINAPI SetScrollPos(
			_In_ HWND hWnd,
			_In_ int nBar,
			_In_ int nPos,
			_In_ BOOL bRedraw);
		*/
        //WINUSERAPI int WINAPI GetScrollPos(_In_ HWND hWnd, _In_ int nBar);
        /*
		WINUSERAPI BOOL WINAPI SetScrollRange(
		    _In_ HWND hWnd,
			_In_ int nBar,
			_In_ int nMinPos,
			_In_ int nMaxPos,
			_In_ BOOL bRedraw);
		*/
        /*
		WINUSERAPI BOOL WINAPI GetScrollRange(
		    _In_ HWND hWnd,
			_In_ int nBar,
			_Out_ LPINT lpMinPos,
			_Out_ LPINT lpMaxPos);
		*/
        /*
		WINUSERAPI BOOL WINAPI ShowScrollBar(
			_In_ HWND hWnd,
			_In_ int wBar,
			_In_ BOOL bShow);
		*/
        /*
		WINUSERAPI BOOL WINAPI EnableScrollBar(
			_In_ HWND hWnd,
			_In_ UINT wSBflags,
			_In_ UINT wArrows);
		*/

        public const UInt32 ESB_ENABLE_BOTH     = 0x0000;
        public const UInt32 ESB_DISABLE_BOTH    = 0x0003;

        public const UInt32 ESB_DISABLE_LEFT    = 0x0001;
        public const UInt32 ESB_DISABLE_RIGHT   = 0x0002;

        public const UInt32 ESB_DISABLE_UP      = 0x0001;
        public const UInt32 ESB_DISABLE_DOWN    = 0x0002;

        public const UInt32 ESB_DISABLE_LTUP    = ESB_DISABLE_LEFT;
        public const UInt32 ESB_DISABLE_RTDN    = ESB_DISABLE_RIGHT;

        #endregion SCROLL


        /*
		WINUSERAPI BOOL WINAPI SetPropA(
			_In_ HWND hWnd,
			_In_ LPCSTR lpString,
			_In_opt_ HANDLE hData);
		
		WINUSERAPI BOOL WINAPI SetPropW(
			_In_ HWND hWnd,
			_In_ LPCWSTR lpString,
			_In_opt_ HANDLE hData);
		*/
        /*
		WINUSERAPI HANDLE WINAPI GetPropA(_In_ HWND hWnd, _In_ LPCSTR lpString);		
		WINUSERAPI HANDLE WINAPI GetPropW(_In_ HWND hWnd, _In_ LPCWSTR lpString);
		*/
        /*
		WINUSERAPI HANDLE WINAPI RemovePropA(_In_ HWND hWnd, _In_ LPCSTR lpString);		
		WINUSERAPI HANDLE WINAPI RemovePropW(_In_ HWND hWnd, _In_ LPCWSTR lpString);
		*/
        /*
		WINUSERAPI int WINAPI EnumPropsExA(
			_In_ HWND hWnd,
			_In_ PROPENUMPROCEXA lpEnumFunc,
			_In_ LPARAM lParam);
			
		WINUSERAPI int WINAPI EnumPropsExW(
			_In_ HWND hWnd,
			_In_ PROPENUMPROCEXW lpEnumFunc,
			_In_ LPARAM lParam);
		*/
        /*
		WINUSERAPI int WINAPI EnumPropsA(_In_ HWND hWnd, _In_ PROPENUMPROCA lpEnumFunc);		
		WINUSERAPI int WINAPI EnumPropsW(_In_ HWND hWnd, _In_ PROPENUMPROCW lpEnumFunc);
		*/
        /*
		WINUSERAPI BOOL WINAPI SetWindowTextA(_In_ HWND hWnd, _In_opt_ LPCSTR lpString);		
		WINUSERAPI BOOL WINAPI SetWindowTextW(_In_ HWND hWnd, _In_opt_ LPCWSTR lpString);
		*/
        /*
		_Ret_range_(0, nMaxCount) WINUSERAPI int WINAPI GetWindowTextA(
			_In_ HWND hWnd,
			_Out_writes_(nMaxCount) LPSTR lpString,
			_In_ int nMaxCount);
			
		_Ret_range_(0, nMaxCount) WINUSERAPI int WINAPI GetWindowTextW(
			_In_ HWND hWnd,
			_Out_writes_(nMaxCount) LPWSTR lpString,
			_In_ int nMaxCount);
		*/
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 GetWindowTextA(IntPtr hwnd, IntPtr lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Int32 GetWindowTextW(IntPtr hwnd, IntPtr lpString, int nMaxCount);


        /*
		WINUSERAPI int WINAPI GetWindowTextLengthA(_In_ HWND hWnd);		
		WINUSERAPI int WINAPI GetWindowTextLengthW(_In_ HWND hWnd);
		*/
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 GetWindowTextLengthA(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int32 GetWindowTextLengthW(IntPtr hwnd);


        // WINUSERAPI BOOL WINAPI GetClientRect(_In_ HWND hWnd, _Out_ LPRECT lpRect);

        // WINUSERAPI BOOL WINAPI GetWindowRect(_In_ HWND hWnd, _Out_ LPRECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, [Out] out WinDef.RECT lpRect);
        /*
		WINUSERAPI BOOL WINAPI AdjustWindowRect(
			_Inout_ LPRECT lpRect,
			_In_ DWORD dwStyle,
			_In_ BOOL bMenu);
		*/
        /*
		WINUSERAPI BOOL WINAPI AdjustWindowRectEx(
			_Inout_ LPRECT lpRect,
			_In_ DWORD dwStyle,
			_In_ BOOL bMenu,
			_In_ DWORD dwExStyle);
		*/

#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0605)
        /*
		WINUSERAPI BOOL WINAPI AdjustWindowRectExForDpi(
			_Inout_ LPRECT lpRect,
			_In_ DWORD dwStyle,
			_In_ BOOL bMenu,
			_In_ DWORD dwExStyle,
			_In_ UINT dpi);
		*/
#endif // WIN32_WINNT_WIN10_LATER WINVER >= 0x0605



#if WIN32_WINNT_NT4_LATER

        public const Int32 HELPINFO_WINDOW    = 0x0001;
		public const Int32 HELPINFO_MENUITEM  = 0x0002;


		public struct HELPINFO      /* Structure pointed to by lParam of WM_HELP */
		{
			public UInt32 cbSize;             /* Size in bytes of this struct  */
			public Int32 iContextType;       /* Either HELPINFO_WINDOW or HELPINFO_MENUITEM */
			public Int32 iCtrlId;            /* Control Id or a Menu item Id. */
			public IntPtr hItemHandle;        /* hWnd of control or hMenu.     */
			public UIntPtr dwContextId;      /* Context Id associated with this item */
			public WinDef.POINT MousePos;           /* Mouse Position in screen co-ordinates */
		}

        // WINUSERAPI BOOL WINAPI SetWindowContextHelpId(_In_ HWND, _In_ DWORD);

        // WINUSERAPI DWORD WINAPI GetWindowContextHelpId(_In_ HWND);

        //WINUSERAPI BOOL WINAPI SetMenuContextHelpId(_In_ HMENU, _In_ DWORD);

        // WINUSERAPI DWORD WINAPI GetMenuContextHelpId(_In_ HMENU);
               
#endif // WIN32_WINNT_NT4_LATER


        #region MB

        public const UInt32 MB_OK                       = 0x00000000;
		public const UInt32 MB_OKCANCEL                 = 0x00000001;
		public const UInt32 MB_ABORTRETRYIGNORE         = 0x00000002;
		public const UInt32 MB_YESNOCANCEL              = 0x00000003;
		public const UInt32 MB_YESNO                    = 0x00000004;
		public const UInt32 MB_RETRYCANCEL              = 0x00000005;
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 MB_CANCELTRYCONTINUE        = 0x00000006;
#endif // WIN32_WINNT_WIN2K_LATER

		public const UInt32 MB_ICONHAND                 = 0x00000010;
		public const UInt32 MB_ICONQUESTION             = 0x00000020;
		public const UInt32 MB_ICONEXCLAMATION          = 0x00000030;
		public const UInt32 MB_ICONASTERISK             = 0x00000040;

#if WIN32_WINNT_NT4_LATER
		public const UInt32 MB_USERICON                 = 0x00000080;
		public const UInt32 MB_ICONWARNING              = MB_ICONEXCLAMATION;
		public const UInt32 MB_ICONERROR                = MB_ICONHAND;
#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 MB_ICONINFORMATION          = MB_ICONASTERISK;
		public const UInt32 MB_ICONSTOP                 = MB_ICONHAND;

		public const UInt32 MB_DEFBUTTON1               = 0x00000000;
		public const UInt32 MB_DEFBUTTON2               = 0x00000100;
		public const UInt32 MB_DEFBUTTON3               = 0x00000200;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 MB_DEFBUTTON4               = 0x00000300;
#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 MB_APPLMODAL                = 0x00000000;
		public const UInt32 MB_SYSTEMMODAL              = 0x00001000;
		public const UInt32 MB_TASKMODAL                = 0x00002000;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 MB_HELP                     = 0x00004000; // Help Button
#endif // WIN32_WINNT_NT4_LATER

		public const UInt32 MB_NOFOCUS                  = 0x00008000;
		public const UInt32 MB_SETFOREGROUND            = 0x00010000;
		public const UInt32 MB_DEFAULT_DESKTOP_ONLY     = 0x00020000;

#if WIN32_WINNT_NT4_LATER
		public const UInt32 MB_TOPMOST                  = 0x00040000;
		public const UInt32 MB_RIGHT                    = 0x00080000;
		public const UInt32 MB_RTLREADING               = 0x00100000;
#endif // WIN32_WINNT_NT4_LATER

//#if _WIN32_WINNT
#if WIN32_WINNT_NT4_LATER
		public const UInt32 MB_SERVICE_NOTIFICATION          = 0x00200000;
#else
		public const UInt32 MB_SERVICE_NOTIFICATION          = 0x00040000;
#endif
		public const UInt32 MB_SERVICE_NOTIFICATION_NT3X     = 0x00040000;
//#endif

		public const UInt32 MB_TYPEMASK                 = 0x0000000F;
		public const UInt32 MB_ICONMASK                 = 0x000000F0;
		public const UInt32 MB_DEFMASK                  = 0x00000F00;
		public const UInt32 MB_MODEMASK                 = 0x00003000;
		public const UInt32 MB_MISCMASK                 = 0x0000C000;


		/*
		WINUSERAPI int WINAPI MessageBoxA(
			_In_opt_ HWND hWnd,
			_In_opt_ LPCSTR lpText,
			_In_opt_ LPCSTR lpCaption,
			_In_ UINT uType);
			
		WINUSERAPI int WINAPI MessageBoxW(
			_In_opt_ HWND hWnd,
			_In_opt_ LPCWSTR lpText,
			_In_opt_ LPCWSTR lpCaption,
			_In_ UINT uType);
		*/

		/*
		WINUSERAPI int WINAPI MessageBoxExA(
			_In_opt_ HWND hWnd,
			_In_opt_ LPCSTR lpText,
			_In_opt_ LPCSTR lpCaption,
			_In_ UINT uType,
			_In_ WORD wLanguageId);
			
		WINUSERAPI int WINAPI MessageBoxExW(
			_In_opt_ HWND hWnd,
			_In_opt_ LPCWSTR lpText,
			_In_opt_ LPCWSTR lpCaption,
			_In_ UINT uType,
			_In_ WORD wLanguageId);
		*/
#if WIN32_WINNT_NT4_LATER

		//typedef VOID(CALLBACK* MSGBOXCALLBACK)(LPHELPINFO lpHelpInfo);
		public delegate void MSGBOXCALLBACK(ref HELPINFO lpHelpInfo);

		public struct MSGBOXPARAMSA
		{
			public UInt32 cbSize;
			public IntPtr hwndOwner;
			public IntPtr hInstance;
			public string lpszText;
			public string lpszCaption;
			public UInt32 dwStyle;
			public string lpszIcon;
			public UIntPtr dwContextHelpId;
			public MSGBOXCALLBACK lpfnMsgBoxCallback;
			public UInt32 dwLanguageId;
		}

		public struct MSGBOXPARAMSW
		{
			public UInt32 cbSize;
			public IntPtr hwndOwner;
			public IntPtr hInstance;
			public string lpszText;
			public string lpszCaption;
			public UInt32 dwStyle;
			public string lpszIcon;
			public UIntPtr dwContextHelpId;
			public MSGBOXCALLBACK lpfnMsgBoxCallback;
			public UInt32 dwLanguageId;
		}
        /*
		WINUSERAPI int WINAPI MessageBoxIndirectA(_In_ CONST MSGBOXPARAMSA* lpmbp);
		WINUSERAPI int WINAPI MessageBoxIndirectW(_In_ CONST MSGBOXPARAMSW* lpmbp);
		*/
#endif // WIN32_WINNT_NT4_LATER

        // WINUSERAPI BOOL WINAPI MessageBeep(_In_ UINT uType);

        #endregion MG



        // WINUSERAPI int WINAPI ShowCursor(_In_ BOOL bShow);

        // WINUSERAPI BOOL WINAPI SetCursorPos(_In_ int X, _In_ int Y);

#if WIN32_WINNT_VISTA_LATER
        // WINUSERAPI BOOL WINAPI SetPhysicalCursorPos(_In_ int X, _In_ int Y);
#endif // WIN32_WINNT_VISTA_LATER

        // WINUSERAPI HCURSOR WINAPI SetCursor(_In_opt_ HCURSOR hCursor);

        // WINUSERAPI BOOL WINAPI GetCursorPos(_Out_ LPPOINT lpPoint);

#if WIN32_WINNT_VISTA_LATER
        // WINUSERAPI BOOL WINAPI GetPhysicalCursorPos(_Out_ LPPOINT lpPoint);
#endif // WIN32_WINNT_VISTA_LATER

        // WINUSERAPI BOOL WINAPI GetClipCursor(_Out_ LPRECT lpRect);

        // WINUSERAPI HCURSOR WINAPI GetCursor(VOID);
        /*
		WINUSERAPI BOOL WINAPI CreateCaret(
			_In_ HWND hWnd,
			_In_opt_ HBITMAP hBitmap,
			_In_ int nWidth,
			_In_ int nHeight);
		*/

        // WINUSERAPI UINT WINAPI GetCaretBlinkTime(VOID);

        // WINUSERAPI BOOL WINAPI SetCaretBlinkTime(_In_ UINT uMSeconds);

        // WINUSERAPI BOOL WINAPI DestroyCaret(VOID);

        // WINUSERAPI BOOL WINAPI HideCaret(_In_opt_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI ShowCaret(_In_opt_ HWND hWnd);

        // WINUSERAPI BOOL WINAPI SetCaretPos(_In_ int X, _In_ int Y);

        // WINUSERAPI BOOL WINAPI GetCaretPos(_Out_ LPPOINT lpPoint);

        // WINUSERAPI BOOL WINAPI ClientToScreen(_In_ HWND hWnd, _Inout_ LPPOINT lpPoint);

        // WINUSERAPI BOOL WINAPI ScreenToClient(_In_ HWND hWnd, _Inout_ LPPOINT lpPoint);

#if WIN32_WINNT_VISTA_LATER
        // WINUSERAPI BOOL WINAPI LogicalToPhysicalPoint(_In_ HWND hWnd, _Inout_ LPPOINT lpPoint);

        // WINUSERAPI BOOL WINAPI PhysicalToLogicalPoint(_In_ HWND hWnd, _Inout_ LPPOINT lpPoint);

#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WINBLUE_LATER
        // WINUSERAPI BOOL WINAPI LogicalToPhysicalPointForPerMonitorDPI(_In_opt_ HWND hWnd, _Inout_ LPPOINT lpPoint);

        // WINUSERAPI BOOL WINAPI PhysicalToLogicalPointForPerMonitorDPI(_In_opt_ HWND hWnd, _Inout_ LPPOINT lpPoint);
#endif // WIN32_WINNT_WINBLUE_LATER
        /*
		WINUSERAPI int WINAPI MapWindowPoints(
			_In_opt_ HWND hWndFrom,
			_In_opt_ HWND hWndTo,
			_Inout_updates_(cPoints) LPPOINT lpPoints,
			_In_ UINT cPoints);
		*/

        //WINUSERAPI HWND WINAPI WindowFromPoint(_In_ POINT Point);

#if WIN32_WINNT_VISTA_LATER
        // WINUSERAPI HWND WINAPI WindowFromPhysicalPoint(_In_ POINT Point);
#endif // WIN32_WINNT_VISTA_LATER

        // WINUSERAPI HWND WINAPI ChildWindowFromPoint(_In_ HWND hWndParent, _In_ POINT Point);
       
        // WINUSERAPI BOOL WINAPI ClipCursor(_In_opt_ CONST RECT* lpRect);


#if WIN32_WINNT_NT4_LATER
        public const UInt32 CWP_ALL             = 0x0000;
		public const UInt32 CWP_SKIPINVISIBLE   = 0x0001;
		public const UInt32 CWP_SKIPDISABLED    = 0x0002;
		public const UInt32 CWP_SKIPTRANSPARENT = 0x0004;

        /*
		WINUSERAPI HWND WINAPI ChildWindowFromPointEx(
			_In_ HWND hwnd,
			_In_ POINT pt,
			_In_ UINT flags);
		*/

#endif // WIN32_WINNT_NT4_LATER


        #region COLOR

        public const UInt32 CTLCOLOR_MSGBOX         = 0;
        public const UInt32 CTLCOLOR_EDIT           = 1;
        public const UInt32 CTLCOLOR_LISTBOX        = 2;
        public const UInt32 CTLCOLOR_BTN            = 3;
        public const UInt32 CTLCOLOR_DLG            = 4;
        public const UInt32 CTLCOLOR_SCROLLBAR      = 5;
        public const UInt32 CTLCOLOR_STATIC         = 6;
        public const UInt32 CTLCOLOR_MAX            = 7;

        public const Int32 COLOR_SCROLLBAR         = 0;
        public const Int32 COLOR_BACKGROUND        = 1;
        public const Int32 COLOR_ACTIVECAPTION     = 2;
        public const Int32 COLOR_INACTIVECAPTION   = 3;
        public const Int32 COLOR_MENU              = 4;
        public const Int32 COLOR_WINDOW            = 5;
        public const Int32 COLOR_WINDOWFRAME       = 6;
        public const Int32 COLOR_MENUTEXT          = 7;
        public const Int32 COLOR_WINDOWTEXT        = 8;
        public const Int32 COLOR_CAPTIONTEXT       = 9;
        public const Int32 COLOR_ACTIVEBORDER      = 10;
        public const Int32 COLOR_INACTIVEBORDER    = 11;
        public const Int32 COLOR_APPWORKSPACE      = 12;
        public const Int32 COLOR_HIGHLIGHT         = 13;
        public const Int32 COLOR_HIGHLIGHTTEXT     = 14;
        public const Int32 COLOR_BTNFACE           = 15;
        public const Int32 COLOR_BTNSHADOW         = 16;
        public const Int32 COLOR_GRAYTEXT          = 17;
        public const Int32 COLOR_BTNTEXT           =  18;
        public const Int32 COLOR_INACTIVECAPTIONTEXT = 19;
        public const Int32 COLOR_BTNHIGHLIGHT      = 20;

#if WIN32_WINNT_NT4_LATER
        public const Int32 COLOR_3DDKSHADOW        = 21;
        public const Int32 COLOR_3DLIGHT           = 22;
        public const Int32 COLOR_INFOTEXT          = 23;
        public const Int32 COLOR_INFOBK            = 24;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
        public const Int32 COLOR_HOTLIGHT          = 26;
        public const Int32 COLOR_GRADIENTACTIVECAPTION = 27;
        public const Int32 COLOR_GRADIENTINACTIVECAPTION = 28;
#if WIN32_WINNT_WINXP_LATER
        public const Int32 COLOR_MENUHILIGHT       = 29;
        public const Int32 COLOR_MENUBAR           = 30;
#endif // WIN32_WINNT_WINXP_LATER
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_NT4_LATER
        public const Int32 COLOR_DESKTOP           = COLOR_BACKGROUND;
        public const Int32 COLOR_3DFACE            = COLOR_BTNFACE;
        public const Int32 COLOR_3DSHADOW          = COLOR_BTNSHADOW;
        public const Int32 COLOR_3DHIGHLIGHT       = COLOR_BTNHIGHLIGHT;
        public const Int32 COLOR_3DHILIGHT         = COLOR_BTNHIGHLIGHT;
        public const Int32 COLOR_BTNHILIGHT        = COLOR_BTNHIGHLIGHT;
#endif // WIN32_WINNT_NT4_LATER


        // WINUSERAPI DWORD WINAPI GetSysColor(_In_ int nIndex);

#if WIN32_WINNT_NT4_LATER

        // WINUSERAPI HBRUSH WINAPI GetSysColorBrush(_In_ int nIndex);

#endif // WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI BOOL WINAPI SetSysColors(
			_In_ int cElements,
			_In_reads_(cElements) CONST INT* lpaElements,
			_In_reads_(cElements) CONST COLORREF* lpaRgbValues);
		*/

        #endregion COLOR


        // WINUSERAPI BOOL WINAPI DrawFocusRect(_In_ HDC hDC, _In_ CONST RECT* lprc);
        /*
		WINUSERAPI int WINAPI FillRect(
			_In_ HDC hDC,
			_In_ CONST RECT* lprc,
			_In_ HBRUSH hbr);
		*/
        /*
		WINUSERAPI int WINAPI FrameRect(
			_In_ HDC hDC,
			_In_ CONST RECT* lprc,
			_In_ HBRUSH hbr);
		*/

        // WINUSERAPI BOOL WINAPI InvertRect(_In_ HDC hDC, _In_ CONST RECT* lprc);
        /*
		WINUSERAPI BOOL WINAPI SetRect(
			_Out_ LPRECT lprc,
			_In_ int xLeft,
			_In_ int yTop,
			_In_ int xRight,
			_In_ int yBottom);
		*/
        [DllImport("user32.dll")]
        public static extern bool SetRect(out WinDef.RECT lprc, Int32 xLeft, Int32 yTop, Int32 xRight, Int32 yBottom);

        // WINUSERAPI BOOL WINAPI SetRectEmpty(_Out_ LPRECT lprc);
        [DllImport("user32.dll")]
        public static extern bool SetRectEmpty(out WinDef.RECT lprc);

        // WINUSERAPI BOOL WINAPI CopyRect(_Out_ LPRECT lprcDst, _In_ CONST RECT* lprcSrc);
        [DllImport("user32.dll")]
        public static extern bool CopyRect(out WinDef.RECT lprcDst, [In] ref WinDef.RECT lprcSrc);

        /*
		WINUSERAPI BOOL WINAPI InflateRect(
			_Inout_ LPRECT lprc,
			_In_ int dx,
			_In_ int dy);
		*/
        [DllImport("User32.dll")]
        public static extern bool InflateRect(ref WinDef.RECT lprc, Int32 dx, Int32 dy);

        /*
		WINUSERAPI BOOL WINAPI IntersectRect(
			_Out_ LPRECT lprcDst,
			_In_ CONST RECT* lprcSrc1,
			_In_ CONST RECT* lprcSrc2);
		*/
        [DllImport("user32.dll")]
        public static extern bool IntersectRect(out WinDef.RECT lprcDst, [In] ref WinDef.RECT lprcSrc1, [In] ref WinDef.RECT lprcSrc2);

        /*
		WINUSERAPI BOOL WINAPI UnionRect(
			_Out_ LPRECT lprcDst,
			_In_ CONST RECT* lprcSrc1,
			_In_ CONST RECT* lprcSrc2);
		*/
        [DllImport("user32.dll")]
        public static extern bool UnionRect(out WinDef.RECT lprcDst, [In] ref WinDef.RECT lprcSrc1, [In] ref WinDef.RECT lprcSrc2);

        /*
		WINUSERAPI BOOL WINAPI SubtractRect(
			_Out_ LPRECT lprcDst,
			_In_ CONST RECT* lprcSrc1,
			_In_ CONST RECT* lprcSrc2);
		*/
        [DllImport("user32.dll")]
        public static extern bool SubtractRect(out WinDef.RECT lprcDst, [In] ref WinDef.RECT lprcSrc1, [In] ref WinDef.RECT lprcSrc2);

        /*
		WINUSERAPI BOOL WINAPI OffsetRect(
			_Inout_ LPRECT lprc,
			_In_ int dx,
			_In_ int dy);
		*/
        [DllImport("user32.dll")]
        public static extern bool OffsetRect([In] ref WinDef.RECT lprc, Int32 dx, Int32 dy);

        // WINUSERAPI BOOL WINAPI IsRectEmpty(_In_ CONST RECT* lprc);
        [DllImport("user32.dll")]
        public static extern bool IsRectEmpty([In] ref WinDef.RECT lprc);

        // WINUSERAPI BOOL WINAPI EqualRect(_In_ CONST RECT* lprc1, _In_ CONST RECT* lprc2);
        [DllImport("user32.dll")]
        public static extern bool EqualRect([In] ref WinDef.RECT lprc1, [In] ref WinDef.RECT lprc2);

        // WINUSERAPI BOOL WINAPI PtInRect(_In_ CONST RECT* lprc, _In_ POINT pt); // no LastError
        [DllImport("user32.dll")]
        public static extern bool PtInRect(ref WinDef.RECT lprc, WinDef.POINT pt);


        #region WINOFFSETS

        // WINUSERAPI WORD WINAPI GetWindowWord(_In_ HWND hWnd, _In_ int nIndex);
        /*
		WINUSERAPI WORD WINAPI SetWindowWord(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ WORD wNewWord);
		*/
        
		//WINUSERAPI LONG WINAPI GetWindowLongA(_In_ HWND hWnd, _In_ int nIndex);		
		//WINUSERAPI LONG WINAPI GetWindowLongW(_In_ HWND hWnd, _In_ int nIndex);	
        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        static extern Int32 InternalGetWindowLongA(IntPtr hwnd, Int32 nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongW", SetLastError = true)]
        static extern Int32 InternalGetWindowLongW(IntPtr hwnd, Int32 nIndex);

        /*
		WINUSERAPI LONG WINAPI SetWindowLongA(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG dwNewLong);
			
		WINUSERAPI LONG WINAPI SetWindowLongW(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG dwNewLong);
		*/
        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        static extern Int32 InternalSetWindowLongA(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongW", SetLastError = true)]
        static extern Int32 InternalSetWindowLongW(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong);

        //#if _WIN64
        // WINUSERAPI LONG_PTR WINAPI GetWindowLongPtrA(_In_ HWND hWnd, _In_ int nIndex);
        // WINUSERAPI LONG_PTR WINAPI GetWindowLongPtrW(_In_ HWND hWnd, _In_ int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrA", SetLastError = true)]
        static extern IntPtr InternalGetWindowLongPtrA(IntPtr hwnd, Int32 nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        static extern IntPtr InternalGetWindowLongPtrW(IntPtr hwnd, Int32 nIndex);
        /*
		WINUSERAPI LONG_PTR WINAPI SetWindowLongPtrA(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG_PTR dwNewLong);

		WINUSERAPI LONG_PTR WINAPI SetWindowLongPtrW(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG_PTR dwNewLong);
		*/
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrA", SetLastError = true)]
        static extern IntPtr InternalSetWindowLongPtrA(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        static extern IntPtr InternalSetWindowLongPtrW(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong);

        //#else  // _WIN64
        // #define GetWindowLongPtrA   GetWindowLongA
        // #define GetWindowLongPtrW   GetWindowLongW
        // #define SetWindowLongPtrA   SetWindowLongA
        // #define SetWindowLongPtrW   SetWindowLongW
        //#endif // _WIN64


        public static IntPtr GetWindowLongPtrA(IntPtr hwnd, Int32 nIndex)
        {
            if (Environment.Is64BitProcess)
                return InternalGetWindowLongPtrA(hwnd, nIndex);
            else
                return (IntPtr)InternalGetWindowLongA(hwnd, nIndex);
        }

        public static IntPtr GetWindowLongPtrW(IntPtr hwnd, Int32 nIndex)
        {
            if (Environment.Is64BitProcess)
                return InternalGetWindowLongPtrW(hwnd, nIndex);
            else
                return (IntPtr)InternalGetWindowLongW(hwnd, nIndex);
        }

        public static IntPtr SetWindowLongPtrA(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong)
        {
            if (Environment.Is64BitProcess)
                return InternalSetWindowLongPtrA(hwnd, nIndex, dwNewLong);
            else
                return (IntPtr)InternalSetWindowLongA(hwnd, nIndex, dwNewLong);
        }

        public static IntPtr SetWindowLongPtrW(IntPtr hwnd, Int32 nIndex, IntPtr dwNewLong)
        {
            if (Environment.Is64BitProcess)
                return InternalSetWindowLongPtrW(hwnd, nIndex, dwNewLong);
            else
                return (IntPtr)InternalSetWindowLongW(hwnd, nIndex, dwNewLong);
        }



        // WINUSERAPI WORD WINAPI GetClassWord(_In_ HWND hWnd, _In_ int nIndex);
        /*
		WINUSERAPI WORD WINAPI SetClassWord(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ WORD wNewWord);
		*/
        /*
		WINUSERAPI DWORD WINAPI GetClassLongA(_In_ HWND hWnd, _In_ int nIndex);
		WINUSERAPI DWORD WINAPI GetClassLongW(_In_ HWND hWnd, _In_ int nIndex);
		*/
        /*
		WINUSERAPI DWORD WINAPI SetClassLongA(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG dwNewLong);
			
		WINUSERAPI DWORD WINAPI SetClassLongW(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG dwNewLong);
		*/

#if _WIN64
		/*
		WINUSERAPI ULONG_PTR WINAPI GetClassLongPtrA(_In_ HWND hWnd, _In_ int nIndex);
		WINUSERAPI ULONG_PTR WINAPI GetClassLongPtrW(_In_ HWND hWnd, _In_ int nIndex);
		*/
		/*
		WINUSERAPI ULONG_PTR WINAPI SetClassLongPtrA(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG_PTR dwNewLong);

		WINUSERAPI ULONG_PTR WINAPI SetClassLongPtrW(
			_In_ HWND hWnd,
			_In_ int nIndex,
			_In_ LONG_PTR dwNewLong);
		*/
#else  // _WIN64

        //#define GetClassLongPtrA    GetClassLongA
        //#define GetClassLongPtrW    GetClassLongW

        //#define SetClassLongPtrA    SetClassLongA
        //#define SetClassLongPtrW    SetClassLongW

#endif // _WIN64



        #endregion WINOFFSETS


#if WIN32_WINNT_WIN2K_LATER

        // WINUSERAPI BOOL WINAPI GetProcessDefaultLayout(_Out_ DWORD *pdwDefaultLayout);

        // WINUSERAPI BOOL WINAPI SetProcessDefaultLayout(_In_ DWORD dwDefaultLayout);

#endif // WIN32_WINNT_WIN2K_LATER

        // WINUSERAPI HWND WINAPI GetDesktopWindow(VOID);

        // WINUSERAPI HWND WINAPI GetParent(_In_ HWND hWnd);

        // WINUSERAPI HWND WINAPI SetParent(_In_ HWND hWndChild, _In_opt_ HWND hWndNewParent);
        /*
		WINUSERAPI BOOL WINAPI EnumChildWindows(
			_In_opt_ HWND hWndParent,
			_In_ WNDENUMPROC lpEnumFunc,
			_In_ LPARAM lParam);
		*/
        /*
		WINUSERAPI HWND WINAPI FindWindowA(_In_opt_ LPCSTR lpClassName, _In_opt_ LPCSTR lpWindowName);		
		WINUSERAPI HWND WINAPI FindWindowW(_In_opt_ LPCWSTR lpClassName, _In_opt_ LPCWSTR lpWindowName);
		*/

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI HWND WINAPI FindWindowExA(
			_In_opt_ HWND hWndParent,
			_In_opt_ HWND hWndChildAfter,
			_In_opt_ LPCSTR lpszClass,
			_In_opt_ LPCSTR lpszWindow);
			
		WINUSERAPI HWND WINAPI FindWindowExW(
			_In_opt_ HWND hWndParent,
			_In_opt_ HWND hWndChildAfter,
			_In_opt_ LPCWSTR lpszClass,
			_In_opt_ LPCWSTR lpszWindow);
		*/
        // WINUSERAPI HWND WINAPI GetShellWindow(VOID);

#endif // WIN32_WINNT_NT4_LATER

        // WINUSERAPI BOOL WINAPI RegisterShellHookWindow(_In_ HWND hwnd);

        // WINUSERAPI BOOL WINAPI DeregisterShellHookWindow(_In_ HWND hwnd);

        // WINUSERAPI BOOL WINAPI EnumWindows(_In_ WNDENUMPROC lpEnumFunc, _In_ LPARAM lParam);
        /*
		WINUSERAPI BOOL WINAPI EnumThreadWindows(
			_In_ DWORD dwThreadId,
			_In_ WNDENUMPROC lpfn,
			_In_ LPARAM lParam);
		*/

        // #define EnumTaskWindows(hTask, lpfn, lParam) EnumThreadWindows(HandleToUlong(hTask), lpfn, lParam)

        /*
		WINUSERAPI int WINAPI GetClassNameA(
			_In_ HWND hWnd,
			_Out_writes_to_(nMaxCount, return) LPSTR lpClassName,
			_In_ int nMaxCount);

		WINUSERAPI int WINAPI GetClassNameW(
			_In_ HWND hWnd,
			_Out_writes_to_(nMaxCount, return) LPWSTR lpClassName,
			_In_ int nMaxCount);
        */
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetClassNameA(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetClassNameW(IntPtr hWnd, IntPtr lpClassName, int nMaxCount);


        // WINUSERAPI HWND WINAPI GetTopWindow(_In_opt_ HWND hWnd);

        // #define GetNextWindow(hWnd, wCmd) GetWindow(hWnd, wCmd)
        // #define GetSysModalWindow() (NULL)
        // #define SetSysModalWindow(hWnd) (NULL)

        // WINUSERAPI DWORD WINAPI GetWindowThreadProcessId(_In_ HWND hWnd, _Out_opt_ LPDWORD lpdwProcessId);

#if WIN32_WINNT_WINXP_LATER

        // WINUSERAPI BOOL WINAPI IsGUIThread(_In_ BOOL bConvert);

#endif // WIN32_WINNT_WINXP_LATER


        // #define GetWindowTask(hWnd) \ ((HANDLE)(DWORD_PTR) GetWindowThreadProcessId(hWnd, NULL))

        // WINUSERAPI HWND WINAPI GetLastActivePopup(_In_ HWND hWnd);

        public const UInt32 GW_HWNDFIRST        = 0;
		public const UInt32 GW_HWNDLAST         = 1;
		public const UInt32 GW_HWNDNEXT         = 2;
		public const UInt32 GW_HWNDPREV         = 3;
		public const UInt32 GW_OWNER            = 4;
		public const UInt32 GW_CHILD            = 5;

#if !WIN32_WINNT_WIN2K_LATER
		public const UInt32 GW_MAX              = 5;
#else
		public const UInt32 GW_ENABLEDPOPUP     = 6;
		public const UInt32 GW_MAX              = 6;
#endif

        // WINUSERAPI HWND WINAPI GetWindow(_In_ HWND hWnd, _In_ UINT uCmd);


        #region WH
        /*
		WINUSERAPI HHOOK WINAPI SetWindowsHookA(_In_ int nFilterType, _In_ HOOKPROC pfnFilterProc);
		WINUSERAPI HHOOK WINAPI SetWindowsHookW(_In_ int nFilterType, _In_ HOOKPROC pfnFilterProc);
		*/


        // WINUSERAPI BOOL WINAPI UnhookWindowsHook(_In_ int nCode, _In_ HOOKPROC pfnFilterProc);

        /*
		WINUSERAPI HHOOK WINAPI SetWindowsHookExA(
			_In_ int idHook,
			_In_ HOOKPROC lpfn,
			_In_opt_ HINSTANCE hmod,
			_In_ DWORD dwThreadId);

		WINUSERAPI HHOOK WINAPI SetWindowsHookExW(
			_In_ int idHook,
			_In_ HOOKPROC lpfn,
			_In_opt_ HINSTANCE hmod,
			_In_ DWORD dwThreadId);
	    */
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookExA(
            Int32 idHook, HookProc lpfn, IntPtr hMod, UInt32 dwThreadId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookExW(
            Int32 idHook, HookProc lpfn, IntPtr hMod, UInt32 dwThreadId);

        // WINUSERAPI BOOL WINAPI UnhookWindowsHookEx(_In_ HHOOK hhk);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        /*
		WINUSERAPI LRESULT WINAPI CallNextHookEx( // no LastError
			_In_opt_ HHOOK hhk,
			_In_ int nCode,
			_In_ WPARAM wParam,
			_In_ LPARAM lParam);
		*/
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(
           IntPtr hhk, Int32 nCode, UIntPtr wParam, IntPtr lParam);

        //#define DefHookProc(nCode, wParam, lParam, phhk)\
        // CallNextHookEx(*phhk, nCode, wParam, lParam)

        #endregion WH


        #region MENUS

        public const UInt32 MF_INSERT           = 0x00000000;
        public const UInt32 MF_CHANGE           = 0x00000080;
        public const UInt32 MF_APPEND           = 0x00000100;
        public const UInt32 MF_DELETE           = 0x00000200;
        public const UInt32 MF_REMOVE           = 0x00001000;

        public const UInt32 MF_BYCOMMAND        = 0x00000000;
        public const UInt32 MF_BYPOSITION       = 0x00000400;

        public const UInt32 MF_SEPARATOR        = 0x00000800;

        public const UInt32 MF_ENABLED          = 0x00000000;
        public const UInt32 MF_GRAYED           = 0x00000001;
        public const UInt32 MF_DISABLED         = 0x00000002;

        public const UInt32 MF_UNCHECKED        = 0x00000000;
        public const UInt32 MF_CHECKED          = 0x00000008;
        public const UInt32 MF_USECHECKBITMAPS  = 0x00000200;

        public const UInt32 MF_STRING           = 0x00000000;
        public const UInt32 MF_BITMAP           = 0x00000004;
        public const UInt32 MF_OWNERDRAW        = 0x00000100;

        public const UInt32 MF_POPUP            = 0x00000010;
        public const UInt32 MF_MENUBARBREAK     = 0x00000020;
        public const UInt32 MF_MENUBREAK        = 0x00000040;

        public const UInt32 MF_UNHILITE         = 0x00000000;
        public const UInt32 MF_HILITE           = 0x00000080;

#if WIN32_WINNT_NT4_LATER
        public const UInt32 MF_DEFAULT          = 0x00001000;
#endif // WIN32_WINNT_NT4_LATER
        public const UInt32 MF_SYSMENU          = 0x00002000;
        public const UInt32 MF_HELP             = 0x00004000;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 MF_RIGHTJUSTIFY     = 0x00004000;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 MF_MOUSESELECT      = 0x00008000;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 MF_END              = 0x00000080;  /* Obsolete -- only used by old RES files */
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 MFT_STRING          = MF_STRING;
        public const UInt32 MFT_BITMAP          = MF_BITMAP;
        public const UInt32 MFT_MENUBARBREAK    = MF_MENUBARBREAK;
        public const UInt32 MFT_MENUBREAK       = MF_MENUBREAK;
        public const UInt32 MFT_OWNERDRAW       = MF_OWNERDRAW;
        public const UInt32 MFT_RADIOCHECK      = 0x00000200;
        public const UInt32 MFT_SEPARATOR       = MF_SEPARATOR;
        public const UInt32 MFT_RIGHTORDER      = 0x00002000;
        public const UInt32 MFT_RIGHTJUSTIFY    = MF_RIGHTJUSTIFY;

        public const UInt32 MFS_GRAYED          = 0x00000003;
        public const UInt32 MFS_DISABLED        = MFS_GRAYED;
        public const UInt32 MFS_CHECKED         = MF_CHECKED;
        public const UInt32 MFS_HILITE          = MF_HILITE;
        public const UInt32 MFS_ENABLED         = MF_ENABLED;
        public const UInt32 MFS_UNCHECKED       = MF_UNCHECKED;
        public const UInt32 MFS_UNHILITE        = MF_UNHILITE;
        public const UInt32 MFS_DEFAULT         = MF_DEFAULT;
#endif // WIN32_WINNT_NT4_LATER


#if WIN32_WINNT_NT4_LATER
		/*
		WINUSERAPI BOOL WINAPI CheckMenuRadioItem(
			_In_ HMENU hmenu,
			_In_ UINT first,
			_In_ UINT last,
			_In_ UINT check,
			_In_ UINT flags);
		*/
#endif // WIN32_WINNT_NT4_LATER

		public struct MENUITEMTEMPLATEHEADER
		{
			public UInt16 versionNumber;
			public UInt16 offset;
		} 

		public struct MENUITEMTEMPLATE // version 0
		{        
			public UInt16 mtOption;
			public UInt16 mtID;
            public Char[] mtString; // WCHAR mtString[1];
		}

        //public const UInt32 MF_END             = 0x00000080; // redefined MSC spec


        #endregion MENUS


        #region SYSCOMMANDS

        public const UInt32 SC_SIZE         = 0xF000;
        public const UInt32 SC_MOVE         = 0xF010;
        public const UInt32 SC_MINIMIZE     = 0xF020;
        public const UInt32 SC_MAXIMIZE     = 0xF030;
        public const UInt32 SC_NEXTWINDOW   = 0xF040;
        public const UInt32 SC_PREVWINDOW   = 0xF050;
        public const UInt32 SC_CLOSE        = 0xF060;
        public const UInt32 SC_VSCROLL      = 0xF070;
        public const UInt32 SC_HSCROLL      = 0xF080;
        public const UInt32 SC_MOUSEMENU    = 0xF090;
        public const UInt32 SC_KEYMENU      = 0xF100;
        public const UInt32 SC_ARRANGE      = 0xF110;
        public const UInt32 SC_RESTORE      = 0xF120;
        public const UInt32 SC_TASKLIST     = 0xF130;
        public const UInt32 SC_SCREENSAVE   = 0xF140;
        public const UInt32 SC_HOTKEY       = 0xF150;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SC_DEFAULT      = 0xF160;
        public const UInt32 SC_MONITORPOWER = 0xF170;
        public const UInt32 SC_CONTEXTHELP  = 0xF180;
        public const UInt32 SC_SEPARATOR    = 0xF00F;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_VISTA_LATER
		public const UInt32 SCF_ISSECURE    = 0x00000001;
#endif // WIN32_WINNT_VISTA_LATER

		// #define GET_SC_WPARAM(wParam) ((int)wParam & 0xFFF0)

		public const UInt32 SC_ICON         = SC_MINIMIZE;
		public const UInt32 SC_ZOOM         = SC_MAXIMIZE;

        #endregion SYSCOMMANDS


        /*
		WINUSERAPI HBITMAP WINAPI LoadBitmapA(_In_opt_ HINSTANCE hInstance, _In_ LPCSTR lpBitmapName);		
		WINUSERAPI HBITMAP WINAPI LoadBitmapW(_In_opt_ HINSTANCE hInstance, _In_ LPCWSTR lpBitmapName);
		*/

        //WINUSERAPI HCURSOR WINAPI LoadCursorA(_In_opt_ HINSTANCE hInstance, _In_ LPCSTR lpCursorName);		
        //WINUSERAPI HCURSOR WINAPI LoadCursorW(_In_opt_ HINSTANCE hInstance, _In_ LPCWSTR lpCursorName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadCursorA(IntPtr hInstance, IntPtr lpCursorName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadCursorW(IntPtr hInstance, IntPtr lpCursorName);
        /*
		WINUSERAPI HCURSOR WINAPI LoadCursorFromFileA(_In_ LPCSTR lpFileName);		
		WINUSERAPI HCURSOR WINAPI LoadCursorFromFileW(_In_ LPCWSTR lpFileName);
		*/
        /*
		WINUSERAPI HCURSOR WINAPI CreateCursor(
			_In_opt_ HINSTANCE hInst,
			_In_ int xHotSpot,
			_In_ int yHotSpot,
			_In_ int nWidth,
			_In_ int nHeight,
			_In_ CONST VOID* pvANDPlane,
			_In_ CONST VOID* pvXORPlane);
		*/
        // WINUSERAPI BOOL WINAPI DestroyCursor(_In_ HCURSOR hCursor);

        //#define CopyCursor(pcur) ((HCURSOR)CopyIcon((HICON)(pcur)))


        public static readonly IntPtr IDC_ARROW           = MAKEINTRESOURCE(32512);
		public static readonly IntPtr IDC_IBEAM           = MAKEINTRESOURCE(32513);
		public static readonly IntPtr IDC_WAIT            = MAKEINTRESOURCE(32514);
		public static readonly IntPtr IDC_CROSS           = MAKEINTRESOURCE(32515);
		public static readonly IntPtr IDC_UPARROW         = MAKEINTRESOURCE(32516);
		public static readonly IntPtr IDC_SIZE            = MAKEINTRESOURCE(32640);  /* OBSOLETE: use IDC_SIZEALL */
		public static readonly IntPtr IDC_ICON            = MAKEINTRESOURCE(32641);  /* OBSOLETE: use IDC_ARROW */
		public static readonly IntPtr IDC_SIZENWSE        = MAKEINTRESOURCE(32642);
		public static readonly IntPtr IDC_SIZENESW        = MAKEINTRESOURCE(32643);
		public static readonly IntPtr IDC_SIZEWE          = MAKEINTRESOURCE(32644);
		public static readonly IntPtr IDC_SIZENS          = MAKEINTRESOURCE(32645);
		public static readonly IntPtr IDC_SIZEALL         = MAKEINTRESOURCE(32646);
		public static readonly IntPtr IDC_NO              = MAKEINTRESOURCE(32648); /*not in win3.1 */
#if WIN32_WINNT_WIN2K_LATER
		public static readonly IntPtr IDC_HAND            = MAKEINTRESOURCE(32649);
#endif // WIN32_WINNT_WIN2K_LATER
		public static readonly IntPtr IDC_APPSTARTING     = MAKEINTRESOURCE(32650); /*not in win3.1 */
#if WIN32_WINNT_NT4_LATER
		public static readonly IntPtr IDC_HELP            = MAKEINTRESOURCE(32651);
#endif // WIN32_WINNT_NT4_LATER


		// WINUSERAPI BOOL WINAPI SetSystemCursor(_In_ HCURSOR hcur, _In_ DWORD id);

		public struct ICONINFO
		{
			public bool fIcon;
			public UInt32 xHotspot;
			public UInt32 yHotspot;
			public IntPtr hbmMask;
			public IntPtr hbmColor;
		}


        //WINUSERAPI HICON WINAPI LoadIconA(_In_opt_ HINSTANCE hInstance, _In_ LPCSTR lpIconName);		
        //WINUSERAPI HICON WINAPI LoadIconW(_In_opt_ HINSTANCE hInstance, _In_ LPCWSTR lpIconName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadIconA(IntPtr hInstance, IntPtr lpIconName);
        [DllImport("user32.dll", SetLastError =  true)]
        public static extern IntPtr LoadIconW(IntPtr hInstance, IntPtr lpIconName);
        /*
		WINUSERAPI UINT WINAPI PrivateExtractIconsA(
			_In_reads_(MAX_PATH) LPCSTR szFileName,
			_In_ int nIconIndex,
			_In_ int cxIcon,
			_In_ int cyIcon,
			_Out_writes_opt_(nIcons) HICON *phicon,
			_Out_writes_opt_(nIcons) UINT *piconid,
			_In_ UINT nIcons,
			_In_ UINT flags);
			
		WINUSERAPI UINT WINAPI PrivateExtractIconsW(
			_In_reads_(MAX_PATH) LPCWSTR szFileName,
			_In_ int nIconIndex,
			_In_ int cxIcon,
			_In_ int cyIcon,
			_Out_writes_opt_(nIcons) HICON *phicon,
			_Out_writes_opt_(nIcons) UINT *piconid,
			_In_ UINT nIcons,
			_In_ UINT flags);
		*/
        /*
		WINUSERAPI HICON WINAPI CreateIcon(
			_In_opt_ HINSTANCE hInstance,
			_In_ int nWidth,
			_In_ int nHeight,
			_In_ BYTE cPlanes,
			_In_ BYTE cBitsPixel,
			_In_ CONST BYTE* lpbANDbits,
			_In_ CONST BYTE* lpbXORbits);
		*/

        // WINUSERAPI BOOL WINAPI DestroyIcon(_In_ HICON hIcon);

        // WINUSERAPI int WINAPI LookupIconIdFromDirectory(_In_reads_bytes_(sizeof(WORD) * 3) PBYTE presbits, _In_ BOOL fIcon);

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI int WINAPI LookupIconIdFromDirectoryEx(
			_In_reads_bytes_(sizeof(WORD) * 3) PBYTE presbits,
			_In_ BOOL fIcon,
			_In_ int cxDesired,
			_In_ int cyDesired,
			_In_ UINT Flags);
		*/
#endif // WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI HICON WINAPI CreateIconFromResource(
			_In_reads_bytes_(dwResSize) PBYTE presbits,
			_In_ DWORD dwResSize,
			_In_ BOOL fIcon,
			_In_ DWORD dwVer);
		*/

#if WIN32_WINNT_NT4_LATER
        /*
		WINUSERAPI HICON WINAPI CreateIconFromResourceEx(
			_In_reads_bytes_(dwResSize) PBYTE presbits,
			_In_ DWORD dwResSize,
			_In_ BOOL fIcon,
			_In_ DWORD dwVer,
			_In_ int cxDesired,
			_In_ int cyDesired,
			_In_ UINT Flags);
		*/

        public struct CURSORSHAPE
		{
			public Int32 xHotSpot;
			public Int32 yHotSpot;
			public Int32 cx;
			public Int32 cy;
			public Int32 cbWidth;
			public Byte Planes;
			public Byte BitsPixel;
		}

#endif // WIN32_WINNT_NT4_LATER


		public const UInt32 IMAGE_BITMAP        = 0;
		public const UInt32 IMAGE_ICON          = 1;
		public const UInt32 IMAGE_CURSOR        = 2;
#if WIN32_WINNT_NT4_LATER
		public const UInt32 IMAGE_ENHMETAFILE   = 3;

        public const UInt32 LR_DEFAULTCOLOR     = 0x00000000;
        public const UInt32 LR_MONOCHROME       = 0x00000001;
        public const UInt32 LR_COLOR            = 0x00000002;
        public const UInt32 LR_COPYRETURNORG    = 0x00000004;
        public const UInt32 LR_COPYDELETEORG    = 0x00000008;
        public const UInt32 LR_LOADFROMFILE     = 0x00000010;
        public const UInt32 LR_LOADTRANSPARENT  = 0x00000020;
        public const UInt32 LR_DEFAULTSIZE      = 0x00000040;
        public const UInt32 LR_VGACOLOR         = 0x00000080;
        public const UInt32 LR_LOADMAP3DCOLORS  = 0x00001000;
        public const UInt32 LR_CREATEDIBSECTION = 0x00002000;
        public const UInt32 LR_COPYFROMRESOURCE = 0x00004000;
        public const UInt32 LR_SHARED           = 0x00008000;

		/*
		WINUSERAPI HANDLE WINAPI LoadImageA(
			_In_opt_ HINSTANCE hInst,
			_In_ LPCSTR name,
			_In_ UINT type,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT fuLoad);
			
		WINUSERAPI HANDLE WINAPI LoadImageW(
			_In_opt_ HINSTANCE hInst,
			_In_ LPCWSTR name,
			_In_ UINT type,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT fuLoad);
		*/
		/*
		WINUSERAPI HANDLE WINAPI CopyImage(
			_In_ HANDLE h,
			_In_ UINT type,
			_In_ int cx,
			_In_ int cy,
			_In_ UINT flags);
		*/
        public const UInt32 DI_MASK         = 0x0001;
        public const UInt32 DI_IMAGE        = 0x0002;
        public const UInt32 DI_NORMAL       = 0x0003;
        public const UInt32 DI_COMPAT       = 0x0004;
        public const UInt32 DI_DEFAULTSIZE  = 0x0008;
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 DI_NOMIRROR     = 0x0010;
#endif // WIN32_WINNT_WINXP_LATER
        /*
		WINUSERAPI BOOL WINAPI DrawIconEx(
			_In_ HDC hdc,
			_In_ int xLeft,
			_In_ int yTop,
			_In_ HICON hIcon,
			_In_ int cxWidth,
			_In_ int cyWidth,
			_In_ UINT istepIfAniCur,
			_In_opt_ HBRUSH hbrFlickerFreeDraw,
			_In_ UINT diFlags);
		*/


#endif // WIN32_WINNT_NT4_LATER


        // WINUSERAPI HICON WINAPI CreateIconIndirect(_In_ PICONINFO piconinfo);

        // WINUSERAPI HICON WINAPI CopyIcon(_In_ HICON hIcon);

        // WINUSERAPI BOOL WINAPI GetIconInfo(_In_ HICON hIcon, _Out_ PICONINFO piconinfo);

#if WIN32_WINNT_VISTA_LATER
#if CSPORTING
        public struct ICONINFOEXA
		{
			public UInt32 cbSize;
			public bool fIcon;
			public UInt32 xHotspot;
			public UInt32 yHotspot;
			public IntPtr hbmMask;
			public IntPtr hbmColor;
			public UInt16 wResID;
			public Byte szModName[MAX_PATH];
			public Byte szResName[MAX_PATH];
		}

        public struct ICONINFOEXW
		{
			public UInt32 cbSize;
			public bool fIcon;
			public UInt32 xHotspot;
			public UInt32 yHotspot;
			public IntPtr hbmMask;
			public IntPtr hbmColor;
			public UInt16 wResID;
			public Char szModName[MAX_PATH];
			public Char szResName[MAX_PATH];
		}
        */
#endif
        /*
		WINUSERAPI BOOL WINAPI GetIconInfoExA(_In_ HICON hicon, _Inout_ PICONINFOEXA piconinfo);		
		WINUSERAPI BOOL WINAPI GetIconInfoExW(_In_ HICON hicon, _Inout_ PICONINFOEXW piconinfo);
		*/
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt16 RES_ICON    = 1;
		public const UInt16 RES_CURSOR  = 2;
#endif // WIN32_WINNT_NT4_LATER


        #region OEMRESOURCE

        public const UInt16 OBM_CLOSE           = 32754;
        public const UInt16 OBM_UPARROW         = 32753;
        public const UInt16 OBM_DNARROW         = 32752;
        public const UInt16 OBM_RGARROW         = 32751;
        public const UInt16 OBM_LFARROW         = 32750;
        public const UInt16 OBM_REDUCE          = 32749;
        public const UInt16 OBM_ZOOM            = 32748;
        public const UInt16 OBM_RESTORE         = 32747;
        public const UInt16 OBM_REDUCED         = 32746;
        public const UInt16 OBM_ZOOMD           = 32745;
        public const UInt16 OBM_RESTORED        = 32744;
        public const UInt16 OBM_UPARROWD        = 32743;
        public const UInt16 OBM_DNARROWD        = 32742;
        public const UInt16 OBM_RGARROWD        = 32741;
        public const UInt16 OBM_LFARROWD        = 32740;
        public const UInt16 OBM_MNARROW         = 32739;
        public const UInt16 OBM_COMBO           = 32738;
        public const UInt16 OBM_UPARROWI        = 32737;
        public const UInt16 OBM_DNARROWI        = 32736;
        public const UInt16 OBM_RGARROWI        = 32735;
        public const UInt16 OBM_LFARROWI        = 32734;

        public const UInt16 OBM_OLD_CLOSE       = 32767;
        public const UInt16 OBM_SIZE            = 32766;
        public const UInt16 OBM_OLD_UPARROW     = 32765;
        public const UInt16 OBM_OLD_DNARROW     = 32764;
        public const UInt16 OBM_OLD_RGARROW     = 32763;
        public const UInt16 OBM_OLD_LFARROW     = 32762;
        public const UInt16 OBM_BTSIZE          = 32761;
        public const UInt16 OBM_CHECK           = 32760;
        public const UInt16 OBM_CHECKBOXES      = 32759;
        public const UInt16 OBM_BTNCORNERS      = 32758;
        public const UInt16 OBM_OLD_REDUCE      = 32757;
        public const UInt16 OBM_OLD_ZOOM        = 32756;
        public const UInt16 OBM_OLD_RESTORE     = 32755;

        public const UInt16 OCR_NORMAL          = 32512;
        public const UInt16 OCR_IBEAM           = 32513;
        public const UInt16 OCR_WAIT            = 32514;
        public const UInt16 OCR_CROSS           = 32515;
        public const UInt16 OCR_UP              = 32516;
        public const UInt16 OCR_SIZE            = 32640;   /* OBSOLETE: use OCR_SIZEALL */
        public const UInt16 OCR_ICON            = 32641;   /* OBSOLETE: use OCR_NORMAL */
        public const UInt16 OCR_SIZENWSE        = 32642;
        public const UInt16 OCR_SIZENESW        = 32643;
        public const UInt16 OCR_SIZEWE          = 32644;
        public const UInt16 OCR_SIZENS          = 32645;
        public const UInt16 OCR_SIZEALL         = 32646;
        public const UInt16 OCR_ICOCUR          = 32647;   /* OBSOLETE: use OIC_WINLOGO */
        public const UInt16 OCR_NO              = 32648;
#if WIN32_WINNT_WIN2K_LATER
        public const UInt16 OCR_HAND            = 32649;
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_NT4_LATER
        public const UInt16 OCR_APPSTARTING     = 32650;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt16 OIC_SAMPLE          = 32512;
        public const UInt16 OIC_HAND            = 32513;
        public const UInt16 OIC_QUES            = 32514;
        public const UInt16 OIC_BANG            = 32515;
        public const UInt16 OIC_NOTE            = 32516;
#if WIN32_WINNT_NT4_LATER
        public const UInt16 OIC_WINLOGO         = 32517;
        public const UInt16 OIC_WARNING         = OIC_BANG;
        public const UInt16 OIC_ERROR           = OIC_HAND;
        public const UInt16 OIC_INFORMATION     = OIC_NOTE;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_VISTA_LATER
        public const UInt16 OIC_SHIELD          = 32518;
#endif // WIN32_WINNT_VISTA_LATER

        #endregion OEMRESOURCE


        // #define ORD_LANGDRIVER    1     
        /* The ordinal number for the entry point of
		** language drivers.
		*/


        #region ICONS

        public static readonly IntPtr IDI_APPLICATION     = MAKEINTRESOURCE(32512);
        public static readonly IntPtr IDI_HAND            = MAKEINTRESOURCE(32513);
        public static readonly IntPtr IDI_QUESTION        = MAKEINTRESOURCE(32514);
        public static readonly IntPtr IDI_EXCLAMATION     = MAKEINTRESOURCE(32515);
        public static readonly IntPtr IDI_ASTERISK        = MAKEINTRESOURCE(32516);
#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr IDI_WINLOGO         = MAKEINTRESOURCE(32517);
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_VISTA_LATER
        public static readonly IntPtr IDI_SHIELD          = MAKEINTRESOURCE(32518);
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_NT4_LATER
        public static readonly IntPtr IDI_WARNING     = IDI_EXCLAMATION;
        public static readonly IntPtr IDI_ERROR       = IDI_HAND;
        public static readonly IntPtr IDI_INFORMATION = IDI_ASTERISK;
#endif // WIN32_WINNT_NT4_LATER

        #endregion ICONS


        #region APISET
        /*
        WINUSERAPI int WINAPI LoadStringA(
            _In_opt_ HINSTANCE hInstance,
            _In_ UINT uID,
            _Out_writes_to_(cchBufferMax, return +1) LPSTR lpBuffer,
            _In_ int cchBufferMax);
            
        WINUSERAPI int WINAPI LoadStringW(
            _In_opt_ HINSTANCE hInstance,
            _In_ UINT uID,
            _Out_writes_to_(cchBufferMax, return +1) LPWSTR lpBuffer,
            _In_ int cchBufferMax);
        */

        #endregion APISET


        public const UInt16 IDOK                = 1;
        public const UInt16 IDCANCEL            = 2;
        public const UInt16 IDABORT             = 3;
        public const UInt16 IDRETRY             = 4;
        public const UInt16 IDIGNORE            = 5;
        public const UInt16 IDYES               = 6;
        public const UInt16 IDNO                = 7;
#if WIN32_WINNT_NT4_LATER
        public const UInt16 IDCLOSE         = 8;
        public const UInt16 IDHELP          = 9;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
        public const UInt16 IDTRYAGAIN      = 10;
        public const UInt16 IDCONTINUE      = 11;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WINXP_LATER
#if !IDTIMEOUT
        public const UInt32 IDTIMEOUT = 32000;
#endif
#endif // WIN32_WINNT_WINXP_LATER


        #region CTLMGR

        #region WINSTYLES

        public const UInt32 ES_LEFT             = 0x0000;
        public const UInt32 ES_CENTER           = 0x0001;
        public const UInt32 ES_RIGHT            = 0x0002;
        public const UInt32 ES_MULTILINE        = 0x0004;
        public const UInt32 ES_UPPERCASE        = 0x0008;
        public const UInt32 ES_LOWERCASE        = 0x0010;
        public const UInt32 ES_PASSWORD         = 0x0020;
        public const UInt32 ES_AUTOVSCROLL      = 0x0040;
        public const UInt32 ES_AUTOHSCROLL      = 0x0080;
        public const UInt32 ES_NOHIDESEL        = 0x0100;
        public const UInt32 ES_OEMCONVERT       = 0x0400;
        public const UInt32 ES_READONLY         = 0x0800;
        public const UInt32 ES_WANTRETURN       = 0x1000;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 ES_NUMBER           = 0x2000;
#endif // WIN32_WINNT_NT4_LATER

        #endregion WINSTYLES


        public const UInt16 EN_SETFOCUS         = 0x0100;
        public const UInt16 EN_KILLFOCUS        = 0x0200;
        public const UInt16 EN_CHANGE           = 0x0300;
        public const UInt16 EN_UPDATE           = 0x0400;
        public const UInt16 EN_ERRSPACE         = 0x0500;
        public const UInt16 EN_MAXTEXT          = 0x0501;
        public const UInt16 EN_HSCROLL          = 0x0601;
        public const UInt16 EN_VSCROLL          = 0x0602;

#if WIN32_WINNT_WIN2K_LATER
        public const UInt16 EN_ALIGN_LTR_EC     = 0x0700;
        public const UInt16 EN_ALIGN_RTL_EC     = 0x0701;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN10_LATER // WINVER >= 0x0604)
        public const UInt16 EN_BEFORE_PASTE     = 0x0800;
        public const UInt16 EN_AFTER_PASTE      = 0x0801;
#endif // WIN32_WINNT_WIN10_LATER WINVER >= 0x0604

#if WIN32_WINNT_NT4_LATER
        /* Edit control EM_SETMARGIN parameters */
        public const UInt32 EC_LEFTMARGIN       = 0x0001;
        public const UInt32 EC_RIGHTMARGIN      = 0x0002;
        public const UInt32 EC_USEFONTINFO      = 0xffff;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 EMSIS_COMPOSITIONSTRING        = 0x0001;

        public const Int32 EIMES_GETCOMPSTRATONCE         = 0x0001;
        public const Int32 EIMES_CANCELCOMPSTRINFOCUS     = 0x0002;
        public const Int32 EIMES_COMPLETECOMPSTRKILLFOCUS = 0x0004;
#endif // WIN32_WINNT_WIN2K_LATER


        #region WINMESSAGES

        public const UInt32 EM_GETSEL               = 0x00B0;
        public const UInt32 EM_SETSEL               = 0x00B1;
        public const UInt32 EM_GETRECT              = 0x00B2;
        public const UInt32 EM_SETRECT              = 0x00B3;
        public const UInt32 EM_SETRECTNP            = 0x00B4;
        public const UInt32 EM_SCROLL               = 0x00B5;
        public const UInt32 EM_LINESCROLL           = 0x00B6;
        public const UInt32 EM_SCROLLCARET          = 0x00B7;
        public const UInt32 EM_GETMODIFY            = 0x00B8;
        public const UInt32 EM_SETMODIFY            = 0x00B9;
        public const UInt32 EM_GETLINECOUNT         = 0x00BA;
        public const UInt32 EM_LINEINDEX            = 0x00BB;
        public const UInt32 EM_SETHANDLE            = 0x00BC;
        public const UInt32 EM_GETHANDLE            = 0x00BD;
        public const UInt32 EM_GETTHUMB             = 0x00BE;
        public const UInt32 EM_LINELENGTH           = 0x00C1;
        public const UInt32 EM_REPLACESEL           = 0x00C2;
        public const UInt32 EM_GETLINE              = 0x00C4;
        public const UInt32 EM_LIMITTEXT            = 0x00C5;
        public const UInt32 EM_CANUNDO              = 0x00C6;
        public const UInt32 EM_UNDO                 = 0x00C7;
        public const UInt32 EM_FMTLINES             = 0x00C8;
        public const UInt32 EM_LINEFROMCHAR         = 0x00C9;
        public const UInt32 EM_SETTABSTOPS          = 0x00CB;
        public const UInt32 EM_SETPASSWORDCHAR      = 0x00CC;
        public const UInt32 EM_EMPTYUNDOBUFFER      = 0x00CD;
        public const UInt32 EM_GETFIRSTVISIBLELINE  = 0x00CE;
        public const UInt32 EM_SETREADONLY          = 0x00CF;
        public const UInt32 EM_SETWORDBREAKPROC     = 0x00D0;
        public const UInt32 EM_GETWORDBREAKPROC     = 0x00D1;
        public const UInt32 EM_GETPASSWORDCHAR      = 0x00D2;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 EM_SETMARGINS           = 0x00D3;
        public const UInt32 EM_GETMARGINS           = 0x00D4;
        public const UInt32 EM_SETLIMITTEXT         = EM_LIMITTEXT;   /* ;win40 Name change */
        public const UInt32 EM_GETLIMITTEXT         = 0x00D5;
        public const UInt32 EM_POSFROMCHAR          = 0x00D6;
        public const UInt32 EM_CHARFROMPOS          = 0x00D7;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 EM_SETIMESTATUS         = 0x00D8;
        public const UInt32 EM_GETIMESTATUS         = 0x00D9;
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN10_LATER //WINVER >= 0x0604)
        public const UInt32 EM_ENABLEFEATURE        = 0x00DA;
#endif // WIN32_WINNT_WIN10_LATER WINVER >= 0x0604

        #endregion WINMESSAGES


#if WIN32_WINNT_WIN10_LATER // WINVER >= 0x0604)

        public enum EDIT_CONTROL_FEATURE
        {
            EDIT_CONTROL_FEATURE_ENTERPRISE_DATA_PROTECTION_PASTE_SUPPORT = 0,
            EDIT_CONTROL_FEATURE_PASTE_NOTIFICATIONS = 1,
        }
        
#endif // WIN32_WINNT_WIN10_LATER WINVER >= 0x0604

        public const Int32 WB_LEFT            = 0;
        public const Int32 WB_RIGHT           = 1;
        public const Int32 WB_ISDELIMITER     = 2;

        public const UInt32 BS_PUSHBUTTON       = 0x00000000;
        public const UInt32 BS_DEFPUSHBUTTON    = 0x00000001;
        public const UInt32 BS_CHECKBOX         = 0x00000002;
        public const UInt32 BS_AUTOCHECKBOX     = 0x00000003;
        public const UInt32 BS_RADIOBUTTON      = 0x00000004;
        public const UInt32 BS_3STATE           = 0x00000005;
        public const UInt32 BS_AUTO3STATE       = 0x00000006;
        public const UInt32 BS_GROUPBOX         = 0x00000007;
        public const UInt32 BS_USERBUTTON       = 0x00000008;
        public const UInt32 BS_AUTORADIOBUTTON  = 0x00000009;
        public const UInt32 BS_PUSHBOX          = 0x0000000A;
        public const UInt32 BS_OWNERDRAW        = 0x0000000B;
        public const UInt32 BS_TYPEMASK         = 0x0000000F;
        public const UInt32 BS_LEFTTEXT         = 0x00000020;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 BS_TEXT             = 0x00000000;
        public const UInt32 BS_ICON             = 0x00000040;
        public const UInt32 BS_BITMAP           = 0x00000080;
        public const UInt32 BS_LEFT             = 0x00000100;
        public const UInt32 BS_RIGHT            = 0x00000200;
        public const UInt32 BS_CENTER           = 0x00000300;
        public const UInt32 BS_TOP              = 0x00000400;
        public const UInt32 BS_BOTTOM           = 0x00000800;
        public const UInt32 BS_VCENTER          = 0x00000C00;
        public const UInt32 BS_PUSHLIKE         = 0x00001000;
        public const UInt32 BS_MULTILINE        = 0x00002000;
        public const UInt32 BS_NOTIFY           = 0x00004000;
        public const UInt32 BS_FLAT             = 0x00008000;
        public const UInt32 BS_RIGHTBUTTON      = BS_LEFTTEXT;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt16 BN_CLICKED          = 0;
        public const UInt16 BN_PAINT            = 1;
        public const UInt16 BN_HILITE           = 2;
        public const UInt16 BN_UNHILITE         = 3;
        public const UInt16 BN_DISABLE          = 4;
        public const UInt16 BN_DOUBLECLICKED    = 5;
#if WIN32_WINNT_NT4_LATER
        public const UInt16 BN_PUSHED           = BN_HILITE;
        public const UInt16 BN_UNPUSHED         = BN_UNHILITE;
        public const UInt16 BN_DBLCLK           = BN_DOUBLECLICKED;
        public const UInt16 BN_SETFOCUS         = 6;
        public const UInt16 BN_KILLFOCUS        = 7;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 BM_GETCHECK        =0x00F0;
        public const UInt32 BM_SETCHECK        =0x00F1;
        public const UInt32 BM_GETSTATE        =0x00F2;
        public const UInt32 BM_SETSTATE        =0x00F3;
        public const UInt32 BM_SETSTYLE        =0x00F4;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 BM_CLICK           =0x00F5;
        public const UInt32 BM_GETIMAGE        =0x00F6;
        public const UInt32 BM_SETIMAGE        =0x00F7;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_VISTA_LATER
        public const UInt32 BM_SETDONTCLICK    =0x00F8;
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt16 BST_UNCHECKED      = 0x0000;
        public const UInt16 BST_CHECKED        = 0x0001;
        public const UInt16 BST_INDETERMINATE  = 0x0002;
        public const UInt16 BST_PUSHED         = 0x0004;
        public const UInt16 BST_FOCUS          = 0x0008;
#endif // WIN32_WINNT_NT4_LATER

        public const UInt32 SS_LEFT             = 0x00000000;
        public const UInt32 SS_CENTER           = 0x00000001;
        public const UInt32 SS_RIGHT            = 0x00000002;
        public const UInt32 SS_ICON             = 0x00000003;
        public const UInt32 SS_BLACKRECT        = 0x00000004;
        public const UInt32 SS_GRAYRECT         = 0x00000005;
        public const UInt32 SS_WHITERECT        = 0x00000006;
        public const UInt32 SS_BLACKFRAME       = 0x00000007;
        public const UInt32 SS_GRAYFRAME        = 0x00000008;
        public const UInt32 SS_WHITEFRAME       = 0x00000009;
        public const UInt32 SS_USERITEM         = 0x0000000A;
        public const UInt32 SS_SIMPLE           = 0x0000000B;
        public const UInt32 SS_LEFTNOWORDWRAP   = 0x0000000C;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SS_OWNERDRAW        = 0x0000000D;
        public const UInt32 SS_BITMAP           = 0x0000000E;
        public const UInt32 SS_ENHMETAFILE      = 0x0000000F;
        public const UInt32 SS_ETCHEDHORZ       = 0x00000010;
        public const UInt32 SS_ETCHEDVERT       = 0x00000011;
        public const UInt32 SS_ETCHEDFRAME      = 0x00000012;
        public const UInt32 SS_TYPEMASK         = 0x0000001F;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 SS_REALSIZECONTROL  = 0x00000040;
#endif // WIN32_WINNT_WINXP_LATER
        public const UInt32 SS_NOPREFIX         = 0x00000080; /* Don't do "&" character translation */
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SS_NOTIFY           = 0x00000100;
        public const UInt32 SS_CENTERIMAGE      = 0x00000200;
        public const UInt32 SS_RIGHTJUST        = 0x00000400;
        public const UInt32 SS_REALSIZEIMAGE    = 0x00000800;
        public const UInt32 SS_SUNKEN           = 0x00001000;
        public const UInt32 SS_EDITCONTROL      = 0x00002000;
        public const UInt32 SS_ENDELLIPSIS      = 0x00004000;
        public const UInt32 SS_PATHELLIPSIS     = 0x00008000;
        public const UInt32 SS_WORDELLIPSIS     = 0x0000C000;
        public const UInt32 SS_ELLIPSISMASK     = 0x0000C000;
#endif // WIN32_WINNT_NT4_LATER


        #region WINMESSAGES

        public const UInt32 STM_SETICON         = 0x0170;
        public const UInt32 STM_GETICON         = 0x0171;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 STM_SETIMAGE        = 0x0172;
        public const UInt32 STM_GETIMAGE        = 0x0173;
        public const UInt16 STN_CLICKED         = 0;
        public const UInt16 STN_DBLCLK          = 1;
        public const UInt16 STN_ENABLE          = 2;
        public const UInt16 STN_DISABLE         = 3;
#endif // WIN32_WINNT_NT4_LATER
        public const UInt32 STM_MSGMAX          = 0x0174;

        #endregion WINMESSAGES


        public static readonly IntPtr WC_DIALOG     = (IntPtr)0x8002;     // MAKEINTATOM(0x8002)

#if !_WIN64
        public const Int32 DWL_MSGRESULT   = 0;
        public const Int32 DWL_DLGPROC     = 4;
        public const Int32 DWL_USER        = 8;
#endif
        //#if _WIN64

        //#undef DWL_MSGRESULT
        //#undef DWL_DLGPROC
        //#undef DWL_USER

        //#endif // _WIN64
        /*
        public const Int32 DWLP_MSGRESULT  = 0;
        public const Int32 DWLP_DLGPROC    = DWLP_MSGRESULT + sizeof(LRESULT);
        public const Int32 DWLP_USER       = DWLP_DLGPROC + sizeof(DLGPROC);
        */


        #region MSG

        /*
		WINUSERAPI BOOL WINAPI IsDialogMessageA(_In_ HWND hDlg, _In_ LPMSG lpMsg);

		WINUSERAPI BOOL WINAPI IsDialogMessageW(_In_ HWND hDlg, _In_ LPMSG lpMsg);
		
		//#ifdef UNICODE
		//#define IsDialogMessage  IsDialogMessageW
		//#else
		//#define IsDialogMessage  IsDialogMessageA
		//#endif // !UNICODE
		*/

        #endregion MSG


        // WINUSERAPI BOOL WINAPI MapDialogRect(_In_ HWND hDlg, _Inout_ LPRECT lpRect);
        /*
		WINUSERAPI int WINAPI DlgDirListA(
			_In_ HWND hDlg,
			_Inout_ LPSTR lpPathSpec,
			_In_ int nIDListBox,
			_In_ int nIDStaticPath,
			_In_ UINT uFileType);

		WINUSERAPI int WINAPI DlgDirListW(
			_In_ HWND hDlg,
			_Inout_ LPWSTR lpPathSpec,
		    _In_ int nIDListBox,
			_In_ int nIDStaticPath,
			_In_ UINT uFileType);
		*/


        public const UInt32 DDL_READWRITE       = 0x0000;
        public const UInt32 DDL_READONLY        = 0x0001;
        public const UInt32 DDL_HIDDEN          = 0x0002;
        public const UInt32 DDL_SYSTEM          = 0x0004;
        public const UInt32 DDL_DIRECTORY       = 0x0010;
        public const UInt32 DDL_ARCHIVE         = 0x0020;

        public const UInt32 DDL_POSTMSGS        = 0x2000;
        public const UInt32 DDL_DRIVES          = 0x4000;
        public const UInt32 DDL_EXCLUSIVE       = 0x8000;


		/*
		WINUSERAPI BOOL WINAPI DlgDirSelectExA(
			_In_ HWND hwndDlg,
			_Out_writes_(chCount) LPSTR lpString,
			_In_ int chCount,
			_In_ int idListBox);

		WINUSERAPI BOOL WINAPI DlgDirSelectExW(
			_In_ HWND hwndDlg,
			_Out_writes_(chCount) LPWSTR lpString,
			_In_ int chCount,
			_In_ int idListBox);
		*/
		/*
		WINUSERAPI int WINAPI DlgDirListComboBoxA(
			_In_ HWND hDlg,
			_Inout_ LPSTR lpPathSpec,
			_In_ int nIDComboBox,
			_In_ int nIDStaticPath,
			_In_ UINT uFiletype);
		
		WINUSERAPI int WINAPI DlgDirListComboBoxW(
			_In_ HWND hDlg,
			_Inout_ LPWSTR lpPathSpec,
			_In_ int nIDComboBox,
			_In_ int nIDStaticPath,
			_In_ UINT uFiletype);
		*/
		/*
		WINUSERAPI BOOL WINAPI DlgDirSelectComboBoxExA(
		    _In_ HWND hwndDlg,
			_Out_writes_(cchOut) LPSTR lpString,
			_In_ int cchOut,
			_In_ int idComboBox);
			
		WINUSERAPI BOOL WINAPI DlgDirSelectComboBoxExW(
			_In_ HWND hwndDlg,
			_Out_writes_(cchOut) LPWSTR lpString,
			_In_ int cchOut,
			_In_ int idComboBox);
		*/


        public const UInt32 DS_ABSALIGN         = 0x01;
        public const UInt32 DS_SYSMODAL         = 0x02;
        public const UInt32 DS_LOCALEDIT        = 0x20;   /* Edit items get Local storage. */
        public const UInt32 DS_SETFONT          = 0x40;   /* User specified font for Dlg controls */
        public const UInt32 DS_MODALFRAME       = 0x80;   /* Can be combined with WS_CAPTION  */
        public const UInt32 DS_NOIDLEMSG        = 0x100;  /* WM_ENTERIDLE message will not be sent */
        public const UInt32 DS_SETFOREGROUND    = 0x200;  /* not in win3.1 */

#if WIN32_WINNT_NT4_LATER
        public const UInt32 DS_3DLOOK           = 0x0004;
        public const UInt32 DS_FIXEDSYS         = 0x0008;
        public const UInt32 DS_NOFAILCREATE     = 0x0010;
        public const UInt32 DS_CONTROL          = 0x0400;
        public const UInt32 DS_CENTER           = 0x0800;
        public const UInt32 DS_CENTERMOUSE      = 0x1000;
        public const UInt32 DS_CONTEXTHELP      = 0x2000;

        public const UInt32 DS_SHELLFONT        = (DS_SETFONT | DS_FIXEDSYS);
#endif // WIN32_WINNT_NT4_LATER

//#if defined(_WIN32_WCE) && (_WIN32_WCE >= 0x0500)
//        public const UInt32 DS_USEPIXELS        = 0x8000;
//#endif

        public const UInt32 DM_GETDEFID         = (WM_USER+0);
        public const UInt32 DM_SETDEFID         = (WM_USER+1);

#if WIN32_WINNT_NT4_LATER
        public const UInt32 DM_REPOSITION       = (WM_USER+2);
#endif // WIN32_WINNT_NT4_LATER

        public const UInt16 DC_HASDEFID         = 0x534B;

        public const Int32 DLGC_WANTARROWS     = 0x0001;      /* Control wants arrow keys         */
        public const Int32 DLGC_WANTTAB        = 0x0002;      /* Control wants tab keys           */
        public const Int32 DLGC_WANTALLKEYS    = 0x0004;      /* Control wants all keys           */
        public const Int32 DLGC_WANTMESSAGE    = 0x0004;      /* Pass message to control          */
        public const Int32 DLGC_HASSETSEL      = 0x0008;      /* Understands EM_SETSEL message    */
        public const Int32 DLGC_DEFPUSHBUTTON  = 0x0010;      /* Default pushbutton               */
        public const Int32 DLGC_UNDEFPUSHBUTTON = 0x0020;     /* Non-default pushbutton           */
        public const Int32 DLGC_RADIOBUTTON    = 0x0040;      /* Radio button                     */
        public const Int32 DLGC_WANTCHARS      = 0x0080;      /* Want WM_CHAR messages            */
        public const Int32 DLGC_STATIC         = 0x0100;      /* Static item: don't include       */
        public const Int32 DLGC_BUTTON         = 0x2000;      /* Button item: can be checked      */

        // #define LB_CTLCODE          0L

        public const Int32 LB_OKAY             = 0;
        public const Int32 LB_ERR              = (-1);
        public const Int32 LB_ERRSPACE         = (-2);

        public const UInt16 LBN_ERRSPACE        = unchecked((UInt16)(-2));
        public const UInt16 LBN_SELCHANGE       = 1;
        public const UInt16 LBN_DBLCLK          = 2;
        public const UInt16 LBN_SELCANCEL       = 3;
        public const UInt16 LBN_SETFOCUS        = 4;
        public const UInt16 LBN_KILLFOCUS       = 5;


        #region WINMESSAGES

        public const UInt32 LB_ADDSTRING            = 0x0180;
        public const UInt32 LB_INSERTSTRING         = 0x0181;
        public const UInt32 LB_DELETESTRING         = 0x0182;
        public const UInt32 LB_SELITEMRANGEEX       = 0x0183;
        public const UInt32 LB_RESETCONTENT         = 0x0184;
        public const UInt32 LB_SETSEL               = 0x0185;
        public const UInt32 LB_SETCURSEL            = 0x0186;
        public const UInt32 LB_GETSEL               = 0x0187;
        public const UInt32 LB_GETCURSEL            = 0x0188;
        public const UInt32 LB_GETTEXT              = 0x0189;
        public const UInt32 LB_GETTEXTLEN           = 0x018A;
        public const UInt32 LB_GETCOUNT             = 0x018B;
        public const UInt32 LB_SELECTSTRING         = 0x018C;
        public const UInt32 LB_DIR                  = 0x018D;
        public const UInt32 LB_GETTOPINDEX          = 0x018E;
        public const UInt32 LB_FINDSTRING           = 0x018F;
        public const UInt32 LB_GETSELCOUNT          = 0x0190;
        public const UInt32 LB_GETSELITEMS          = 0x0191;
        public const UInt32 LB_SETTABSTOPS          = 0x0192;
        public const UInt32 LB_GETHORIZONTALEXTENT  = 0x0193;
        public const UInt32 LB_SETHORIZONTALEXTENT  = 0x0194;
        public const UInt32 LB_SETCOLUMNWIDTH       = 0x0195;
        public const UInt32 LB_ADDFILE              = 0x0196;
        public const UInt32 LB_SETTOPINDEX          = 0x0197;
        public const UInt32 LB_GETITEMRECT          = 0x0198;
        public const UInt32 LB_GETITEMDATA          = 0x0199;
        public const UInt32 LB_SETITEMDATA          = 0x019A;
        public const UInt32 LB_SELITEMRANGE         = 0x019B;
        public const UInt32 LB_SETANCHORINDEX       = 0x019C;
        public const UInt32 LB_GETANCHORINDEX       = 0x019D;
        public const UInt32 LB_SETCARETINDEX        = 0x019E;
        public const UInt32 LB_GETCARETINDEX        = 0x019F;
        public const UInt32 LB_SETITEMHEIGHT        = 0x01A0;
        public const UInt32 LB_GETITEMHEIGHT        = 0x01A1;
        public const UInt32 LB_FINDSTRINGEXACT      = 0x01A2;
        public const UInt32 LB_SETLOCALE            = 0x01A5;
        public const UInt32 LB_GETLOCALE            = 0x01A6;
        public const UInt32 LB_SETCOUNT             = 0x01A7;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 LB_INITSTORAGE          = 0x01A8;
        public const UInt32 LB_ITEMFROMPOINT        = 0x01A9;
#endif // WIN32_WINNT_NT4_LATER
//#if defined(_WIN32_WCE) && (_WIN32_WCE >= 0x0400)
//        public const UInt32 LB_MULTIPLEADDSTRING  = 0x01B1;
//#endif


#if WIN32_WINNT_WINXP_LATER
        public const UInt32 LB_GETLISTBOXINFO       = 0x01B2;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 LB_MSGMAX               = 0x01B3;
//#elif defined(_WIN32_WCE) && (_WIN32_WCE >= 0x0400)
//        public const UInt32 LB_MSGMAX             = 0x01B1;
#elif WIN32_WINNT_NT4_LATER
        public const UInt32 LB_MSGMAX               = 0x01B0;
#else
        public const UInt32 LB_MSGMAX               = 0x01A8;
#endif

        #endregion WINMESSAGES


        #region WINSTYLES

        public const UInt32 LBS_NOTIFY            = 0x0001;
        public const UInt32 LBS_SORT              = 0x0002;
        public const UInt32 LBS_NOREDRAW          = 0x0004;
        public const UInt32 LBS_MULTIPLESEL       = 0x0008;
        public const UInt32 LBS_OWNERDRAWFIXED    = 0x0010;
        public const UInt32 LBS_OWNERDRAWVARIABLE = 0x0020;
        public const UInt32 LBS_HASSTRINGS        = 0x0040;
        public const UInt32 LBS_USETABSTOPS       = 0x0080;
        public const UInt32 LBS_NOINTEGRALHEIGHT  = 0x0100;
        public const UInt32 LBS_MULTICOLUMN       = 0x0200;
        public const UInt32 LBS_WANTKEYBOARDINPUT = 0x0400;
        public const UInt32 LBS_EXTENDEDSEL       = 0x0800;
        public const UInt32 LBS_DISABLENOSCROLL   = 0x1000;
        public const UInt32 LBS_NODATA            = 0x2000;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 LBS_NOSEL             = 0x4000;
#endif // WIN32_WINNT_NT4_LATER
        public const UInt32 LBS_COMBOBOX          = 0x8000;

        public const UInt32 LBS_STANDARD          = (LBS_NOTIFY | LBS_SORT | WS_VSCROLL | WS_BORDER);

        #endregion WINSTYLES


        public const Int32 CB_OKAY             =0;
        public const Int32 CB_ERR              =(-1);
        public const Int32 CB_ERRSPACE         =(-2);

        public const UInt16 CBN_ERRSPACE        = unchecked((UInt16)(-1));
        public const UInt16 CBN_SELCHANGE       = 1;
        public const UInt16 CBN_DBLCLK          = 2;
        public const UInt16 CBN_SETFOCUS        = 3;
        public const UInt16 CBN_KILLFOCUS       = 4;
        public const UInt16 CBN_EDITCHANGE      = 5;
        public const UInt16 CBN_EDITUPDATE      = 6;
        public const UInt16 CBN_DROPDOWN        = 7;
        public const UInt16 CBN_CLOSEUP         = 8;
        public const UInt16 CBN_SELENDOK        = 9;
        public const UInt16 CBN_SELENDCANCEL    = 10;


        #region WINSTYLES

        public const UInt32 CBS_SIMPLE            = 0x0001;
        public const UInt32 CBS_DROPDOWN          = 0x0002;
        public const UInt32 CBS_DROPDOWNLIST      = 0x0003;
        public const UInt32 CBS_OWNERDRAWFIXED    = 0x0010;
        public const UInt32 CBS_OWNERDRAWVARIABLE = 0x0020;
        public const UInt32 CBS_AUTOHSCROLL       = 0x0040;
        public const UInt32 CBS_OEMCONVERT        = 0x0080;
        public const UInt32 CBS_SORT              = 0x0100;
        public const UInt32 CBS_HASSTRINGS        = 0x0200;
        public const UInt32 CBS_NOINTEGRALHEIGHT  = 0x0400;
        public const UInt32 CBS_DISABLENOSCROLL   = 0x0800;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 CBS_UPPERCASE         = 0x2000;
        public const UInt32 CBS_LOWERCASE         = 0x4000;
#endif // WIN32_WINNT_NT4_LATER

        #endregion WINSTYLES


        #region WINMESSAGES

        public const UInt32 CB_GETEDITSEL               = 0x0140;
        public const UInt32 CB_LIMITTEXT                = 0x0141;
        public const UInt32 CB_SETEDITSEL               = 0x0142;
        public const UInt32 CB_ADDSTRING                = 0x0143;
        public const UInt32 CB_DELETESTRING             = 0x0144;
        public const UInt32 CB_DIR                      = 0x0145;
        public const UInt32 CB_GETCOUNT                 = 0x0146;
        public const UInt32 CB_GETCURSEL                = 0x0147;
        public const UInt32 CB_GETLBTEXT                = 0x0148;
        public const UInt32 CB_GETLBTEXTLEN             = 0x0149;
        public const UInt32 CB_INSERTSTRING             = 0x014A;
        public const UInt32 CB_RESETCONTENT             = 0x014B;
        public const UInt32 CB_FINDSTRING               = 0x014C;
        public const UInt32 CB_SELECTSTRING             = 0x014D;
        public const UInt32 CB_SETCURSEL                = 0x014E;
        public const UInt32 CB_SHOWDROPDOWN             = 0x014F;
        public const UInt32 CB_GETITEMDATA              = 0x0150;
        public const UInt32 CB_SETITEMDATA              = 0x0151;
        public const UInt32 CB_GETDROPPEDCONTROLRECT    = 0x0152;
        public const UInt32 CB_SETITEMHEIGHT            = 0x0153;
        public const UInt32 CB_GETITEMHEIGHT            = 0x0154;
        public const UInt32 CB_SETEXTENDEDUI            = 0x0155;
        public const UInt32 CB_GETEXTENDEDUI            = 0x0156;
        public const UInt32 CB_GETDROPPEDSTATE          = 0x0157;
        public const UInt32 CB_FINDSTRINGEXACT          = 0x0158;
        public const UInt32 CB_SETLOCALE                = 0x0159;
        public const UInt32 CB_GETLOCALE                = 0x015A;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 CB_GETTOPINDEX              = 0x015b;
        public const UInt32 CB_SETTOPINDEX              = 0x015c;
        public const UInt32 CB_GETHORIZONTALEXTENT      = 0x015d;
        public const UInt32 CB_SETHORIZONTALEXTENT      = 0x015e;
        public const UInt32 CB_GETDROPPEDWIDTH          = 0x015f;
        public const UInt32 CB_SETDROPPEDWIDTH          = 0x0160;
        public const UInt32 CB_INITSTORAGE              = 0x0161;
//#if defined(_WIN32_WCE) &&(_WIN32_WCE >= 0x0400)
//        public const UInt32 CB_MULTIPLEADDSTRING      = 0x0163;
//#endif
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 CB_GETCOMBOBOXINFO          = 0x0164;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 CB_MSGMAX                   = 0x0165;
//#elif defined(_WIN32_WCE) && (_WIN32_WCE >= 0x0400)
//        public const UInt32 CB_MSGMAX                 = 0x0163;
#elif WIN32_WINNT_NT4_LATER
        public const UInt32 CB_MSGMAX                   = 0x0162;
#else
        public const UInt32 CB_MSGMAX                   = 0x015B;
#endif

        #endregion WINMESSAGES


        #region WINSTYLES

        public const UInt32 SBS_HORZ                    = 0x0000;
        public const UInt32 SBS_VERT                    = 0x0001;
        public const UInt32 SBS_TOPALIGN                = 0x0002;
        public const UInt32 SBS_LEFTALIGN               = 0x0002;
        public const UInt32 SBS_BOTTOMALIGN             = 0x0004;
        public const UInt32 SBS_RIGHTALIGN              = 0x0004;
        public const UInt32 SBS_SIZEBOXTOPLEFTALIGN     = 0x0002;
        public const UInt32 SBS_SIZEBOXBOTTOMRIGHTALIGN = 0x0004;
        public const UInt32 SBS_SIZEBOX                 = 0x0008;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SBS_SIZEGRIP                = 0x0010;
#endif // WIN32_WINNT_NT4_LATER

        #endregion WINSTYLES


        #region WINMESSAGES

        public const UInt32 SBM_SETPOS                  = 0x00E0; /*not in win3.1 */
        public const UInt32 SBM_GETPOS                  = 0x00E1; /*not in win3.1 */
        public const UInt32 SBM_SETRANGE                = 0x00E2; /*not in win3.1 */
        public const UInt32 SBM_SETRANGEREDRAW          = 0x00E6; /*not in win3.1 */
        public const UInt32 SBM_GETRANGE                = 0x00E3; /*not in win3.1 */
        public const UInt32 SBM_ENABLE_ARROWS           = 0x00E4; /*not in win3.1 */
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SBM_SETSCROLLINFO           = 0x00E9;
        public const UInt32 SBM_GETSCROLLINFO           = 0x00EA;
#endif // WIN32_WINNT_NT4_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 SBM_GETSCROLLBARINFO        = 0x00EB;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_NT4_LATER
        public const UInt32 SIF_RANGE           = 0x0001;
        public const UInt32 SIF_PAGE            = 0x0002;
        public const UInt32 SIF_POS             = 0x0004;
        public const UInt32 SIF_DISABLENOSCROLL = 0x0008;
        public const UInt32 SIF_TRACKPOS        = 0x0010;
        public const UInt32 SIF_ALL             = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);


        public struct SCROLLINFO
        {
            public UInt32 cbSize;
            public UInt32 fMask;
            public Int32 nMin;
            public Int32 nMax;
            public UInt32 nPage;
            public Int32 nPos;
            public Int32 nTrackPos;
        }
        /*
        WINUSERAPI int WINAPI SetScrollInfo(
            _In_ HWND hwnd,
            _In_ int nBar,
            _In_ LPCSCROLLINFO lpsi,
            _In_ BOOL redraw);
        */
        /*
        WINUSERAPI BOOL WINAPI GetScrollInfo(
            _In_ HWND hwnd,
            _In_ int nBar,
            _Inout_ LPSCROLLINFO lpsi);
        */


#endif // WIN32_WINNT_NT4_LATER

        #endregion WINMESSAGES

            
        #endregion CTLMGR


        #region MDI

        public const UInt32 MDIS_ALLCHILDSTYLES    = 0x0001;

		public const UInt32 MDITILE_VERTICAL       = 0x0000; /*not in win3.1 */
		public const UInt32 MDITILE_HORIZONTAL     = 0x0001; /*not in win3.1 */
		public const UInt32 MDITILE_SKIPDISABLED   = 0x0002; /*not in win3.1 */
#if WIN32_WINNT_WIN2K_LATER
		public const UInt32 MDITILE_ZORDER         = 0x0004;
#endif // WIN32_WINNT_WIN2K_LATER


        public struct MDICREATESTRUCTA
        {
            public string szClass;
            public string szTitle;
            public IntPtr hOwner;
            public Int32 x;
            public Int32 y;
            public Int32 cx;
            public Int32 cy;
            public UInt32 style;
            public IntPtr lParam;        /* app-defined stuff */
        }

        public struct MDICREATESTRUCTW
        {
            public string szClass;
            public string szTitle;
            public IntPtr hOwner;
            public Int32 x;
            public Int32 y;
            public Int32 cx;
            public Int32 cy;
            public UInt32 style;
            public IntPtr lParam;        /* app-defined stuff */
        }

        public struct CLIENTCREATESTRUCT
        {
            public IntPtr hWindowMenu;
            public UInt32 idFirstChild;
        }
        /*
        WINUSERAPI LRESULT WINAPI DefFrameProcA(
            _In_ HWND hWnd,
            _In_opt_ HWND hWndMDIClient,
            _In_ UINT uMsg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);
            
        WINUSERAPI LRESULT WINAPI DefFrameProcW(
            _In_ HWND hWnd,
            _In_opt_ HWND hWndMDIClient,
            _In_ UINT uMsg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);
        */
        /*
        WINUSERAPI LRESULT WINAPI DefMDIChildProcA(
            _In_ HWND hWnd,
            _In_ UINT uMsg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);

        //WINUSERAPI LRESULT WINAPI DefMDIChildProcW(
            _In_ HWND hWnd,
            _In_ UINT uMsg,
            _In_ WPARAM wParam,
            _In_ LPARAM lParam);
        */


        #region MSG
        // WINUSERAPI BOOL WINAPI TranslateMDISysAccel(_In_ HWND hWndClient, _In_ LPMSG lpMsg);
        #endregion MSG


        // WINUSERAPI UINT WINAPI ArrangeIconicWindows(_In_ HWND hWnd);
        /*
        WINUSERAPI HWND WINAPI CreateMDIWindowA(
            _In_ LPCSTR lpClassName,
            _In_ LPCSTR lpWindowName,
            _In_ DWORD dwStyle,
            _In_ int X,
            _In_ int Y,
            _In_ int nWidth,
            _In_ int nHeight,
            _In_opt_ HWND hWndParent,
            _In_opt_ HINSTANCE hInstance,
            _In_ LPARAM lParam);
            
        WINUSERAPI HWND WINAPI CreateMDIWindowW(
            _In_ LPCWSTR lpClassName,
            _In_ LPCWSTR lpWindowName,
            _In_ DWORD dwStyle,
            _In_ int X,
            _In_ int Y,
            _In_ int nWidth,
            _In_ int nHeight,
            _In_opt_ HWND hWndParent,
            _In_opt_ HINSTANCE hInstance,
            _In_ LPARAM lParam);
        */

#if WIN32_WINNT_NT4_LATER
        /*
        WINUSERAPI WORD WINAPI TileWindows(
            _In_opt_ HWND hwndParent,
            _In_ UINT wHow,
            _In_opt_ CONST RECT* lpRect,
            _In_ UINT cKids,
            _In_reads_opt_(cKids) const HWND FAR * lpKids);
        */
        /*
        WINUSERAPI WORD WINAPI CascadeWindows(
            _In_opt_ HWND hwndParent,
            _In_ UINT wHow,
            _In_opt_ CONST RECT* lpRect,
            _In_ UINT cKids,
            _In_reads_opt_(cKids) const HWND FAR * lpKids);
        */
#endif // WIN32_WINNT_NT4_LATER

        #endregion MDI


//-----------------------------------------------------------------------------------------------------


        #region HELP

        // typedef DWORD HELPPOLY;

#if CSPORTING
        public struct MULTIKEYHELPA 
        {
            public UInt32  mkSize;
            public CHAR mkKeylist;
            public CHAR szKeyphrase[1];
        }

        public struct MULTIKEYHELPW
        {
            public UInt32 mkSize;
            public WCHAR mkKeylist;
            public WCHAR szKeyphrase[1];
        }

        public struct HELPWININFOA
        {
            int wStructSize;
            int x;
            int y;
            int dx;
            int dy;
            int wMax;
            CHAR rgchMember[2];
        }

        public struct HELPWININFOW
        {
            int wStructSize;
            int x;
            int y;
            int dx;
            int dy;
            int wMax;
            WCHAR rgchMember[2];
        }
#endif       

        public const UInt32 HELP_CONTEXT      = 0x0001;  /* Display topic in ulTopic */
        public const UInt32 HELP_QUIT         = 0x0002;  /* Terminate help */
        public const UInt32 HELP_INDEX        = 0x0003;  /* Display index */
        public const UInt32 HELP_CONTENTS     = 0x0003;
        public const UInt32 HELP_HELPONHELP   = 0x0004;  /* Display help on using help */
        public const UInt32 HELP_SETINDEX     = 0x0005;  /* Set current Index for multi index help */
        public const UInt32 HELP_SETCONTENTS  = 0x0005;
        public const UInt32 HELP_CONTEXTPOPUP = 0x0008;
        public const UInt32 HELP_FORCEFILE    = 0x0009;
        public const UInt32 HELP_KEY          = 0x0101;  /* Display topic for keyword in offabData */
        public const UInt32 HELP_COMMAND      = 0x0102;
        public const UInt32 HELP_PARTIALKEY   = 0x0105;
        public const UInt32 HELP_MULTIKEY     = 0x0201;
        public const UInt32 HELP_SETWINPOS    = 0x0203;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 HELP_CONTEXTMENU  = 0x000a;
        public const UInt32 HELP_FINDER       = 0x000b;
        public const UInt32 HELP_WM_HELP      = 0x000c;
        public const UInt32 HELP_SETPOPUP_POS = 0x000d;

        public const UInt32 HELP_TCARD              = 0x8000;
        public const UInt32 HELP_TCARD_DATA         = 0x0010;
        public const UInt32 HELP_TCARD_OTHER_CALLER = 0x0011;

        // These are in winhelp.h in Win95.
        //#define IDH_NO_HELP                     28440
        //#define IDH_MISSING_CONTEXT             28441 // Control doesn't have matching help context
        //#define IDH_GENERIC_HELP_BUTTON         28442 // Property sheet help button
        //#define IDH_OK                          28443
        //#define IDH_CANCEL                      28444
        //#define IDH_HELP                        28445

#endif // WIN32_WINNT_NT4_LATER


        /*
        WINUSERAPI BOOL WINAPI WinHelpA(
            _In_opt_ HWND hWndMain,
            _In_opt_ LPCSTR lpszHelp,
            _In_ UINT uCommand,
            _In_ ULONG_PTR dwData);

        WINUSERAPI BOOL WINAPI WinHelpW(
            _In_opt_ HWND hWndMain,
            _In_opt_ LPCWSTR lpszHelp,
            _In_ UINT uCommand,
            _In_ ULONG_PTR dwData);
        */

        #endregion HELP


#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 GR_GDIOBJECTS       = 0;       /* Count of GDI objects */
        public const UInt32 GR_USEROBJECTS      = 1;       /* Count of USER objects */
#endif // WIN32_WINNT_WIN2K_LATER
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 GR_GDIOBJECTS_PEAK  = 2;       /* Peak count of GDI objects */
        public const UInt32 GR_USEROBJECTS_PEAK = 4;       /* Peak count of USER objects */
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 GR_GLOBAL           = unchecked((UInt32)(-2)); //((HANDLE)-2)
#endif // WIN32_WINNT_WIN7_LATER


#if WIN32_WINNT_WIN2K_LATER
        // WINUSERAPI DWORD WINAPI GetGuiResources(_In_ HANDLE hProcess, _In_ DWORD uiFlags);
#endif // WIN32_WINNT_WIN2K_LATER


        #region SYSPARAMSINFO

        public const UInt32 SPI_GETBEEP                 = 0x0001;
        public const UInt32 SPI_SETBEEP                 = 0x0002;
        public const UInt32 SPI_GETMOUSE                = 0x0003;
        public const UInt32 SPI_SETMOUSE                = 0x0004;
        public const UInt32 SPI_GETBORDER               = 0x0005;
        public const UInt32 SPI_SETBORDER               = 0x0006;
        public const UInt32 SPI_GETKEYBOARDSPEED        = 0x000A;
        public const UInt32 SPI_SETKEYBOARDSPEED        = 0x000B;
        public const UInt32 SPI_LANGDRIVER              = 0x000C;
        public const UInt32 SPI_ICONHORIZONTALSPACING   = 0x000D;
        public const UInt32 SPI_GETSCREENSAVETIMEOUT    = 0x000E;
        public const UInt32 SPI_SETSCREENSAVETIMEOUT    = 0x000F;
        public const UInt32 SPI_GETSCREENSAVEACTIVE     = 0x0010;
        public const UInt32 SPI_SETSCREENSAVEACTIVE     = 0x0011;
        public const UInt32 SPI_GETGRIDGRANULARITY      = 0x0012;
        public const UInt32 SPI_SETGRIDGRANULARITY      = 0x0013;
        public const UInt32 SPI_SETDESKWALLPAPER        = 0x0014;
        public const UInt32 SPI_SETDESKPATTERN          = 0x0015;
        public const UInt32 SPI_GETKEYBOARDDELAY        = 0x0016;
        public const UInt32 SPI_SETKEYBOARDDELAY        = 0x0017;
        public const UInt32 SPI_ICONVERTICALSPACING     = 0x0018;
        public const UInt32 SPI_GETICONTITLEWRAP        = 0x0019;
        public const UInt32 SPI_SETICONTITLEWRAP        = 0x001A;
        public const UInt32 SPI_GETMENUDROPALIGNMENT    = 0x001B;
        public const UInt32 SPI_SETMENUDROPALIGNMENT    = 0x001C;
        public const UInt32 SPI_SETDOUBLECLKWIDTH       = 0x001D;
        public const UInt32 SPI_SETDOUBLECLKHEIGHT      = 0x001E;
        public const UInt32 SPI_GETICONTITLELOGFONT     = 0x001F;
        public const UInt32 SPI_SETDOUBLECLICKTIME      = 0x0020;
        public const UInt32 SPI_SETMOUSEBUTTONSWAP      = 0x0021;
        public const UInt32 SPI_SETICONTITLELOGFONT     = 0x0022;
        public const UInt32 SPI_GETFASTTASKSWITCH       = 0x0023;
        public const UInt32 SPI_SETFASTTASKSWITCH       = 0x0024;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SPI_SETDRAGFULLWINDOWS      = 0x0025;
        public const UInt32 SPI_GETDRAGFULLWINDOWS      = 0x0026;
        public const UInt32 SPI_GETNONCLIENTMETRICS     = 0x0029;
        public const UInt32 SPI_SETNONCLIENTMETRICS     = 0x002A;
        public const UInt32 SPI_GETMINIMIZEDMETRICS     = 0x002B;
        public const UInt32 SPI_SETMINIMIZEDMETRICS     = 0x002C;
        public const UInt32 SPI_GETICONMETRICS          = 0x002D;
        public const UInt32 SPI_SETICONMETRICS          = 0x002E;
        public const UInt32 SPI_SETWORKAREA             = 0x002F;
        public const UInt32 SPI_GETWORKAREA             = 0x0030;
        public const UInt32 SPI_SETPENWINDOWS           = 0x0031;

        public const UInt32 SPI_GETHIGHCONTRAST         = 0x0042;
        public const UInt32 SPI_SETHIGHCONTRAST         = 0x0043;
        public const UInt32 SPI_GETKEYBOARDPREF         = 0x0044;
        public const UInt32 SPI_SETKEYBOARDPREF         = 0x0045;
        public const UInt32 SPI_GETSCREENREADER         = 0x0046;
        public const UInt32 SPI_SETSCREENREADER         = 0x0047;
        public const UInt32 SPI_GETANIMATION            = 0x0048;
        public const UInt32 SPI_SETANIMATION            = 0x0049;
        public const UInt32 SPI_GETFONTSMOOTHING        = 0x004A;
        public const UInt32 SPI_SETFONTSMOOTHING        = 0x004B;
        public const UInt32 SPI_SETDRAGWIDTH            = 0x004C;
        public const UInt32 SPI_SETDRAGHEIGHT           = 0x004D;
        public const UInt32 SPI_SETHANDHELD             = 0x004E;
        public const UInt32 SPI_GETLOWPOWERTIMEOUT      = 0x004F;
        public const UInt32 SPI_GETPOWEROFFTIMEOUT      = 0x0050;
        public const UInt32 SPI_SETLOWPOWERTIMEOUT      = 0x0051;
        public const UInt32 SPI_SETPOWEROFFTIMEOUT      = 0x0052;
        public const UInt32 SPI_GETLOWPOWERACTIVE       = 0x0053;
        public const UInt32 SPI_GETPOWEROFFACTIVE       = 0x0054;
        public const UInt32 SPI_SETLOWPOWERACTIVE       = 0x0055;
        public const UInt32 SPI_SETPOWEROFFACTIVE       = 0x0056;
        public const UInt32 SPI_SETCURSORS              = 0x0057;
        public const UInt32 SPI_SETICONS                = 0x0058;
        public const UInt32 SPI_GETDEFAULTINPUTLANG     = 0x0059;
        public const UInt32 SPI_SETDEFAULTINPUTLANG     = 0x005A;
        public const UInt32 SPI_SETLANGTOGGLE           = 0x005B;
        public const UInt32 SPI_GETWINDOWSEXTENSION     = 0x005C;
        public const UInt32 SPI_SETMOUSETRAILS          = 0x005D;
        public const UInt32 SPI_GETMOUSETRAILS          = 0x005E;
        public const UInt32 SPI_SETSCREENSAVERRUNNING   = 0x0061;
        public const UInt32 SPI_SCREENSAVERRUNNING      = SPI_SETSCREENSAVERRUNNING;
#endif // WIN32_WINNT_NT4_LATER
        public const UInt32 SPI_GETFILTERKEYS          = 0x0032;
        public const UInt32 SPI_SETFILTERKEYS          = 0x0033;
        public const UInt32 SPI_GETTOGGLEKEYS          = 0x0034;
        public const UInt32 SPI_SETTOGGLEKEYS          = 0x0035;
        public const UInt32 SPI_GETMOUSEKEYS           = 0x0036;
        public const UInt32 SPI_SETMOUSEKEYS           = 0x0037;
        public const UInt32 SPI_GETSHOWSOUNDS          = 0x0038;
        public const UInt32 SPI_SETSHOWSOUNDS          = 0x0039;
        public const UInt32 SPI_GETSTICKYKEYS          = 0x003A;
        public const UInt32 SPI_SETSTICKYKEYS          = 0x003B;
        public const UInt32 SPI_GETACCESSTIMEOUT       = 0x003C;
        public const UInt32 SPI_SETACCESSTIMEOUT       = 0x003D;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SPI_GETSERIALKEYS          = 0x003E;
        public const UInt32 SPI_SETSERIALKEYS          = 0x003F;
#endif // WIN32_WINNT_NT4_LATER
        public const UInt32 SPI_GETSOUNDSENTRY         = 0x0040;
        public const UInt32 SPI_SETSOUNDSENTRY         = 0x0041;
#if WIN32_WINNT_NT4_LATER
        public const UInt32 SPI_GETSNAPTODEFBUTTON     = 0x005F;
        public const UInt32 SPI_SETSNAPTODEFBUTTON     = 0x0060;
#endif // WIN32_WINNT_NT4_LATER
#if WIN32_WINNT_NT4_LATER //(_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
        public const UInt32 SPI_GETMOUSEHOVERWIDTH     = 0x0062;
        public const UInt32 SPI_SETMOUSEHOVERWIDTH     = 0x0063;
        public const UInt32 SPI_GETMOUSEHOVERHEIGHT    = 0x0064;
        public const UInt32 SPI_SETMOUSEHOVERHEIGHT    = 0x0065;
        public const UInt32 SPI_GETMOUSEHOVERTIME      = 0x0066;
        public const UInt32 SPI_SETMOUSEHOVERTIME      = 0x0067;
        public const UInt32 SPI_GETWHEELSCROLLLINES    = 0x0068;
        public const UInt32 SPI_SETWHEELSCROLLLINES    = 0x0069;
        public const UInt32 SPI_GETMENUSHOWDELAY       = 0x006A;
        public const UInt32 SPI_SETMENUSHOWDELAY       = 0x006B;
#if WIN32_WINNT_VISTA_LATER // >= 0x0600)
        public const UInt32 SPI_GETWHEELSCROLLCHARS   = 0x006C;
        public const UInt32 SPI_SETWHEELSCROLLCHARS   = 0x006D;
#endif
        public const UInt32 SPI_GETSHOWIMEUI          = 0x006E;
        public const UInt32 SPI_SETSHOWIMEUI          = 0x006F;
#endif


#if WIN32_WINNT_WIN2K_LATER //(WINVER >= 0x0500)
        public const UInt32 SPI_GETMOUSESPEED         = 0x0070;
        public const UInt32 SPI_SETMOUSESPEED         = 0x0071;
        public const UInt32 SPI_GETSCREENSAVERRUNNING = 0x0072;
        public const UInt32 SPI_GETDESKWALLPAPER      = 0x0073;
#endif // WINVER >= 0x0500

#if WIN32_WINNT_VISTA_LATER // (WINVER >= 0x0600)
        public const UInt32 SPI_GETAUDIODESCRIPTION   = 0x0074;
        public const UInt32 SPI_SETAUDIODESCRIPTION   = 0x0075;

        public const UInt32 SPI_GETSCREENSAVESECURE   = 0x0076;
        public const UInt32 SPI_SETSCREENSAVESECURE   = 0x0077;
#endif // WINVER >= 0x0600

#if WIN32_WINNT_WIN7_LATER //(_WIN32_WINNT >= 0x0601)
        public const UInt32 SPI_GETHUNGAPPTIMEOUT           = 0x0078;
        public const UInt32 SPI_SETHUNGAPPTIMEOUT           = 0x0079;
        public const UInt32 SPI_GETWAITTOKILLTIMEOUT        = 0x007A;
        public const UInt32 SPI_SETWAITTOKILLTIMEOUT        = 0x007B;
        public const UInt32 SPI_GETWAITTOKILLSERVICETIMEOUT = 0x007C;
        public const UInt32 SPI_SETWAITTOKILLSERVICETIMEOUT = 0x007D;
        public const UInt32 SPI_GETMOUSEDOCKTHRESHOLD       = 0x007E;
        public const UInt32 SPI_SETMOUSEDOCKTHRESHOLD       = 0x007F;
        public const UInt32 SPI_GETPENDOCKTHRESHOLD         = 0x0080;
        public const UInt32 SPI_SETPENDOCKTHRESHOLD         = 0x0081;
        public const UInt32 SPI_GETWINARRANGING             = 0x0082;
        public const UInt32 SPI_SETWINARRANGING             = 0x0083;
        public const UInt32 SPI_GETMOUSEDRAGOUTTHRESHOLD    = 0x0084;
        public const UInt32 SPI_SETMOUSEDRAGOUTTHRESHOLD    = 0x0085;
        public const UInt32 SPI_GETPENDRAGOUTTHRESHOLD      = 0x0086;
        public const UInt32 SPI_SETPENDRAGOUTTHRESHOLD      = 0x0087;
        public const UInt32 SPI_GETMOUSESIDEMOVETHRESHOLD   = 0x0088;
        public const UInt32 SPI_SETMOUSESIDEMOVETHRESHOLD   = 0x0089;
        public const UInt32 SPI_GETPENSIDEMOVETHRESHOLD     = 0x008A;
        public const UInt32 SPI_SETPENSIDEMOVETHRESHOLD     = 0x008B;
        public const UInt32 SPI_GETDRAGFROMMAXIMIZE         = 0x008C;
        public const UInt32 SPI_SETDRAGFROMMAXIMIZE         = 0x008D;
        public const UInt32 SPI_GETSNAPSIZING               = 0x008E;
        public const UInt32 SPI_SETSNAPSIZING               = 0x008F;
        public const UInt32 SPI_GETDOCKMOVING               = 0x0090;
        public const UInt32 SPI_SETDOCKMOVING               = 0x0091;
#endif // _WIN32_WINNT >= 0x0601

#if WIN32_WINNT_WIN8_LATER //(WINVER >= 0x0602)

        //#define MAX_TOUCH_PREDICTION_FILTER_TAPS 3


        public struct TOUCHPREDICTIONPARAMETERS
        {
            public UInt32 cbSize;
            public UInt32 dwLatency;       // Latency in millisecs
            public UInt32 dwSampleTime;    // Sample time in millisecs (used to deduce velocity)
            public UInt32 bUseHWTimeStamp; // Use H/W TimeStamps
        }
        
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_LATENCY 8
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_SAMPLETIME 8
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_USE_HW_TIMESTAMP 1
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_RLS_DELTA 0.001f
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_RLS_LAMBDA_MIN 0.9f
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_RLS_LAMBDA_MAX 0.999f
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_RLS_LAMBDA_LEARNING_RATE 0.001f
        //#define TOUCHPREDICTIONPARAMETERS_DEFAULT_RLS_EXPO_SMOOTH_ALPHA 0.99f
        

        public const UInt32 SPI_GETTOUCHPREDICTIONPARAMETERS = 0x009C;
        public const UInt32 SPI_SETTOUCHPREDICTIONPARAMETERS = 0x009D;

        //#define MAX_LOGICALDPIOVERRIDE  2
        //#define MIN_LOGICALDPIOVERRIDE  -2

        public const UInt32 SPI_GETLOGICALDPIOVERRIDE       = 0x009E;
        public const UInt32 SPI_SETLOGICALDPIOVERRIDE       = 0x009F;


        public const UInt32 SPI_GETMENURECT   = 0x00A2;
        public const UInt32 SPI_SETMENURECT   = 0x00A3;

#endif // WINVER >= 0x0602


#if WIN32_WINNT_WIN2K_LATER //(WINVER >= 0x0500)
        public const UInt32 SPI_GETACTIVEWINDOWTRACKING         = 0x1000;
        public const UInt32 SPI_SETACTIVEWINDOWTRACKING         = 0x1001;
        public const UInt32 SPI_GETMENUANIMATION                = 0x1002;
        public const UInt32 SPI_SETMENUANIMATION                = 0x1003;
        public const UInt32 SPI_GETCOMBOBOXANIMATION            = 0x1004;
        public const UInt32 SPI_SETCOMBOBOXANIMATION            = 0x1005;
        public const UInt32 SPI_GETLISTBOXSMOOTHSCROLLING       = 0x1006;
        public const UInt32 SPI_SETLISTBOXSMOOTHSCROLLING       = 0x1007;
        public const UInt32 SPI_GETGRADIENTCAPTIONS             = 0x1008;
        public const UInt32 SPI_SETGRADIENTCAPTIONS             = 0x1009;
        public const UInt32 SPI_GETKEYBOARDCUES                 = 0x100A;
        public const UInt32 SPI_SETKEYBOARDCUES                 = 0x100B;
        public const UInt32 SPI_GETMENUUNDERLINES               = SPI_GETKEYBOARDCUES;
        public const UInt32 SPI_SETMENUUNDERLINES               = SPI_SETKEYBOARDCUES;
        public const UInt32 SPI_GETACTIVEWNDTRKZORDER           = 0x100C;
        public const UInt32 SPI_SETACTIVEWNDTRKZORDER           = 0x100D;
        public const UInt32 SPI_GETHOTTRACKING                  = 0x100E;
        public const UInt32 SPI_SETHOTTRACKING                  = 0x100F;
        public const UInt32 SPI_GETMENUFADE                     = 0x1012;
        public const UInt32 SPI_SETMENUFADE                     = 0x1013;
        public const UInt32 SPI_GETSELECTIONFADE                = 0x1014;
        public const UInt32 SPI_SETSELECTIONFADE                = 0x1015;
        public const UInt32 SPI_GETTOOLTIPANIMATION             = 0x1016;
        public const UInt32 SPI_SETTOOLTIPANIMATION             = 0x1017;
        public const UInt32 SPI_GETTOOLTIPFADE                  = 0x1018;
        public const UInt32 SPI_SETTOOLTIPFADE                  = 0x1019;
        public const UInt32 SPI_GETCURSORSHADOW                 = 0x101A;
        public const UInt32 SPI_SETCURSORSHADOW                 = 0x101B;
#if WIN32_WINNT_WINXP_LATER // (_WIN32_WINNT >= 0x0501)
        public const UInt32 SPI_GETMOUSESONAR                   = 0x101C;
        public const UInt32 SPI_SETMOUSESONAR                   = 0x101D;
        public const UInt32 SPI_GETMOUSECLICKLOCK               = 0x101E;
        public const UInt32 SPI_SETMOUSECLICKLOCK               = 0x101F;
        public const UInt32 SPI_GETMOUSEVANISH                  = 0x1020;
        public const UInt32 SPI_SETMOUSEVANISH                  = 0x1021;
        public const UInt32 SPI_GETFLATMENU                     = 0x1022;
        public const UInt32 SPI_SETFLATMENU                     = 0x1023;
        public const UInt32 SPI_GETDROPSHADOW                   = 0x1024;
        public const UInt32 SPI_SETDROPSHADOW                   = 0x1025;
        public const UInt32 SPI_GETBLOCKSENDINPUTRESETS         = 0x1026;
        public const UInt32 SPI_SETBLOCKSENDINPUTRESETS         = 0x1027;
#endif // _WIN32_WINNT >= 0x0501

        public const UInt32 SPI_GETUIEFFECTS                    = 0x103E;
        public const UInt32 SPI_SETUIEFFECTS                    = 0x103F;

#if WIN32_WINNT_VISTA_LATER //(_WIN32_WINNT >= 0x0600)
        public const UInt32 SPI_GETDISABLEOVERLAPPEDCONTENT     = 0x1040;
        public const UInt32 SPI_SETDISABLEOVERLAPPEDCONTENT     = 0x1041;
        public const UInt32 SPI_GETCLIENTAREAANIMATION          = 0x1042;
        public const UInt32 SPI_SETCLIENTAREAANIMATION          = 0x1043;
        public const UInt32 SPI_GETCLEARTYPE                    = 0x1048;
        public const UInt32 SPI_SETCLEARTYPE                    = 0x1049;
        public const UInt32 SPI_GETSPEECHRECOGNITION            = 0x104A;
        public const UInt32 SPI_SETSPEECHRECOGNITION            = 0x104B;
#endif // _WIN32_WINNT >= 0x0600

#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)
        public const UInt32 SPI_GETCARETBROWSING                = 0x104C;
        public const UInt32 SPI_SETCARETBROWSING                = 0x104D;
        public const UInt32 SPI_GETTHREADLOCALINPUTSETTINGS     = 0x104E;
        public const UInt32 SPI_SETTHREADLOCALINPUTSETTINGS     = 0x104F;
        public const UInt32 SPI_GETSYSTEMLANGUAGEBAR            = 0x1050;
        public const UInt32 SPI_SETSYSTEMLANGUAGEBAR            = 0x1051;
#endif // WINVER >= 0x0601

        public const UInt32 SPI_GETFOREGROUNDLOCKTIMEOUT        = 0x2000;
        public const UInt32 SPI_SETFOREGROUNDLOCKTIMEOUT        = 0x2001;
        public const UInt32 SPI_GETACTIVEWNDTRKTIMEOUT          = 0x2002;
        public const UInt32 SPI_SETACTIVEWNDTRKTIMEOUT          = 0x2003;
        public const UInt32 SPI_GETFOREGROUNDFLASHCOUNT         = 0x2004;
        public const UInt32 SPI_SETFOREGROUNDFLASHCOUNT         = 0x2005;
        public const UInt32 SPI_GETCARETWIDTH                   = 0x2006;
        public const UInt32 SPI_SETCARETWIDTH                   = 0x2007;

#if WIN32_WINNT_WINXP_LATER //(_WIN32_WINNT >= 0x0501)
        public const UInt32 SPI_GETMOUSECLICKLOCKTIME           = 0x2008;
        public const UInt32 SPI_SETMOUSECLICKLOCKTIME           = 0x2009;
        public const UInt32 SPI_GETFONTSMOOTHINGTYPE            = 0x200A;
        public const UInt32 SPI_SETFONTSMOOTHINGTYPE            = 0x200B;

        /* constants for SPI_GETFONTSMOOTHINGTYPE and SPI_SETFONTSMOOTHINGTYPE: */
        //#define FE_FONTSMOOTHINGSTANDARD            0x0001
        //#define FE_FONTSMOOTHINGCLEARTYPE           0x0002

        public const UInt32 SPI_GETFONTSMOOTHINGCONTRAST           = 0x200C;
        public const UInt32 SPI_SETFONTSMOOTHINGCONTRAST           = 0x200D;

        public const UInt32 SPI_GETFOCUSBORDERWIDTH             = 0x200E;
        public const UInt32 SPI_SETFOCUSBORDERWIDTH             = 0x200F;
        public const UInt32 SPI_GETFOCUSBORDERHEIGHT            = 0x2010;
        public const UInt32 SPI_SETFOCUSBORDERHEIGHT            = 0x2011;

        public const UInt32 SPI_GETFONTSMOOTHINGORIENTATION           = 0x2012;
        public const UInt32 SPI_SETFONTSMOOTHINGORIENTATION           = 0x2013;

        //#define FE_FONTSMOOTHINGORIENTATIONBGR   0x0000
        //#define FE_FONTSMOOTHINGORIENTATIONRGB   0x0001
#endif // _WIN32_WINNT >= 0x0501

#if WIN32_WINNT_VISTA_LATER //(_WIN32_WINNT >= 0x0600)
        public const UInt32 SPI_GETMINIMUMHITRADIUS             = 0x2014;
        public const UInt32 SPI_SETMINIMUMHITRADIUS             = 0x2015;
        public const UInt32 SPI_GETMESSAGEDURATION              = 0x2016;
        public const UInt32 SPI_SETMESSAGEDURATION              = 0x2017;
#endif // _WIN32_WINNT >= 0x0600

#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0602)
        public const UInt32 SPI_GETCONTACTVISUALIZATION         = 0x2018;
        public const UInt32 SPI_SETCONTACTVISUALIZATION         = 0x2019;

        //#define CONTACTVISUALIZATION_OFF                 0x0000
        //#define CONTACTVISUALIZATION_ON                  0x0001
        //#define CONTACTVISUALIZATION_PRESENTATIONMODE    0x0002

        public const UInt32 SPI_GETGESTUREVISUALIZATION         = 0x201A;
        public const UInt32 SPI_SETGESTUREVISUALIZATION         = 0x201B;

        //#define GESTUREVISUALIZATION_OFF                 0x0000
        //#define GESTUREVISUALIZATION_ON                  0x001F
        //#define GESTUREVISUALIZATION_TAP                 0x0001
        //#define GESTUREVISUALIZATION_DOUBLETAP           0x0002
        //#define GESTUREVISUALIZATION_PRESSANDTAP         0x0004
        //#define GESTUREVISUALIZATION_PRESSANDHOLD        0x0008
        //#define GESTUREVISUALIZATION_RIGHTTAP            0x0010
#endif // WINVER >= 0x0602

#if WIN32_WINNT_WIN8_LATER //(WINVER >= 0x0602)
        public const UInt32 SPI_GETMOUSEWHEELROUTING            = 0x201C;
        public const UInt32 SPI_SETMOUSEWHEELROUTING            = 0x201D;

        //#define MOUSEWHEEL_ROUTING_FOCUS                  0
        //#define MOUSEWHEEL_ROUTING_HYBRID                 1
#if WIN32_WINNT_WINBLUE_LATER //(WINVER >= 0x0603)
        //#define MOUSEWHEEL_ROUTING_MOUSE_POS              2
#endif // WINVER >= 0x0603
#endif // WINVER >= 0x0602

#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0604)
        public const UInt32 SPI_GETPENVISUALIZATION                  = 0x201E;
        public const UInt32 SPI_SETPENVISUALIZATION                  = 0x201F;
        /* constants for SPI_{GET|SET}PENVISUALIZATION */
        //#define PENVISUALIZATION_ON                      0x0023
        //#define PENVISUALIZATION_OFF                     0x0000
        //#define PENVISUALIZATION_TAP                     0x0001
        //#define PENVISUALIZATION_DOUBLETAP               0x0002
        //#define PENVISUALIZATION_CURSOR                  0x0020

        public const UInt32 SPI_GETPENARBITRATIONTYPE                = 0x2020;
        public const UInt32 SPI_SETPENARBITRATIONTYPE                = 0x2021;
        /* constants for SPI_{GET|SET}PENARBITRATIONTYPE */
        //#define PENARBITRATIONTYPE_NONE                  0x0000
        //#define PENARBITRATIONTYPE_WIN8                  0x0001
        //#define PENARBITRATIONTYPE_FIS                   0x0002
        //#define PENARBITRATIONTYPE_SPT                   0x0003
        //#define PENARBITRATIONTYPE_MAX                   0x0004
#endif // WINVER >= 0x0604

#endif // WINVER >= 0x0500

        //#define SPIF_UPDATEINIFILE    0x0001
        //#define SPIF_SENDWININICHANGE 0x0002
        //#define SPIF_SENDCHANGE       SPIF_SENDWININICHANGE


        //#define METRICS_USEDEFAULT -1
# if _WINGDI_
# if !NOGDI

        /*
        public struct NONCLIENTMETRICSA
        {
            public UInt32    cbSize;
            public Int32     iBorderWidth;
            public Int32     iScrollWidth;
            public Int32     iScrollHeight;
            public Int32     iCaptionWidth;
            public Int32     iCaptionHeight;
            public WinGdi.LOGFONTA lfCaptionFont;
            public Int32     iSmCaptionWidth;
            public Int32     iSmCaptionHeight;
            public WinGdi.LOGFONTA lfSmCaptionFont;
            public Int32     iMenuWidth;
            public Int32     iMenuHeight;
            public WinGdi.LOGFONTA lfMenuFont;
            public WinGdi.LOGFONTA lfStatusFont;
            public WinGdi.LOGFONTA lfMessageFont;
#if WIN32_WINNT_VISTA_LATER //(WINVER >= 0x0600)
            public Int32     iPaddedBorderWidth;
#endif // WINVER >= 0x0600 
        }

        public struct NONCLIENTMETRICSW
        {
            public UInt32    cbSize;
            public Int32     iBorderWidth;
            public Int32     iScrollWidth;
            public Int32     iScrollHeight;
            public Int32     iCaptionWidth;
            public Int32     iCaptionHeight;
            public WinGdi.LOGFONTW lfCaptionFont;
            public Int32     iSmCaptionWidth;
            public Int32     iSmCaptionHeight;
            public WinGdi.LOGFONTW lfSmCaptionFont;
            public Int32     iMenuWidth;
            public Int32     iMenuHeight;
            public WinGdi.LOGFONTW lfMenuFont;
            public WinGdi.LOGFONTW lfStatusFont;
            public WinGdi.LOGFONTW lfMessageFont;
#if WIN32_WINNT_VISTA_LATER //(WINVER >= 0x0600)
            public Int32     iPaddedBorderWidth;
#endif // WINVER >= 0x0600 
        }   
        */


#endif // NOGDI
#endif // _WINGDI_

        public const Int32 ARW_BOTTOMLEFT              = 0x0000;
        public const Int32 ARW_BOTTOMRIGHT             = 0x0001;
        public const Int32 ARW_TOPLEFT                 = 0x0002;
        public const Int32 ARW_TOPRIGHT                = 0x0003;
        public const Int32 ARW_STARTMASK               = 0x0003;
        public const Int32 ARW_STARTRIGHT              = 0x0001;
        public const Int32 ARW_STARTTOP                = 0x0002;

        public const Int32 ARW_LEFT                    = 0x0000;
        public const Int32 ARW_RIGHT                   = 0x0000;
        public const Int32 ARW_UP                      = 0x0004;
        public const Int32 ARW_DOWN                    = 0x0004;
        public const Int32 ARW_HIDE                    = 0x0008;


        public struct MINIMIZEDMETRICS
        {
            public UInt32    cbSize;
            public Int32     iWidth;
            public Int32     iHorzGap;
            public Int32     iVertGap;
            public Int32     iArrange;
        }   
        
#if _WINGDI_
#if !NOGDI
        /*
        public struct ICONMETRICSA
        {
            public UInt32    cbSize;
            public Int32     iHorzSpacing;
            public Int32     iVertSpacing;
            public Int32     iTitleWrap;
            public WinGdi.LOGFONTA  lfFont;
        }   

        public struct ICONMETRICSW
        {
            public UInt32    cbSize;
            public Int32     iHorzSpacing;
            public Int32     iVertSpacing;
            public Int32     iTitleWrap;
            public WinGdi.LOGFONTW  lfFont;
        }   
        */

#endif // NOGDI
#endif // _WINGDI_

        public struct ANIMATIONINFO
        {
            public UInt32 cbSize;
            public Int32 iMinAnimate;
        }

        public struct SERIALKEYSA
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public string lpszActivePort;
            public string lpszPort;
            public UInt32 iBaudRate;
            public UInt32 iPortState;
            public UInt32 iActive;
        }

        public struct SERIALKEYSW
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public string lpszActivePort;
            public string lpszPort;
            public UInt32 iBaudRate;
            public UInt32 iPortState;
            public UInt32 iActive;
        }

        public const UInt32 SERKF_SERIALKEYSON  = 0x00000001;
        public const UInt32 SERKF_AVAILABLE     = 0x00000002;
        public const UInt32 SERKF_INDICATOR     = 0x00000004;


        public struct HIGHCONTRASTA
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public string lpszDefaultScheme;
        }

        public struct HIGHCONTRASTW
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public string lpszDefaultScheme;
        }


        public const UInt32 HCF_HIGHCONTRASTON  = 0x00000001;
        public const UInt32 HCF_AVAILABLE       = 0x00000002;
        public const UInt32 HCF_HOTKEYACTIVE    = 0x00000004;
        public const UInt32 HCF_CONFIRMHOTKEY   = 0x00000008;
        public const UInt32 HCF_HOTKEYSOUND     = 0x00000010;
        public const UInt32 HCF_INDICATOR       = 0x00000020;
        public const UInt32 HCF_HOTKEYAVAILABLE = 0x00000040;
        public const UInt32 HCF_LOGONDESKTOP    = 0x00000100;
        public const UInt32 HCF_DEFAULTDESKTOP  = 0x00000200;

        public const UInt32 CDS_UPDATEREGISTRY           = 0x00000001;
        public const UInt32 CDS_TEST                     = 0x00000002;
        public const UInt32 CDS_FULLSCREEN               = 0x00000004;
        public const UInt32 CDS_GLOBAL                   = 0x00000008;
        public const UInt32 CDS_SET_PRIMARY              = 0x00000010;
        public const UInt32 CDS_VIDEOPARAMETERS          = 0x00000020;
#if WIN32_WINNT_VISTA_LATER //(WINVER >= 0x0600)
        public const UInt32 CDS_ENABLE_UNSAFE_MODES      = 0x00000100;
        public const UInt32 CDS_DISABLE_UNSAFE_MODES     = 0x00000200;
#endif // WINVER >= 0x0600
        public const UInt32 CDS_RESET                    = 0x40000000;
        public const UInt32 CDS_RESET_EX                 = 0x20000000;
        public const UInt32 CDS_NORESET                  = 0x10000000;

//#include <tvout.h>

        public const Int32 DISP_CHANGE_SUCCESSFUL       = 0;
        public const Int32 DISP_CHANGE_RESTART          = 1;
        public const Int32 DISP_CHANGE_FAILED           = -1;
        public const Int32 DISP_CHANGE_BADMODE          = -2;
        public const Int32 DISP_CHANGE_NOTUPDATED       = -3;
        public const Int32 DISP_CHANGE_BADFLAGS         = -4;
        public const Int32 DISP_CHANGE_BADPARAM         = -5;
#if WIN32_WINNT_WINXP_LATER // >= 0x0501)
        public const Int32 DISP_CHANGE_BADDUALVIEW      = -6;
#endif // _WIN32_WINNT >= 0x0501


#if _WINGDI_
#if !NOGDI

        // WINUSERAPI LONG WINAPI ChangeDisplaySettingsA(_In_opt_ DEVMODEA* lpDevMode, _In_ DWORD dwFlags);
        // WINUSERAPI LONG WINAPI ChangeDisplaySettingsW(_In_opt_ DEVMODEW* lpDevMode, _In_ DWORD dwFlags);

        /*
        WINUSERAPI LONG WINAPI ChangeDisplaySettingsExA(
            _In_opt_ LPCSTR lpszDeviceName,
            _In_opt_ DEVMODEA* lpDevMode,
            _Reserved_ HWND hwnd,
            _In_ DWORD dwflags,
            _In_opt_ LPVOID lParam);
            
        WINUSERAPI LONG WINAPI ChangeDisplaySettingsExW(
            _In_opt_ LPCWSTR lpszDeviceName,
            _In_opt_ DEVMODEW* lpDevMode,
            _Reserved_ HWND hwnd,
            _In_ DWORD dwflags,
            _In_opt_ LPVOID lParam);
        */

        public const Int32 ENUM_CURRENT_SETTINGS       = (-1);
        public const Int32 ENUM_REGISTRY_SETTINGS      = (-2);
        /*
        WINUSERAPI BOOL WINAPI EnumDisplaySettingsA(
            _In_opt_ LPCSTR lpszDeviceName,
            _In_ DWORD iModeNum,
            _Inout_ DEVMODEA* lpDevMode);
            
        WINUSERAPI BOOL WINAPI EnumDisplaySettingsW(
            _In_opt_ LPCWSTR lpszDeviceName,
            _In_ DWORD iModeNum,
            _Inout_ DEVMODEW* lpDevMode);
        */


#if WIN32_WINNT_WIN2K_LATER //(WINVER >= 0x0500)
        /*
        WINUSERAPI BOOL WINAPI EnumDisplaySettingsExA(
            _In_opt_ LPCSTR lpszDeviceName,
            _In_ DWORD iModeNum,
            _Inout_ DEVMODEA* lpDevMode,
            _In_ DWORD dwFlags);

        WINUSERAPI BOOL WINAPI EnumDisplaySettingsExW(
            _In_opt_ LPCWSTR lpszDeviceName,
            _In_ DWORD iModeNum,
            _Inout_ DEVMODEW* lpDevMode,
            _In_ DWORD dwFlags);
        */

        public const UInt32 EDS_RAWMODE                   = 0x00000002;
        public const UInt32 EDS_ROTATEDMODE               = 0x00000004;
        /*
        WINUSERAPI BOOL WINAPI EnumDisplayDevicesA(
            _In_opt_ LPCSTR lpDevice,
            _In_ DWORD iDevNum,
            _Inout_ PDISPLAY_DEVICEA lpDisplayDevice,
            _In_ DWORD dwFlags);
            
        WINUSERAPI BOOL WINAPI EnumDisplayDevicesW(
            _In_opt_ LPCWSTR lpDevice,
            _In_ DWORD iDevNum,
            _Inout_ PDISPLAY_DEVICEW lpDisplayDevice,
            _In_ DWORD dwFlags);
        */

        public const UInt32 EDD_GET_DEVICE_INTERFACE_NAME = 0x00000001;

#endif // WINVER >= 0x0500 */

#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)
        /*
        WINUSERAPI LONG WINAPI GetDisplayConfigBufferSizes(
            _In_ UINT32 flags,
            _Out_ UINT32* numPathArrayElements,
            _Out_ UINT32* numModeInfoArrayElements);
        */
        /*
        WINUSERAPI LONG WINAPI SetDisplayConfig(
            _In_ UINT32 numPathArrayElements,
            _In_reads_opt_(numPathArrayElements) DISPLAYCONFIG_PATH_INFO* pathArray,
            _In_ UINT32 numModeInfoArrayElements,
            _In_reads_opt_(numModeInfoArrayElements) DISPLAYCONFIG_MODE_INFO* modeInfoArray,
            _In_ UINT32 flags);
        */
        /*
        WINUSERAPI _Success_(return == ERROR_SUCCESS) LONG WINAPI QueryDisplayConfig(
            _In_ UINT32 flags,
            _Inout_ UINT32* numPathArrayElements,
            _Out_writes_to_(*numPathArrayElements, *numPathArrayElements) DISPLAYCONFIG_PATH_INFO* pathArray,
            _Inout_ UINT32* numModeInfoArrayElements,
            _Out_writes_to_(*numModeInfoArrayElements, *numModeInfoArrayElements) DISPLAYCONFIG_MODE_INFO* modeInfoArray,
            _When_(!(flags & QDC_DATABASE_CURRENT), _Pre_null_)
            _When_(flags & QDC_DATABASE_CURRENT, _Out_)
            DISPLAYCONFIG_TOPOLOGY_ID* currentTopologyId);
        */
        // WINUSERAPI LONG WINAPI DisplayConfigGetDeviceInfo(_Inout_ DISPLAYCONFIG_DEVICE_INFO_HEADER* requestPacket);

        // WINUSERAPI LONG WINAPI DisplayConfigSetDeviceInfo(_In_ DISPLAYCONFIG_DEVICE_INFO_HEADER* setPacket);

#endif // WINVER >= 0x0601


#endif // NOGDI
#endif // _WINGDI_

        /*
        WINUSERAPI _Success_(return != FALSE) BOOL WINAPI SystemParametersInfoA(
            _In_ UINT uiAction,
            _In_ UINT uiParam,
            _Pre_maybenull_ _Post_valid_ PVOID pvParam,
            _In_ UINT fWinIni);
            
        WINUSERAPI _Success_(return != FALSE) BOOL WINAPI SystemParametersInfoW(
            _In_ UINT uiAction,
            _In_ UINT uiParam,
            _Pre_maybenull_ _Post_valid_ PVOID pvParam,
            _In_ UINT fWinIni);
        */

#if WIN32_WINNT_WIN10_LATER // (WINVER >= 0x0605)
        /*
        WINUSERAPI _Success_(return != FALSE) BOOL WINAPI SystemParametersInfoForDpi(
            _In_ UINT uiAction,
            _In_ UINT uiParam,
            _Pre_maybenull_ _Post_valid_ PVOID pvParam,
            _In_ UINT fWinIni,
            _In_ UINT dpi);
        */
#endif // WINVER >= 0x0605


        #endregion SYSPARAMSINFO


        public struct FILTERKEYS
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public UInt32 iWaitMSec;            // Acceptance Delay
            public UInt32 iDelayMSec;           // Delay Until Repeat
            public UInt32 iRepeatMSec;          // Repeat Rate
            public UInt32 iBounceMSec;          // Debounce Time
        }


        public const UInt32 FKF_FILTERKEYSON    = 0x00000001;
        public const UInt32 FKF_AVAILABLE       = 0x00000002;
        public const UInt32 FKF_HOTKEYACTIVE    = 0x00000004;
        public const UInt32 FKF_CONFIRMHOTKEY   = 0x00000008;
        public const UInt32 FKF_HOTKEYSOUND     = 0x00000010;
        public const UInt32 FKF_INDICATOR       = 0x00000020;
        public const UInt32 FKF_CLICKON         = 0x00000040;


        public struct STICKYKEYS
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
        } 


        public const UInt32 SKF_STICKYKEYSON    = 0x00000001;
        public const UInt32 SKF_AVAILABLE       = 0x00000002;
        public const UInt32 SKF_HOTKEYACTIVE    = 0x00000004;
        public const UInt32 SKF_CONFIRMHOTKEY   = 0x00000008;
        public const UInt32 SKF_HOTKEYSOUND     = 0x00000010;
        public const UInt32 SKF_INDICATOR       = 0x00000020;
        public const UInt32 SKF_AUDIBLEFEEDBACK = 0x00000040;
        public const UInt32 SKF_TRISTATE        = 0x00000080;
        public const UInt32 SKF_TWOKEYSOFF      = 0x00000100;
#if WIN32_WINNT_WIN2K_LATER //(_WIN32_WINNT >= 0x0500)
        public const UInt32 SKF_LALTLATCHED       = 0x10000000;
        public const UInt32 SKF_LCTLLATCHED       = 0x04000000;
        public const UInt32 SKF_LSHIFTLATCHED     = 0x01000000;
        public const UInt32 SKF_RALTLATCHED       = 0x20000000;
        public const UInt32 SKF_RCTLLATCHED       = 0x08000000;
        public const UInt32 SKF_RSHIFTLATCHED     = 0x02000000;
        public const UInt32 SKF_LWINLATCHED       = 0x40000000;
        public const UInt32 SKF_RWINLATCHED       = 0x80000000;
        public const UInt32 SKF_LALTLOCKED        = 0x00100000;
        public const UInt32 SKF_LCTLLOCKED        = 0x00040000;
        public const UInt32 SKF_LSHIFTLOCKED      = 0x00010000;
        public const UInt32 SKF_RALTLOCKED        = 0x00200000;
        public const UInt32 SKF_RCTLLOCKED        = 0x00080000;
        public const UInt32 SKF_RSHIFTLOCKED      = 0x00020000;
        public const UInt32 SKF_LWINLOCKED        = 0x00400000;
        public const UInt32 SKF_RWINLOCKED        = 0x00800000;
#endif // _WIN32_WINNT >= 0x0500


        public struct MOUSEKEYS
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public UInt32 iMaxSpeed;
            public UInt32 iTimeToMaxSpeed;
            public UInt32 iCtrlSpeed;
            public UInt32 dwReserved1;
            public UInt32 dwReserved2;
        }


        public const UInt32 MKF_MOUSEKEYSON     = 0x00000001;
        public const UInt32 MKF_AVAILABLE       = 0x00000002;
        public const UInt32 MKF_HOTKEYACTIVE    = 0x00000004;
        public const UInt32 MKF_CONFIRMHOTKEY   = 0x00000008;
        public const UInt32 MKF_HOTKEYSOUND     = 0x00000010;
        public const UInt32 MKF_INDICATOR       = 0x00000020;
        public const UInt32 MKF_MODIFIERS       = 0x00000040;
        public const UInt32 MKF_REPLACENUMBERS  = 0x00000080;
#if WIN32_WINNT_WIN2K_LATER
        public const UInt32 MKF_LEFTBUTTONSEL   = 0x10000000;
        public const UInt32 MKF_RIGHTBUTTONSEL  = 0x20000000;
        public const UInt32 MKF_LEFTBUTTONDOWN  = 0x01000000;
        public const UInt32 MKF_RIGHTBUTTONDOWN = 0x02000000;
        public const UInt32 MKF_MOUSEMODE       = 0x80000000;
#endif // WIN32_WINNT_WIN2K_LATER


        public struct ACCESSTIMEOUT
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public UInt32 iTimeOutMSec;
        } 


        public const UInt32 ATF_TIMEOUTON       = 0x00000001;
        public const UInt32 ATF_ONOFFFEEDBACK   = 0x00000002;

        public const UInt32 SSGF_NONE       = 0;
        public const UInt32 SSGF_DISPLAY    = 3;

        public const UInt32 SSTF_NONE       = 0;
        public const UInt32 SSTF_CHARS      = 1;
        public const UInt32 SSTF_BORDER     = 2;
        public const UInt32 SSTF_DISPLAY    = 3;

        public const UInt32 SSWF_NONE     = 0;
        public const UInt32 SSWF_TITLE    = 1;
        public const UInt32 SSWF_WINDOW   = 2;
        public const UInt32 SSWF_DISPLAY  = 3;
        public const UInt32 SSWF_CUSTOM   = 4;


        /*
        public struct SOUNDSENTRYA
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public UInt32 iFSTextEffect;
            public UInt32 iFSTextEffectMSec;
            public UInt32 iFSTextEffectColorBits;
            public UInt32 iFSGrafEffect;
            public UInt32 iFSGrafEffectMSec;
            public UInt32 iFSGrafEffectColor;
            public UInt32 iWindowsEffect;
            public UInt32 iWindowsEffectMSec;
            public string lpszWindowsEffectDLL;
            public UInt32 iWindowsEffectOrdinal;
        } 

        public struct SOUNDSENTRYW
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
            public UInt32 iFSTextEffect;
            public UInt32 iFSTextEffectMSec;
            public UInt32 iFSTextEffectColorBits;
            public UInt32 iFSGrafEffect;
            public UInt32 iFSGrafEffectMSec;
            public UInt32 iFSGrafEffectColor;
            public UInt32 iWindowsEffect;
            public UInt32 iWindowsEffectMSec;
            public string lpszWindowsEffectDLL;
            public UInt32 iWindowsEffectOrdinal;
        } 
        */


        public const UInt32 SSF_SOUNDSENTRYON   = 0x00000001;
        public const UInt32 SSF_AVAILABLE       = 0x00000002;
        public const UInt32 SSF_INDICATOR       = 0x00000004;


#if WIN32_WINNT_VISTA_LATER //(_WIN32_WINNT >= 0x0600)
        //WINUSERAPI BOOL WINAPI SoundSentry(VOID);
#endif // WIN32_WINNT_VISTA_LATER


        public struct TOGGLEKEYS
        {
            public UInt32 cbSize;
            public UInt32 dwFlags;
        }


        public const UInt32 TKF_TOGGLEKEYSON    = 0x00000001;
        public const UInt32 TKF_AVAILABLE       = 0x00000002;
        public const UInt32 TKF_HOTKEYACTIVE    = 0x00000004;
        public const UInt32 TKF_CONFIRMHOTKEY   = 0x00000008;
        public const UInt32 TKF_HOTKEYSOUND     = 0x00000010;
        public const UInt32 TKF_INDICATOR       = 0x00000020;


#if WIN32_WINNT_VISTA_LATER //(_WIN32_WINNT >= 0x0600)
#if CSPORTING
        public  struct AUDIODESCRIPTION 
        {
            public UInt32 cbSize;   // sizeof(AudioDescriptionType)
            bool Enabled;  // On/Off
            LCID Locale;   // locale ID for language
        } 
#endif // CSPORTING
#endif // WIN32_WINNT_VISTA_LATER


        // WINUSERAPI VOID WINAPI SetDebugErrorLevel(_In_ DWORD dwLevel);


        public const UInt32 SLE_ERROR       = 0x00000001;
        public const UInt32 SLE_MINORERROR  = 0x00000002;
        public const UInt32 SLE_WARNING     = 0x00000003;


        //WINUSERAPI  VOID WINAPI SetLastErrorEx( _In_ DWORD dwErrCode, _In_ DWORD dwType);
        /*
        WINUSERAPI int WINAPI InternalGetWindowText(
            _In_ HWND hWnd,
            _Out_writes_to_(cchMaxCount, return + 1) LPWSTR pString,
            _In_ int cchMaxCount);
        */

#if WIN32_WINNT_NT4_LATER //defined(WINNT)
        /*
        WINUSERAPI BOOL WINAPI EndTask(
            _In_ HWND hWnd,
            _In_ BOOL fShutDown,
            _In_ BOOL fForce);
        */
#endif

        // WINUSERAPI BOOL WINAPI CancelShutdown(VOID);


#if WIN32_WINNT_WIN2K_LATER //(WINVER >= 0x0500)

        public const UInt32 MONITOR_DEFAULTTONULL       = 0x00000000;
        public const UInt32 MONITOR_DEFAULTTOPRIMARY    = 0x00000001;
        public const UInt32 MONITOR_DEFAULTTONEAREST    = 0x00000002;


        // WINUSERAPI HMONITOR WINAPI MonitorFromPoint(_In_ POINT pt, _In_ DWORD dwFlags);

        // WINUSERAPI HMONITOR WINAPI MonitorFromRect(_In_ LPCRECT lprc, _In_ DWORD dwFlags);

        // WINUSERAPI HMONITOR WINAPI MonitorFromWindow(_In_ HWND hwnd, _In_ DWORD dwFlags);


        public const UInt32 MONITORINFOF_PRIMARY        = 0x00000001;

//#ifndef CCHDEVICENAME
//#define CCHDEVICENAME 32
//#endif


        public struct MONITORINFO
        {
            public UInt32   cbSize;
            public WinDef.RECT    rcMonitor;
            public WinDef.RECT    rcWork;
            public UInt32   dwFlags;
        }

#if CSPORTING
        public  struct MONITORINFOEXA
        {
            public MONITORINFO DUMMYSTRUCTNAME;
            public Byte szDevice[CCHDEVICENAME];
        }
        public struct MONITORINFOEXW
        {
            public MONITORINFO DUMMYSTRUCTNAME;
            public Char szDevice[CCHDEVICENAME];
        }
#endif // CSPORTING
        /*
        WINUSERAPI BOOL WINAPI GetMonitorInfoA(
            _In_ HMONITOR hMonitor,
            _Inout_ LPMONITORINFO lpmi);

        WINUSERAPI BOOL WINAPI GetMonitorInfoW(
            _In_ HMONITOR hMonitor,
            _Inout_ LPMONITORINFO lpmi);
        */

        //typedef BOOL(CALLBACK* MONITORENUMPROC)(HMONITOR, HDC, LPRECT, LPARAM);
        /*
        WINUSERAPI BOOL WINAPI EnumDisplayMonitors(
            _In_opt_ HDC hdc,
            _In_opt_ LPCRECT lprcClip,
            _In_ MONITORENUMPROC lpfnEnum,
            _In_ LPARAM dwData);
        */


        #region NOWINABLE

        /*
        WINUSERAPI VOID WINAPI NotifyWinEvent(
            _In_ DWORD event,
            _In_ HWND  hwnd,
            _In_ LONG  idObject,
            _In_ LONG  idChild);
        */
        /*
        typedef VOID (CALLBACK* WINEVENTPROC)(
            HWINEVENTHOOK hWinEventHook,
            DWORD         event,
            HWND          hwnd,
            LONG          idObject,
            LONG          idChild,
            DWORD         idEventThread,
            DWORD         dwmsEventTime);
        */
        /*
        WINUSERAPI HWINEVENTHOOK WINAPI SetWinEventHook(
            _In_ DWORD eventMin,
            _In_ DWORD eventMax,
            _In_opt_ HMODULE hmodWinEventProc,
            _In_ WINEVENTPROC pfnWinEventProc,
            _In_ DWORD idProcess,
            _In_ DWORD idThread,
            _In_ DWORD dwFlags);
        */
#if WIN32_WINNT_WINXP_LATER //(_WIN32_WINNT >= 0x0501)
        // WINUSERAPI BOOL WINAPI IsWinEventHookInstalled(_In_ DWORD event);
#endif // WIN32_WINNT_WINXP_LATER


        public const UInt32 WINEVENT_OUTOFCONTEXT   = 0x0000;  // Events are ASYNC
        public const UInt32 WINEVENT_SKIPOWNTHREAD  = 0x0001;  // Don't call back for events on installer's thread
        public const UInt32 WINEVENT_SKIPOWNPROCESS = 0x0002;  // Don't call back for events on installer's process
        public const UInt32 WINEVENT_INCONTEXT      = 0x0004;  // Events are SYNC, this causes your dll to be injected into every process


        // WINUSERAPI BOOL WINAPI UnhookWinEvent(_In_ HWINEVENTHOOK hWinEventHook);


        public const Int32 CHILDID_SELF        = 0;
        public const Int32 INDEXID_OBJECT      = 0;
        public const Int32 INDEXID_CONTAINER   = 0;

        public const UInt32 OBJID_WINDOW        = (0x00000000);
        public const UInt32 OBJID_SYSMENU       = (0xFFFFFFFF);
        public const UInt32 OBJID_TITLEBAR      = (0xFFFFFFFE);
        public const UInt32 OBJID_MENU          = (0xFFFFFFFD);
        public const UInt32 OBJID_CLIENT        = (0xFFFFFFFC);
        public const UInt32 OBJID_VSCROLL       = (0xFFFFFFFB);
        public const UInt32 OBJID_HSCROLL       = (0xFFFFFFFA);
        public const UInt32 OBJID_SIZEGRIP      = (0xFFFFFFF9);
        public const UInt32 OBJID_CARET         = (0xFFFFFFF8);
        public const UInt32 OBJID_CURSOR        = (0xFFFFFFF7);
        public const UInt32 OBJID_ALERT         = (0xFFFFFFF6);
        public const UInt32 OBJID_SOUND         = (0xFFFFFFF5);
        public const UInt32 OBJID_QUERYCLASSNAMEIDX = (0xFFFFFFF4);
        public const UInt32 OBJID_NATIVEOM      = (0xFFFFFFF0);

        public const UInt32 EVENT_MIN           = 0x00000001;
        public const UInt32 EVENT_MAX           = 0x7FFFFFFF;

        public const UInt32 EVENT_SYSTEM_SOUND              = 0x0001;

        public const UInt32 EVENT_SYSTEM_ALERT              = 0x0002;

        public const UInt32 EVENT_SYSTEM_FOREGROUND         = 0x0003;

        public const UInt32 EVENT_SYSTEM_MENUSTART          = 0x0004;
        public const UInt32 EVENT_SYSTEM_MENUEND            = 0x0005;

        public const UInt32 EVENT_SYSTEM_MENUPOPUPSTART     = 0x0006;
        public const UInt32 EVENT_SYSTEM_MENUPOPUPEND       = 0x0007;

        public const UInt32 EVENT_SYSTEM_CAPTURESTART       = 0x0008;
        public const UInt32 EVENT_SYSTEM_CAPTUREEND         = 0x0009;

        public const UInt32 EVENT_SYSTEM_MOVESIZESTART      = 0x000A;
        public const UInt32 EVENT_SYSTEM_MOVESIZEEND        = 0x000B;

        public const UInt32 EVENT_SYSTEM_CONTEXTHELPSTART   = 0x000C;
        public const UInt32 EVENT_SYSTEM_CONTEXTHELPEND     = 0x000D;

        public const UInt32 EVENT_SYSTEM_DRAGDROPSTART      = 0x000E;
        public const UInt32 EVENT_SYSTEM_DRAGDROPEND        = 0x000F;

        public const UInt32 EVENT_SYSTEM_DIALOGSTART        = 0x0010;
        public const UInt32 EVENT_SYSTEM_DIALOGEND          = 0x0011;

        public const UInt32 EVENT_SYSTEM_SCROLLINGSTART     = 0x0012;
        public const UInt32 EVENT_SYSTEM_SCROLLINGEND       = 0x0013;

        public const UInt32 EVENT_SYSTEM_SWITCHSTART        = 0x0014;
        public const UInt32 EVENT_SYSTEM_SWITCHEND          = 0x0015;

        public const UInt32 EVENT_SYSTEM_MINIMIZESTART      = 0x0016;
        public const UInt32 EVENT_SYSTEM_MINIMIZEEND        = 0x0017;

#if WIN32_WINNT_VISTA_LATER
        public const UInt32 EVENT_SYSTEM_DESKTOPSWITCH      = 0x0020;
#endif // WIN32_WINNT_VISTA_LATER


#if WIN32_WINNT_WIN8_LATER

        public const UInt32 EVENT_SYSTEM_SWITCHER_APPGRABBED    = 0x0024;

        public const UInt32 EVENT_SYSTEM_SWITCHER_APPOVERTARGET = 0x0025;

        public const UInt32 EVENT_SYSTEM_SWITCHER_APPDROPPED    = 0x0026;

        public const UInt32 EVENT_SYSTEM_SWITCHER_CANCELLED     = 0x0027;

#endif // WIN32_WINNT_WIN8_LATER


#if WIN32_WINNT_WIN8_LATER

        public const UInt32 EVENT_SYSTEM_IME_KEY_NOTIFICATION  = 0x0029;

#endif // WIN32_WINNT_WIN8_LATER


#if WIN32_WINNT_WIN7_LATER
        public const UInt32 EVENT_SYSTEM_END        = 0x00FF;

        public const UInt32 EVENT_OEM_DEFINED_START     = 0x0101;
        public const UInt32 EVENT_OEM_DEFINED_END       = 0x01FF;

        public const UInt32 EVENT_UIA_EVENTID_START         = 0x4E00;
        public const UInt32 EVENT_UIA_EVENTID_END           = 0x4EFF;

        public const UInt32 EVENT_UIA_PROPID_START          = 0x7500;
        public const UInt32 EVENT_UIA_PROPID_END            = 0x75FF;
#endif // WIN32_WINNT_WIN7_LATER

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 EVENT_CONSOLE_CARET             = 0x4001;
        public const UInt32 EVENT_CONSOLE_UPDATE_REGION     = 0x4002;
        public const UInt32 EVENT_CONSOLE_UPDATE_SIMPLE     = 0x4003;
        public const UInt32 EVENT_CONSOLE_UPDATE_SCROLL     = 0x4004;
        public const UInt32 EVENT_CONSOLE_LAYOUT            = 0x4005;
        public const UInt32 EVENT_CONSOLE_START_APPLICATION = 0x4006;
        public const UInt32 EVENT_CONSOLE_END_APPLICATION   = 0x4007;

#if _WIN64
        public const UInt32 CONSOLE_APPLICATION_16BIT       = 0x0000;
#else
        public const UInt32 CONSOLE_APPLICATION_16BIT       = 0x0001;
#endif

        public const UInt32 CONSOLE_CARET_SELECTION         = 0x0001;
        public const UInt32 CONSOLE_CARET_VISIBLE           = 0x0002;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 EVENT_CONSOLE_END       = 0x40FF;
#endif // WIN32_WINNT_WIN7_LATER

        public const UInt32 EVENT_OBJECT_CREATE                 = 0x8000;  // hwnd + ID + idChild is created item
        public const UInt32 EVENT_OBJECT_DESTROY                = 0x8001;  // hwnd + ID + idChild is destroyed item
        public const UInt32 EVENT_OBJECT_SHOW                   = 0x8002;  // hwnd + ID + idChild is shown item
        public const UInt32 EVENT_OBJECT_HIDE                   = 0x8003;  // hwnd + ID + idChild is hidden item
        public const UInt32 EVENT_OBJECT_REORDER                = 0x8004;  // hwnd + ID + idChild is parent of zordering children

        public const UInt32 EVENT_OBJECT_FOCUS                  = 0x8005;  // hwnd + ID + idChild is focused item
        public const UInt32 EVENT_OBJECT_SELECTION              = 0x8006;  // hwnd + ID + idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex
        public const UInt32 EVENT_OBJECT_SELECTIONADD           = 0x8007;  // hwnd + ID + idChild is item added
        public const UInt32 EVENT_OBJECT_SELECTIONREMOVE        = 0x8008;  // hwnd + ID + idChild is item removed
        public const UInt32 EVENT_OBJECT_SELECTIONWITHIN        = 0x8009;  // hwnd + ID + idChild is parent of changed selected items

        public const UInt32 EVENT_OBJECT_STATECHANGE            = 0x800A;  // hwnd + ID + idChild is item w/ state change
        public const UInt32 EVENT_OBJECT_LOCATIONCHANGE         = 0x800B;  // hwnd + ID + idChild is moved/sized item

        public const UInt32 EVENT_OBJECT_NAMECHANGE             = 0x800C;  // hwnd + ID + idChild is item w/ name change
        public const UInt32 EVENT_OBJECT_DESCRIPTIONCHANGE      = 0x800D;  // hwnd + ID + idChild is item w/ desc change
        public const UInt32 EVENT_OBJECT_VALUECHANGE            = 0x800E;  // hwnd + ID + idChild is item w/ value change
        public const UInt32 EVENT_OBJECT_PARENTCHANGE           = 0x800F;  // hwnd + ID + idChild is item w/ new parent
        public const UInt32 EVENT_OBJECT_HELPCHANGE             = 0x8010;  // hwnd + ID + idChild is item w/ help change
        public const UInt32 EVENT_OBJECT_DEFACTIONCHANGE        = 0x8011;  // hwnd + ID + idChild is item w/ def action change
        public const UInt32 EVENT_OBJECT_ACCELERATORCHANGE      = 0x8012;  // hwnd + ID + idChild is item w/ keybd accel change

#if WIN32_WINNT_VISTA_LATER
        public const UInt32 EVENT_OBJECT_INVOKED                = 0x8013;  // hwnd + ID + idChild is item invoked
        public const UInt32 EVENT_OBJECT_TEXTSELECTIONCHANGED   = 0x8014;  // hwnd + ID + idChild is item w? test selection change

        public const UInt32 EVENT_OBJECT_CONTENTSCROLLED        = 0x8015;
#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 EVENT_SYSTEM_ARRANGMENTPREVIEW      = 0x8016;
#endif // WIN32_WINNT_WIN7_LATER

#if WIN32_WINNT_WIN8_LATER
        public const UInt32 EVENT_OBJECT_CLOAKED                = 0x8017;
        public const UInt32 EVENT_OBJECT_UNCLOAKED              = 0x8018;

        public const UInt32 EVENT_OBJECT_LIVEREGIONCHANGED      = 0x8019;

        public const UInt32 EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED = 0x8020;

        public const UInt32 EVENT_OBJECT_DRAGSTART              = 0x8021;
        public const UInt32 EVENT_OBJECT_DRAGCANCEL             = 0x8022;
        public const UInt32 EVENT_OBJECT_DRAGCOMPLETE           = 0x8023;

        public const UInt32 EVENT_OBJECT_DRAGENTER              = 0x8024;
        public const UInt32 EVENT_OBJECT_DRAGLEAVE              = 0x8025;
        public const UInt32 EVENT_OBJECT_DRAGDROPPED            = 0x8026;

        public const UInt32 EVENT_OBJECT_IME_SHOW               = 0x8027;
        public const UInt32 EVENT_OBJECT_IME_HIDE               = 0x8028;

        public const UInt32 EVENT_OBJECT_IME_CHANGE             = 0x8029;

        public const UInt32 EVENT_OBJECT_TEXTEDIT_CONVERSIONTARGETCHANGED = 0x8030;

#endif // WIN32_WINNT_WIN8_LATER

#if WIN32_WINNT_WIN7_LATER
        public const UInt32 EVENT_OBJECT_END                    = 0x80FF;

        public const UInt32 EVENT_AIA_START                     = 0xA000;
        public const UInt32 EVENT_AIA_END                       = 0xAFFF;
#endif // WIN32_WINNT_WIN7_LATER

        public const UInt32 SOUND_SYSTEM_STARTUP            = 1;
        public const UInt32 SOUND_SYSTEM_SHUTDOWN           = 2;
        public const UInt32 SOUND_SYSTEM_BEEP               = 3;
        public const UInt32 SOUND_SYSTEM_ERROR              = 4;
        public const UInt32 SOUND_SYSTEM_QUESTION           = 5;
        public const UInt32 SOUND_SYSTEM_WARNING            = 6;
        public const UInt32 SOUND_SYSTEM_INFORMATION        = 7;
        public const UInt32 SOUND_SYSTEM_MAXIMIZE           = 8;
        public const UInt32 SOUND_SYSTEM_MINIMIZE           = 9;
        public const UInt32 SOUND_SYSTEM_RESTOREUP          = 10;
        public const UInt32 SOUND_SYSTEM_RESTOREDOWN        = 11;
        public const UInt32 SOUND_SYSTEM_APPSTART           = 12;
        public const UInt32 SOUND_SYSTEM_FAULT              = 13;
        public const UInt32 SOUND_SYSTEM_APPEND             = 14;
        public const UInt32 SOUND_SYSTEM_MENUCOMMAND        = 15;
        public const UInt32 SOUND_SYSTEM_MENUPOPUP          = 16;
        public const UInt32 CSOUND_SYSTEM                   = 16;

        public const UInt32 ALERT_SYSTEM_INFORMATIONAL      = 1;       // MB_INFORMATION
        public const UInt32 ALERT_SYSTEM_WARNING            = 2;       // MB_WARNING
        public const UInt32 ALERT_SYSTEM_ERROR              = 3;       // MB_ERROR
        public const UInt32 ALERT_SYSTEM_QUERY              = 4;       // MB_QUESTION
        public const UInt32 ALERT_SYSTEM_CRITICAL           = 5;       // HardSysErrBox
        public const UInt32 CALERT_SYSTEM                   = 6;


        public struct GUITHREADINFO
        {
            public UInt32   cbSize;
            public UInt32   flags;
            public IntPtr    hwndActive;
            public IntPtr    hwndFocus;
            public IntPtr    hwndCapture;
            public IntPtr    hwndMenuOwner;
            public IntPtr    hwndMoveSize;
            public IntPtr    hwndCaret;
            public WinDef.RECT    rcCaret;
        } 


        public const UInt32 GUI_CARETBLINKING   = 0x00000001;
        public const UInt32 GUI_INMOVESIZE      = 0x00000002;
        public const UInt32 GUI_INMENUMODE      = 0x00000004;
        public const UInt32 GUI_SYSTEMMENUMODE  = 0x00000008;
        public const UInt32 GUI_POPUPMENUMODE   = 0x00000010;
#if WIN32_WINNT_WINXP_LATER
#if _WIN64
        public const UInt32 GUI_16BITTASK       = 0x00000000;
#else
        public const UInt32 GUI_16BITTASK       = 0x00000020;
#endif
#endif // WIN32_WINNT_WINXP_LATER


        // WINUSERAPI BOOL WINAPI GetGUIThreadInfo(_In_ DWORD idThread, _Inout_ PGUITHREADINFO pgui);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO lpgui);

        // WINUSERAPI BOOL WINAPI BlockInput(BOOL fBlockIt);

#if WIN32_WINNT_VISTA_LATER

        public const UInt32 USER_DEFAULT_SCREEN_DPI = 96;

        // WINUSERAPI BOOL WINAPI SetProcessDPIAware(VOID);

        // WINUSERAPI BOOL WINAPI IsProcessDPIAware(VOID);

#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0605)

        // WINUSERAPI DPI_AWARENESS_CONTEXT WINAPI SetThreadDpiAwarenessContext(_In_ DPI_AWARENESS_CONTEXT dpiContext);

        // WINUSERAPI DPI_AWARENESS_CONTEXT WINAPI GetThreadDpiAwarenessContext(VOID);

        // WINUSERAPI DPI_AWARENESS_CONTEXT WINAPI GetWindowDpiAwarenessContext(_In_ HWND hwnd);

        // WINUSERAPI DPI_AWARENESS WINAPI GetAwarenessFromDpiAwarenessContext(_In_ DPI_AWARENESS_CONTEXT value);

        // WINUSERAPI BOOL WINAPI AreDpiAwarenessContextsEqual(_In_ DPI_AWARENESS_CONTEXT dpiContextA, _In_ DPI_AWARENESS_CONTEXT dpiContextB);

        // WINUSERAPI BOOL WINAPI IsValidDpiAwarenessContext(_In_ DPI_AWARENESS_CONTEXT value);

        // WINUSERAPI UINT WINAPI GetDpiForWindow(_In_ HWND hwnd);

        // WINUSERAPI UINT WINAPI GetDpiForSystem(VOID);

        // WINUSERAPI BOOL WINAPI EnableNonClientDpiScaling(_In_ HWND hwnd);

        // WINUSERAPI BOOL WINAPI InheritWindowMonitor(_In_ HWND hwnd, _In_opt_ HWND hwndInherit);

#endif // WIN32_WINNT_WIN10_LATER (WINVER >= 0x0605)

#if WIN32_WINNT_WIN10_LATER //(WINVER >= 0x0605)

        // WINUSERAPI BOOL WINAPI SetProcessDpiAwarenessContext(_In_ DPI_AWARENESS_CONTEXT value);

#endif // WIN32_WINNT_WIN10_LATER (WINVER >= 0x0605)

        /*
        WINUSERAPI UINT WINAPI GetWindowModuleFileNameA(
            _In_ HWND hwnd,
            _Out_writes_to_(cchFileNameMax, return) LPSTR pszFileName,
            _In_ UINT cchFileNameMax);
            
        WINUSERAPI UINT WINAPI GetWindowModuleFileNameW(
            _In_ HWND hwnd,
            _Out_writes_to_(cchFileNameMax, return) LPWSTR pszFileName,
            _In_ UINT cchFileNameMax);
        */


        #region STATE_FLAGS

        public const UInt32 STATE_SYSTEM_UNAVAILABLE        = 0x00000001;  // Disabled
        public const UInt32 STATE_SYSTEM_SELECTED           = 0x00000002;
        public const UInt32 STATE_SYSTEM_FOCUSED            = 0x00000004;
        public const UInt32 STATE_SYSTEM_PRESSED            = 0x00000008;
        public const UInt32 STATE_SYSTEM_CHECKED            = 0x00000010;
        public const UInt32 STATE_SYSTEM_MIXED              = 0x00000020;  // 3-state checkbox or toolbar button
        public const UInt32 STATE_SYSTEM_INDETERMINATE      = STATE_SYSTEM_MIXED;
        public const UInt32 STATE_SYSTEM_READONLY           = 0x00000040;
        public const UInt32 STATE_SYSTEM_HOTTRACKED         = 0x00000080;
        public const UInt32 STATE_SYSTEM_DEFAULT            = 0x00000100;
        public const UInt32 STATE_SYSTEM_EXPANDED           = 0x00000200;
        public const UInt32 STATE_SYSTEM_COLLAPSED          = 0x00000400;
        public const UInt32 STATE_SYSTEM_BUSY               = 0x00000800;
        public const UInt32 STATE_SYSTEM_FLOATING           = 0x00001000;  // Children "owned" not "contained" by parent
        public const UInt32 STATE_SYSTEM_MARQUEED           = 0x00002000;
        public const UInt32 STATE_SYSTEM_ANIMATED           = 0x00004000;
        public const UInt32 STATE_SYSTEM_INVISIBLE          = 0x00008000;
        public const UInt32 STATE_SYSTEM_OFFSCREEN          = 0x00010000;
        public const UInt32 STATE_SYSTEM_SIZEABLE           = 0x00020000;
        public const UInt32 STATE_SYSTEM_MOVEABLE           = 0x00040000;
        public const UInt32 STATE_SYSTEM_SELFVOICING        = 0x00080000;
        public const UInt32 STATE_SYSTEM_FOCUSABLE          = 0x00100000;
        public const UInt32 STATE_SYSTEM_SELECTABLE         = 0x00200000;
        public const UInt32 STATE_SYSTEM_LINKED             = 0x00400000;
        public const UInt32 STATE_SYSTEM_TRAVERSED          = 0x00800000;
        public const UInt32 STATE_SYSTEM_MULTISELECTABLE    = 0x01000000;  // Supports multiple selection
        public const UInt32 STATE_SYSTEM_EXTSELECTABLE      = 0x02000000;  // Supports extended selection
        public const UInt32 STATE_SYSTEM_ALERT_LOW          = 0x04000000;  // This information is of low priority
        public const UInt32 STATE_SYSTEM_ALERT_MEDIUM       = 0x08000000;  // This information is of medium priority
        public const UInt32 STATE_SYSTEM_ALERT_HIGH         = 0x10000000;  // This information is of high priority
        public const UInt32 STATE_SYSTEM_PROTECTED          = 0x20000000;  // access to this is restricted
        public const UInt32 STATE_SYSTEM_VALID              = 0x3FFFFFFF;

        #endregion STATEFLAGS


        public const UInt32 CCHILDREN_TITLEBAR              = 5;
        public const UInt32 CCHILDREN_SCROLLBAR             = 5;


        public struct CURSORINFO
        {
            public UInt32   cbSize;
            public UInt32   flags;
            public IntPtr hCursor;
            public WinDef.POINT   ptScreenPos;
        } 

        public const UInt32 CURSOR_SHOWING     = 0x00000001;
#if WIN32_WINNT_WIN8_LATER
        public const UInt32 CURSOR_SUPPRESSED  = 0x00000002;
#endif // WIN32_WINNT_WIN8_LATER

        // WINUSERAPI BOOL WINAPI GetCursorInfo(_Inout_ PCURSORINFO pci);

        public struct WINDOWINFO
        {
            public UInt32 cbSize;
            public WinDef.RECT rcWindow;
            public WinDef.RECT rcClient;
            public UInt32 dwStyle;
            public UInt32 dwExStyle;
            public UInt32 dwWindowStatus;
            public UInt32 cxWindowBorders;
            public UInt32 cyWindowBorders;
            public UInt16 atomWindowType;
            public UInt16 wCreatorVersion;
        } 

        public const UInt32 WS_ACTIVECAPTION    = 0x0001;

        // WINUSERAPI BOOL WINAPI GetWindowInfo(_In_ HWND hwnd, _Inout_ PWINDOWINFO pwi);

#if CSPORTING
        public struct TITLEBARINFO
        {
            public UInt32 cbSize;
            public WinDef.RECT rcTitleBar;
            public UInt32 rgstate[CCHILDREN_TITLEBAR + 1];
        } 
        TITLEBARINFO, *PTITLEBARINFO, *LPTITLEBARINFO;
#endif //CSPORTING
        // WINUSERAPI BOOL WINAPI GetTitleBarInfo(_In_ HWND hwnd, _Inout_ PTITLEBARINFO pti);

#if WIN32_WINNT_VISTA_LATER
#if CSPORTING
        public struct TITLEBARINFOEX
        {
            public UInt32 cbSize;
            public WinDef.RECT rcTitleBar;
            public UInt32 rgstate[CCHILDREN_TITLEBAR + 1];
            public WinDef.RECT rgrect[CCHILDREN_TITLEBAR + 1];
        } 
#endif // CSPORTING
#endif // WIN32_WINNT_VISTA_LATER


#if CSPORTING
        public struct MENUBARINFO
        {
            public UInt32 cbSize;
            public WinDef.RECT rcBar;          // rect of bar, popup, item
            public IntPtr hMenu;         // real menu handle of bar, popup
            public IntPtr hwndMenu;       // hwnd of item submenu if one
            public bool fBarFocused:1;  // bar, popup has the focus
            public bool fFocused:1;     // item has the focus
        }
#endif // CSPORTING
        /*
        WINUSERAPI BOOL WINAPI GetMenuBarInfo(
            _In_ HWND hwnd,
            _In_ LONG idObject,
            _In_ LONG idItem,
            _Inout_ PMENUBARINFO pmbi);
        */

#if CSPORTING
        public struct SCROLLBARINFO
        {
            public UInt32 cbSize;
            public WinDef.RECT rcScrollBar;
            public Int32 dxyLineButton;
            public Int32 xyThumbTop;
            public Int32 xyThumbBottom;
            public Int32 reserved;
            public UInt32 rgstate[CCHILDREN_SCROLLBAR + 1];
        } 
#endif // CSPORTING
        /*
        WINUSERAPI BOOL WINAPI GetScrollBarInfo(
            _In_ HWND hwnd,
            _In_ LONG idObject,
            _Inout_ PSCROLLBARINFO psbi);
        */

        public struct COMBOBOXINFO
        {
            public UInt32 cbSize;
            public WinDef.RECT rcItem;
            public WinDef.RECT rcButton;
            public UInt32 stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }


        // WINUSERAPI BOOL WINAPI GetComboBoxInfo(_In_ HWND hwndCombo, _Inout_ PCOMBOBOXINFO pcbi);


        public const UInt32 GA_PARENT       = 1;
        public const UInt32 GA_ROOT         = 2;
        public const UInt32 GA_ROOTOWNER    = 3;


        // WINUSERAPI HWND WINAPI GetAncestor(_In_ HWND hwnd, _In_ UINT gaFlags);

        //WINUSERAPI HWND WINAPI RealChildWindowFromPoint(_In_ HWND hwndParent, _In_ POINT ptParentClientCoords);

        /*
        WINUSERAPI UINT WINAPI RealGetWindowClassA(
            _In_ HWND hwnd,
            _Out_writes_to_(cchClassNameMax, return) LPSTR ptszClassName,
            _In_ UINT cchClassNameMax);

        WINUSERAPI UINT WINAPI RealGetWindowClassW(
            _In_ HWND hwnd,
            _Out_writes_to_(cchClassNameMax, return) LPWSTR ptszClassName,
            _In_ UINT cchClassNameMax);
        */

        public struct ALTTABINFO
        {
            public UInt32 cbSize;
            public Int32 cItems;
            public Int32 cColumns;
            public Int32 cRows;
            public Int32 iColFocus;
            public Int32 iRowFocus;
            public Int32 cxItem;
            public Int32 cyItem;
            public WinDef.POINT ptStart;
        }
        /*
        WINUSERAPI BOOL WINAPI GetAltTabInfoA(
            _In_opt_ HWND hwnd,
            _In_ int iItem,
            _Inout_ PALTTABINFO pati,
            _Out_writes_opt_(cchItemText) LPSTR pszItemText,
            _In_ UINT cchItemText);
            
        WINUSERAPI BOOL WINAPI GetAltTabInfoW(
            _In_opt_ HWND hwnd,
            _In_ int iItem,
            _Inout_ PALTTABINFO pati,
            _Out_writes_opt_(cchItemText) LPWSTR pszItemText,
            _In_ UINT cchItemText);
        */

        // WINUSERAPI DWORD WINAPI GetListBoxInfo(_In_ HWND hwnd);

        #endregion WINABLE

#endif // WINVER >= 0x0500 */


#if WIN32_WINNT_WIN2K_LATER
        //WINUSERAPI BOOL WINAPI LockWorkStation(VOID);
#endif // WIN32_WINNT_WIN2K_LATER

#if WIN32_WINNT_WIN2K_LATER
        /*
        WINUSERAPI BOOL WINAPI UserHandleGrantAccess(
            _In_ HANDLE hUserHandle,
            _In_ HANDLE hJob,
            _In_ BOOL   bGrant);
        */
#endif // WIN32_WINNT_WIN2K_LATER


#if WIN32_WINNT_WINXP_LATER


        //DECLARE_HANDLE(HRAWINPUT);


        // #define GET_RAWINPUT_CODE_WPARAM(wParam)    ((wParam) & 0xff)

        public const UInt32 RIM_INPUT       = 0;

        public const UInt32 RIM_INPUTSINK   = 1;


        public struct RAWINPUTHEADER 
        {
            public UInt32 dwType;
            public UInt32 dwSize;
            public IntPtr hDevice;
            public UIntPtr wParam;
        } 


        public const UInt16 RIM_TYPEMOUSE       = 0;
        public const UInt16 RIM_TYPEKEYBOARD    = 1;
        public const UInt16 RIM_TYPEHID         = 2;
        public const UInt16 RIM_TYPEMAX         = 2;


//Disable warning C4201:nameless struct/union
//#if _MSC_VER >= 1200
//#pragma warning(push)
//#endif
//#pragma warning(disable : 4201)

#if CSPORTING
        public struct RAWMOUSE 
        {
            public UInt16 usFlags;

            union DUMMYUNIONNAME
            {
                public UInt32 ulButtons;
                public struct DUMMYSTRUCTNAME
                {
                    public UInt16  usButtonFlags;
                    public UInt16  usButtonData;
                }
            } 
            public UInt32 ulRawButtons;
            public UInt32 lLastX;
            public UInt32 lLastY;
            public UInt32 ulExtraInformation;
        } 
#endif // CSPORTING

//#if _MSC_VER >= 1200
//#pragma warning(pop)
//#endif


        public const UInt32 RI_MOUSE_LEFT_BUTTON_DOWN   = 0x0001;  // Left Button changed to down.
        public const UInt32 RI_MOUSE_LEFT_BUTTON_UP     = 0x0002;  // Left Button changed to up.
        public const UInt32 RI_MOUSE_RIGHT_BUTTON_DOWN  = 0x0004;  // Right Button changed to down.
        public const UInt32 RI_MOUSE_RIGHT_BUTTON_UP    = 0x0008;  // Right Button changed to up.
        public const UInt32 RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010;  // Middle Button changed to down.
        public const UInt32 RI_MOUSE_MIDDLE_BUTTON_UP   = 0x0020;  // Middle Button changed to up.

        public const UInt32 RI_MOUSE_BUTTON_1_DOWN      = RI_MOUSE_LEFT_BUTTON_DOWN;
        public const UInt32 RI_MOUSE_BUTTON_1_UP        = RI_MOUSE_LEFT_BUTTON_UP;
        public const UInt32 RI_MOUSE_BUTTON_2_DOWN      = RI_MOUSE_RIGHT_BUTTON_DOWN;
        public const UInt32 RI_MOUSE_BUTTON_2_UP        = RI_MOUSE_RIGHT_BUTTON_UP;
        public const UInt32 RI_MOUSE_BUTTON_3_DOWN      = RI_MOUSE_MIDDLE_BUTTON_DOWN;
        public const UInt32 RI_MOUSE_BUTTON_3_UP        = RI_MOUSE_MIDDLE_BUTTON_UP;

        public const UInt32 RI_MOUSE_BUTTON_4_DOWN      = 0x0040;
        public const UInt32 RI_MOUSE_BUTTON_4_UP        = 0x0080;
        public const UInt32 RI_MOUSE_BUTTON_5_DOWN      = 0x0100;
        public const UInt32 RI_MOUSE_BUTTON_5_UP        = 0x0200;

        public const UInt32 RI_MOUSE_WHEEL              = 0x0400;
#if WIN32_WINNT_VISTA_LATER //(WINVER >= 0x0600)
        public const UInt32 RI_MOUSE_HWHEEL             = 0x0800;
#endif // WINVER >= 0x0600 */

        public const UInt32 MOUSE_MOVE_RELATIVE         = 0;
        public const UInt32 MOUSE_MOVE_ABSOLUTE         = 1;
        public const UInt32 MOUSE_VIRTUAL_DESKTOP    = 0x02;  // the coordinates are mapped to the virtual desktop
        public const UInt32 MOUSE_ATTRIBUTES_CHANGED = 0x04;  // requery for mouse attributes
#if WIN32_WINNT_VISTA_LATER //(WINVER >= 0x0600)
        public const UInt32 MOUSE_MOVE_NOCOALESCE    = 0x08;  // do not coalesce mouse moves
#endif // WINVER >= 0x0600 */


        public struct RAWKEYBOARD 
        {
            public UInt16 MakeCode;
            public UInt16 Flags;
            public UInt16 Reserved;
            public UInt16 VKey;
            public UInt32   Message;
            public UInt32 ExtraInformation;
        } 


        public const UInt32 KEYBOARD_OVERRUN_MAKE_CODE    = 0xFF;

        public const UInt32 RI_KEY_MAKE             = 0;
        public const UInt32 RI_KEY_BREAK            = 1;
        public const UInt32 RI_KEY_E0               = 2;
        public const UInt32 RI_KEY_E1               = 4;
        public const UInt32 RI_KEY_TERMSRV_SET_LED  = 8;
        public const UInt32 RI_KEY_TERMSRV_SHADOW   = 0x10;


#if CSPORTING
        public struct RAWHID 
        {
            public UInt32 dwSizeHid;    // byte size of each report
            public UInt32 dwCount;      // number of input packed
            public Byte bRawData[1];
        } 
#endif // CSPORTING

#if CSPORTING
        public struct RAWINPUT 
        {
            public RAWINPUTHEADER header;
            union data
            {
                public RAWMOUSE    mouse;
                public RAWKEYBOARD keyboard;
                public RAWHID      hid;
            }
        } 
#endif // CSPORTING


        //#if _WIN64
        //#define RAWINPUT_ALIGN(x)   (((x) + sizeof(QWORD) - 1) & ~(sizeof(QWORD) - 1))
        //#else   // _WIN64
        //#define RAWINPUT_ALIGN(x)   (((x) + sizeof(DWORD) - 1) & ~(sizeof(DWORD) - 1))
        //#endif  // _WIN64

        //#define NEXTRAWINPUTBLOCK(ptr) ((PRAWINPUT)RAWINPUT_ALIGN((ULONG_PTR)((PBYTE)(ptr) + (ptr)->header.dwSize)))


        public const UInt32 RID_INPUT               = 0x10000003;
        public const UInt32 RID_HEADER              = 0x10000005;

        /*
        WINUSERAPI UINT WINAPI GetRawInputData(
            _In_ HRAWINPUT hRawInput,
            _In_ UINT uiCommand,
            _Out_writes_bytes_to_opt_(*pcbSize, return) LPVOID pData,
            _Inout_ PUINT pcbSize,
            _In_ UINT cbSizeHeader);
        */

        public const UInt32 RIDI_PREPARSEDDATA      = 0x20000005;
        public const UInt32 RIDI_DEVICENAME         = 0x20000007;  // the return valus is the character length, not the byte size
        public const UInt32 RIDI_DEVICEINFO         = 0x2000000b;


        public struct RID_DEVICE_INFO_MOUSE 
        {
            public UInt32 dwId;
            public UInt32 dwNumberOfButtons;
            public UInt32 dwSampleRate;
            public bool fHasHorizontalWheel;
        } 

        public struct RID_DEVICE_INFO_KEYBOARD 
        {
            public UInt32 dwType;
            public UInt32 dwSubType;
            public UInt32 dwKeyboardMode;
            public UInt32 dwNumberOfFunctionKeys;
            public UInt32 dwNumberOfIndicators;
            public UInt32 dwNumberOfKeysTotal;
        } 

        public struct RID_DEVICE_INFO_HID 
        {
            public UInt32 dwVendorId;
            public UInt32 dwProductId;
            public UInt32 dwVersionNumber;
            public UInt16 usUsagePage;
            public UInt16 usUsage;
        }

#if CSPORTING
        public struct RID_DEVICE_INFO 
        {
            public UInt32 cbSize;
            public UInt32 dwType;
            
            union DUMMYUNIONNAME
            {
                public RID_DEVICE_INFO_MOUSE mouse;
                public RID_DEVICE_INFO_KEYBOARD keyboard;
                public RID_DEVICE_INFO_HID hid;
            }
        } 
#endif // CSPORTING

        /*
        WINUSERAPI UINT WINAPI GetRawInputDeviceInfoA(
            _In_opt_ HANDLE hDevice,
            _In_ UINT uiCommand,
            _Inout_updates_bytes_to_opt_(*pcbSize, *pcbSize) LPVOID pData,
            _Inout_ PUINT pcbSize);
            
        WINUSERAPI UINT WINAPI GetRawInputDeviceInfoW(
            _In_opt_ HANDLE hDevice,
            _In_ UINT uiCommand,
            _Inout_updates_bytes_to_opt_(*pcbSize, *pcbSize) LPVOID pData,
            _Inout_ PUINT pcbSize);
        */
        /*
        WINUSERAPI UINT WINAPI GetRawInputBuffer(
            _Out_writes_bytes_opt_(* pcbSize) PRAWINPUT pData,
            _Inout_ PUINT pcbSize,
            _In_ UINT cbSizeHeader);
        */
        public struct RAWINPUTDEVICE
        {
            public UInt16 usUsagePage; // Toplevel collection UsagePage
            public UInt16 usUsage;     // Toplevel collection Usage
            public UInt32 dwFlags;
            public IntPtr hwndTarget;    // Target hwnd. NULL = follows keyboard focus
        }


        public const UInt32 RIDEV_REMOVE            = 0x00000001;
        public const UInt32 RIDEV_EXCLUDE           = 0x00000010;
        public const UInt32 RIDEV_PAGEONLY          = 0x00000020;
        public const UInt32 RIDEV_NOLEGACY          = 0x00000030;
        public const UInt32 RIDEV_INPUTSINK         = 0x00000100;
        public const UInt32 RIDEV_CAPTUREMOUSE      = 0x00000200;  // effective when mouse nolegacy is specified, otherwise it would be an error
        public const UInt32 RIDEV_NOHOTKEYS         = 0x00000200;  // effective for keyboard.
        public const UInt32 RIDEV_APPKEYS           = 0x00000400;  // effective for keyboard.
#if WIN32_WINNT_WINXP_LATER
        public const UInt32 RIDEV_EXINPUTSINK       = 0x00001000;
        public const UInt32 RIDEV_DEVNOTIFY         = 0x00002000;
#endif // WIN32_WINNT_WINXP_LATER
        public const UInt32 RIDEV_EXMODEMASK        = 0x000000F0;

        //#define RIDEV_EXMODE(mode)  ((mode) & RIDEV_EXMODEMASK)

#if WIN32_WINNT_WINXP_LATER
        public const UInt32 GIDC_ARRIVAL             = 1;
        public const UInt32 GIDC_REMOVAL             = 2;
#endif // WIN32_WINNT_WINXP_LATER

#if WIN32_WINNT_WIN7_LATER
        //#define GET_DEVICE_CHANGE_WPARAM(wParam)  (LOWORD(wParam))
#elif WIN32_WINNT_WINXP_LATER
        //#define GET_DEVICE_CHANGE_LPARAM(lParam)  (LOWORD(lParam))
#endif // WIN32_WINNT_WIN7_LATER


        /*
        WINUSERAPI BOOL WINAPI RegisterRawInputDevices(
            _In_reads_(uiNumDevices) PCRAWINPUTDEVICE pRawInputDevices,
            _In_ UINT uiNumDevices,
            _In_ UINT cbSize);
        */
        /*
        WINUSERAPI UINT WINAPI GetRegisteredRawInputDevices(
            _Out_writes_opt_( *puiNumDevices) PRAWINPUTDEVICE pRawInputDevices,
            _Inout_ PUINT puiNumDevices,
            _In_ UINT cbSize);
        */

        public struct RAWINPUTDEVICELIST 
        {
            public IntPtr hDevice;
            public UInt32 dwType;
        }
        /*
        WINUSERAPI UINT WINAPI GetRawInputDeviceList(
            _Out_writes_opt_(*puiNumDevices) PRAWINPUTDEVICELIST pRawInputDeviceList,
            _Inout_ PUINT puiNumDevices,
            _In_ UINT cbSize);
        */
        /*
        WINUSERAPI LRESULT WINAPI DefRawInputProc(
            _In_reads_(nInput) PRAWINPUT* paRawInput,
            _In_ INT nInput,
            _In_ UINT cbSizeHeader);
        */

#endif // WIN32_WINNT_WINXP_LATER


#if WIN32_WINNT_WIN8_LATER

        public const UInt32 POINTER_DEVICE_PRODUCT_STRING_MAX = 520;

        public const UInt32 PDC_ARRIVAL                   = 0x001;
        public const UInt32 PDC_REMOVAL                   = 0x002;
        public const UInt32 PDC_ORIENTATION_0             = 0x004;
        public const UInt32 PDC_ORIENTATION_90            = 0x008;
        public const UInt32 PDC_ORIENTATION_180           = 0x010;
        public const UInt32 PDC_ORIENTATION_270           = 0x020;
        public const UInt32 PDC_MODE_DEFAULT              = 0x040;
        public const UInt32 PDC_MODE_CENTERED             = 0x080;
        public const UInt32 PDC_MAPPING_CHANGE            = 0x100;
        public const UInt32 PDC_RESOLUTION                = 0x200;
        public const UInt32 PDC_ORIGIN                    = 0x400;
        public const UInt32 PDC_MODE_ASPECTRATIOPRESERVED = 0x800;


        public enum POINTER_DEVICE_TYPE : UInt32
        {
            POINTER_DEVICE_TYPE_INTEGRATED_PEN = 0x00000001,
            POINTER_DEVICE_TYPE_EXTERNAL_PEN   = 0x00000002,
            POINTER_DEVICE_TYPE_TOUCH          = 0x00000003,
#if WIN32_WINNT_WINBLUE_LATER
            POINTER_DEVICE_TYPE_TOUCH_PAD      = 0x00000004,
#endif // WIN32_WINNT_WINBLUE_LATER
            POINTER_DEVICE_TYPE_MAX            = 0xFFFFFFFF
        }


#if CSPORTING
        public struct POINTER_DEVICE_INFO 
        {
            public UInt32 displayOrientation;
            public IntPtr device;
            POINTER_DEVICE_TYPE pointerDeviceType;
            public IntPtr monitor;
            public UInt32 startingCursorId;
            public UInt16 maxActiveContacts;
            public Char productString[POINTER_DEVICE_PRODUCT_STRING_MAX];
        } 
#endif // CSPORTING

        public struct POINTER_DEVICE_PROPERTY 
        {
            public Int32 logicalMin;
            public Int32 logicalMax;
            public Int32 physicalMin;
            public Int32 physicalMax;
            public UInt32 unit;
            public UInt32 unitExponent;
            public UInt16 usagePageId;
            public UInt16 usageId;
        } 

        public  enum POINTER_DEVICE_CURSOR_TYPE : UInt32
        {
            POINTER_DEVICE_CURSOR_TYPE_UNKNOWN   = 0x00000000,
            POINTER_DEVICE_CURSOR_TYPE_TIP       = 0x00000001,
            POINTER_DEVICE_CURSOR_TYPE_ERASER    = 0x00000002,
            POINTER_DEVICE_CURSOR_TYPE_MAX       = 0xFFFFFFFF
        } 

        public  struct POINTER_DEVICE_CURSOR_INFO 
        {
            public UInt32 cursorId;
            public POINTER_DEVICE_CURSOR_TYPE cursor;
        }
        /*
        WINUSERAPI BOOL WINAPI GetPointerDevices(
            _Inout_ UINT32* deviceCount,
            _Out_writes_opt_(*deviceCount) POINTER_DEVICE_INFO *pointerDevices);
        */
        /*
        WINUSERAPI BOOL WINAPI GetPointerDevice(
            _In_ HANDLE device,
            _Out_writes_(1) POINTER_DEVICE_INFO *pointerDevice);
        */
        /*
        WINUSERAPI BOOL WINAPI GetPointerDeviceProperties(
            _In_ HANDLE device,
            _Inout_ UINT32* propertyCount,
            _Out_writes_opt_(*propertyCount) POINTER_DEVICE_PROPERTY *pointerProperties);
        */
        // WINUSERAPI BOOL WINAPI RegisterPointerDeviceNotifications(_In_ HWND window, _In_ BOOL notifyRange);
        /*
        WINUSERAPI BOOL WINAPI GetPointerDeviceRects(
            _In_ HANDLE device,
            _Out_writes_(1) RECT* pointerDeviceRect,
            _Out_writes_(1) RECT* displayRect);
        */
        /*
        WINUSERAPI BOOL WINAPI GetPointerDeviceCursors(
            _In_ HANDLE device,
            _Inout_ UINT32* cursorCount,
            _Out_writes_opt_(*cursorCount) POINTER_DEVICE_CURSOR_INFO *deviceCursors);
        */
        /*
        WINUSERAPI BOOL WINAPI GetRawPointerDeviceData(
            _In_ UINT32 pointerId,
            _In_ UINT32 historyCount,
            _In_ UINT32 propertiesCount,
            _In_reads_(propertiesCount) POINTER_DEVICE_PROPERTY* pProperties,
            _Out_writes_(historyCount * propertiesCount) LONG* pValues);
        */


#endif // WINVER >= 0x0602


#if WIN32_WINNT_VISTA_LATER

        public const UInt32 MSGFLT_ADD = 1;
        public const UInt32 MSGFLT_REMOVE = 2;

        // WINUSERAPI BOOL WINAPI ChangeWindowMessageFilter(_In_ UINT message, _In_ DWORD dwFlag);

#endif // WIN32_WINNT_VISTA_LATER

#if WIN32_WINNT_WIN7_LATER

        public const UInt32 MSGFLTINFO_NONE                         = (0);
        public const UInt32 MSGFLTINFO_ALREADYALLOWED_FORWND        = (1);
        public const UInt32 MSGFLTINFO_ALREADYDISALLOWED_FORWND     = (2);
        public const UInt32 MSGFLTINFO_ALLOWED_HIGHER               = (3);

        public struct CHANGEFILTERSTRUCT 
        {
            public UInt32 cbSize;
            public UInt32 ExtStatus;
        } 

        public const UInt32 MSGFLT_RESET                            = (0);
        public const UInt32 MSGFLT_ALLOW                            = (1);
        public const UInt32 MSGFLT_DISALLOW                         = (2);

        /*
        WINUSERAPI BOOL WINAPI ChangeWindowMessageFilterEx(
            _In_ HWND hwnd,                                         // Window
            _In_ UINT message,                                      // WM_ message
            _In_ DWORD action,                                      // Message filter action value
            _Inout_opt_ PCHANGEFILTERSTRUCT pChangeFilterStruct);   // Optional
        */

#endif // WIN32_WINNT_WIN7_LATER


#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)


        //DECLARE_HANDLE(HGESTUREINFO);

        public const UInt32 GF_BEGIN                        = 0x00000001;
        public const UInt32 GF_INERTIA                      = 0x00000002;
        public const UInt32 GF_END                          = 0x00000004;

        public const UInt32 GID_BEGIN                       = 1;
        public const UInt32 GID_END                         = 2;
        public const UInt32 GID_ZOOM                        = 3;
        public const UInt32 GID_PAN                         = 4;
        public const UInt32 GID_ROTATE                      = 5;
        public const UInt32 GID_TWOFINGERTAP                = 6;
        public const UInt32 GID_PRESSANDTAP                 = 7;
        public const UInt32 GID_ROLLOVER                    = GID_PRESSANDTAP;


        public struct GESTUREINFO 
        {
            public UInt32 cbSize;                    // size, in bytes, of this structure (including variable length Args field)
            public UInt32 dwFlags;                  // see GF_* flags
            public UInt32 dwID;                     // gesture ID, see GID_* defines
            public IntPtr hwndTarget;                // handle to window targeted by this gesture
            public WinDef.POINTS ptsLocation;             // current location of this gesture
            public UInt32 dwInstanceID;             // internally used
            public UInt32 dwSequenceID;             // internally used
            public UInt64 ullArguments;         // arguments for gestures whose arguments fit in 8 BYTES
            public UInt32 cbExtraArgs;               // size, in bytes, of extra arguments, if any, that accompany this gesture
        } 

        public struct GESTURENOTIFYSTRUCT 
        {
            public UInt32 cbSize;                    // size, in bytes, of this structure
            public UInt32 dwFlags;                  // unused
            public IntPtr hwndTarget;                // handle to window targeted by the gesture
            public WinDef.POINTS ptsLocation;             // starting location
            public UInt32 dwInstanceID;             // internally used
        } 

        //#define GID_ROTATE_ANGLE_TO_ARGUMENT(_arg_)     ((USHORT)((((_arg_) + 2.0 * 3.14159265) / (4.0 * 3.14159265)) * 65535.0))
        //#define GID_ROTATE_ANGLE_FROM_ARGUMENT(_arg_)   ((((double)(_arg_) / 65535.0) * 4.0 * 3.14159265) - 2.0 * 3.14159265)

        // WINUSERAPI BOOL WINAPI GetGestureInfo(_In_ HGESTUREINFO hGestureInfo, _Out_ PGESTUREINFO pGestureInfo);

        /*
        WINUSERAPI BOOL WINAPI GetGestureExtraArgs(
            _In_ HGESTUREINFO hGestureInfo,
            _In_ UINT cbExtraArgs,
            _Out_writes_bytes_(cbExtraArgs) PBYTE pExtraArgs);
        */
        // WINUSERAPI BOOL WINAPI CloseGestureInfoHandle(_In_ HGESTUREINFO hGestureInfo);

        public struct tagGESTURECONFIG 
        {
            public UInt32 dwID;                     // gesture ID
            public UInt32 dwWant;                   // settings related to gesture ID that are to be turned on
            public UInt32 dwBlock;                  // settings related to gesture ID that are to be turned off
        }


        public const UInt32 GC_ALLGESTURES                              = 0x00000001;

        public const UInt32 GC_ZOOM                                     = 0x00000001;

        public const UInt32 GC_PAN                                      = 0x00000001;
        public const UInt32 GC_PAN_WITH_SINGLE_FINGER_VERTICALLY        = 0x00000002;
        public const UInt32 GC_PAN_WITH_SINGLE_FINGER_HORIZONTALLY      = 0x00000004;
        public const UInt32 GC_PAN_WITH_GUTTER                          = 0x00000008;
        public const UInt32 GC_PAN_WITH_INERTIA                         = 0x00000010;

        public const UInt32 GC_ROTATE                                   = 0x00000001;

        public const UInt32 GC_TWOFINGERTAP                             = 0x00000001;

        public const UInt32 GC_PRESSANDTAP                              = 0x00000001;
        public const UInt32 GC_ROLLOVER                                 = GC_PRESSANDTAP;

        public const UInt32 GESTURECONFIGMAXCOUNT           = 256;  // Maximum number of gestures that can be included
                                                                    // in a single call to SetGestureConfig / GetGestureConfig
        /*
        WINUSERAPI BOOL WINAPI SetGestureConfig(
            _In_ HWND hwnd,                                     // window for which configuration is specified
            _In_ DWORD dwReserved,                              // reserved, must be 0
            _In_ UINT cIDs,                                     // count of GESTURECONFIG structures
            _In_reads_(cIDs) PGESTURECONFIG pGestureConfig,    // array of GESTURECONFIG structures, dwIDs will be processed in the
                                                               // order specified and repeated occurances will overwrite previous ones
            _In_ UINT cbSize);                                  // sizeof(GESTURECONFIG)
        */

        public const UInt32 GCF_INCLUDE_ANCESTORS   = 0x00000001;   // If specified, GetGestureConfig returns consolidated configuration
                                                                    // for the specified window and it's parent window chain
        /*
        WINUSERAPI BOOL WINAPI GetGestureConfig(
            _In_ HWND hwnd,                                     // window for which configuration is required
            _In_ DWORD dwReserved,                              // reserved, must be 0
            _In_ DWORD dwFlags,                                 // see GCF_* flags
            _In_ PUINT pcIDs,                                   // *pcIDs contains the size, in number of GESTURECONFIG structures,
                                                        // of the buffer pointed to by pGestureConfig
            _Inout_updates_(*pcIDs) PGESTURECONFIG pGestureConfig,
                                                        // pointer to buffer to receive the returned array of GESTURECONFIG structures
            _In_ UINT cbSize);                                  // sizeof(GESTURECONFIG)
        */

#endif // WINVER >= 0x0601 */


#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)

        public const UInt32 NID_INTEGRATED_TOUCH  = 0x00000001;
        public const UInt32 NID_EXTERNAL_TOUCH    = 0x00000002;
        public const UInt32 NID_INTEGRATED_PEN    = 0x00000004;
        public const UInt32 NID_EXTERNAL_PEN      = 0x00000008;
        public const UInt32 NID_MULTI_INPUT       = 0x00000040;
        public const UInt32 NID_READY             = 0x00000080;

#endif // WINVER >= 0x0601 */


        public const UInt32 MAX_STR_BLOCKREASON = 256;


        //WINUSERAPI BOOL WINAPI ShutdownBlockReasonCreate(_In_ HWND hWnd, _In_ LPCWSTR pwszReason);
        /*
        WINUSERAPI BOOL WINAPI ShutdownBlockReasonQuery(
            _In_ HWND hWnd,
            _Out_writes_opt_(*pcchBuff) LPWSTR pwszBuff,
            _Inout_ DWORD *pcchBuff);
        */
        // WINUSERAPI BOOL WINAPI ShutdownBlockReasonDestroy(_In_ HWND hWnd);


#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)


        public enum INPUT_MESSAGE_DEVICE_TYPE  
        {
            IMDT_UNAVAILABLE        = 0x00000000,       // not specified
            IMDT_KEYBOARD           = 0x00000001,       // from keyboard
            IMDT_MOUSE              = 0x00000002,       // from mouse
            IMDT_TOUCH              = 0x00000004,       // from touch
            IMDT_PEN                = 0x00000008,       // from pen
#if WIN32_WINNT_WINBLUE_LATER // (WINVER >= 0x0603)
            IMDT_TOUCHPAD           = 0x00000010,       // from touchpad
#endif // WINVER >= 0x0603
        }

        public enum INPUT_MESSAGE_ORIGIN_ID 
        {
            IMO_UNAVAILABLE = 0x00000000,  // not specified
            IMO_HARDWARE    = 0x00000001,  // from a hardware device or injected by a UIAccess app
            IMO_INJECTED    = 0x00000002,  // injected via SendInput() by a non-UIAccess app
            IMO_SYSTEM      = 0x00000004,  // injected by the system
        }

        public  struct INPUT_MESSAGE_SOURCE 
        {
            public INPUT_MESSAGE_DEVICE_TYPE deviceType;
            public INPUT_MESSAGE_ORIGIN_ID   originId;
        }


        // WINUSERAPI BOOL WINAPI GetCurrentInputMessageSource(_Out_ INPUT_MESSAGE_SOURCE *inputMessageSource);

        // WINUSERAPI BOOL WINAPI GetCIMSSM(_Out_ INPUT_MESSAGE_SOURCE *inputMessageSource);


#endif // WINVER >= 0x0601


#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)

        public enum AR_STATE 
        {
            AR_ENABLED        = 0x0,
            AR_DISABLED       = 0x1,
            AR_SUPPRESSED     = 0x2,
            AR_REMOTESESSION  = 0x4,
            AR_MULTIMON       = 0x8,
            AR_NOSENSOR       = 0x10,
            AR_NOT_SUPPORTED  = 0x20,
            AR_DOCKED         = 0x40,
            AR_LAPTOP         = 0x80
        }


#if !MIDL_PASS
        // Don't define this for MIDL compiler passes over winuser.h. Some of them
        // don't include winnt.h (where DEFINE_ENUM_FLAG_OPERATORS is defined and
        // get compile errors.
        //DEFINE_ENUM_FLAG_OPERATORS(AR_STATE)
#endif


        public enum ORIENTATION_PREFERENCE 
        {
            ORIENTATION_PREFERENCE_NONE              = 0x0,
            ORIENTATION_PREFERENCE_LANDSCAPE         = 0x1,
            ORIENTATION_PREFERENCE_PORTRAIT          = 0x2,
            ORIENTATION_PREFERENCE_LANDSCAPE_FLIPPED = 0x4,
            ORIENTATION_PREFERENCE_PORTRAIT_FLIPPED  = 0x8
        }


#if !MIDL_PASS
        // Don't define this for MIDL compiler passes over winuser.h. Some of them
        // don't include winnt.h (where DEFINE_ENUM_FLAG_OPERATORS is defined and
        // get compile errors.
        //DEFINE_ENUM_FLAG_OPERATORS(ORIENTATION_PREFERENCE)
#endif

        // WINUSERAPI BOOL WINAPI GetAutoRotationState(_Out_ PAR_STATE pState);

        // WINUSERAPI BOOL WINAPI GetDisplayAutoRotationPreferences(_Out_ ORIENTATION_PREFERENCE *pOrientation);
        /*
        WINUSERAPI BOOL WINAPI GetDisplayAutoRotationPreferencesByProcessId(
            _In_ DWORD dwProcessId,
            _Out_ ORIENTATION_PREFERENCE *pOrientation,
            _Out_ BOOL *fRotateScreen);
        */
        // WINUSERAPI BOOL WINAPI SetDisplayAutoRotationPreferences(_In_ ORIENTATION_PREFERENCE orientation);


#endif // WINVER >= 0x0601


#if WIN32_WINNT_WIN7_LATER //(WINVER >= 0x0601)


        // WINUSERAPI BOOL WINAPI IsImmersiveProcess(_In_ HANDLE hProcess);

        // WINUSERAPI BOOL WINAPI SetProcessRestrictionExemption(_In_ BOOL fEnableExemption);


#endif // WINVER >= 0x0601


        //#if _MSC_VER >= 1200
        //#pragma warning(pop)
        //#endif


        //#if !defined(RC_INVOKED) /* RC complains about long symbols in #ifs */
        //#if defined(ISOLATION_AWARE_ENABLED) && (ISOLATION_AWARE_ENABLED != 0)
        //# include "winuser.inl"
        //#endif /* ISOLATION_AWARE_ENABLED */
        //#endif /* RC */

        //# ifdef __cplusplus
        //}
        //#endif  /* __cplusplus */
    }
#pragma warning restore IDE1006
}                           
