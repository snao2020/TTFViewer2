using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    public class DumpItemViewBehavior : Behavior<ItemsControl>
    {
        /*
        public static readonly DependencyProperty IsSelectionActiveProperty =
            DependencyProperty.RegisterAttached(
                "IsSelectionActive", typeof(bool), typeof(DumpItemViewBehavior));

        public static bool GetIsSelectionActive(DependencyObject dep)
        {
            return (bool)dep.GetValue(IsSelectionActiveProperty);
        }

        public static void SetIsSelectionActive(DependencyObject dep, bool value)
        {
            dep.SetValue(IsSelectionActiveProperty, value);
        }
        */

        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.RegisterAttached(
                "Index", typeof(UInt32), typeof(DumpItemViewBehavior));

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
                "IsSelected", typeof(bool), typeof(DumpItemViewBehavior));

        public static bool GetIsSelected(DependencyObject dep)
        {
            return (bool)dep.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject dep, bool selected)
        {
            dep.SetValue(IsSelectedProperty, selected);
        }


        protected override void OnAttached()
        {
            base.OnAttached();

            //AssociatedObject.MouseDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            //AssociatedObject.MouseDown -= OnMouseDown;

            base.OnDetaching();
        }
#if false
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //Debug.WriteLine($"DumpItemViewBehavior_MouseDown {sender} {e.Source} {e.OriginalSource}");
            
            if (e.Source is ContentControl cc)
            {
                //e.Handled = true;
                bool result = cc.Focus();
                //Debug.WriteLine($"MouseDown {result}");
            }
            if (e.Source is ContentControl tb && e.ClickCount == 2 && e.ChangedButton == MouseButton.Left)
            {
                if (AssociatedObject.DataContext is DumpItemViewModel vm)
                {
                    UInt32 filePosition = vm.FilePosition;
                    UInt32 index = DumpItemViewBehavior.GetIndex(tb);
                    //Debug.WriteLine($"MouseDown {AssociatedObject.DataContext} {filePosition:X8} {index:X2}");
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
        }
#endif
    }




    public class HexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //if (values[0] is Byte?[] bytes && values[1] is UInt32 index)
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
            return "";
            //else
            //throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AsciiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Bytes bytes && values[1] is UInt32 index)
            {
                var b = bytes[(int)index];
                if (b == null)
                    return " ";
                if (0x20 <= b && b < 0x7F)
                {
                    return $"{(Char)b}";
                }
                else
                    return ".";
            }
            else
                return "";
                //throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /*
    public class BackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values[0] is Range selection && values[1] is UInt32 filePosition && values[2] is UInt32 index)
            {
                if (selection.Start <= filePosition + index && filePosition + index < selection.End)
                    return Brushes.Red;
                else
                    return Brushes.Yellow;
            }
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    */
    public class IsSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Range selection && values[1] is UInt32 filePosition && values[2] is UInt32 index)
            {
                if (selection.Start <= filePosition + index && filePosition + index < selection.End)
                    return true;
                else
                    return false;
            }
            return false;
            //throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BridgeIsSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Range selection && values[1] is UInt32 filePosition)
            {
                if (selection.Start <= filePosition + 7 && filePosition + 8 < selection.End)
                    return true;
                else
                    return false;
            }
            return false;
            //throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
