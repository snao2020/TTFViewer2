using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    /// <summary>
    /// TTFTreeView.xaml の相互作用ロジック
    /// </summary>
    public partial class TTFTreeView : UserControl
    {
        public TTFTreeView()
        {
            InitializeComponent();
            //Selection3.SelectionChanged += SelectionChanged;
        }
        /*
        private void SelectionChanged(object sender, SelectionChangedArgs args)
        {
            if (args.DumpItemSelected)
                Debug.WriteLine("TTFTreeView.SelectionChanged");
        }
        */
        private void Window_Selected(object sender, RoutedEventArgs e)
        {
            //if (DataContext is TTFViewModel vm)
            {
                if (e.OriginalSource is TreeViewItem tvi && tvi.DataContext is ItemViewModel ivm)
                {
                    UInt32 position = ivm.FilePosition;
                    UInt32 length = ivm.FileLength;
                    //vm.SelectCommand.Execute(new Range { Start = position, End = position + length });
                    /*
                    if(FindResource("Selection") is Selection2 sel)
                    {
                        sel.Range = new Range2(position, position + length);
                    }
                    */
                    //Selection3.Instance.Range = new Range2(position, position + length);
                    //Selection.Instance.TreeViewItemSelected(new Range(position, position + length));
                    var range = new Range(position, position + length);
                    SelectedRange.RaiseSelectedRangeChanged(range);
                }
                else
                    //Selection.Instance.Clear();
                    SelectedRange.RaiseSelectedRangeChanged(Range.Empty);
            }
            //else
                //Selection.Instance.Clear();
                //SelectedRange.RaiseSelectedRangeChanged(Range.Empty);
        }
    }
#if false
    public class SelectAction : TriggerAction<TreeView>
    {
        protected override void Invoke(object parameter)
        {
            if (parameter is MySelectEventArgs e)
            {
                if(AssociatedObject.SelectedItem is ItemViewModel ivm)
                {
                    MyRange range = e.MyRange;
                    if (range == null || range.Start == ivm.FilePosition && range.End - range.Start == ivm.FileLength)
                        return;
                }
                Debug.WriteLine("SelectAction");
                /*
                ItemsControl parent = AssociatedObject;
                foreach (var i in e.SelectList)
                {
                    for(int j = 0; j < parent.Items.Count; j++)
                    {
                        if(parent.Items[j] is ItemViewModel ivm && ivm.FilePosition == i.FilePosition)
                        {
                            PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                            if (itemsHost != null)
                            {
                                itemsHost.BringIndexIntoViewPublic(j);
                            }
                            parent = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                            if(parent is TreeViewItem tvi)
                                tvi.IsExpanded = true;
                            AssociatedObject.UpdateLayout();
                            break;
                        }
                    }
                    Debug.WriteLine($"    {i.Text} {i.FilePosition:X8} {i.FileLength:X8}");
                }
                if (parent is TreeViewItem tvi2)
                    tvi2.IsSelected = true;

                //Exception exception = e.GetException();
                //if (exception != null)
                //    MessageBox.Show(exception.Message);
                */
            }
        }
    }
#endif
}
