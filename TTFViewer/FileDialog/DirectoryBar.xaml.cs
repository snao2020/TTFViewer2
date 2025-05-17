using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FileDialogSample
{
    /// <summary>
    /// DirectoryBar.xaml の相互作用ロジック
    /// </summary>
    /// 
    public class DirectoryName
    {
        public bool IsBlank { get; }

        public string FullName { get; }

        public string TypeName
        {
            get
            {
                if(FullName.EndsWith("\\"))
                {
                    return IsBlank ? "\\   " : FullName.Remove(FullName.Length - 1);
                }
                else
                {
                    return IsBlank ? "   " : FullName.Substring(FullName.LastIndexOf('\\'));
                }
            }
        }

        public DirectoryName(bool isBlank, string fullName)
        {
            IsBlank = isBlank;
            FullName = fullName;
        }
    }


    public partial class DirectoryBar : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public static readonly DependencyProperty DirectoryProperty
            = DependencyProperty.Register(
            "Directory", typeof(string), typeof(DirectoryBar),
            new FrameworkPropertyMetadata("C:\\"));


        public string Directory
        {
            get => (string)GetValue(DirectoryProperty);
            set => SetValue(DirectoryProperty, value);
        }


        public DirectoryBar()
        {
            InitializeComponent();
        }

        
        Drives GetDrives()
        {
            return FindResource("Drives") as Drives;
        }


        string GetDefaultPath()
        {
            var list = GetDrives().DirectoryList;
            if (list != null && list.Count > 0)
                return list[0].Name;
            else
                return null;
        }
        

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {           
            var drives = GetDrives();
            drives.PropertyChanged += Drives_PropertyChanged;
            DirectoryMenu.IsEnabled = drives.DirectoryList != null;

            if (string.IsNullOrEmpty(Directory))
                Directory = GetDefaultPath();
        }
        

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            GetDrives().PropertyChanged -= Drives_PropertyChanged;
        }


        private void Drives_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DirectoryMenu.IsEnabled = GetDrives().DirectoryList != null;

            foreach (var i in DirectoryMenu.Items)
            {
                if(i is MenuItem menuItem)
                {
                    menuItem.IsSubmenuOpen = false;
                }
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is MenuItem mi)
            {
                if(mi.Tag is MenuItem parent)
                {
                    if(mi.DataContext is DirectoryName dn)
                    {
                        Directory = dn.FullName;
                    }
                    parent.IsSubmenuOpen = false;
                }
            }
        }
    }


    class FullPathListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<DirectoryName> result = null;
            if (value is string dir) // && Regex.IsMatch(dir, @"^[A-Za-z]:\\"))
            {
                result = new List<DirectoryName>();
                if (dir.Last() != '\\')
                {
                    var indexes = new List<int>();
                    for (int i = 0; i < dir.Length; i++)
                        if (dir[i] == '\\')
                            indexes.Add(i);

                    foreach (var i in indexes)
                    {
                        if (dir[i - 1] == ':')
                            result.Add(new DirectoryName(false, dir.Substring(0, i + 1)));
                        else
                            result.Add(new DirectoryName(false, dir.Substring(0, i)));
                    }
                }
                result.Add(new DirectoryName(false, dir));
                result.Add(new DirectoryName(true, dir));
            }
            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    class SubDirectoryListConverter : IMultiValueConverter
    {
        static List<DirectoryName> ClosedList = new List<DirectoryName> { new DirectoryName(false, "\\Closed") };

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is bool isOpen && values[1] is DirectoryName dn)
            {
                if (isOpen)
                {
                    var result = new List<DirectoryName>();
                    if (!dn.IsBlank && dn.FullName.EndsWith(":\\"))
                    {
                        if (Application.Current.Resources["Drives"] is Drives drives)
                        {
                            foreach (var i in drives.DirectoryList)
                                result.Add(new DirectoryName(false, i.Name));
                        }
                    }
                    else
                    {
                        int pos = dn.FullName.LastIndexOf('\\');
                        if (pos >= 0)
                        {
                            var subDirStr = dn.IsBlank ? dn.FullName : dn.FullName.Substring(0, pos);
                            if (subDirStr.Last() == ':')
                                subDirStr += '\\';

                            result.Add(new DirectoryName(true, subDirStr));
                            try
                            {
                                var subDirs = new DirectoryInfo(subDirStr).GetDirectories();
                                Array.ForEach(subDirs, i => result.Add(new DirectoryName(false, i.FullName)));
                            }
                            catch // eat DirectoryNotFoundException, SecurityException
                            {
                            }
                        }
                    }
                    return result;
                }
                else
                    return ClosedList;
            }
            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
