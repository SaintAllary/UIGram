using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuslanMessager
{
    class MessageUiForm : ListBoxItem, IPreview
    {
        MessageUiForm() : base() {
            
        }

        public long ID { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set ; }
        public string TextPreview { get; set; }
        public string PhoneNumber { get ; set; }
    }
}
