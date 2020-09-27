using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RuslanMessager
{
    public class MessageUiForm : ListBoxItem, IMessage
    {
        FormattedText MessageTextFormatted;
        TextBlock MessageText_TextBlock;
        TextBlock TimeTextBlock;
        Card msgCard;
        PackIcon packIcon;
        StackPanel panel;
        private int DateTimeBlockWidth = 36;
        private int DateTimeBlockMarginTop = -20;
        private int DateTimeBlockMarginRight = 6;
        private int DateTimeCardPaddingBottom = 8;
        private int DateTimeCardPaddingRight = 36;

        [Obsolete]
        public MessageUiForm(string _messageText, string _SendDateTime, string _SenderName, bool IsMyMsg = true, bool _isRead = false) : base() {
            packIcon = new PackIcon();
            packIcon.Foreground = Brushes.White;
            myTurn = IsMyMsg;
            msgCard = new Card();
            panel = new StackPanel();



            MessageText = string.Empty;
            MessageText = _messageText;
            SendDateTime = _SendDateTime;
            SenderName = _SenderName;
            DoesRead = _isRead;

            MessageTextFormatted = new FormattedText(MessageText, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Calibri"), 12, System.Windows.Media.Brushes.Black);

            MessageText_TextBlock = new TextBlock();
            MessageText_TextBlock.Text = MessageTextFormatted.Text;
            MessageText_TextBlock.TextWrapping = System.Windows.TextWrapping.Wrap;
            MessageText_TextBlock.FontFamily = new FontFamily("Calibri");
            //MessageText_TextBlock.Width = MessageTextFormatted.Width + 10; //BUG

            msgCard.Width = (MessageTextFormatted.Width + 10) < 350 ? MessageTextFormatted.Width + 10 + DateTimeBlockWidth : 350 + 10 + DateTimeBlockWidth;
            msgCard.Background = System.Windows.Media.Brushes.CadetBlue;
            //msgCard.Foreground = System.Windows.Media.Brushes.Red;
            msgCard.Padding = new System.Windows.Thickness(5, 5, DateTimeCardPaddingRight, /*3*/DateTimeCardPaddingBottom);
            msgCard.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            msgCard.UniformCornerRadius = 3;

            msgCard.FontSize = 12;
            msgCard.FontFamily = new System.Windows.Media.FontFamily("Calibri");

            ChooseAlignment();

            msgCard.Content = MessageText_TextBlock;

            panel.Children.Add(msgCard);

            packIcon.Kind = PackIconKind.NetworkStrength4;
            packIcon.Width = 9;
            packIcon.Height = 9;
            packIcon.Margin = new System.Windows.Thickness(-4, -8, 0, 0);

            panel.Children.Add(packIcon);

            TimeTextBlock = new TextBlock();
            TimeTextBlock.Text = DateTime.Parse(SendDateTime).ToShortTimeString();
            TimeTextBlock.TextWrapping = System.Windows.TextWrapping.Wrap;
            TimeTextBlock.Margin = new System.Windows.Thickness(0, DateTimeBlockMarginTop, DateTimeBlockMarginRight, 0);
            TimeTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
            TimeTextBlock.FontFamily = new FontFamily("Calibri");

            panel.Children.Add(TimeTextBlock);

            //MessageBox.Show($"{MessageText_TextBlock.ActualWidth}");

            this.AddChild(panel);
            this.Background = Brushes.Transparent;

            if ((MessageTextFormatted.Width + 10) > 350)
                ChangeDateTimeForLongMsg();
        }

        private void ChangeDateTimeForLongMsg() {
            DateTimeBlockWidth = 6;
            DateTimeBlockMarginTop = -20;
            DateTimeBlockMarginRight = 6;
            DateTimeCardPaddingBottom = 20;
            DateTimeCardPaddingRight = 6;

            msgCard.Padding = new System.Windows.Thickness(5, 5, DateTimeCardPaddingRight, /*3*/DateTimeCardPaddingBottom);
            msgCard.Width = (MessageTextFormatted.Width + 10) < 350 ? MessageTextFormatted.Width + 10 + DateTimeBlockWidth : 350 + 10 + DateTimeBlockWidth;
            TimeTextBlock.Margin = new System.Windows.Thickness(0, DateTimeBlockMarginTop, DateTimeBlockMarginRight, 0);
        }

        private void ChooseAlignment() {
            if (MyTurn) {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                msgCard.Background = new SolidColorBrush(Color.FromRgb(239, 253, 222));
                //packIcon.Foreground = new SolidColorBrush(Color.FromRgb(239, 253, 222));
                packIcon.Foreground = Brushes.Transparent;
                //packIcon.Kind = PackIconKind.None;
                //(packIcon.RenderTransform).((ScaleTransform)packIcon.RenderTransform).ScaleX = -1;
            }
            else
                msgCard.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        public string MessageContentUrl { get; set; }
        public string SendDateTime { get; set; }
        public string SenderName { get; set; }
        public string MessageText { get; set; }
        public bool DoesRead { get; set; }

        private bool myTurn;
        public bool MyTurn {
            get { return myTurn; }
            set {
                myTurn = value;
                ChooseAlignment();
            }
        }

    }
}