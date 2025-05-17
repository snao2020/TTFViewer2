using System;
using System.Runtime.InteropServices;

namespace Win32
{
    public static partial class Kernel32
    {
        /*
        WINBASEAPI _When_(lpModuleName == NULL, _Ret_notnull_) _When_(lpModuleName != NULL, _Ret_maybenull_)
        HMODULE WINAPI GetModuleHandleA(_In_opt_ LPCSTR lpModuleName);

        WINBASEAPI _When_(lpModuleName == NULL, _Ret_notnull_) _When_(lpModuleName != NULL, _Ret_maybenull_)
        HMODULE WINAPI GetModuleHandleW(_In_opt_ LPCWSTR lpModuleName);
        */

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr GetModuleHandleA(string lpModuleName);

        [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetModuleHandleW(string lpModuleName);
    }
}
