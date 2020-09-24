using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace RuslanMessager
{
    class IconButton : StackPanel
    {
        public PackIconKind PackIconKind { get; set; }
        public PackIcon Icon { get; set; }
        public string TextInsise { get; set; }
        public IconButton()
        {
            #region InitializeButtonStyle 
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62' />");

            Button myButton = (Button)XamlReader.Parse(sb.ToString());
            myButton.Height = 62;
            myButton.Width = 72;
            myButton.Padding = new System.Windows.Thickness(0, 8, 0, 0);
            Children.Add(myButton);
            #endregion

            StackPanel stackPanel = new StackPanel() { Width = 72, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = System.Windows.VerticalAlignment.Center };
            stackPanel.Children.Add(Icon = new PackIcon()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                Height = 30,
                Width = 30,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Bottom
                ,
                //Kind = PackIconKind
            });




            stackPanel.Children.Add(new Label()
            {
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            ,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch,
                FontSize = 11,
                Width = 72,
                Content = TextInsise
            });

            myButton.Content = stackPanel;
        }
      

    }
}
