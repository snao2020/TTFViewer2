namespace Win32
{
    public static partial class Kernel32
    {
        //WINBASEAPI _Check_return_ _Post_equals_last_error_ DWORD WINAPI GetLastError(VOID);
        //
        //--> use Marshal.GetLastWin32Error
    }
}
