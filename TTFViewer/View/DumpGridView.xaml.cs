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
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    /// <summary>
    /// DumpGrid.xaml の相互作用ロジック
    /// </summary>
    public partial class DumpGridView : UserControl
    {
        public DumpGridView()
        {
            InitializeComponent();
        }
        /*
        public void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"MenuItem_Click {sender} {e.Source} {e.OriginalSource}");

        }

        private void MenuItemSelectAll_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{sender} {e.Source} {e.OriginalSource}");
            DataGrid.SelectAll();
        }

        private void MenuItemClearSelect_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{sender} {e.Source} {e.OriginalSource}");
            DataGrid.SelectedCells.Clear();
        }

        private void UserControl_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(DataGrid.Columns[0].Width);
            Debug.WriteLine(DataGrid.Columns[2].Width);
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("PreviewMouseDown");
        }

        private void UserControl_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"main=menuitem_Click {sender}");
        }
        */
    }
}
