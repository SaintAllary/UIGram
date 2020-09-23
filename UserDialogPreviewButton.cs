using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RuslanMessager
{
    class UserDialogPreviewButton : StackPanel
    {
        public UserDialogPreviewButton() {
            //0-------------------
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62' />");
            
            Button myButton = (Button)XamlReader.Parse(sb.ToString());


            //1-------------------
            Grid MainPanel1 = new Grid();
            //MainPanel1.Orientation = Orientation.Horizontal;
            //MainPanel1.Background = Brushes.IndianRed;
            //MainPanel1.Width = 300;
            MainPanel1.RowDefinitions.Add(new RowDefinition());
            MainPanel1.RowDefinitions.Add(new RowDefinition());

            MainPanel1.Children.Add(new Button() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right });

            //2-------------------
            StackPanel MainPanel2 = new StackPanel();
            MainPanel2.Orientation = Orientation.Vertical;
            MainPanel2.Background = Brushes.LightGreen;

            //3-------------------
            StackPanel MainPanel3 = new StackPanel();
            MainPanel3.Orientation = Orientation.Horizontal;
            MainPanel3.Background = Brushes.AliceBlue;

            MainPanel3.Children.Add(new Label() { Content = "UserName" });
            MainPanel3.Children.Add(new Label() { Content = "<" });
            MainPanel3.Children.Add(new Label() { Content = "22:22" });


            //2-------------------
            MainPanel2.Children.Add(MainPanel3);
            MainPanel2.Children.Add(new Label() { Content = "preview last msg" });


            //1-------------------
            MainPanel1.Children.Add(MainPanel2);


            //0-------------------
            myButton.Content = MainPanel1;

            //--------------------
            Children.Add(myButton);
        }

    }
}
