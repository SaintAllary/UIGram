﻿using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using RuslanMessager.Properties;
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
            if (MainWindow1.RenderSize.Width < 815)
                ResizeColoum(3, 0, 0, GridUnitType.Pixel);
            else if (MainWindowGrid.ColumnDefinitions[3].MinWidth == 0 && MainWindow1.RenderSize.Width > 815)
                ResizeColoum(3, 380, 1, GridUnitType.Star);

            if (MainWindowGrid.ColumnDefinitions[3].Width.Value > 140) {
                this.MyMsg.Width = MainWindowGrid.ColumnDefinitions[3].Width.Value - 46 * 3;
                MessageBox.Show(MainWindowGrid.ColumnDefinitions[3].Width.Value.ToString());
            }

        }

        public void Test(object sender, RoutedEventArgs e) {
            this.ChatGrid.RowDefinitions[0].Height = new GridLength(54);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(46);
        }
        private void CloseWindow_CanExec(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }
        private void CloseWindow_Exec(object sender, ExecutedRoutedEventArgs e) {
            this.ChatGrid.RowDefinitions[0].Height = new GridLength(0);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(0);
        }

        private void ColorZone_Loaded(object sender, RoutedEventArgs e) { }

        private void ResizeColoum(int indexPosition, double minWidth, double value, GridUnitType gridUnitType) {
            MainWindowGrid.ColumnDefinitions[indexPosition].Width = new GridLength(1, gridUnitType);
            MainWindowGrid.ColumnDefinitions[indexPosition].MinWidth = minWidth;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e) { }

        private void AddUserButton_Click(object sender, RoutedEventArgs e) {
            PreviewInfoSerializable preview = new PreviewInfoSerializable();

            AddUserDialog addUserDialog = new AddUserDialog();
            addUserDialog.ShowDialog();

            preview.UserName = addUserDialog.NameTextBox.Text;
            preview.PhoneNumber = addUserDialog.NumberTextBox.Text;


            if (addUserDialog.DoexExecuted == true) {
                PreviewsPanel.Children.Add(new UserDialogPreviewButton(preview.UserName) { });
            }

        }

        private void FastAddUserBtn_Click(object sender, RoutedEventArgs e) {
            PreviewsPanel.Children.Add(new UserDialogPreviewButton("TEST USER") { });
            PreviewsPanel.Children.Add(new UserDialogPreviewButton("TEST USER") { });
            PreviewsPanel.Children.Add(new UserDialogPreviewButton("TEST USER") { });
        }

        [Obsolete]
        private void Button_Click(object sender, RoutedEventArgs e) {
            var msg = new MessageUiForm(this.MyMsg.Text);
            //msg.MessageText = ;

            this.MessageListBox.Items.Add(msg);
        }

        private void OpenFile_Executed(object sender, ExecutedRoutedEventArgs e) {

        }


    }
}
