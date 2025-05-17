using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using TTFViewer.Model;

namespace TTFViewer.ViewModel
{
    class TTFViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public event EventHandler<ErrorEventArgs> ErrorEvent;

        private void RaiseErrorEvent(ErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }


        ItemViewModel _ItemViewModel;
        public ItemViewModel ItemViewModel
        {
            get
            {
                return _ItemViewModel;
            }
            private set
            {
                if (value != _ItemViewModel)
                {
                    _ItemViewModel = value;
                    RaisePropertyChanged("ItemViewModel");
                }
            }
        }


        DumpViewModel _DumpViewModel;
        public DumpViewModel DumpViewModel
        {
            get
            {
                return _DumpViewModel;
            }
            private set
            {
                if (value != _DumpViewModel)
                {
                    _DumpViewModel = value;
                    RaisePropertyChanged("DumpViewModel");
                }
            }
        }

        static readonly string StaticWindowTitle = "TTFViewer";
        string _WindowTitle = StaticWindowTitle;
        public string WindowTitle
        {
            get
            {
                return _WindowTitle;
            }
            private set
            {
                if (value != _WindowTitle)
                {
                    _WindowTitle = value;
                    RaisePropertyChanged("WindowTitle");
                }
            }
        }


        public RelayCommand OpenCommand { get; }
        public RelayCommand CloseCommand { get; }


        TTFModel TTFModel;

        public TTFViewModel(TTFModel model)
        {
            TTFModel = model;
            OpenCommand = new RelayCommand(OnOpenCommand, null);
            CloseCommand = new RelayCommand(OnCloseCommand, (o) => { return TTFModel.RootItemModel != null; });

            TTFModel.PropertyChanged += OnModelPropertyChanged;
            TTFModel.ErrorEvent += OnError;
        }


        void OnOpenCommand(object parameter)
        {
            if (parameter is string path)
                TTFModel.Open(path);
        }


        void OnCloseCommand(object parameter)
        {
            TTFModel.Close();
        }


        void OnError(object sender, ErrorEventArgs e)
        {
            RaiseErrorEvent(e);
        }


        void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsOpen":
                    IsOpenPropertyChanged();
                    break;
            }
        }


        void IsOpenPropertyChanged()
        {
            CloseCommand.RaiseCanExecuteChanged();

            if (TTFModel.IsOpen)
            {
                ItemViewModel = new TTFItemViewModel(null, TTFModel.RootItemModel)
                {
                    IsExpanded = true,
                };

                DumpViewModel = new DumpViewModel(TTFModel.DumpModel);

                string filePath = TTFModel.FilePath;
                if (filePath != null)
                    WindowTitle = $"{Path.GetFileName(filePath)} {StaticWindowTitle}";
            }
            else
            {
                WindowTitle = StaticWindowTitle;
                DumpViewModel = null;
                ItemViewModel = null;
            }
        }
    }
}
