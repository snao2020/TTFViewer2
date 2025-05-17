using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;
using TTFViewer.ViewModel;

namespace TTFViewer.View
{
    class TTFTreeViewBehavior : Behavior<TreeView>
    {
        RoutedEventHandler TreeViewItem_SelectedHandler;

        protected override void OnAttached()
        {
            base.OnAttached();

            //AssociatedObject.SourceInitialized += OnSourceInitialized;
            //AssociatedObject.Closing += OnClosing;
            //AssociatedObject.Closed += OnClosed;
            AssociatedObject.TargetUpdated += Tv_TargetUpdated;
            if (TreeViewItem_SelectedHandler == null)
                TreeViewItem_SelectedHandler = new RoutedEventHandler(TreeViewItem_Selected);
            AssociatedObject.AddHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);
            //AddCommandBindings();

            SelectedRange.SelectedRangeChanged  += SelectedRangeChanged;
        }

        protected override void OnDetaching()
        {
            //RemoveCommandBindings();
            SelectedRange.SelectedRangeChanged -= SelectedRangeChanged;

            AssociatedObject.RemoveHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);
            //AssociatedObject.SourceInitialized -= OnSourceInitialized;
            //AssociatedObject.Closing -= OnClosing;
            //AssociatedObject.Closed -= OnClosed;
            AssociatedObject.TargetUpdated -= Tv_TargetUpdated;

            base.OnDetaching();
        }

        /*
        private void SelectedRangeChanged(object sender, SelectedRangeChangedEventArgs e)
        {
            Range range = e.Range;
            Debug.WriteLine($"TTFTreeViewBehavior.SelectEventProvider_SelectEvent {range.Start:X8} {range.End:X8}");
            if (e.Handled)
                return;
            if (AssociatedObject.SelectedItem is ItemViewModel curr && (curr == null || curr.FilePosition != range.Start || curr.FileLength != range.End - range.Start)
                && AssociatedObject.DataContext is TTFViewModel vm)
            {
                //var path = new List<ItemViewModel>();
                //ItemViewModelHelper.Find(ref path, vm.ItemViewModel, range.Start);
                var path = ItemViewModelHelper.GetPath(vm, range.Start);
                if (path == null)
                    return;
                ItemsControl parent = AssociatedObject;           
                foreach (var i in path)
                {
                    for (int j = 0; j < parent.Items.Count; j++)
                    {
                        if (parent.Items[j] is ItemViewModel ivm && ivm.FilePosition == i.FilePosition)
                        {
                            PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                            if (itemsHost != null)
                            {
                                itemsHost.BringIndexIntoViewPublic(j);
                            }
                            parent = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                            if (parent is TreeViewItem tvi)
                                tvi.IsExpanded = true;
                            AssociatedObject.UpdateLayout();
                            break;
                        }
                    }
                    Debug.WriteLine($"    {i.Text} {i.FilePosition:X8} {i.FileLength:X8}");
                }
                if (parent is TreeViewItem tvi2)
                {
                    tvi2.IsSelected = true;
                    e.Handled = true;
                }

                //Exception exception = e.GetException();
                //if (exception != null)
                //    MessageBox.Show(exception.Message);

            }

        }
        */
        private string SelectPath(List<string> paths)
        {
            string result = null;
            if (paths != null)
            {
                if (paths.Count == 1)
                    result = paths[0];
                else if (paths.Count > 1)
                {
                    var spd = new SelectPathDialog(paths)
                    {
                        Owner = Application.Current.MainWindow,
                    };

                    if (spd.ShowDialog() == true)
                        result = spd.Path;
                }
            }
            return result;
        }

        TreeViewItem ExpandChild(TreeViewItem parent, Range range)
        {
            for (int j = 0; j < parent.Items.Count; j++)
            {
                if (parent.Items[j] is ItemViewModel ivm && ivm.FilePosition <= range.Start && range.End <= ivm.FilePosition + ivm.FileLength)
                {
                    PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                    if (itemsHost != null)
                    {
                        itemsHost.BringIndexIntoViewPublic(j);
                    }
                    var p = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                    if (p is TreeViewItem tvi)
                    {
                        tvi.IsExpanded = true;
                        AssociatedObject.UpdateLayout();
                        return ExpandChild(tvi, range);
                    }
                    break;
                }
            }
            return parent;
        }


        private void SelectedRangeChanged(object sender, SelectedRangeChangedEventArgs e)
        {
            Range range = e.Range;
            //Debug.WriteLine($"TTFTreeViewBehavior.SelectEventProvider_SelectEvent {range.Start:X8} {range.End:X8}");
            if (e.Handled)
                return;
            if (AssociatedObject.SelectedItem is ItemViewModel curr && (curr == null || curr.FilePosition != range.Start || curr.FileLength != range.End - range.Start)
                && AssociatedObject.DataContext is TTFViewModel vm)
            {
#if false
                var path = SelectPath(vm.FileSegmentViewModel.FindPath(range.Start));
                if (path != null)
                {
                    ItemsControl parent = AssociatedObject;
                    string[] names = path.Split('\\');

                    //foreach (var i in path)
                    foreach(var i in names)
                    {
                        for (int j = 0; j < parent.Items.Count; j++)
                        {
                            if (parent.Items[j] is ItemViewModel ivm && ivm.Name == i)
                            {
                                PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                                    BindingFlags.NonPublic | BindingFlags.Instance);
                                VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                                if (itemsHost != null)
                                {
                                    itemsHost.BringIndexIntoViewPublic(j);
                                }
                                parent = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                                if (parent is TreeViewItem tvi)
                                    tvi.IsExpanded = true;
                                AssociatedObject.UpdateLayout();
                                break;
                            }
                        }
                    }
                    var tvi4 = parent as TreeViewItem;
                    //if (tvi4 == null)
                    //    return;
                    //parent = ExpandChild(parent as TreeViewItem, range);
                    parent = ExpandChild(tvi4, range);
                    /*
                    if(parent is TreeViewItem tvi2)
                    {
                        for (int j = 0; j < parent.Items.Count; j++)
                        {
                            if (parent.Items[j] is ItemViewModel ivm && ivm.FilePosition <= range.Start && range.End <= ivm.FilePosition + ivm.FileLength)
                            {
                                PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                                    BindingFlags.NonPublic | BindingFlags.Instance);
                                VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                                if (itemsHost != null)
                                {
                                    itemsHost.BringIndexIntoViewPublic(j);
                                }
                                parent = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                                if (parent is TreeViewItem tvi)
                                    tvi.IsExpanded = true;
                                AssociatedObject.UpdateLayout();
                                break;
                            }
                        }
                    }
                    */
                    if (parent is TreeViewItem tvi3)
                    {
                        tvi3.IsSelected = true;
                        e.Handled = true;
                    }

                    //Exception exception = e.GetException();
                    //if (exception != null)
                    //    MessageBox.Show(exception.Message);

                }
#endif
            }
        }
       
#if false
        //var path = new List<ItemViewModel>();
        //ItemViewModelHelper.Find(ref path, vm.ItemViewModel, range.Start);
        var path = ItemViewModelHelper.GetPath(vm, range.Start);
                if (path == null)
                    return;
                ItemsControl parent = AssociatedObject;
                foreach (var i in path)
                {
                    for (int j = 0; j < parent.Items.Count; j++)
                    {
                        if (parent.Items[j] is ItemViewModel ivm && ivm.FilePosition == i.FilePosition)
                        {
                            PropertyInfo pi = typeof(ItemsControl).GetProperty("ItemsHost",
                                BindingFlags.NonPublic | BindingFlags.Instance);
                            VirtualizingPanel itemsHost = pi.GetValue(parent) as VirtualizingPanel;
                            if (itemsHost != null)
                            {
                                itemsHost.BringIndexIntoViewPublic(j);
                            }
                            parent = parent.ItemContainerGenerator.ContainerFromIndex(j) as ItemsControl;
                            if (parent is TreeViewItem tvi)
                                tvi.IsExpanded = true;
                            AssociatedObject.UpdateLayout();
                            break;
                        }
                    }
                    Debug.WriteLine($"    {i.Text} {i.FilePosition:X8} {i.FileLength:X8}");
                }
                if (parent is TreeViewItem tvi2)
                {
                    tvi2.IsSelected = true;
                    e.Handled = true;
                }

                //Exception exception = e.GetException();
                //if (exception != null)
                //    MessageBox.Show(exception.Message);

            }

        }
#endif

        void TargetUpdated()
        {
            /*
            if (AssociatedObject.FindName("TreeView") is TreeView tv)
            {
                //Debug.WriteLine("tv_TargetUpdated {0}", tv.Items.Count);
                if (tv.Items.Count > 0)
                {
                    //Debug.WriteLine("--{0} {1}",
                    //    tv.Items[0],
                    //    tv.ItemContainerGenerator.ContainerFromIndex(0));
                    if (tv.ItemContainerGenerator.ContainerFromIndex(0) is TreeViewItem tvi)
                        tvi.IsSelected = true;
                }
            }
            */
            if (AssociatedObject.Items.Count > 0)
            {
                if (AssociatedObject.ItemContainerGenerator.ContainerFromIndex(0) is TreeViewItem tvi)
                {
                    tvi.IsSelected = true;
                    //if (AssociatedObject.Parent is UIElement ue && ue.IsFocused
                    //    || AssociatedObject.IsFocused)
                    //    tvi.Focus();
                }
            }
        }

        private void Tv_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            var scope = FocusManager.GetFocusScope(AssociatedObject);
            var elm = FocusManager.GetFocusedElement(scope);
            if(elm is TreeViewItem || elm is TreeView || elm == AssociatedObject.Parent)
                FocusManager.SetFocusedElement(scope, AssociatedObject);
            AssociatedObject.Dispatcher.BeginInvoke(new Action(TargetUpdated), DispatcherPriority.Background, null);
        }

        void OnSourceInitialized(object sender, EventArgs e)
        {
            //var wp = Properties.Settings.Default.WindowPlacement;
            //AssociatedObject.SetWindowPlacement(wp);
            //Properties.Settings.Default.WindowPlacement = null;
            //if (AssociatedObject.FindName("TreeView") is TreeView tv)
            //{
            //    tv.TargetUpdated += Tv_TargetUpdated;
            //}
        }

        /*
        static TreeView GetTreeView(TreeViewItem tvi)
        {
            TreeView result = null;
            for (DependencyObject dep = tvi; result == null && dep != null; dep = VisualTreeHelper.GetParent(dep))
            {
                if (dep is TreeView tv)
                    result = tv;
            }
            return result;
        }
        */

        void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem tvi)
            {
                for (var parent = VisualTreeHelper.GetParent(tvi); parent != null; parent = VisualTreeHelper.GetParent(parent))
                {
                    if (parent is TreeViewItem parentTVI)
                        parentTVI.IsExpanded = true;
                }

                DependencyObject scope = FocusManager.GetFocusScope(tvi);
                if (FocusManager.GetFocusedElement(scope) is TreeViewItem focused)
                {
                    FocusManager.SetFocusedElement(scope, tvi);
                    /*
                    TreeView curr = GetTreeView(focused);
                    if (curr == null || curr == GetTreeView(tvi))
                    {
                        FocusManager.SetFocusedElement(scope, tvi);
                    }
                    */
                }
            }
        }

        /*
        void AddCommandBindings()
        {
            if (OpenCommandBinding == null)
                OpenCommandBinding = new CommandBinding(ApplicationCommands.Open, Open_Executed, Open_CanExecute);
            AssociatedObject.CommandBindings.Add(OpenCommandBinding);

            if (CloseCommandBinding == null)
                CloseCommandBinding = new CommandBinding(ApplicationCommands.Close, Close_Executed, Close_CanExecute);
            AssociatedObject.CommandBindings.Add(CloseCommandBinding);
            if (CloseVMCommand != null)
                CloseVMCommand.CanExecuteChanged += Close_CanExecuteChanged;

            if (ExitCommand != null)
            {
                if (ExitCommandBinding == null)
                    ExitCommandBinding = new CommandBinding(ExitCommand, Exit_Executed);
                AssociatedObject.CommandBindings.Add(ExitCommandBinding);
            }
        }


        void RemoveCommandBindings()
        {
            if (ExitCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(ExitCommandBinding);

            if (CloseVMCommand != null)
                CloseVMCommand.CanExecuteChanged -= Close_CanExecuteChanged;
            if (CloseCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(CloseCommandBinding);

            if (OpenCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(OpenCommandBinding);
        }


        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = OpenVMCommand != null && OpenVMCommand.CanExecute(null);
        }


        void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OpenVMCommand != null && OpenVMCommand.CanExecute(null))
            {
                FileDialog fd = new FileDialog
                {
                    Owner = AssociatedObject
                };

                if (fd.ShowDialog() == true)
                {
                    if (fd.FileInfo != null)
                    {
                        OpenVMCommand.Execute(fd.FileInfo.FullName);
                        FocusManager.SetFocusedElement(AssociatedObject, AssociatedObject.FindName("TreeView") as UIElement);

                    }
                }
            }
        }


        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CloseVMCommand != null && CloseVMCommand.CanExecute(null);
        }


        void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CloseVMCommand != null && CloseVMCommand.CanExecute(null))
            {
                CloseVMCommand.Execute(null);
            }
        }


        void Close_CanExecuteChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }


        void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AssociatedObject.Close();
        }
        */
    }
}
