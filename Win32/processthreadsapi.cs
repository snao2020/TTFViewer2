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

    public static partial class Kernel32
    {

        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public UInt32 dwProcessId;
            public UInt32 dwThreadId;
        }

        public struct STARTUPINFOA
        {
            public UInt32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public UInt32 dwX;
            public UInt32 dwY;
            public UInt32 dwXSize;
            public UInt32 dwYSize;
            public UInt32 dwXCountChars;
            public UInt32 dwYCountChars;
            public UInt32 dwFillAttribute;
            public UInt32 dwFlags;
            public UInt16 wShowWindow;
            public UInt16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        public struct STARTUPINFOW
        {
            public UInt32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public UInt32 dwX;
            public UInt32 dwY;
            public UInt32 dwXSize;
            public UInt32 dwYSize;
            public UInt32 dwXCountChars;
            public UInt32 dwYCountChars;
            public UInt32 dwFillAttribute;
            public UInt32 dwFlags;
            public UInt16 wShowWindow;
            public UInt16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }


#if WIN32_WINNT_NT4_LATER
        /*
        WINBASEAPI DWORD WINAPI QueueUserAPC( 
            _In_ PAPCFUNC pfnAPC,
            _In_ HANDLE hThread,
            _In_ ULONG_PTR dwData);
        */
#endif // WIN32_WINNT_NT4_LATER

        /*
        WINBASEAPI BOOL WINAPI GetProcessTimes(
            _In_ HANDLE hProcess,
            _Out_ LPFILETIME lpCreationTime,
            _Out_ LPFILETIME lpExitTime,
            _Out_ LPFILETIME lpKernelTime,
            _Out_ LPFILETIME lpUserTime);
        */

        // WINBASEAPI HANDLE WINAPI GetCurrentProcess(VOID);

        // WINBASEAPI DWORD WINAPI GetCurrentProcessId(VOID);


        // WINBASEAPI DECLSPEC_NORETURN VOID WINAPI ExitProcess(_In_ UINT uExitCode);


        // WINBASEAPI BOOL WINAPI TerminateProcess(_In_ HANDLE hProcess, _In_ UINT uExitCode);

        // WINBASEAPI BOOL WINAPI GetExitCodeProcess(_In_ HANDLE hProcess, _Out_ LPDWORD lpExitCode);

        // WINBASEAPI BOOL WINAPI SwitchToThread(VOID);

        /*
        WINBASEAPI _Ret_maybenull_ HANDLE WINAPI CreateThread(
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ SIZE_T dwStackSize,
            _In_ LPTHREAD_START_ROUTINE lpStartAddress,
            _In_opt_ __drv_aliasesMem LPVOID lpParameter,
            _In_ DWORD dwCreationFlags,
            _Out_opt_ LPDWORD lpThreadId);
        */

        /*
        WINBASEAPI _Ret_maybenull_ HANDLE WINAPI CreateRemoteThread(
            _In_ HANDLE hProcess,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ SIZE_T dwStackSize,
            _In_ LPTHREAD_START_ROUTINE lpStartAddress,
            _In_opt_ LPVOID lpParameter,
            _In_ DWORD dwCreationFlags,
            _Out_opt_ LPDWORD lpThreadId);
        */


        // WINBASEAPI HANDLE WINAPI GetCurrentThread(VOID);

        // WINBASEAPI DWORD WINAPI GetCurrentThreadId(VOID); // no LastError
        [DllImport("kernel32.dll")]
        public static extern UInt32 GetCurrentThreadId();

        /*
        WINBASEAPI _Ret_maybenull_ HANDLE WINAPI OpenThread(
            _In_ DWORD dwDesiredAccess,
            _In_ BOOL bInheritHandle,
            _In_ DWORD dwThreadId);
        */

        // WINBASEAPI BOOL WINAPI SetThreadPriority(_In_ HANDLE hThread, _In_ int nPriority);

        // WINBASEAPI BOOL WINAPI SetThreadPriorityBoost(_In_ HANDLE hThread, _In_ BOOL bDisablePriorityBoost);

        // WINBASEAPI BOOL WINAPI GetThreadPriorityBoost(_In_ HANDLE hThread, _Out_ PBOOL pDisablePriorityBoost);

        // WINBASEAPI int WINAPI GetThreadPriority(_In_ HANDLE hThread);

        // WINBASEAPI DECLSPEC_NORETURN VOID WINAPI ExitThread(_In_ DWORD dwExitCode);


        // WINBASEAPI BOOL WINAPI TerminateThread(_In_ HANDLE hThread, _In_ DWORD dwExitCode);


        // WINBASEAPI _Success_(return != 0) BOOL WINAPI GetExitCodeThread(_In_ HANDLE hThread, _Out_ LPDWORD lpExitCode);

        // WINBASEAPI DWORD WINAPI SuspendThread(_In_ HANDLE hThread);

        // WINBASEAPI DWORD WINAPI ResumeThread(_In_ HANDLE hThread);


#if !TLS_OUT_OF_INDEXES
        public static UInt32 TLS_OUT_OF_INDEXES = 0xFFFFFFFF;
#endif

        // _Must_inspect_result_ WINBASEAPI DWORD WINAPI TlsAlloc(VOID);

        // WINBASEAPI LPVOID WINAPI TlsGetValue(_In_ DWORD dwTlsIndex);

        // WINBASEAPI BOOL WINAPI TlsSetValue(_In_ DWORD dwTlsIndex, _In_opt_ LPVOID lpTlsValue);

        // WINBASEAPI BOOL WINAPI TlsFree(_In_ DWORD dwTlsIndex);


        /*
        WINBASEAPI BOOL WINAPI CreateProcessA(
            _In_opt_ LPCSTR lpApplicationName,
            _Inout_opt_ LPSTR lpCommandLine,
            _In_opt_ LPSECURITY_ATTRIBUTES lpProcessAttributes,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ BOOL bInheritHandles,
            _In_ DWORD dwCreationFlags,
            _In_opt_ LPVOID lpEnvironment,
            _In_opt_ LPCSTR lpCurrentDirectory,
            _In_ LPSTARTUPINFOA lpStartupInfo,
            _Out_ LPPROCESS_INFORMATION lpProcessInformation);

        WINBASEAPI BOOL WINAPI CreateProcessW(
            _In_opt_ LPCWSTR lpApplicationName,
            _Inout_opt_ LPWSTR lpCommandLine,
            _In_opt_ LPSECURITY_ATTRIBUTES lpProcessAttributes,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ BOOL bInheritHandles,
            _In_ DWORD dwCreationFlags,
            _In_opt_ LPVOID lpEnvironment,
            _In_opt_ LPCWSTR lpCurrentDirectory,
            _In_ LPSTARTUPINFOW lpStartupInfo,
            _Out_ LPPROCESS_INFORMATION lpProcessInformation);
        */

        // WINBASEAPI BOOL WINAPI SetProcessShutdownParameters(_In_ DWORD dwLevel, _In_ DWORD dwFlags);

        // WINBASEAPI DWORD WINAPI GetProcessVersion(_In_ DWORD ProcessId);

        /*
        WINBASEAPI VOID WINAPI GetStartupInfoW(_Out_ LPSTARTUPINFOW lpStartupInfo);
        */
        /*
        WINADVAPI BOOL WINAPI CreateProcessAsUserW(
            _In_opt_ HANDLE hToken,
            _In_opt_ LPCWSTR lpApplicationName,
            _Inout_opt_ LPWSTR lpCommandLine,
            _In_opt_ LPSECURITY_ATTRIBUTES lpProcessAttributes,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ BOOL bInheritHandles,
            _In_ DWORD dwCreationFlags,
            _In_opt_ LPVOID lpEnvironment,
            _In_opt_ LPCWSTR lpCurrentDirectory,
            _In_ LPSTARTUPINFOW lpStartupInfo,
            _Out_ LPPROCESS_INFORMATION lpProcessInformation);
        */


#if !MIDL_PASS
#if WIN32_WINNT_WIN8_LATER
        /*
        FORCEINLINE HANDLE GetCurrentProcessToken (VOID)
        {
            return (HANDLE)(LONG_PTR) -4;
        }
        */
        /*
        FORCEINLINE HANDLE GetCurrentThreadToken (VOID)
        {
            return (HANDLE)(LONG_PTR) -5;
        }
        */
        /*
        FORCEINLINE HANDLE GetCurrentThreadEffectiveToken (VOID)
        {
            return (HANDLE)(LONG_PTR) -6;
        }
        */

#endif // WIN32_WINNT_WIN8_LATER
#endif // !defined(MIDL_PASS)

        // WINADVAPI _Must_inspect_result_ BOOL APIENTRY SetThreadToken(_In_opt_ PHANDLE Thread, _In_opt_ HANDLE Token);

        /*
        WINADVAPI BOOL WINAPI OpenProcessToken(
            _In_ HANDLE ProcessHandle,
            _In_ DWORD DesiredAccess,
            _Outptr_ PHANDLE TokenHandle);
        */

        /*
        WINADVAPI BOOL WINAPI OpenThreadToken(
            _In_ HANDLE ThreadHandle,
            _In_ DWORD DesiredAccess,
            _In_ BOOL OpenAsSelf,
            _Outptr_ PHANDLE TokenHandle);
        */

        // WINBASEAPI BOOL WINAPI SetPriorityClass(_In_ HANDLE hProcess, _In_ DWORD dwPriorityClass);

        // WINBASEAPI BOOL WINAPI SetThreadStackGuarantee(_Inout_ PULONG StackSizeInBytes);

        // WINBASEAPI DWORD WINAPI GetPriorityClass(_In_ HANDLE hProcess);

        // WINBASEAPI BOOL WINAPI ProcessIdToSessionId(_In_ DWORD dwProcessId, _Out_ DWORD * pSessionId);

        // typedef struct _PROC_THREAD_ATTRIBUTE_LIST *PPROC_THREAD_ATTRIBUTE_LIST, *LPPROC_THREAD_ATTRIBUTE_LIST;

#if WIN32_WINNT_WINXP_LATER

        // WINBASEAPI DWORD WINAPI GetProcessId(_In_ HANDLE Process);

#endif // WIN32_WINNT_WINXP_LATER


#if WIN32_WINNT_WS03_LATER

        // WINBASEAPI DWORD WINAPI GetThreadId(_In_ HANDLE Thread);

#endif // WIN32_WINNT_WS03_LATER


#if WIN32_WINNT_VISTA_LATER

        // WINBASEAPI VOID WINAPI FlushProcessWriteBuffers(VOID);

#endif // WIN32_WINNT_VISTA_LATER


#if WIN32_WINNT_VISTA_LATER

        // WINBASEAPI DWORD WINAPI GetProcessIdOfThread(_In_ HANDLE Thread);

        /*
        WINBASEAPI _Success_(return != FALSE) BOOL WINAPI InitializeProcThreadAttributeList(
            _Out_writes_bytes_to_opt_(*lpSize, *lpSize) LPPROC_THREAD_ATTRIBUTE_LIST lpAttributeList,
            _In_ DWORD dwAttributeCount,
            _Reserved_ DWORD dwFlags,
            _When_(lpAttributeList == nullptr, _Out_) _When_(lpAttributeList != nullptr, _Inout_) PSIZE_T lpSize);
        */

        // WINBASEAPI VOID WINAPI DeleteProcThreadAttributeList(_Inout_ LPPROC_THREAD_ATTRIBUTE_LIST lpAttributeList);


        public const UInt32 PROCESS_AFFINITY_ENABLE_AUTO_UPDATE     = 0x00000001;

        // WINBASEAPI BOOL WINAPI SetProcessAffinityUpdateMode(_In_ HANDLE hProcess, _In_ DWORD dwFlags);

        // WINBASEAPI BOOL WINAPI QueryProcessAffinityUpdateMode(_In_ HANDLE hProcess, _Out_opt_ LPDWORD lpdwFlags);


        public const UInt32 PROC_THREAD_ATTRIBUTE_REPLACE_VALUE     = 0x00000001;

        /*
        WINBASEAPI BOOL WINAPI UpdateProcThreadAttribute(
            _Inout_ LPPROC_THREAD_ATTRIBUTE_LIST lpAttributeList,
            _In_ DWORD dwFlags,
            _In_ DWORD_PTR Attribute,
            _In_reads_bytes_opt_(cbSize) PVOID lpValue,
            _In_ SIZE_T cbSize,
            _Out_writes_bytes_opt_(cbSize) PVOID lpPreviousValue,
            _In_opt_ PSIZE_T lpReturnSize);
        */

#endif // WIN32_WINNT_VISTA_LATER

        /*
        WINBASEAPI _Ret_maybenull_ HANDLE WINAPI CreateRemoteThreadEx(
            _In_ HANDLE hProcess,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ SIZE_T dwStackSize,
            _In_ LPTHREAD_START_ROUTINE lpStartAddress,
            _In_opt_ LPVOID lpParameter,
            _In_ DWORD dwCreationFlags,
            _In_opt_ LPPROC_THREAD_ATTRIBUTE_LIST lpAttributeList,
            _Out_opt_ LPDWORD lpThreadId);
        */


#if !MIDL_PASS

        //#if (defined(_WIN32_WINNT) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)


#if WIN32_WINNT_WIN8_LATER

        // WINBASEAPI VOID WINAPI GetCurrentThreadStackLimits(_Out_ PULONG_PTR LowLimit, _Out_ PULONG_PTR HighLimit);

#endif

        //WINBASEAPI BOOL WINAPI GetThreadContext(_In_ HANDLE hThread, _Inout_ LPCONTEXT lpContext);


#if WIN32_WINNT_WIN8_LATER
        /*
        WINBASEAPI BOOL WINAPI GetProcessMitigationPolicy(
            _In_ HANDLE hProcess,
            _In_ PROCESS_MITIGATION_POLICY MitigationPolicy,
            _Out_writes_bytes_(dwLength) PVOID lpBuffer,
            _In_ SIZE_T dwLength);
        */
#endif


        // WINBASEAPI BOOL WINAPI SetThreadContext(_In_ HANDLE hThread, _In_ CONST CONTEXT * lpContext);

#if WIN32_WINNT_WIN8_LATER
        /*
        WINBASEAPI BOOL WINAPI SetProcessMitigationPolicy(
            _In_ PROCESS_MITIGATION_POLICY MitigationPolicy,
            _In_reads_bytes_(dwLength) PVOID lpBuffer,
            _In_ SIZE_T dwLength);
        */
#endif


        //#endif // (defined(_WIN32_WINNT) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)
#endif // defined(MIDL_PASS)


        //#if (defined(_WIN32_WINNT) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)

        /*
        WINBASEAPI BOOL WINAPI FlushInstructionCache(
            _In_ HANDLE hProcess,
            _In_reads_bytes_opt_(dwSize) LPCVOID lpBaseAddress,
            _In_ SIZE_T dwSize);
        */

        /*
        WINBASEAPI BOOL WINAPI GetThreadTimes(
            _In_ HANDLE hThread,
            _Out_ LPFILETIME lpCreationTime,
            _Out_ LPFILETIME lpExitTime,
            _Out_ LPFILETIME lpKernelTime,
            _Out_ LPFILETIME lpUserTime);
        */
        /*
        WINBASEAPI HANDLE WINAPI OpenProcess(
            _In_ DWORD dwDesiredAccess,
            _In_ BOOL bInheritHandle,
            _In_ DWORD dwProcessId);
        */

        // WINBASEAPI BOOL WINAPI IsProcessorFeaturePresent(_In_ DWORD ProcessorFeature);


        //#endif // (defined(_WIN32_WINNT) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)


        //#if ((_WIN32_WINNT >= 0x0501) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)

        // WINBASEAPI BOOL WINAPI GetProcessHandleCount(_In_ HANDLE hProcess, _Out_ PDWORD pdwHandleCount);

        //#endif // ((_WIN32_WINNT >= 0x0501) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)


        //#if ((_WIN32_WINNT >= 0x0502) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)

        // WINBASEAPI DWORD WINAPI GetCurrentProcessorNumber(VOID);

        //#endif // ((_WIN32_WINNT >= 0x0502) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)


        //#if ((_WIN32_WINNT >= 0x0601) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)

        /*
        WINBASEAPI BOOL WINAPI SetThreadIdealProcessorEx(
            _In_ HANDLE hThread,
            _In_ PPROCESSOR_NUMBER lpIdealProcessor,
            _Out_opt_ PPROCESSOR_NUMBER lpPreviousIdealProcessor);
        */

        // WINBASEAPI BOOL WINAPI GetThreadIdealProcessorEx(_In_ HANDLE hThread, _Out_ PPROCESSOR_NUMBER lpIdealProcessor);

        // WINBASEAPI VOID WINAPI GetCurrentProcessorNumberEx(_Out_ PPROCESSOR_NUMBER ProcNumber);

        //#endif // ((_WIN32_WINNT >= 0x0601) && !defined(_CONTRACT_GEN)) || (_APISET_PROCESSTHREADS_VER > 0x0100)


        //#if !defined(_CONTRACT_GEN) || (_APISET_PROCESSTHREADS_VER >= 0x0102)


#if WIN32_WINNT_WINXP_LATER

        // WINBASEAPI BOOL WINAPI GetProcessPriorityBoost(_In_ HANDLE hProcess, _Out_ PBOOL pDisablePriorityBoost);

        // WINBASEAPI BOOL WINAPI SetProcessPriorityBoost(_In_ HANDLE hProcess, _In_ BOOL bDisablePriorityBoost);

        // WINBASEAPI BOOL WINAPI GetThreadIOPendingFlag(_In_ HANDLE hThread, _Out_ PBOOL lpIOIsPending);
        /*
        WINBASEAPI BOOL WINAPI GetSystemTimes(
            _Out_opt_ PFILETIME lpIdleTime,
            _Out_opt_ PFILETIME lpKernelTime,
            _Out_opt_ PFILETIME lpUserTime);
        */

#endif // WIN32_WINNT_WINXP_LATER

        public enum THREAD_INFORMATION_CLASS
        {
            ThreadMemoryPriority,
            ThreadAbsoluteCpuPriority,
            ThreadDynamicCodePolicy,
            ThreadInformationClassMax
        }


#if WIN32_WINNT_WIN8_LATER

        public struct MEMORY_PRIORITY_INFORMATION
        {
            public UInt32 MemoryPriority;
        }
        
        /*
        WINBASEAPI BOOL WINAPI GetThreadInformation(
            _In_ HANDLE hThread,
            _In_ THREAD_INFORMATION_CLASS ThreadInformationClass,
            _Out_writes_bytes_(ThreadInformationSize) LPVOID ThreadInformation,
            _In_ DWORD ThreadInformationSize);
        */
        /*
        WINBASEAPI BOOL WINAPI SetThreadInformation(
            _In_ HANDLE hThread,
            _In_ THREAD_INFORMATION_CLASS ThreadInformationClass,
            _In_reads_bytes_(ThreadInformationSize) LPVOID ThreadInformation,
            _In_ DWORD ThreadInformationSize);
        */

#endif // WIN32_WINNT_WIN8_LATER


#if WIN32_WINNT_WINBLUE_LATER

        // WINBASEAPI BOOL WINAPI IsProcessCritical(_In_ HANDLE hProcess, _Out_ PBOOL Critical);

#endif // WIN32_WINNT_WINBLUE_LATER


#if WIN32_WINNT_WIN10_LATER

        /*
        WINBASEAPI BOOL WINAPI SetProtectedPolicy(
            _In_ LPCGUID PolicyGuid,
            _In_ ULONG_PTR PolicyValue,
            _Out_opt_ PULONG_PTR OldPolicyValue);
        */

        // WINBASEAPI BOOL WINAPI QueryProtectedPolicy(_In_ LPCGUID PolicyGuid, _Out_ PULONG_PTR PolicyValue);


#endif // WIN32_WINNT_WIN10_LATER


//#endif // !defined(_CONTRACT_GEN) || (_APISET_PROCESSTHREADS_VER >= 0x0102)


//#if !defined(_CONTRACT_GEN) || (_APISET_PROCESSTHREADS_VER >= 0x0103)


        // WINBASEAPI DWORD WINAPI SetThreadIdealProcessor(_In_ HANDLE hThread, _In_ DWORD dwIdealProcessor);


        public enum PROCESS_INFORMATION_CLASS
        {
            ProcessMemoryPriority,
            ProcessMemoryExhaustionInfo,
            ProcessAppMemoryInfo,
            ProcessInPrivateInfo,
            ProcessActivityThrottleStateInfo,
            ProcessActivityThrottlePolicyInfo,
            ProcessInformationClassMax
        }


        public  struct APP_MEMORY_INFORMATION
        {
            public UInt64 AvailableCommit;
            public UInt64 PrivateCommitUsage;
            public UInt64 PeakPrivateCommitUsage;
            public UInt64 TotalCommitUsage;
        }


        public const UInt32 PME_CURRENT_VERSION = 1;


        public  enum PROCESS_MEMORY_EXHAUSTION_TYPE
        {
            PMETypeFailFastOnCommitFailure,
            PMETypeMax
        }

        public const UInt32 PME_FAILFAST_ON_COMMIT_FAIL_DISABLE    = 0x0;
        public const UInt32 PME_FAILFAST_ON_COMMIT_FAIL_ENABLE     = 0x1;


        public struct PROCESS_MEMORY_EXHAUSTION_INFO
        {
            public UInt16 Version;
            public UInt16 Reserved;
            public PROCESS_MEMORY_EXHAUSTION_TYPE Type;
            public UIntPtr Value;
        }

        public enum PROCESS_ACTIVITY_THROTTLE_STATE
        {
            ProcessActivityThrottleStateSystemManaged = 0,
            ProcessActivityThrottleStateForceOn = 1,
            ProcessActivityThrottleStateForceOff = 2,
            MaxProcessActivityThrottleState
        }

        public enum PROCESS_ACTIVITY_THROTTLE_POLICY_OP
        {
            ProcessActivityThrottlePolicyDisable = 0,
            ProcessActivityThrottlePolicyEnable = 1,
            ProcessActivityThrottlePolicyDefault = 2,
            MaxProcessActivityThrottlePolicy
        }


        public const UInt32 PROCESS_ACTIVITY_THROTTLE_EXECUTIONSPEED = 0x1;
        public const UInt32 PROCESS_ACTIVITY_THROTTLE_DELAYTIMERS = 0x2;
        public const UInt32 PROCESS_ACTIVITY_THROTTLE_ALL = ((PROCESS_ACTIVITY_THROTTLE_EXECUTIONSPEED | PROCESS_ACTIVITY_THROTTLE_DELAYTIMERS));

        public struct PROCESS_ACTIVITY_THROTTLE_POLICY
        {
            public PROCESS_ACTIVITY_THROTTLE_POLICY_OP Operation;
            public UInt32 PolicyFlags;
        }


#if WIN32_WINNT_WIN8_LATER

        /*
        WINBASEAPI BOOL WINAPI SetProcessInformation(
            _In_ HANDLE hProcess,
            _In_ PROCESS_INFORMATION_CLASS ProcessInformationClass,
            _In_reads_bytes_(ProcessInformationSize) LPVOID ProcessInformation,
            _In_ DWORD ProcessInformationSize);
        */
        /*
        WINBASEAPI BOOL WINAPI GetProcessInformation(
            _In_ HANDLE hProcess,
            _In_ PROCESS_INFORMATION_CLASS ProcessInformationClass,
            _Out_writes_bytes_(ProcessInformationSize) LPVOID ProcessInformation,
            _In_ DWORD ProcessInformationSize);
        */

#endif // WIN32_WINNT_WIN8_LATER


#if WIN32_WINNT_WIN10_LATER

        /*
        _Success_(return != FALSE) BOOL WINAPI GetSystemCpuSetInformation(
            _Out_writes_bytes_to_opt_(BufferLength, *ReturnedLength) PSYSTEM_CPU_SET_INFORMATION Information,
            _In_ ULONG BufferLength,
            _Always_(_Out_) PULONG ReturnedLength,
            _In_opt_ HANDLE Process,
            _Reserved_ ULONG Flags);
        */
        /*
        _Success_(return != FALSE) BOOL WINAPI GetProcessDefaultCpuSets(
            _In_ HANDLE Process,
            _Out_writes_to_opt_(CpuSetIdCount, *RequiredIdCount) PULONG CpuSetIds,
            _In_ ULONG CpuSetIdCount,
            _Always_(_Out_) PULONG RequiredIdCount);
        */
        /*
        _Success_(return != FALSE) BOOL WINAPI SetProcessDefaultCpuSets(
            _In_ HANDLE Process,
            _In_reads_opt_(CpuSetIdCount) const ULONG * CpuSetIds,
            _In_ ULONG CpuSetIdCount);
        */
        /*
        _Success_(return != FALSE) BOOL WINAPI GetThreadSelectedCpuSets(
            _In_ HANDLE Thread,
            _Out_writes_to_opt_(CpuSetIdCount, *RequiredIdCount) PULONG CpuSetIds,
            _In_ ULONG CpuSetIdCount,
            _Always_(_Out_) PULONG RequiredIdCount);
        */
        /*
        _Success_(return != FALSE) BOOL WINAPI SetThreadSelectedCpuSets(
            _In_ HANDLE Thread,
            _In_reads_(CpuSetIdCount) const ULONG * CpuSetIds,
            _In_ ULONG CpuSetIdCount);
        */

#endif // WIN32_WINNT_WIN10_LATER

        /*
        WINADVAPI BOOL WINAPI CreateProcessAsUserA(
            _In_opt_ HANDLE hToken,
            _In_opt_ LPCSTR lpApplicationName,
            _Inout_opt_ LPSTR lpCommandLine,
            _In_opt_ LPSECURITY_ATTRIBUTES lpProcessAttributes,
            _In_opt_ LPSECURITY_ATTRIBUTES lpThreadAttributes,
            _In_ BOOL bInheritHandles,
            _In_ DWORD dwCreationFlags,
            _In_opt_ LPVOID lpEnvironment,
            _In_opt_ LPCSTR lpCurrentDirectory,
            _In_ LPSTARTUPINFOA lpStartupInfo,
            _Out_ LPPROCESS_INFORMATION lpProcessInformation);
        */

        //WINBASEAPI BOOL WINAPI GetProcessShutdownParameters(_Out_ LPDWORD lpdwLevel, _Out_ LPDWORD lpdwFlags);

        // WINBASEAPI HRESULT WINAPI SetThreadDescription(_In_ HANDLE hThread, _In_ PCWSTR lpThreadDescription);

        // WINBASEAPI HRESULT WINAPI GetThreadDescription(_In_ HANDLE hThread, _Outptr_result_z_ PWSTR * ppszThreadDescription);

        //#endif // !defined(_CONTRACT_GEN) || (_APISET_PROCESSTHREADS_VER >= 0x0103)

    }
    
#pragma warning restore IDE1006 
}
