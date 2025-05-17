using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    class DumpGridViewBehavior : Behavior<DumpGridView>
    {
        public static readonly DependencyProperty IndexProperty =
        DependencyProperty.RegisterAttached(
            "Index", typeof(UInt32), typeof(DumpGridViewBehavior),
            new FrameworkPropertyMetadata(0U, FrameworkPropertyMetadataOptions.Inherits));

        public static UInt32 GetIndex(DependencyObject dep)
        {
            return (UInt32)dep.GetValue(IndexProperty);
        }

        public static void SetIndex(DependencyObject dep, UInt32 value)
        {
            dep.SetValue(IndexProperty, value);
        }

        /*
        public static RelayCommand ContextMenuSelectCommand
            = new RelayCommand(o => ExecuteContextMenu(o), (o) => true);

        private static void ExecuteContextMenu(object o)
        {
            Debug.WriteLine($"ExecuteContextMenu {o}");
            if (o is MenuItem mi && ContextMenu.ItemsControlFromItemContainer(mi) is ContextMenu cm
                && cm.PlacementTarget is DataGridCell cell)
            {
                //Debug.WriteLine($"    {cm.PlacementTarget}");
                Select(cell);
            }
            //throw new NotImplementedException();
        }
        */

        CommandBinding SelectCommandBinding;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            //AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;

            if (AssociatedObject.Resources["SelectCommand"] is ICommand exitCommand)
            {
                if (SelectCommandBinding == null)
                    SelectCommandBinding = new CommandBinding(exitCommand, Select_Executed);
                AssociatedObject.CommandBindings.Add(SelectCommandBinding);
            }
                            
        }


        protected override void OnDetaching()
        {
            if (SelectCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(SelectCommandBinding);

            //AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            base.OnDetaching();
        }


        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.FindResource("DumpLineMetrics") is DumpLineMetrics dlm)
            {
                dlm.MetricsChanged += MetricsChanged;
                dlm.Load(AssociatedObject);
            }
        }


        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.FindResource("DumpLineMetrics") is DumpLineMetrics dlm)
            {
                dlm.Unload(AssociatedObject);
                dlm.MetricsChanged -= MetricsChanged;
            }
        }

        /*
        private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Debug.WriteLine($"MouseDoubleClick {Keyboard.FocusedElement}{sender} {e.Source} {e.OriginalSource}");
            if (Keyboard.FocusedElement is DataGridCell cell)
                Select(cell);

        }
       */
        void Select_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Debug.WriteLine($"Select_Executed {sender} {e.Parameter}");
            if (e.Parameter is MenuItem mi && ContextMenu.ItemsControlFromItemContainer(mi) is ContextMenu cm
                && cm.PlacementTarget is DataGridCell cell)
            {
                //Debug.WriteLine($"    {cm.PlacementTarget}");
                Select(cell);
            }
            //throw new NotImplementedException();
        }


        static void Select(DataGridCell cell)
        {
            //Debug.WriteLine($"Select {DumpGridViewBehavior.GetIndex(cell.Column)} {cell.DataContext}");
        }

        private void MetricsChanged(object sender, EventArgs e)
        {
            if (AssociatedObject.FindResource("DumpLineMetrics") is DumpLineMetrics dlm)
            {
                double rowSpacing = 0.0;
                if (AssociatedObject.TryFindResource("RowSpacing") is double d0)
                    rowSpacing = d0;
                AssociatedObject.DataGrid.RowHeight = dlm.Height + rowSpacing;

                double colSpacing = 0.0;
                if (AssociatedObject.TryFindResource("ColSpacing") is double d1)
                    colSpacing = d1;

                foreach (var dgc in AssociatedObject.DataGrid.Columns)
                {
                    if (dgc.CellStyle == AssociatedObject.Resources["PositionCellStyle"])
                        dgc.Width = dlm.PositionWidth; // + colSpacing / 2;
                    else if (dgc.CellStyle == AssociatedObject.Resources["HexCellStyle"])
                        dgc.Width = dlm.HexWidth + colSpacing;
                    else if (dgc.CellStyle == AssociatedObject.Resources["BridgeCellStyle"])
                        dgc.Width = dlm.BridgeWidth + colSpacing;
                    else if (dgc.CellStyle == AssociatedObject.Resources["AsciiCellStyle"])
                        dgc.Width = dlm.AsciiWidth;
                }
            }
        }       
    }


    public class GridHexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Bytes bytes)
            {
                if (values[1] is UInt32 index)
                {
                    var b = bytes[(int)index];
                    if (b == null)
                        return "  ";
                    return $"{b:X2}";
                }
            }
            throw new NotImplementedException();
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GridAsciiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Bytes bytes)
            {
                if (values[1] is UInt32 index)
                {
                    var b = bytes[(int)index];
                    if (b == null)
                        return "";
                    if (0x20 <= b && b < 0x7F)
                    {
                        return $"{(Char)b}";
                    }
                    else
                        return ".";
                }
            }
            throw new NotImplementedException();
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
