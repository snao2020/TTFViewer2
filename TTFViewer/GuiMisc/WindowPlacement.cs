using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Win32;

namespace WindowPlacementSample
{
    public static class WindowPlacementHelper
    {
        public static string GetWindowPlacement(this Window window, bool ownerRelative)
        {
            string result = null;
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd != null)
            {
                var wp = new User32.WINDOWPLACEMENT
                {
                    length = (uint)Marshal.SizeOf(typeof(User32.WINDOWPLACEMENT))
                };
                if (User32.GetWindowPlacement(hwnd, ref wp))
                {
                    if (ownerRelative && window.Owner != null)
                    {
                        IntPtr hwndOwner = new WindowInteropHelper(window.Owner).Handle;
                        User32.GetWindowRect(hwndOwner, out WinDef.RECT rcParent);
                        User32.OffsetRect(ref wp.rcNormalPosition, -rcParent.left, -rcParent.top);
                    }

                    result = $"{wp.length},{wp.flags},{wp.showCmd},"
                                + $"{wp.ptMinPosition.x},{wp.ptMinPosition.y},"
                                + $"{wp.ptMaxPosition.x},{wp.ptMaxPosition.y},"
                                + $"{wp.rcNormalPosition.left},{wp.rcNormalPosition.top},"
                                + $"{wp.rcNormalPosition.right},{wp.rcNormalPosition.bottom}";
                }
            }
            return result;
        }


        public static bool SetWindowPlacement(this Window window, string text, bool ownerRelative)
        {
            bool result = false;

            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd != null)
            {
                if (text != null)
                {
                    string[] p = text.Split(',');
                    if (p.Length > 10)
                    {
                        var wp = new User32.WINDOWPLACEMENT
                        {
                            length = UInt32.Parse(p[0]),
                            flags = UInt32.Parse(p[1]),
                            showCmd = UInt32.Parse(p[2]),
                            ptMinPosition = new WinDef.POINT
                            {
                                x = Int32.Parse(p[3]),
                                y = Int32.Parse(p[4]),
                            },
                            ptMaxPosition = new WinDef.POINT
                            {
                                x = Int32.Parse(p[5]),
                                y = Int32.Parse(p[6]),
                            },
                            rcNormalPosition = new WinDef.RECT
                            {
                                left = Int32.Parse(p[7]),
                                top = Int32.Parse(p[8]),
                                right = Int32.Parse(p[9]),
                                bottom = Int32.Parse(p[10]),
                            }
                        };

                        if (ownerRelative && window.Owner != null)
                        {
                            IntPtr hwndOwner = new WindowInteropHelper(window.Owner).Handle;
                            User32.GetWindowRect(hwndOwner, out WinDef.RECT rcParent);
                            User32.OffsetRect(ref wp.rcNormalPosition, rcParent.left, rcParent.top);
                        }

                        WinDef.POINT anchorPoint = Win32Helper.TopLeft(wp.rcNormalPosition);
                        WinDef.SIZE windowSize = Win32Helper.Size(wp.rcNormalPosition);
                        User32.CalculatePopupWindowPosition(ref anchorPoint, ref windowSize, User32.TPM_WORKAREA, IntPtr.Zero, out wp.rcNormalPosition);

                        if (wp.showCmd == User32.SW_HIDE || wp.showCmd == User32.SW_SHOWMINIMIZED
                            || wp.showCmd == User32.SW_MINIMIZE || wp.showCmd == User32.SW_SHOWMINNOACTIVE)
                        {
                            wp.showCmd = User32.SW_SHOWNORMAL;
                        }
                        result = User32.SetWindowPlacement(hwnd, ref wp);
                    }
                }
            }
            return result;
        }
    }
}
