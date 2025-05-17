using FileDialogSample;
using GuiMisc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using WindowPlacementSample;

namespace TTFViewer.View
{
    public class ErrorMessageAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            if (parameter is ErrorEventArgs e)
            {
                Exception exception = e.GetException();
                if (exception != null)
                    MessageBox.Show(exception.Message);
            }
        }
    }


    class FrameViewBehavior : Behavior<Window>
    {
        public static DependencyProperty OpenVMCommandProperty =
            DependencyProperty.Register("OpenVMCommand", typeof(ICommand), typeof(FrameViewBehavior), null);

        public ICommand OpenVMCommand
        {
            get { return (ICommand)GetValue(OpenVMCommandProperty); }
            set { SetValue(OpenVMCommandProperty, value); }
        }


        public static DependencyProperty CloseVMCommandProperty =
            DependencyProperty.Register("CloseVMCommand", typeof(ICommand), typeof(FrameViewBehavior), null);

        public ICommand CloseVMCommand
        {
            get { return (ICommand)GetValue(CloseVMCommandProperty); }
            set { SetValue(CloseVMCommandProperty, value); }
        }


        //public ICommand ExitCommand { private get; set; }


        CommandBinding OpenCommandBinding;
        CommandBinding CloseCommandBinding;
        CommandBinding ExitCommandBinding;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SourceInitialized += OnSourceInitialized;
            AssociatedObject.Closing += OnClosing;
            AssociatedObject.Closed += OnClosed;
            //if (TreeViewItem_SelectedHandler == null)
            //    TreeViewItem_SelectedHandler = new RoutedEventHandler(TreeViewItem_Selected);
            //AssociatedObject.AddHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);
            AddCommandBindings();
        }
        /*
        void func()
        {
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
        }
        
        private void Tv_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            AssociatedObject.Dispatcher.BeginInvoke(new Action(func), DispatcherPriority.Background, null);
        }
        */
        protected override void OnDetaching()
        {
            RemoveCommandBindings();

            //AssociatedObject.RemoveHandler(TreeViewItem.SelectedEvent, TreeViewItem_SelectedHandler);
            AssociatedObject.SourceInitialized -= OnSourceInitialized;
            AssociatedObject.Closing -= OnClosing;
            AssociatedObject.Closed -= OnClosed;

            base.OnDetaching();
        }


        void OnSourceInitialized(object sender, EventArgs e)
        {
            string wpString = Properties.Settings.Default.WindowPlacement;
            AssociatedObject.SetWindowPlacement(wpString, false);
            //AssociatedObject.SetWindowPlacement(wp);
            //Properties.Settings.Default.WindowPlacement = null;

            //var wp = Properties.Settings.Default.WindowPlacement;
            //AssociatedObject.SetWindowPlacement(wp);
            //Properties.Settings.Default.WindowPlacement = null;

            //if (AssociatedObject.FindName("TreeView") is TreeView tv)
            //{
            //    tv.TargetUpdated += Tv_TargetUpdated;
            //}
        }


        void OnClosing(object sender, CancelEventArgs e)
        {
            ICommand command = CloseVMCommand;
            if (command != null && command.CanExecute(null))
                command.Execute(null);

            //var wp = AssociatedObject.GetWindowPlacement();
            //Properties.Settings.Default.WindowPlacement = wp;

            string wpString = AssociatedObject.GetWindowPlacement(false);
            Properties.Settings.Default.WindowPlacement = wpString;
        }


        void OnClosed(object sender, EventArgs e)
        {
            //if (AssociatedObject.FindName("TreeView") is TreeView tv)
            //{
            //    tv.TargetUpdated -= Tv_TargetUpdated;
            //}
            Properties.Settings.Default.Save();
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
                    TreeView curr = GetTreeView(focused);
                    if (curr == null || curr == GetTreeView(tvi))
                    {
                        FocusManager.SetFocusedElement(scope, tvi);
                    }
                }
            }
        }
        */

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

            if(AssociatedObject.Resources["ExitCommand"] is ICommand exitCommand)
            if (exitCommand != null)
            {
                if (ExitCommandBinding == null)
                    ExitCommandBinding = new CommandBinding(exitCommand, Exit_Executed);
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
                var fileName = Properties.Settings.Default.FileDialogFileName;
                string dir = null;
                FileInfo fi = null;

                if (string.IsNullOrEmpty(fileName))
                    dir = "c:\\Windows\\Fonts";
                else
                    fi = new FileInfo(fileName);
                
                FileDialog fd = new FileDialog
                {
                    Owner = AssociatedObject,
                    Directory = dir,
                    FileInfo = fi,
                    Filters = new string[]{ "*.ttf", "*.ttc", "*.otf", "*.otc", "*.ttf|*.ttc|*.otf|*.otc"},
                    FilterIndex = Properties.Settings.Default.FileDialogExtIndex,
                };
                fd.PropertyChanged += FileDialogPropertyChanged;
                if (fd.ShowDialog() == true)
                {
                    if (fd.FileInfo != null)
                    {
                        Properties.Settings.Default.FileDialogFileName = fd.FileInfo.FullName;
                        Properties.Settings.Default.FileDialogExtIndex = fd.FilterIndex;
                        OpenVMCommand.Execute(fd.FileInfo.FullName);
                        //FocusManager.SetFocusedElement(AssociatedObject, AssociatedObject.FindName("TreeView") as UIElement);
                    }
                }
                fd.PropertyChanged -= FileDialogPropertyChanged;
            }
        }

        private void FileDialogPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(sender is FileDialog fd && e.PropertyName == "IsWindowOpen")
            {
                if (fd.IsWindowOpen)
                {
                    var wp = Properties.Settings.Default.FileDialogPlacement;
                    fd.SetWindowPlacement(wp, true);
                }
                else
                {
                    var wp = fd.GetWindowPlacement(true);
                    Properties.Settings.Default.FileDialogPlacement = wp;
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
    }
}
