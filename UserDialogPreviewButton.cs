using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace RuslanMessager
{
    class UserDialogPreviewButton : StackPanel
    {
        public UserDialogPreviewButton()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' 
                            xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ");
            sb.Append(@"Style='{StaticResource MaterialDesignFlatButton}' Height ='62' />");

  
            Button myButton = (Button)XamlReader.Parse(sb.ToString());

            Children.Add(myButton);

         

     


        }

    }
}
