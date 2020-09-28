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
        StackPanel BumpPreviewCollection { get; set; }
        public long CurrentChatID { get; set; }
        public DateTime LastDateToLoad { get; set; }
        public Message LastMSG { get; set; }
        public DateTime CurrentLoadedDate { get; private set; }
        public bool ChatScrollViewerVerticalOffsetZeroPointerFixer { get; private set; }
        public bool IsAdminModeEnabled { get; private set; }

        public MainWindow() {
            InitializeComponent();
            InitializeLogic();
        }

        public void InitializeLogic() {
            if (!Directory.Exists(Properties.Resources.UserDataDirPath)) {
                Directory.CreateDirectory(Properties.Resources.UserDataDirPath);
            }
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

            if (MainWindow1.RenderSize.Width < 715)
                ResizeColoum(3, 0, 0, GridUnitType.Pixel);
            else if (MainWindowGrid.ColumnDefinitions[3].MinWidth == 0 && MainWindow1.RenderSize.Width > 715)
                ResizeColoum(3, 380, 1, GridUnitType.Star);
            if (MainWindowGrid.ColumnDefinitions[3].Width.Value > 140) {
                this.MyMsg.Width = MainWindowGrid.ColumnDefinitions[3].Width.Value - 46 * 3;
            }
        }

        [Obsolete]
        public void LoadChatFromPrev(object sender, RoutedEventArgs e) {
            //SwitchChatCleaner();
            this.ChatTopName_TextBlock.Text = (sender as Button).Tag.ToString();

            this.ChatGrid.RowDefinitions[0].Height = new GridLength(54);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(46);

            CreateAllDirsByID();

            SetCurrentChatIdID(sender);

            ClearCurrentDialog();

            LoadChatMsgs();

            UpdatePreview();

            CurrentLoadedDate = LoadLastChatFile();
        }


        public void ClearCurrentDialog() {
            MessageListBox.Items.Clear();
            //LastMSG = null;

        }
        public void UpdatePreview() {

        }

        [Obsolete]
        private void LoadChatMsgs() {
            var s = XmlFunctions.GetDayJournal(CurrentChatID, LoadLastChatFile().ToShortDateString());
            if (s != null) {
                foreach (var item in s.Messages) {
                    MessageListBox.Items.Add(new MessageUiForm(item.MessageText, item.SendDateTime, item.SenderName, item.DoesRead) {
                        DoesRead = item.DoesRead,
                        MessageContentUrl = item.MessageContentUrl,
                        MyTurn = item.MyTurn,
                        SenderName = item.SenderName,
                        SendDateTime = item.SendDateTime,
                    });

                }
                MoveChatScrollToDownEnd();
                //LastMSG = s.Messages.Last();

                //LastDateToLoad = DateTime.Parse(DateTime.Parse(s.Messages.First().SendDateTime).ToShortDateString());
            }

            GC.Collect();
        }

        private void UpdatePreviewInfo(Message message) {

        }


        private DateTime LoadLastChatFile() {
            DateTime lastDate = DateTime.Parse(DateTime.Now.ToShortDateString());



            List<string> filePaths = Directory.GetFiles(Properties.Resources.UserDataDirPath + "\\" + CurrentChatID).ToList();

            if (filePaths.Count == 0)
                return DateTime.Now;

            var PrevFilePath = DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[0]));
            for (int i = 0; i < filePaths.Count; i++) {
                if (PrevFilePath < DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]))) {
                    PrevFilePath = DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
                }
            }


            return PrevFilePath;//Последний день переписки.
        }

        private string LoadPrevLastChatFile() {

            DateTime lastDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            List<string> filePaths = Directory.GetFiles(Properties.Resources.UserDataDirPath + "\\" + CurrentChatID).ToList();
            var PrevFilePath = DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[0]));
            for (int i = 0; i < filePaths.Count; i++) {
                if (PrevFilePath < DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[i])) && DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[i])) < CurrentLoadedDate) {
                    PrevFilePath = DateTime.Parse(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
                }
            }
            if (PrevFilePath == CurrentLoadedDate)
                return null;

            CurrentLoadedDate = PrevFilePath;//Предыдущий существующий день переписки.


            return PrevFilePath.ToShortDateString();
        }

        private void SetCurrentChatIdID(object sender) {
            foreach (var item in PreviewsPanel.Children) {
                if (item is UserDialogPreviewButton)
                    if ((item as UserDialogPreviewButton).Children[0] == (sender as Button))
                        CurrentChatID = (item as UserDialogPreviewButton).ID;
            }
        }

        private void CloseWindow_CanExec(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }


        public void CreateAllDirsByID() {
            foreach (var item in PreviewsPanel.Children) {


                if (item is UserDialogPreviewButton) {
                    var s = item as UserDialogPreviewButton;
                    var creatingPAth = Properties.Resources.UserDataDirPath + "\\" + s.ID;

                    if (!Directory.Exists(creatingPAth))
                        Directory.CreateDirectory(creatingPAth);
                }
            }
        }

        private void CloseWindow_Exec(object sender, ExecutedRoutedEventArgs e) {
            this.ChatGrid.RowDefinitions[0].Height = new GridLength(0);
            this.ChatGrid.RowDefinitions[2].Height = new GridLength(0);
            SwitchChatCleaner();
        }

        private void SwitchChatCleaner() {
            LastDateToLoad = DateTime.Parse(DateTime.Now.ToShortDateString());
        }

        private void ColorZone_Loaded(object sender, RoutedEventArgs e) { }

        private void ResizeColoum(int indexPosition, double minWidth, double value, GridUnitType gridUnitType) {
            MainWindowGrid.ColumnDefinitions[indexPosition].Width = new GridLength(1, gridUnitType);
            MainWindowGrid.ColumnDefinitions[indexPosition].MinWidth = minWidth;
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e) {
            if (File.Exists(Properties.Resources.PreviewSavePath)) {

                foreach (var item in XmlFunctions.GetPreviewListInfo().userPreviewSerializables)
                    this.PreviewsPanel.Children.Add(new UserDialogPreviewButton(item.UserName) { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL, TextPreview = item.LastMSG.MessageText, DateTimePreviewer = item.LastMSG.SendDateTime });

                BumpPreviewCollection = new StackPanel();
                //this.PreviewsChatsGrid.Children.Add(BumpPreviewCollection);
                this.BumpPreviewCollection.HorizontalAlignment = HorizontalAlignment.Stretch;
                this.BumpPreviewCollection.VerticalAlignment = VerticalAlignment.Stretch;
                Grid.SetColumn(BumpPreviewCollection, 1);
                Grid.SetRow(BumpPreviewCollection, 1);

                foreach (var item in XmlFunctions.GetPreviewListInfo().userPreviewSerializables)
                    BumpPreviewCollection.Children.Add(new UserDialogPreviewButton(item.UserName) { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL });
            }

            GC.Collect();
        }

        private long GetBiggestID() {
            long tmp = 0;
            foreach (var item in this.PreviewsPanel.Children) {

                if (item is UserDialogPreviewButton) {
                    if ((item as UserDialogPreviewButton).ID > tmp) {
                        tmp = (item as UserDialogPreviewButton).ID;
                    }
                }
            }
            return tmp;

        }
        private void AddUserButton_Click(object sender, RoutedEventArgs e) {
            AddUserDialog addUserDialog = new AddUserDialog();
            addUserDialog.ShowDialog();


            UserDialogPreviewButton userDialogPreviewButton = new UserDialogPreviewButton(addUserDialog.NameTextBox.Text) { PhoneNumber = addUserDialog.NumberTextBox.Text, ID = GetBiggestID() + 1 };

            if (addUserDialog.DoexExecuted == true) {
                PreviewsPanel.Children.Add(userDialogPreviewButton);
            }
        }

        private void FastAddUserBtn_Click(object sender, RoutedEventArgs e) {
            //PreviewsPanel.Children.Add(new UserDialogPreviewButton("TEST USER") { });
        }

        [Obsolete]
        private void SendMsgBtn_Click(object sender, RoutedEventArgs e) {
            if (this.MyMsg.Text != "") {//НЕ ПУСТОЕ СООБЩЕНИЕ
                var msg = new MessageUiForm(this.MyMsg.Text.Trim(), DateTime.Now.ToString(), this.ChatTopName_TextBlock.Text);

                this.MessageListBox.Items.Add(msg);

                MoveChatScrollToDownEnd();

                UpdatePreviewFull();

                XmlFunctions.UpdateDayJournal(msg, CurrentChatID);

                this.MyMsg.Text = "";
            }
        }


        public void UpdatePreviewFull() {
            Message message = new Message() {
                MyTurn = true,
                DoesRead = false,
                SendDateTime = DateTime.Now.ToString(),
                MessageText = this.MyMsg.Text.Trim(),
                SenderName = this.ChatTopName_TextBlock.Text,
                MessageContentUrl = null
            };

            XmlFunctions.UpdatePreviewByMsg(GetPreviewSerList(1, message));

            foreach (var item in PreviewsPanel.Children) {
                if (item is UserDialogPreviewButton) {
                    var s = (item as UserDialogPreviewButton);
                    if (s.ID == CurrentChatID) {
                        s.TextPreview = message.MessageText;
                        s.DateTimePreviewer = message.SendDateTime;
                    }
                }
            }

        }

        private void MoveChatScrollToDownEnd() {
            this.ChatScrollViewer.ScrollToEnd();
        }


        private UserPreviewSerializableList GetPreviewSerList(long ID = long.MaxValue, Message msg = null) {
            UserPreviewSerializableList userPreviewSerializableList = new UserPreviewSerializableList();
            try {
                foreach (var inneritem in this.PreviewsPanel.Children) {
                    if (inneritem is UserDialogPreviewButton) {
                        UserDialogPreviewButton item = inneritem as UserDialogPreviewButton;
                        UserPreviewSerializable userPreviewSerializable = new UserPreviewSerializable() { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL, UserName = item.UserName, LastMSG = new Message() { SendDateTime = item.DateTimePreviewer, MessageText = item.TextPreview, MessageContentUrl = item.PictureURL, SenderName = item.UserName } };
                        userPreviewSerializableList.userPreviewSerializables.Add(userPreviewSerializable);
                        if (ID != long.MaxValue)
                            userPreviewSerializable.LastMSG = msg;


                    }
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            return userPreviewSerializableList;

        }

        private void PostSave() {
            CleanSerializableFile(Properties.Resources.PreviewSavePath);

            UserPreviewSerializableList prev = GetPreviewSerList();

            //try {
            //    foreach (var inneritem in this.PreviewsPanel.Children) {
            //        if (inneritem is UserDialogPreviewButton) {
            //            UserDialogPreviewButton item = inneritem as UserDialogPreviewButton;
            //            prev.userPreviewSerializables.Add(new UserPreviewSerializable() { ID = item.ID, PhoneNumber = item.PhoneNumber, PictureURL = item.PictureURL, UserName = item.UserName });
            //        }
            //    }

            //}
            //catch (Exception ex) {
            //    MessageBox.Show(ex.Message);
            //}

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserPreviewSerializableList));

            using (FileStream fs = new FileStream(Properties.Resources.PreviewSavePath, FileMode.OpenOrCreate)) {
                xmlSerializer.Serialize(fs, prev);
            }

        }

        private void CleanSerializableFile(string path) {
            if (File.Exists(path))
                File.Delete(path);
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

        }

        private void PostSave(object sender, System.ComponentModel.CancelEventArgs e) {
            PostSave();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if ((sender as TextBox).Text == "") {
                this.LeftScrollViewer.Content = this.PreviewsPanel;
                return;
            }

            this.LeftScrollViewer.Content = BumpPreviewCollection;

            BumpPreviewCollection.Children.Clear();
            foreach (var item in this.PreviewsPanel.Children) {
                if ((item as UserDialogPreviewButton).UserName.Contains((sender as TextBox).Text)) {
                    BumpPreviewCollection.Children.Add(new UserDialogPreviewButton((item as UserDialogPreviewButton).UserName) { ID = (item as UserDialogPreviewButton).ID, PhoneNumber = (item as UserDialogPreviewButton).PhoneNumber, PictureURL = (item as UserDialogPreviewButton).PictureURL });
                }
            }
        }

        private void SendMsgBtnToMe_Click(object sender, RoutedEventArgs e) {
            if (this.MyMsg.Text != "") {//НЕ ПУСТОЕ СООБЩЕНИЕ
                var msg = new MessageUiForm(this.MyMsg.Text.Trim(), DateTime.Now.ToString(), this.ChatTopName_TextBlock.Text, false);

                this.MessageListBox.Items.Add(msg);

                MoveChatScrollToDownEnd();

                UpdatePreviewFull();

                XmlFunctions.UpdateDayJournal(msg, CurrentChatID);

                this.MyMsg.Text = "";



            }
        }

        [Obsolete]
        private void ChatScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e) {
            if (this.ChatGrid.RowDefinitions[0].Height != new GridLength(0)) {
                if (this.ChatScrollViewer.VerticalOffset == 0 /*&& this.ChatScrollViewer.ScrollableHeight != 0*/) {
                    if (ChatScrollViewerVerticalOffsetZeroPointerFixer) {
                        DayMessageJournalSerializable s = XmlFunctions.GetDayJournal(CurrentChatID, LoadPrevLastChatFile());

                        if (s != null) {
                            int index = 0;
                            foreach (var item in s.Messages) {
                                MessageListBox.Items.Insert(index, new MessageUiForm(item.MessageText, item.SendDateTime, item.SenderName, item.DoesRead) {
                                    DoesRead = item.DoesRead,
                                    MessageContentUrl = item.MessageContentUrl,
                                    MyTurn = item.MyTurn,
                                    SenderName = item.SenderName,
                                    SendDateTime = item.SendDateTime,
                                });
                                index++;

                            }
                            this.ChatScrollViewer.LineDown();
                            this.ChatScrollViewer.LineDown();
                            this.ChatScrollViewer.LineDown();
                            this.ChatScrollViewer.LineDown();
                            this.ChatScrollViewer.LineDown();
                            //MessageBox.Show(this.ChatScrollViewer.VerticalOffset.ToString());

                            GC.Collect();
                            ChatScrollViewerVerticalOffsetZeroPointerFixer = false;
                        }
                        //MessageBox.Show(CurrentLoadedDate.ToString());


                    }
                    else
                        ChatScrollViewerVerticalOffsetZeroPointerFixer = true;
                }
            }

        }

        private void AdminPanel_CanExec(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void AdminPanel_Exec(object sender, ExecutedRoutedEventArgs e) {
            if (IsAdminModeEnabled) {
                this.AddUser.IsEnabled = true;
                this.AddUser.Opacity = 1;
                this.MessageListBox.IsHitTestVisible = true;

                IsAdminModeEnabled = false;
            }
            else {
                this.AddUser.IsEnabled = false;
                this.AddUser.Opacity = 0;
                this.MessageListBox.IsHitTestVisible = false;

                IsAdminModeEnabled = true;
            }
        }
    }
}
