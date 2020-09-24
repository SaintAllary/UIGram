using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    class MessageParent
    {
        double id { get; set; }
        string messageText { get; set; }
        string messageContentUrl { get; set; }
        string DateTimeToString { get; set; }

        MessageParent() {
            id = 0;
            messageText = "ParentEmptyMessage";
            messageContentUrl = "ParentEmptyContentUrl";
            DateTimeToString = "ParentEmptyDateTime";
        }
    }
}
