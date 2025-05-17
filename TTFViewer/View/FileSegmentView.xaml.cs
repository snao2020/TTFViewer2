using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TTFViewer.View
{
    /// <summary>
    /// FileSegmentView.xaml の相互作用ロジック
    /// </summary>
    public partial class FileSegmentView : UserControl
    {
        public FileSegmentView()
        {
            InitializeComponent();
        }
    }


    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                string result;
                if (str.Length > 0 && str[0] == '~')
                    result = "";
                else
                    result = str;
                return result;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class PathStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string str)
            {
                string result = "";
                //var array = str.Split('\\');
                foreach(var i in str.Split('\\'))
                {
                    if (i.Length > 0 && i[0] != '~')
                        result += "\\" + i;
                }
                if (string.IsNullOrEmpty(result))
                    result = "\\";
                return result;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MyTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            //Debug.WriteLine($"SelectTemplate {item} {container}");
            FrameworkElement element = container as FrameworkElement;
            /*
            if (element != null && item != null && item is Task)
            {
                Task taskitem = item as Task;

                if (taskitem.Priority == 1)
                    return
                        element.FindResource("importantTaskTemplate") as DataTemplate;
                else
                    return
                        element.FindResource("myTaskTemplate") as DataTemplate;
            }

            return null;
            */
            if (item == null)
                return element.FindResource("GapDataTemplate") as DataTemplate;
            return element.FindResource("MyDataTemplate") as DataTemplate;
        }

    }
}
