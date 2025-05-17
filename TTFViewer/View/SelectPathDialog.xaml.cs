using GuiMisc;
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
using System.Windows.Shapes;
using WindowPlacementSample;

namespace TTFViewer.View
{
    /// <summary>
    /// SelectPathDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectPathDialog : Window
    {
        public List<string> Paths { get; set; }
        public string Path { get; private set; }

        public SelectPathDialog(List<string> paths)
        {
            Paths = paths;
            InitializeComponent();
            DataContext = this;
        }


        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var wp = Properties.Settings.Default.SelectPathDialogPlacement;
            this.SetWindowPlacement(wp, true);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PathListBox.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var wp = this.GetWindowPlacement(true);
            Properties.Settings.Default.SelectPathDialogPlacement = wp;
        }


        private void ListBoxItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (PathListBox.SelectedItem is string path)
                    Path = path;

                DialogResult = true;
            }
        }

        private void ButtonOk(object sender, RoutedEventArgs e)
        {
            if (PathListBox.SelectedItem is string path)
                Path = path;

            DialogResult = true;
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
