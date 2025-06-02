using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FileDialogSample
{
    /// <summary>
    /// Dialog.xaml の相互作用ロジック
    /// </summary>
    public partial class FileDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        bool _IsWindowOpen;
        public bool IsWindowOpen
        {
            get => _IsWindowOpen;
            set
            {
                _IsWindowOpen = value;
                NotifyPropertyChanged("IsWindowOpen");
            }
        }


        string _Directory = "C:\\";
        public string Directory
        {
            get => _Directory;
            set
            {
                if (value != _Directory)
                {
                    if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, @"^[A-Za-z]:\\"))
                    {
                        _Directory = value;
                        NotifyPropertyChanged("Directory");
                    }
                }
            }
        }


        string[] _Filters = new string[] { "*.*" };
        public string[] Filters
        {
            get => _Filters;
            set
            {
                if (value != _Filters)
                {
                    if (value != null && value.Length > 0)
                    {
                        _Filters = value;
                        FilterIndex = Math.Min(FilterIndex, value.Length - 1);
                        NotifyPropertyChanged("Filters");
                    }
                }
            }
        }


        int _FilterIndex = 0;
        public int FilterIndex
        {
            get => _FilterIndex;
            set
            {
                if(value != _FilterIndex)
                {
                    if (value >= 0 && value < Filters.Length)
                    {
                        _FilterIndex = value;
                        NotifyPropertyChanged("FilterIndex");
                    }
                }
            }
        }


        FileInfo _FileInfo;
        public FileInfo FileInfo
        {
            get => _FileInfo;
            set
            {
                if (value != _FileInfo)
                {
                    _FileInfo = value;
                    NotifyPropertyChanged("FileInfo");
                }
            }
        }


        public FileDialog()
        {
            InitializeComponent();
        }


        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            IsWindowOpen = true;
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            IsWindowOpen = false;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            object item = null;
            if (FileInfo != null)
            {
                Directory = FileInfo.DirectoryName;
                var items = FileListBox.ItemContainerGenerator.Items;
                item = items.FirstOrDefault(i => i is FileInfo fi && fi.Name.Equals(FileInfo.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (item != null)
            {
                FileListBox.SelectedItem = item;
                FileListBox.ScrollIntoView(item);
            }
            else
            {
                FileInfo = null;
                FileListBox.SelectedIndex = 0;
            }

            DependencyPropertyDescriptor
                .FromProperty(ListBox.ItemsSourceProperty, typeof(ListBox))
                .AddValueChanged(FileListBox, FileListBox_ItemsSourceChanged);
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            DependencyPropertyDescriptor
                .FromProperty(ListBox.ItemsSourceProperty, typeof(ListBox))
                .RemoveValueChanged(FileListBox, FileListBox_ItemsSourceChanged);
        }


        private void ExtensionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(e.Source is MenuItem mi)
            {
                FilterIndex = Array.IndexOf(Filters, mi.Header);
            }
        }


        private void FileListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = e.AddedItems;
            if (items != null && items.Count > 0)
                FileInfo = items[0] as FileInfo;
            else
                FileInfo = null;
        }


        private void FileListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && FileInfo != null)
            {
                DialogResult = true;
            }
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileInfo != null)
                DialogResult = true;
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        
        void FileListBox_ItemsSourceChanged(object sender, EventArgs e)
        {
            FileListBox.SelectedIndex = 0;
            FileListBox.ScrollIntoView(FileListBox.SelectedItem);
        }
    }


    class FilterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length == 2 && values[0] is string[] filters && values[1] is int index && 0 <= index && index < filters.Length)
            {
                return filters[index];
            }
            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    

    class FileListConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length == 3 && values[0] is string directory && values[1] is string[] filters && values[2] is int index && 0 <= index && index < filters.Length)
            {
                var pattern = $"^({filters[index].Replace(".", @"\.").Replace("*", ".*").Replace("?", ".")})$";
                var di = new DirectoryInfo(directory);
                FileInfo[] files = null;
                try
                {
                    //files = di.EnumerateFiles().Where(file => Regex.IsMatch(file.Name, pattern)).ToArray();
                    files = di.EnumerateFiles().Where(file => Regex.IsMatch(file.Name, pattern, RegexOptions.IgnoreCase)).ToArray();
                }
                catch // eat DirectoryNotFoundException, SecurityException
                {
                }
                return files;                
            }
            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
