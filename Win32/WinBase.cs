namespace Win32
{
    public static partial class Kernel32
    {
        /*
        WINBASEAPI _Success_(return != 0) DWORD WINAPI FormatMessageA(
            _In_ DWORD dwFlags,
            _In_opt_ LPCVOID lpSource,
            _In_ DWORD dwMessageId,
            _In_ DWORD dwLanguageId,
            _When_((dwFlags & FORMAT_MESSAGE_ALLOCATE_BUFFER) != 0, _At_((LPSTR*) lpBuffer, _Outptr_result_z_))
            _When_((dwFlags & FORMAT_MESSAGE_ALLOCATE_BUFFER) == 0, _Out_writes_z_(nSize))
                        LPSTR lpBuffer,
            _In_     DWORD nSize,
            _In_opt_ va_list* Arguments);

        WINBASEAPI _Success_(return != 0) DWORD WINAPI FormatMessageW(
            _In_ DWORD dwFlags,
            _In_opt_ LPCVOID lpSource,
            _In_ DWORD dwMessageId,
            _In_ DWORD dwLanguageId,
            _When_((dwFlags & FORMAT_MESSAGE_ALLOCATE_BUFFER) != 0, _At_((LPWSTR*) lpBuffer, _Outptr_result_z_))
            _When_((dwFlags & FORMAT_MESSAGE_ALLOCATE_BUFFER) == 0, _Out_writes_z_(nSize))
                 LPWSTR lpBuffer,
            _In_     DWORD nSize,
            _In_opt_ va_list* Arguments);
        */


        //WINBASEAPI ATOM WINAPI GlobalFindAtomA(_In_opt_ LPCSTR lpString);
        //WINBASEAPI ATOM WINAPI GlobalFindAtomW(_In_opt_ LPCWSTR lpString);

        /*
        WINBASEAPI UINT WINAPI GlobalGetAtomNameA(
            _In_ ATOM nAtom,
            _Out_writes_to_(nSize, return + 1) LPSTR lpBuffer,
            _In_ int nSize);

        WINBASEAPI UINT WINAPI GlobalGetAtomNameW(
            _In_ ATOM nAtom,
            _Out_writes_to_(nSize, return + 1) LPWSTR lpBuffer,
            _In_ int nSize);

        //#ifdef UNICODE
        //#define GlobalGetAtomName  GlobalGetAtomNameW
        //#else
        //#define GlobalGetAtomName  GlobalGetAtomNameA
        //#endif // !UNICODE
        */

        // WINBASEAPI ATOM WINAPI FindAtomA(_In_opt_ LPCSTR lpString);

        // WINBASEAPI ATOM WINAPI FindAtomW(_In_opt_ LPCWSTR lpString);

        //# ifdef UNICODE
        //#define FindAtom  FindAtomW
        //#else
        //#define FindAtom  FindAtomA
        //#endif // !UNICODE

        /*
        WINBASEAPI UINT WINAPI GetAtomNameA(
            _In_ ATOM nAtom,
            _Out_writes_to_(nSize, return + 1) LPSTR lpBuffer,
            _In_ int nSize);

        WINBASEAPI UINT WINAPI GetAtomNameW(
            _In_ ATOM nAtom,
            _Out_writes_to_(nSize, return + 1) LPWSTR lpBuffer,
            _In_ int nSize);

        //#ifdef UNICODE
        //#define GetAtomName  GetAtomNameW
        //#else
        //#define GetAtomName  GetAtomNameA
        //#endif // !UNICODE
        */
    }
}
