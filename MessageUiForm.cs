using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace RuslanMessager
{
    public class MessageUiForm : ListBoxItem, IMessage
    {
        FormattedText MessageTextFormatted;
        TextBlock textBlock;

        [Obsolete]
        public MessageUiForm() : base() {
            Card msgCard = new Card();

            MessageText = "SOME TEXT new line aaa AAAAAAABAaaaaaaaaaaaaaaaaaaBBBBBBBBBBBBBBBBaaaaaaaaaaaaaaaaaaaaaaaA\n\n\n\n\n\n\naaa";
            MessageTextFormatted = new FormattedText(MessageText, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Calibri"), 12, System.Windows.Media.Brushes.Black);

            textBlock = new TextBlock();
            textBlock.Text = MessageTextFormatted.Text;
            textBlock.TextWrapping = System.Windows.TextWrapping.Wrap;

            msgCard.Width= (MessageTextFormatted.Width + 10) < 350 ? MessageTextFormatted.Width + 10 : 350 + 10;
            msgCard.Background = System.Windows.Media.Brushes.CadetBlue;
            //msgCard.Foreground = System.Windows.Media.Brushes.Red;
            msgCard.Padding = new System.Windows.Thickness(4, 2, 0, 0);
            msgCard.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            msgCard.UniformCornerRadius = 6;

            msgCard.FontSize = 12;
            msgCard.FontFamily = new System.Windows.Media.FontFamily("Calibri");


            msgCard.Content = textBlock;

            ChooseAlignment();

            this.AddChild(msgCard);
        }

        private void ChooseAlignment() {
            if (MyTurn) {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            }
        }

        public string MessageContentUrl { get; set; }
        public string SendDateTime { get; set; }
        public string SenderName { get; set; }
        public string MessageText { get; set; }
        public bool DoesRead { get; set; }
        public bool MyTurn { get; set; }
    }
}
