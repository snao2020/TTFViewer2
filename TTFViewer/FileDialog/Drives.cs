using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Win32;
using WindowsHooks;

namespace FileDialogSample
{
    class Drives : CallWndProcHook, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        List<DirectoryInfo> _DirectoryList;
        public List<DirectoryInfo> DirectoryList
        {
            get => _DirectoryList;
            private set
            {
                if (value != _DirectoryList)
                {
                    _DirectoryList = value;
                    OnPropertyChanged("DirectoryList");
                }
            }
        }


        CancellationTokenSource TokenSource;


        public Drives()
        {
            AddHandler(User32.WM_DEVICECHANGE, WmDeviceChange, true);

            Update();
        }


        void WmDeviceChange(IntPtr hwnd, UIntPtr wParam, IntPtr lParam)
        {
            switch ((UInt32)wParam)
            {
                case User32.DBT_DEVICEARRIVAL:
                case User32.DBT_DEVICEREMOVECOMPLETE:
                    User32.DEV_BROADCAST_HDR hdr = (User32.DEV_BROADCAST_HDR)Marshal.PtrToStructure(lParam, typeof(User32.DEV_BROADCAST_HDR));
                    if (hdr.dbch_devicetype == User32.DBT_DEVTYP_VOLUME)
                        Update();
                    break;
            }
        }


        public void Update()
        {
#pragma warning disable CS4014  // no await
            UpdateAsync();
#pragma warning restore CS4014
        }


        async Task UpdateAsync()
        {
            DirectoryList = null;

            if (TokenSource != null)
                TokenSource.Cancel();

            TokenSource = new CancellationTokenSource();

            var t = Task<string>.Run(() => CreateDirectoryList(TokenSource.Token));
            List<DirectoryInfo> ret = await t;

            if (ret != null)
                DirectoryList = ret;
        }


        List<DirectoryInfo> CreateDirectoryList(CancellationToken ct)
        {
            var result = new List<DirectoryInfo>();
            foreach (var di in DriveInfo.GetDrives())
            {
                if (di.IsReady)
                {
                    result.Add(di.RootDirectory);
                }
            }

            if (ct.IsCancellationRequested)
                result = null;

            return result;
        }
    }
}
