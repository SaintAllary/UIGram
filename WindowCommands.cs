using System.Windows.Controls;
using System.Windows.Input;

namespace RuslanMessager
{
    public class WindowCommands
    {
        //public static RoutedCommand MyExitChatCommand { get; set; }
        public WindowCommands() {
            //MyExitChatCommand = new RoutedCommand("MyExitChatCommand", typeof(TextBox));

            Open = new RoutedCommand("Open", typeof(TextBox));
        }

        public static RoutedCommand Open { get; set; }
    }
}