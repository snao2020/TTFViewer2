using System;
using System.Collections.Generic;
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
    /// DumpItemView.xaml の相互作用ロジック
    /// </summary>
    public partial class DumpItemView : UserControl
    {
        public DumpItemView()
        {
            InitializeComponent();
        }
#if false
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.RegisterAttached(
                "Index", typeof(UInt32), typeof(DumpItemView));

        public static UInt32 GetIndex(DependencyObject dep)
        {
            return (UInt32)dep.GetValue(IndexProperty);
        }

        public static void SetIndex(DependencyObject dep, UInt32 value)
        {
            dep.SetValue(IndexProperty, value);
        }


        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached(
                "IsSelected", typeof(bool), typeof(DumpItemView));

        public static bool GetIsSelected(DependencyObject dep)
        {
            return (bool)dep.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject dep, bool selected)
        {
            dep.SetValue(IsSelectedProperty, selected);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine($"MouseDown {sender} {e.Source} {e.OriginalSource}");
            if (e.Source is ContentControl cc)
            {
                //e.Handled = true;
                bool result = cc.Focus();
                Debug.WriteLine($"MouseDown {result}");
            }
            if (sender is TextBlock tb && e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
            {
                if (DataContext is DumpItemViewModel vm)
                {
                    UInt32 filePosition = vm.FilePosition;
                    UInt32 index = DumpItemViewBehavior.GetIndex(tb);
                    Debug.WriteLine($"MouseDown {DataContext} {filePosition:X8} {index:X2}");
                    Window w = Application.Current.MainWindow;
                    {
                        if (w.DataContext is TTFViewModel viewModel)
                            viewModel.Select(filePosition + index);
                    }

                    //var args = new SelectionChangedArgs(filePosition + index);
                    //Selection3.RaiseSelectionChanged(args);
                    //if (tb.FindResource("Selection") is Selection2 sel)
                    //    sel.Select(filePosition + index);
                    //    Debug.WriteLine($"{sel.Range.Start} {sel.Range.End}");
                }
            }
        }
#endif
        /*
        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            var scope = FocusManager.GetFocusScope(this);
            var elm = FocusManager.GetFocusedElement(scope);
            Debug.WriteLine($"CotFocus {elm} {sender} {e.Source} {e.OriginalSource}");
        }
        
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
            Debug.WriteLine($"UserControl_MouseDown {sender} {e.Source} {e.OriginalSource}");
        }

        private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine($"ItemsControl_MouseDown {sender} {e.Source} {e.OriginalSource}");
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine($"TextBlock_MouseDown {sender} {e.Source} {e.OriginalSource}");
        }
        */
    }
}
