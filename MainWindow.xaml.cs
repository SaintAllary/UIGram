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

namespace RuslanMessager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) {
            MainWindow1.Close();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e) {
            MainWindow1.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e) {
            if (MainWindow1.WindowState == WindowState.Maximized)
                MainWindow1.WindowState = WindowState.Normal;
            else
                MainWindow1.WindowState = WindowState.Maximized;
        }


        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void MainWindow1_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (MainWindow1.RenderSize.Width < 815) {
                //MessageBox.Show("test");
                MainWindowGrid.ColumnDefinitions[2].Width = new GridLength(0);
                MainWindowGrid.ColumnDefinitions[2].MinWidth = 0;
            }
            else if (MainWindowGrid.ColumnDefinitions[2].MinWidth == 0 && MainWindow1.RenderSize.Width > 815) {
                MainWindowGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                MainWindowGrid.ColumnDefinitions[2].MinWidth = 380;
            }
        }
    }
}
