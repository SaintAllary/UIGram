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
        //public Label MessageNameLabel { get; set; }

        private Label messageNameLabel;

        public Label MessageNameLabel
        {
            get { return MessageNameLabel; }
            set { MessageNameLabel.Content = value; }
        }

        public Label TextPreviewLabel { get; set; }
        public Label DateTimePreviewLabel { get; set; }
        public UserDialogPreviewButton()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62' />");

            Button myButton = (Button)XamlReader.Parse(sb.ToString());

            myButton.Padding = new Thickness(0);
            myButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            myButton.VerticalContentAlignment = VerticalAlignment.Stretch;

                    Height = 62;
            //Labels
            MessageNameLabel = new Label() { FontWeight = FontWeights.Bold, Margin = new System.Windows.Thickness(0, 6, 0, 0), FontSize = 13, Content= "Empty name" };
            TextPreviewLabel = new Label() { FontFamily = new FontFamily("Colibri"), FontWeight = FontWeights.DemiBold, Margin = new Thickness(0, 2, 0, 0), Content= "Empty Message........" };
            DateTimePreviewLabel = new Label() { VerticalAlignment = VerticalAlignment.Top ,Content="00.00.00"};
    


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

            grid.Children.Add(new Ellipse() { Fill = Brushes.Aquamarine, Margin = new System.Windows.Thickness(8) });// АВАТАРКА



            Grid secondGrid = new Grid();


            Grid.SetRow(TextPreviewLabel, 1);
            Grid.SetColumn(secondGrid, 1);

            secondGrid.ColumnDefinitions.Add(new ColumnDefinition());
            secondGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width=new System.Windows.GridLength(75)});
            secondGrid.RowDefinitions.Add(new RowDefinition());
            secondGrid.RowDefinitions.Add(new RowDefinition());

            secondGrid.Children.Add(MessageNameLabel);
            secondGrid.Children.Add(TextPreviewLabel);
            secondGrid.Children.Add(innerPanelTime);

            grid.Children.Add(secondGrid);



            myButton.Content = grid;
            Children.Add(myButton);




            //                                                < materialDesign:PackIcon Kind = "CheckAll" VerticalAlignment = "Top" Margin = "0,4,0,0" ></ materialDesign:PackIcon >




        }

    }
}
