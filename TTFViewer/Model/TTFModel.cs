using System;
using System.ComponentModel;
using System.IO;

namespace TTFViewer.Model
{
    class TTFModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public event EventHandler<ErrorEventArgs> ErrorEvent;

        private void RaiseErrorEvent(Exception exception)
        {
            ErrorEvent?.Invoke(this, new ErrorEventArgs(exception));
        }


        TableModel _RootItemModel;
        public TableModel RootItemModel
        {
            get
            {
                return _RootItemModel;
            }
            private set
            {
                if(value != _RootItemModel)
                {
                    _RootItemModel = value;
                    RaisePropertyChanged("RootItemModel");
                }
            }
        }


        DumpModel _DumpModel;
        public DumpModel DumpModel
        {
            get
            {
                return _DumpModel;
            }
            private set
            {
                if (value != _DumpModel)
                {
                    _DumpModel = value;
                    RaisePropertyChanged("DumpModel");
                }
            }
        }


        bool _IsOpen;
        public bool IsOpen
        {
            get => _IsOpen;
            private set
            {
                if (value != _IsOpen)
                {
                    _IsOpen = value;
                    RaisePropertyChanged("IsOpen");
                }
            }
        }


        public string FilePath { get; private set; }

        BinaryReader BinaryReader;


        public void Open(string path)
        {
            Close();

            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch(Exception e)
            {
                RaiseErrorEvent(e);
            }

            if (fs != null)
            {
                BinaryReader = new BinaryReader(fs);
                FilePath = path;
                RootItemModel = new TableModel(BinaryReader);
                DumpModel = new DumpModel(BinaryReader);
                IsOpen = true;
            }
        }


        public void Close()
        {
            IsOpen = false;
            FilePath = null;
            DumpModel = null;
            RootItemModel = null;
            if (BinaryReader != null)
            {
                BinaryReader.Dispose();
                BinaryReader = null;
            }
        }
    }
}
