using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    interface IMessage
    {
        /// <summary>
        /// Can contain url on any media
        /// </summary>
        string MessageContentUrl { get; set; }
        /// <summary>
        /// Contain full data
        /// </summary>
        string SendDateTime { get; set; }
        string SenderName { get; set; }
        string MessageText { get; set; }
        /// <summary>
        /// Show does message read
        /// </summary>
        bool DoesRead { get; set; }
        /// <summary>
        /// Show 
        /// </summary>
        bool MyTurn { get; set; }

    }
}