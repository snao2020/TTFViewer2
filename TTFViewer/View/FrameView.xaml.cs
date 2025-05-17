using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FrameView : Window
    {
        public FrameView()
        {
            InitializeComponent();
        }

        private void Frame_GotFocus(object sender, RoutedEventArgs e)
        {
            /*
                var scope = FocusManager.GetFocusScope(this);
                var elm = FocusManager.GetFocusedElement(scope);
                Debug.WriteLine($"CotFocus {elm} {sender} {e.Source} {e.OriginalSource}");
            */
        }

        private void Frame_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && e.NewValue is ItemViewModel ivm)
            {
                string str = ivm.Text;
                for(ivm = ivm.Parent; ivm != null && ivm.Text != null; ivm = ivm.Parent)
                {
                    str = $"{ivm.Text}\\" + str;
                }
                StatusTextBlock.Text = str;
            }
            else
                StatusTextBlock.Text = null;
        }
        /*
private void Window_Selected(object sender, RoutedEventArgs e)
{
   if (DataContext is TTFViewModel vm)
   {
       if (e.OriginalSource is TreeViewItem tvi && tvi.DataContext is ItemViewModel ivm)
           vm.SelectCommand.Execute(ivm);
   }                
}
*/
    }
}
