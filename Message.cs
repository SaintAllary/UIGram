using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    class Message
    {
        /// <summary>
        /// Can contain url on any media
        /// </summary>
        public string MessageContentUrl { get; set; }
        /// <summary>
        /// Contain full data
        /// </summary>
        public string SendDateTime { get; set; }
        public string SenderName { get; set; }
        public string MessageText { get; set; }
        /// <summary>
        /// Show does message read
        /// </summary>
        public bool DoesRead { get; set; }
        /// <summary>
        /// Show 
        /// </summary>
        public bool MyTurn { get; set; }

        public Message()
        {
          

        }
    }
}
