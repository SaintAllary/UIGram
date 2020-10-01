using MaterialDesignThemes.Wpf;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RuslanMessager
{
    internal class UserDialogPreviewButton : StackPanel
    {
        public Label NamePreviewLabel { get; set; }
        public PackIcon StatusPreviewIcon { get; set; }
        public Label TextPreviewLabel { get; set; }
        public Label DateTimePreviewLabel { get; set; }
        private Ellipse Avatar { get; set; }

        private string dateTimePreviewer;
        public string DateTimePreviewer {
            get { return dateTimePreviewer; }
            set {
                dateTimePreviewer = value;
                if (value != null && value != DateTime.MinValue.ToString())
                    DateTimePreviewLabel.Content = DateTime.Parse(value).ToShortDateString() == DateTime.Now.ToShortDateString() ? DateTime.Parse(value).ToShortTimeString() : DateTime.Parse(value).ToShortDateString();
            }
        }

        public double GeneralHeight { get; set; }
        private long id;
        public long ID {
            get { return id; }
            set { id = value; }
        }

        private string userName;
        public string UserName {
            get { return userName; }
            set {
                userName = value;
                NamePreviewLabel.Content = value;
            }
        }

        private string phoneNumber;
        public string PhoneNumber {
            get { return phoneNumber; }
            set {
                phoneNumber = value;
            }
        }

        private string pictureURL;
        public string PictureURL {
            get { return pictureURL; }
            set {
                pictureURL = value;
                if (value != null) {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(value, UriKind.RelativeOrAbsolute));
                    imageBrush.Stretch = Stretch.UniformToFill;
                    Avatar.Fill = imageBrush;
                }
                else {
                    Avatar.Fill = new SolidColorBrush(Color.FromRgb(41, 58, 76));
                }
            }
        }

        private string textPreview;
        public string TextPreview {
            get { return textPreview; }
            set {
                textPreview = value;
                TextPreviewLabel.Content = value;
            }
        }

        public Button myButton { get; set; }
        private long icon;

        public long Icon
        {
            get { return icon; }
            set { icon = value;
                if (value != long.MaxValue)
                {
                StatusPreviewIcon.Kind = (PackIconKind)value;

                }
            }
        }

        public bool MyTurn { get; set; }

        public UserDialogPreviewButton() {
            Height = 62;

            #region Main Signature of Dialog

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62' />");

            Button myButton = (Button)XamlReader.Parse(sb.ToString());

            myButton.Padding = new Thickness(0);
            myButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            myButton.VerticalContentAlignment = VerticalAlignment.Stretch;
            myButton.Tag = UserName.ToString();
            myButton.BorderBrush = (Brush)Application.Current.Resources["SecondaryAccentBrush"];


            #endregion Main Signature of Dialog

            #region Labels initializing

            //Labels
            NamePreviewLabel = new Label() { FontWeight = FontWeights.Bold, Margin = new System.Windows.Thickness(0, 6, 0, 0), FontSize = 13, Content = "Empty name" };
            TextPreviewLabel = new Label() { FontFamily = new FontFamily("Colibri"), FontWeight = FontWeights.DemiBold, Margin = new Thickness(0, 2, 0, 0), Content = "Empty Message........" };
            DateTimePreviewLabel = new Label() { VerticalAlignment = VerticalAlignment.Top, Content = "00.00.00" };

            #endregion Labels initializing

            #region Grids settings

            StackPanel innerPanelTime = new StackPanel();
            innerPanelTime.FlowDirection = FlowDirection.RightToLeft;
            innerPanelTime.Orientation = Orientation.Horizontal;
            Grid.SetColumn(innerPanelTime, 1);
            Grid.SetRow(innerPanelTime, 0);
            innerPanelTime.Children.Add(DateTimePreviewLabel);

            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Height = 62;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(62) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(new Ellipse() { Fill = new SolidColorBrush(Color.FromRgb(103, 58, 183)), Margin = new System.Windows.Thickness(8) });// АВАТАРКА

            Grid secondGrid = new Grid();
            Grid.SetRow(TextPreviewLabel, 1);
            Grid.SetColumn(secondGrid, 1);

            secondGrid.ColumnDefinitions.Add(new ColumnDefinition());
            secondGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(75) });
            secondGrid.RowDefinitions.Add(new RowDefinition());
            secondGrid.RowDefinitions.Add(new RowDefinition());

            secondGrid.Children.Add(NamePreviewLabel);
            secondGrid.Children.Add(TextPreviewLabel);
            secondGrid.Children.Add(innerPanelTime);

            grid.Children.Add(secondGrid);

            myButton.Content = grid;
            Children.Add(myButton);

            #endregion Grids settings

            #region Icon

            StatusPreviewIcon = new PackIcon();
            StatusPreviewIcon.Kind = PackIconKind.CheckAll;
            StatusPreviewIcon.VerticalAlignment = VerticalAlignment.Top;
            StatusPreviewIcon.Margin = new Thickness(0, 4, 0, 0);
            innerPanelTime.Children.Add(StatusPreviewIcon);

            #endregion Icon
        }

        [Obsolete]
        public UserDialogPreviewButton(string userOutName) {
            Height = 62;

            #region Main Signature of Dialog

            //StringBuilder sb = new StringBuilder();
            //sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
            //                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            ////sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}'/>");
            //sb.Append(@"Style='{DynamicResource MaterialDesignFlatButton}'/>");

            //Button myButton = (Button)XamlReader.Parse(sb.ToString());
            myButton = new Button();

            foreach (var item in Application.Current.Windows) {
                if (item.GetType() == typeof(MainWindow))
                    myButton.Click += (item as MainWindow).LoadChatFromPrev;
            }

            myButton.Padding = new Thickness(0);
            myButton.Height = 62;
            myButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            myButton.VerticalContentAlignment = VerticalAlignment.Stretch;
            myButton.Tag = userOutName.ToString();
            myButton.BorderThickness = new Thickness(0);
            myButton.BorderBrush = (Brush)Application.Current.Resources["SecondaryAccentBrush"];
            myButton.Style = Application.Current.FindResource("CustomMaterialDesignFlatButton") as Style;

            #endregion Main Signature of Dialog

            #region Labels initializing

            //Labels
            NamePreviewLabel = new Label() { FontWeight = FontWeights.Bold, Margin = new System.Windows.Thickness(0, 6, 0, 0), FontSize = 13 };
            TextPreviewLabel = new Label() { FontFamily = new FontFamily("Colibri"), VerticalContentAlignment = VerticalAlignment.Top, FontWeight = FontWeights.DemiBold, Margin = new Thickness(0, -6, 0, 0), Height = 25 };
            DateTimePreviewLabel = new Label() { VerticalAlignment = VerticalAlignment.Top };

            #endregion Labels initializing

            #region Grids settings

            Avatar = new Ellipse() { Margin = new Thickness(8) };
            StackPanel innerPanelTime = new StackPanel();
            innerPanelTime.FlowDirection = FlowDirection.RightToLeft;
            innerPanelTime.Orientation = Orientation.Horizontal;
            Grid.SetColumn(innerPanelTime, 1);
            Grid.SetRow(innerPanelTime, 0);
            innerPanelTime.Children.Add(DateTimePreviewLabel);

            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Height = 62;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(62) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(Avatar);// АВАТАРКА

            Grid secondGrid = new Grid();
            Grid.SetRow(TextPreviewLabel, 1);
            Grid.SetColumn(secondGrid, 1);

            secondGrid.ColumnDefinitions.Add(new ColumnDefinition());
            secondGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(75) });
            secondGrid.RowDefinitions.Add(new RowDefinition());
            secondGrid.RowDefinitions.Add(new RowDefinition());

            secondGrid.Children.Add(NamePreviewLabel);
            secondGrid.Children.Add(TextPreviewLabel);
            secondGrid.Children.Add(innerPanelTime);

            secondGrid.ColumnDefinitions[1].Width = new GridLength(100);

            grid.Children.Add(secondGrid);

            myButton.Content = grid;
            Children.Add(myButton);

            #endregion Grids settings

            #region Icon


            StatusPreviewIcon = new PackIcon();
            StatusPreviewIcon.Kind = PackIconKind.None;
            StatusPreviewIcon.VerticalAlignment = VerticalAlignment.Top;
            StatusPreviewIcon.Margin = new Thickness(0, 4, 0, 0);
            innerPanelTime.Children.Add(StatusPreviewIcon);


            #endregion Icon

            UserName = userOutName;
            Avatar.Fill = new SolidColorBrush(Color.FromRgb(41, 58, 76));
        }
    }
}