using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Interop;
using Win32;
using WindowsHooks;

namespace SysMenu
{
    class SysMenuHook : CallWndProcHook
    {
        MsgFilter MsgFilter = new MsgFilter();

        public SysMenuHook()
        {
            AddHandler(User32.WM_CREATE, WmCreate, false);
            AddHandler(User32.WM_DESTROY, WmDestroy, false);
            AddHandler(User32.WM_ENTERMENULOOP, WmEnterMenuLoop, false);
            AddHandler(User32.WM_EXITMENULOOP, WmExitMenuLoop, false);
            AddHandler(User32.WM_INITMENUPOPUP, WmInitMenuPopup, false);
        }


        protected override void DoHook()
        {
            base.DoHook();

            if (MenuHelper.RegPostKeyDown == 0)
                throw new Win32Exception("Win32API RegisterWindowMessage is failed.");

            Window.Loaded += Window_Loaded;
            Window.PreviewKeyUp += PreviewKeyUp;
            Window.LostMouseCapture += LostMouseCapture;

            if (Window.IsLoaded)
            {
                IntPtr hwnd = new WindowInteropHelper(Window).Handle;
                User32.GetSystemMenu(hwnd, false);
            }
        }


        protected override void DoUnhook()
        {
            Window.LostMouseCapture -= LostMouseCapture;
            Window.PreviewKeyUp -= PreviewKeyUp;
            Window.Loaded -= Window_Loaded;

            base.DoUnhook();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(Window).Handle;
            User32.GetSystemMenu(hwnd, false);
        }


        private void PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System)
            {
                var menu = FindDescendent<Menu>(Window, (i => i.IsMainMenu && HasEnableItem(i)));
                if (menu == null)
                    e.Handled = true;
            }
        }


        private void LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is MenuBase)
            {
                uint flags = 0;
                if (e.LeftButton == MouseButtonState.Pressed)
                    flags = User32.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF_ABSOLUTE;
                else if (e.RightButton == MouseButtonState.Pressed)
                    flags = User32.MOUSEEVENTF_RIGHTDOWN | User32.MOUSEEVENTF_ABSOLUTE;

                if (flags != 0)
                {
                    IntPtr hwnd = new WindowInteropHelper(Window).Handle;
                    uint pos = User32.GetMessagePos();
                    int ht = (int)User32.SendMessageW(hwnd, User32.WM_NCHITTEST, UIntPtr.Zero, (IntPtr)pos);
                    if (ht >= User32.HTCAPTION)
                    {
                        int x = (short)(pos & 0xFFFF);
                        int y = (short)((pos >> 16) & 0xFFFF);
                        User32.mouse_event(flags, (uint)x, (uint)y, 0, UIntPtr.Zero);
                    }
                }
            }
        }


        static T FindDescendent<T>(DependencyObject parent, Func<T, bool> cond) where T : DependencyObject
        {
            T result = null;
            if (parent != null)
            {
                foreach (var item in LogicalTreeHelper.GetChildren(parent))
                {
                    if (item is T t && cond(t))
                        return t;
                    else
                    {
                        T ret = FindDescendent(item as DependencyObject, cond);
                        if (ret != null)
                            return ret;
                    }
                }
            }
            return result;
        }


        static bool HasEnableItem(Menu menu)
        {
            foreach (var i in menu.Items)
            {
                if (i is UIElement fe && fe.IsEnabled)
                    return true;
            }
            return false;
        }


        private void WmCreate(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            User32.CREATESTRUCTW cs = (User32.CREATESTRUCTW)Marshal.PtrToStructure(lParam, typeof(User32.CREATESTRUCTW));
            if ((uint)cs.lpszClass == 0x8000) // 0x8000 PopupMenuWindow class
                MsgFilter.AddPopupMenu(hwnd);
        }


        private void WmDestroy(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            MsgFilter.RemovePopupMenu(hwnd);
        }


        private void WmEnterMenuLoop(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            if (Window.FindResource("AddHookCommand") is RoutedCommand rc)
            {
                rc.Execute(MsgFilter, Window);
            }

        }

        private void WmExitMenuLoop(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            if (Window.FindResource("RemoveHookCommand") is RoutedCommand rc)
            {
                rc.Execute(MsgFilter, Window);
            }
        }


        private void WmInitMenuPopup(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            if (((uint)lParam & 0xFFFF0000) != 0) // SystemMenu
            {
                IntPtr hmenu = new IntPtr((long)(ulong)wParam);
                int ht = (int)User32.SendMessageW(hwnd, User32.WM_NCHITTEST, UIntPtr.Zero, (IntPtr)User32.GetMessagePos());
                if (ht == User32.HTCAPTION || ht == User32.HTMINBUTTON)
                {
                    HwndSource hwndSource = HwndSource.FromHwnd(hwnd);
                    if (hwndSource?.RootVisual is Window window)
                    {
                        if (window.WindowState == WindowState.Maximized
                            || window.WindowState == WindowState.Minimized)
                        {
                            User32.SetMenuDefaultItem(hmenu, User32.SC_RESTORE, false);
                        }
                        else
                            User32.SetMenuDefaultItem(hmenu, User32.SC_MAXIMIZE, false);
                    }
                }
            }
        }
    }


    class MsgFilter : MsgFilterProcHook
    {
        List<PopupMenu> List = new List<PopupMenu>();

        public MsgFilter()
        {
            AddHandler(MenuHelper.RegPostKeyDown, ((hwnd, wParam, lParam) => DispatchTop(hwnd, MenuHelper.RegPostKeyDown, wParam, lParam)), false);
            AddHandler(User32.WM_MENUSELECT, ((hwnd, wParam, lParam) => DispatchHMenu(lParam, hwnd, User32.WM_MENUSELECT, wParam, lParam)), false);
                        
            AddHandler(User32.WM_KEYDOWN, ((hwnd, wParam, lParam) => 
            {
                uint vk = (uint)wParam;

                if ((UInt32)wParam == User32.VK_ESCAPE && List.Count == 1)
                {
                    User32.EndMenu();
                    return true;
                }
                else
                {
                    return DispatchTop(hwnd, User32.WM_KEYDOWN, wParam, lParam);
                }
            }), false);
            
            AddHandler(User32.WM_CHAR, ((hwnd, wParam, lParam) => DispatchTop(hwnd, User32.WM_CHAR, wParam, lParam)), false);
            AddHandler(User32.WM_MOUSEMOVE, ((hwnd, wParam, lParam) => DispatchPoint(Win32Helper.LParamToPoint(lParam), hwnd, User32.WM_MOUSEMOVE, wParam, lParam)), false);
            AddHandler(User32.WM_LBUTTONDOWN, ((hwnd, wParam, lParam) => DispatchPoint(Win32Helper.LParamToPoint(lParam), hwnd, User32.WM_LBUTTONDOWN, wParam, lParam)), false);
            AddHandler(User32.WM_RBUTTONDOWN, ((hwnd, wParam, lParam) => DispatchPoint(Win32Helper.LParamToPoint(lParam), hwnd, User32.WM_RBUTTONDOWN, wParam, lParam)), false);
        }


        public void AddPopupMenu(IntPtr hwnd)
        {           
            List.Insert(0, new PopupMenu(hwnd));
        }


        public void RemovePopupMenu(IntPtr hwnd)
        {
            int index = 0;
            foreach (PopupMenu item in List)
            {
                if (item.HasHWnd(hwnd))
                    break;
                else
                    index++;
            }
            if (index < List.Count)
            {
                List.RemoveRange(0, index + 1);
            }
        }



        bool DispatchTop(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            PopupMenu pm = List.Count > 0 ? List[0] : null;
            if (pm != null)
                cancel = pm.DispatchMessage(hwnd, message, wParam, lParam);

            return cancel;
        }


        bool DispatchHMenu(IntPtr hmenu, IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            PopupMenu pm = List.Find(i => i.HasHMenu(hmenu));
            if (pm != null)
                cancel = pm.DispatchMessage(hwnd, User32.WM_MENUSELECT, wParam, lParam);

            return cancel;
        }


        bool DispatchPoint(WinDef.POINT ptScreen, IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;
            PopupMenu pm = List.Find(i => i.HitTest(ptScreen));
            if (pm != null)
                cancel = pm.DispatchMessage(hwnd, message, wParam, lParam);

            return cancel;
        }
    }


    class PopupMenu
    {
        IntPtr HWndPopup;
        bool FirstMessageDone;
        int MouseSelection;
        int KeyboardSelection;

        public PopupMenu(IntPtr hwndPopup)
        {
            HWndPopup = hwndPopup;
        }


        public bool HasHWnd(IntPtr hwnd)
        {
            return hwnd == HWndPopup;
        }


        public bool HasHMenu(IntPtr hmenu)
        {
            return hmenu == MenuHelper.GetHMenu(HWndPopup);
        }


        public bool HitTest(WinDef.POINT ptScreen)
        {
            User32.GetWindowRect(HWndPopup, out WinDef.RECT rc);
            return User32.PtInRect(ref rc, ptScreen);
        }


        public bool DispatchMessage(IntPtr hwnd, UInt32 message, UIntPtr wParam, IntPtr lParam)
        {
            if (message == MenuHelper.RegPostKeyDown)
                return WmRegPostKeyDown(hwnd, wParam, lParam);
            switch(message)
            {
                case User32.WM_MENUSELECT: return WmMenuSelect(hwnd, wParam, lParam);
                case User32.WM_KEYDOWN: return WmKeyDown(hwnd, wParam, lParam);
                case User32.WM_CHAR: return WmChar(hwnd, wParam, lParam);
                case User32.WM_MOUSEMOVE: return WmMouseMove(hwnd, wParam, lParam);
                case User32.WM_LBUTTONDOWN: return WmMouseButtonDown(hwnd, wParam, lParam);
                case User32.WM_RBUTTONDOWN: return WmMouseButtonDown(hwnd, wParam, lParam);
                default: return false;
            }
        }


        bool WmMenuSelect(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            IntPtr hmenu = lParam;
            uint flags = ((uint)wParam >> 16) & 0xFFFF;

            if (flags == 0xFFFF)
                flags = uint.MaxValue;

            if (!FirstMessageDone)
            {
                FirstMessageDone = true;

                int pos = MenuHelper.GetHilitePos(hmenu);
                if (pos >= 0)
                    KeyboardSelection = pos;

                if (flags != uint.MaxValue && (flags & User32.MFS_GRAYED) != 0)
                {
                    if (MenuHelper.GetFirstEnabled(hmenu) < 0)
                    {
                        cancel = true;
                        MenuHelper.ShowSelect(HWndPopup, -1);
                    }
                    else
                        User32.PostMessageW(hwnd, User32.WM_KEYDOWN, (UIntPtr)User32.VK_DOWN, IntPtr.Zero);
                }
            }
            return cancel;
        }


        bool WmKeyDown(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            uint vk = (uint)wParam;

            FirstMessageDone = true;

            IntPtr hmenu = MenuHelper.GetHMenu(HWndPopup);
            int sel;
            switch (vk)
            {
                case User32.VK_RETURN:
                    sel = MenuHelper.GetHilitePos(hmenu);
                    if (sel < 0)
                        cancel = true;
                    break;

                case User32.VK_UP:
                case User32.VK_DOWN:
                    if (MenuHelper.GetFirstEnabled(hmenu) < 0)
                        cancel = true;
                    else
                    {
                        int pos = KeyboardSelection;

                        // menuitem at pos may not have MF_HILITE
                        if (pos >= 0)
                            MenuHelper.ShowSelect(HWndPopup, pos);

                        User32.PostMessageW(hwnd, MenuHelper.RegPostKeyDown, (UIntPtr)vk, hmenu);
                    }
                    break;
            }

            return cancel;
        }


        bool WmChar(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            IntPtr hmenu = MenuHelper.GetHMenu(HWndPopup);
            int pos = MenuHelper.GetMnemonicItem(hmenu, (char)wParam);
            uint state = MenuHelper.GetState(hmenu, pos);
            if (state != uint.MaxValue && (state & User32.MFS_GRAYED) != 0)
                cancel = true;

            return cancel;
        }


        bool WmRegPostKeyDown(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            uint vk = (uint)wParam;
            IntPtr hmenu = lParam;

            if (vk == User32.VK_UP || vk == User32.VK_DOWN)
            {
                if (hmenu == MenuHelper.GetHMenu(HWndPopup))
                {
                    int pos = MenuHelper.GetHilitePos(hmenu);
                    if (pos >= 0)
                    {
                        KeyboardSelection = pos;

                        uint state = MenuHelper.GetState(hmenu, pos);
                        if (state != uint.MaxValue && (state & User32.MFS_GRAYED) != 0)
                            User32.PostMessageW(hwnd, User32.WM_KEYDOWN, (UIntPtr)vk, IntPtr.Zero);
                    }
                }
            }
            return cancel;
        }


        bool WmMouseMove(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            FirstMessageDone = true;

            IntPtr hmenu = MenuHelper.GetHMenu(HWndPopup);
            WinDef.POINT ptScreen = Win32Helper.LParamToPoint(lParam);
            int mouseSel = MenuHelper.HitTest(HWndPopup, ptScreen);
            uint state = MenuHelper.GetState(hmenu, mouseSel);
            if (state != uint.MaxValue && (state & User32.MFS_GRAYED) != 0)
                mouseSel = -1;

            int oldMouseSel = MouseSelection;
            int keyboardSel = KeyboardSelection;

            if (mouseSel != oldMouseSel)
            {
                MouseSelection = mouseSel;
                if (mouseSel >= 0)
                {
                    if (mouseSel != keyboardSel)
                        KeyboardSelection = mouseSel;
                }
                else
                {
                    cancel = true;
                    if (oldMouseSel == keyboardSel)
                        MenuHelper.ShowSelect(HWndPopup, -1);
                }
            }
            else if (mouseSel != keyboardSel || mouseSel < 0)
                cancel = true;

            return cancel;
        }


        bool WmMouseButtonDown(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            bool cancel = false;

            FirstMessageDone = true;

            IntPtr hmenu = MenuHelper.GetHMenu(HWndPopup);
            WinDef.POINT ptScreen = Win32Helper.LParamToPoint(lParam);
            int pos = MenuHelper.HitTest(HWndPopup, ptScreen);
            uint state = MenuHelper.GetState(hmenu, pos);
            if (state != uint.MaxValue && (state & User32.MFS_GRAYED) != 0)
                cancel = true;

            return cancel;
        }
    }


    static class MenuHelper
    {
        static uint _RegPostKeyDown;
        public static uint RegPostKeyDown
        {
            get
            {
                if (_RegPostKeyDown == 0)
                    _RegPostKeyDown = User32.RegisterWindowMessageW("MenuHookPostKeyDownMessage");
                return _RegPostKeyDown;
            }
        }


        public static uint GetGuiThreadFlags()
        {
            var gti = new User32.GUITHREADINFO()
            {
                cbSize = (uint)Marshal.SizeOf<User32.GUITHREADINFO>(),
            };

            if (User32.GetGUIThreadInfo(Kernel32.GetCurrentThreadId(), ref gti))
                return gti.flags;
            else
                return uint.MaxValue;
        }


        public static uint GetState(IntPtr hmenu, int pos)
        {
            uint result = uint.MaxValue;
            if (pos >= 0)
            {
                var mii = new User32.MENUITEMINFOW()
                {
                    cbSize = (uint)Marshal.SizeOf<User32.MENUITEMINFOW>(),
                    fMask = User32.MIIM_STATE,
                };
                if (User32.GetMenuItemInfoW(hmenu, (uint)pos, true, ref mii))
                    result = mii.fState;
            }
            return result;
        }


        static uint mn_selectitem = 0x1E5;

        public static void ShowSelect(IntPtr hwnd, int pos)
        {
            User32.SendMessageW(hwnd, mn_selectitem, (UIntPtr)(uint)pos, IntPtr.Zero);
        }


        public static IntPtr GetHMenu(IntPtr hwnd)
        {
            return User32.SendMessageW(hwnd, User32.MN_GETHMENU, UIntPtr.Zero, IntPtr.Zero);
        }


        public static int HitTest(IntPtr hwnd, WinDef.POINT ptScreen)
        {
            IntPtr hmenu = GetHMenu(hwnd);
            return User32.MenuItemFromPoint(hwnd, hmenu, ptScreen);
        }

        public static int GetHilitePos(IntPtr hmenu)
        {
            int result = -1;
            if (hmenu != IntPtr.Zero)
            {
                int count = User32.GetMenuItemCount(hmenu);
                for (int i = 0; result < 0 && i < count; i++)
                {
                    uint state = GetState(hmenu, i);
                    if (state != uint.MaxValue && (state & User32.MFS_HILITE) != 0)
                        result = i;
                }
            }
            return result;
        }



        public static int GetFirstEnabled(IntPtr hmenu)
        {
            int result = -1;
            if (hmenu != IntPtr.Zero)
            {
                int count = User32.GetMenuItemCount(hmenu);
                for (int i = 0; result < 0 && i < count; i++)
                {
                    uint state = GetState(hmenu, i);
                    if (state != uint.MaxValue && (state & User32.MFS_GRAYED) == 0)
                        result = i;
                }
            }
            return result;
        }


        public static int GetMnemonicItem(IntPtr hmenu, char c)
        {
            int result = -1;
            if (hmenu != null && c != '\0')
            {
                c = char.ToUpper(c);

                int count = User32.GetMenuItemCount(hmenu);
                for (int i = 0; result < 0 && i < count; i++)
                {
                    var mii = new User32.MENUITEMINFOW()
                    {
                        cbSize = (uint)Marshal.SizeOf<User32.MENUITEMINFOW>(),
                        fMask = User32.MIIM_STRING,
                        dwTypeData = IntPtr.Zero,
                        cch = 0,
                    };
                    if (User32.GetMenuItemInfoW(hmenu, (uint)i, true, ref mii))
                    {
                        mii.cch++;
                        var ssh = new StringSafeHandle((Int32)mii.cch, true);
                        mii.dwTypeData = ssh;
                        if (User32.GetMenuItemInfoW(hmenu, (uint)i, true, ref mii))
                        {
                            string name = ssh.ToString();
                            int index = name.IndexOf('&');
                            if (0 <= index && index + 1 < mii.cch
                                && char.ToUpper(name[index + 1]) == c)
                                result = i;
                        }
                    }
                }
            }
            return result;
        }
    }
}
