using System;

namespace RuslanMessager
{
    [Serializable]
    public class Message : IMessage
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

        public Message(IMessage message) {
            MessageContentUrl = message.MessageContentUrl;
            MessageText = message.MessageText;
            MyTurn = message.MyTurn;
            SenderName = message.SenderName;
            SendDateTime = message.SendDateTime;
            DoesRead = message.DoesRead;
        }

        public Message() {
        }
    }
}