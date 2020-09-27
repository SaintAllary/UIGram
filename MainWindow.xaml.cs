using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using RuslanMessager.Properties;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace RuslanMessager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public long CurrentChatID { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitializeLogic();
        }

        public void InitializeLogic()
        {
            if (!Directory.Exists(Properties.Resources.UserDataDirPath))
            {
                Directory.CreateDirectory(Properties.Resources.UserDataDirPath);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.Close();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow1.WindowState == WindowState.Maximized)
                MainWindow1.WindowState = WindowState.Normal;
            else
                MainWindow1.WindowState = WindowState.Maximized;
        }


        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MainWindow1_SizeChanged(object sender, SizeChangedEventArgs e) {

            if (MainWindow1.RenderSize.Width < 815)
                ResizeColoum(3, 0, 0, GridUnitType.Pixel);
            else if (MainWindowGrid.ColumnDefinitions[3].MinWidth == 0 && MainWindow1.RenderSize.Width > 815)
                ResizeColoum(3, 380, 1, GridUnitType.Star);


            if (MainWindowGrid.ColumnDefinitions[3].Width.Value > 140) {

                this.MyMsg.Width = MainWindowGrid.ColumnDefinitions[3].Width.Value - 46 * 3;
 
            }

        }

        public void Test(object sender, RoutedEventArgs e)
        {
            this.ChatGrid.RowDefinitions[0].Height = new GridLength(54);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(46);

            CreateAllDirsByID();


            foreach (var item in PreviewsPanel.Children)
            {
                if (item is UserDialogPreviewButton)
                    if ((item as UserDialogPreviewButton).Children[0] == (sender as Button))
                        CurrentChatID = (item as UserDialogPreviewButton).ID;

            }

    






        }

        public void CreateAllDirsByID()
        {
            foreach (var item in PreviewsPanel.Children)
            {


                if (item is UserDialogPreviewButton)
                {
                    var s = item as UserDialogPreviewButton;
                    var creatingPAth = Properties.Resources.UserDataDirPath + "\\" + s.ID;

                    if (!Directory.Exists(creatingPAth))
                        Directory.CreateDirectory(creatingPAth);
                }
            }
        }


        private void CloseWindow_CanExec(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseWindow_Exec(object sender, ExecutedRoutedEventArgs e)
        {
            this.ChatGrid.RowDefinitions[0].Height = new GridLength(0);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(0);
        }

        private void ColorZone_Loaded(object sender, RoutedEventArgs e) { }

        private void ResizeColoum(int indexPosition, double minWidth, double value, GridUnitType gridUnitType)
        {
            MainWindowGrid.ColumnDefinitions[indexPosition].Width = new GridLength(1, gridUnitType);
            MainWindowGrid.ColumnDefinitions[indexPosition].MinWidth = minWidth;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Properties.Resources.PreviewSavePath))
            {

                foreach (var item in XmlFunctions.GetPreviewListInfo().userPreviewSerializables)
                    (LeftScrollViewer.Content as StackPanel).Children.Add(new UserDialogPreviewButton(item.UserName) { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL });

            }

            GC.Collect();
        }

        private long GetBiggestID()
        {
            long tmp = 0;
            foreach (var item in (LeftScrollViewer.Content as StackPanel).Children)
            {

                if (item is UserDialogPreviewButton)
                {
                    if ((item as UserDialogPreviewButton).ID > tmp)
                    {
                        tmp = (item as UserDialogPreviewButton).ID;
                    }

                }
            }
            return tmp;

        }
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {


            AddUserDialog addUserDialog = new AddUserDialog();
            addUserDialog.ShowDialog();



            UserDialogPreviewButton userDialogPreviewButton = new UserDialogPreviewButton(addUserDialog.NameTextBox.Text) { PhoneNumber = addUserDialog.NumberTextBox.Text, ID = GetBiggestID() + 1 };

            if (addUserDialog.DoexExecuted == true)
            {
                PreviewsPanel.Children.Add(userDialogPreviewButton);
            }

        }

        private void FastAddUserBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        [Obsolete]
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var msg = new MessageUiForm(this.MyMsg.Text);

            this.MessageListBox.Items.Add(msg);

            XmlFunctions.UpdateDayJournal(msg, CurrentChatID);


        }

        
        //private long GetCurrentChatId()
        //{

        //    //return PreviewsPanel
        //}
        private void PostSave()
        {

            CleanSerializableFile(Properties.Resources.PreviewSavePath);

            UserPreviewSerializableList prev = new UserPreviewSerializableList();

            try
            {
                foreach (var inneritem in (LeftScrollViewer.Content as StackPanel).Children)
                {
                    if (inneritem is UserDialogPreviewButton)
                    {
                        UserDialogPreviewButton item = inneritem as UserDialogPreviewButton;
                        prev.userPreviewSerializables.Add(new UserPreviewSerializable() { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL, UserName = item.UserName });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserPreviewSerializableList));

            using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, prev);
            }

        }

        private void CleanSerializableFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void PostSave(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PostSave();
        }

    }
}
