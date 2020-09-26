using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RuslanMessager
{
    class UserDialogPreviewButton : StackPanel
    {
        public Label MessageNamePreviewLabel { get; set; }
        public PackIcon StatusPreviewIcon { get; set; }
        public Label TextPreviewLabel { get; set; }
        public Label DateTimePreviewLabel { get; set; }
        public UserDialogPreviewButton()
        {
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
            #endregion

            #region Labels initializing
            //Labels
            MessageNamePreviewLabel = new Label() { FontWeight = FontWeights.Bold, Margin = new System.Windows.Thickness(0, 6, 0, 0), FontSize = 13, Content = "Empty name" };
            TextPreviewLabel = new Label() { FontFamily = new FontFamily("Colibri"), FontWeight = FontWeights.DemiBold, Margin = new Thickness(0, 2, 0, 0), Content = "Empty Message........" };
            DateTimePreviewLabel = new Label() { VerticalAlignment = VerticalAlignment.Top, Content = "00.00.00" };
            #endregion

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

            grid.Children.Add(new Ellipse() { Fill =new  SolidColorBrush(Color.FromRgb(103,58,183)), Margin = new System.Windows.Thickness(8) });// АВАТАРКА



            Grid secondGrid = new Grid();
            Grid.SetRow(TextPreviewLabel, 1);
            Grid.SetColumn(secondGrid, 1);

            secondGrid.ColumnDefinitions.Add(new ColumnDefinition());
            secondGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=new System.Windows.GridLength(75)});
            secondGrid.RowDefinitions.Add(new RowDefinition());
            secondGrid.RowDefinitions.Add(new RowDefinition());

            secondGrid.Children.Add(MessageNamePreviewLabel);
            secondGrid.Children.Add(TextPreviewLabel);
            secondGrid.Children.Add(innerPanelTime);

            grid.Children.Add(secondGrid);



            myButton.Content = grid;
            Children.Add(myButton);



            #endregion

            #region Icon    
            StatusPreviewIcon = new PackIcon();
            StatusPreviewIcon.Kind = PackIconKind.CheckAll;
            StatusPreviewIcon.VerticalAlignment = VerticalAlignment.Top;
            StatusPreviewIcon.Margin = new Thickness(0, 4, 0, 0);
            innerPanelTime.Children.Add(StatusPreviewIcon);
            #endregion

        }

        public UserDialogPreviewButton(string userOutName)
        {

          
            
            Height = 62;

            #region Main Signature of Dialog
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62'  />");

            Button myButton = (Button)XamlReader.Parse(sb.ToString());

            foreach (var item in Application.Current.Windows)
            {
                if (item.GetType() == typeof(MainWindow))
                    myButton.Click += (item as MainWindow).Test;
            }

            myButton.Padding = new Thickness(0);
            myButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            myButton.VerticalContentAlignment = VerticalAlignment.Stretch;
            #endregion

            #region Labels initializing
            //Labels
            MessageNamePreviewLabel = new Label() { FontWeight = FontWeights.Bold, Margin = new System.Windows.Thickness(0, 6, 0, 0), FontSize = 13,Content = userOutName };
            TextPreviewLabel = new Label() { FontFamily = new FontFamily("Colibri"), FontWeight = FontWeights.DemiBold, Margin = new Thickness(0, 2, 0, 0) };
            DateTimePreviewLabel = new Label() { VerticalAlignment = VerticalAlignment.Top};
            #endregion

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

            secondGrid.Children.Add(MessageNamePreviewLabel);
            secondGrid.Children.Add(TextPreviewLabel);
            secondGrid.Children.Add(innerPanelTime);

            grid.Children.Add(secondGrid);



            myButton.Content = grid;
            Children.Add(myButton);



            #endregion

            #region Icon    
            StatusPreviewIcon = null;
            #endregion

        }
    }
}
