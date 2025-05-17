using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
#if false
    public class BringIndexIntoViewAction : TriggerAction<ItemsControl>
    {
        protected override void Invoke(object parameter)
        {
            //if(AssociatedObject.FindResource("Selection") is Selection2 selection)
            {
                //Range2 range = selection.Range;
                //Range range = Selection.Instance.Range;
                SelectedRange selection = AssociatedObject.FindResource("SelectedRange") as SelectedRange;
                //if (sel == null)
                //    return;
                //Range range = sel.Range;
                if (range != null && range.Start < range.End)
                {
                    PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    VirtualizingPanel itemsHost = pi.GetValue(AssociatedObject) as VirtualizingPanel;
                    if (itemsHost != null)
                    {
                        int end = (int)(range.End / 16U);
                        itemsHost.BringIndexIntoViewPublic(end);
                        AssociatedObject.UpdateLayout();
                        int start = (int)(range.Start / 16U);
                        itemsHost.BringIndexIntoViewPublic(start);
                    }
                }
            }
        }
    }
#endif

    public class DumpViewBehavior : Behavior<DumpView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.FindResource("DumpLineMetrics") is DumpLineMetrics dlm)
                dlm.Load(AssociatedObject);

            //AssociatedObject.MouseDown += OnMouseDown;
            SelectedRange.SelectedRangeChanged += SelectedRangeChanged;
            ((SelectedRange)AssociatedObject.Resources["SelectedRange"]).PropertyChanged += SelectedRange_PropertyChanged;
        }


        protected override void OnDetaching()
        {
            ((SelectedRange)AssociatedObject.Resources["SelectedRange"]).PropertyChanged -= SelectedRange_PropertyChanged;
            SelectedRange.SelectedRangeChanged -= SelectedRangeChanged;
            //AssociatedObject.MouseDown -= OnMouseDown;

            if (AssociatedObject.FindResource("DumpLineMetrics") is DumpLineMetrics dlm)
                dlm.Unload(AssociatedObject);

            base.OnDetaching();
        }


        private void SelectedRange_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var range = ((SelectedRange)(AssociatedObject.Resources["SelectedRange"])).Range;
            if (range.Start < range.End)
            {
                PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                VirtualizingPanel itemsHost = pi.GetValue(AssociatedObject.MySelector) as VirtualizingPanel;
                if (itemsHost != null)
                {
                    int end = (int)((range.End - 1)/ 16U);
                    itemsHost.BringIndexIntoViewPublic(end);
                    AssociatedObject.UpdateLayout();
                    int start = (int)(range.Start / 16U);
                    itemsHost.BringIndexIntoViewPublic(start);
                }
            }
        }


        private void SelectedRangeChanged(object sender, SelectedRangeChangedEventArgs e)
        {
            //Debug.WriteLine($"DumpViewBehavior.SelectEventProvider_SelectEvent {e.Range.Start:X8} {e.Range.End:X8}");
            //((MyRange)AssociatedObject.Resources["SelectedRange"]).Start = e.MyRange.Start;
            //((MyRange)AssociatedObject.Resources["SelectedRange"]).End = e.MyRange.End;
            /*
            if (AssociatedObject.Resources["SelectedRange"] is SelectedRange)
            {
                ((SelectedRange)AssociatedObject.Resources["SelectedRange"]).Range = e.Range;
                var selection = (SelectedRange)AssociatedObject.Resources["SelectedRange"];
                    Debug.WriteLine($"     {selection.Range.Start} {selection.Range.End}");
            }
            */
            if (e.Handled)
                return;

            var range = e.Range;
            var selection = (SelectedRange)AssociatedObject.Resources["SelectedRange"];
            selection.Range = range;
            //Debug.WriteLine($"     {selection.Range.Start:X8} {selection.Range.End:X8}");

            //if(AssociatedObject.FindResource("Selection") is Selection2 selection)
            {
                //Range2 range = selection.Range;
                //Range range = Selection.Instance.Range;
                //MyRange range = AssociatedObject.FindResource("SelectedRange") as MyRange;
                //if (sel == null)
                //    return;
                //Range range = sel.Range;
            }
        }
#if false
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Debug.WriteLine($"DumpItemViewBehavior_MouseDown {sender} {e.Source} {e.OriginalSource}");

            if (e.OriginalSource is TextBlock tb && tb.TemplatedParent is ContentControl cc)
            {
                //e.Handled = true;
                bool result = cc.Focus();
                //Debug.WriteLine($"MouseDown {result} {cc.TemplatedParent}");
                //Debug.WriteLine($"MouseDown {cc.DataContext}");

//#if false
                if (e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
                {
                    if (cc.DataContext is DumpItemViewModel vm)
                    {
                        UInt32 filePosition = vm.FilePosition;
                        UInt32 index = DumpItemViewBehavior.GetIndex(cc);
                        //Debug.WriteLine($"MouseDown {vm} {filePosition:X8} {index:X2}");
                        SelectedRange.RaiseSelectedRangeChanged(new Range(filePosition + index, filePosition + index + 1));
                        /*
                        Window w = Application.Current.MainWindow;
                        {
                            if (w.DataContext is TTFViewModel viewModel)
                                viewModel.Select(filePosition + index);
                        }
                        */
                        //var args = new SelectionChangedArgs(filePosition + index);
                        //Selection3.RaiseSelectionChanged(args);
                        //if (tb.FindResource("Selection") is Selection2 sel)
                        //    sel.Select(filePosition + index);
                        //    Debug.WriteLine($"{sel.Range.Start} {sel.Range.End}");
                    }
                }
//#endif
            }
        }
#endif
    }
}
