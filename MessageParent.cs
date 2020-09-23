using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    class MessageParent : Card
    {
        MessageParent() {
            this.Height = 50;
            this.Width = 50;
            this.Content = "test msg";
        }
    }
}
