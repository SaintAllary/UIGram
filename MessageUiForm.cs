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
        TextBlock textBlock;
        Card msgCard;
        PackIcon packIcon;
        StackPanel panel;

        [Obsolete]
        public MessageUiForm(string _messageText, bool IsMyMsg = true) : base() {
            packIcon = new PackIcon();
            myTurn = IsMyMsg;
            msgCard = new Card();

            MessageText = string.Empty;
            MessageText = _messageText;

            MessageTextFormatted = new FormattedText(MessageText, CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Calibri"), 12, System.Windows.Media.Brushes.Black);

            textBlock = new TextBlock();
            textBlock.Text = MessageTextFormatted.Text;
            textBlock.TextWrapping = System.Windows.TextWrapping.Wrap;

            msgCard.Width = (MessageTextFormatted.Width + 10) < 350 ? MessageTextFormatted.Width + 10 : 350 + 10;
            msgCard.Background = System.Windows.Media.Brushes.CadetBlue;
            //msgCard.Foreground = System.Windows.Media.Brushes.Red;
            msgCard.Padding = new System.Windows.Thickness(4, 2, 0, 0);
            msgCard.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            msgCard.UniformCornerRadius = 3;

            msgCard.FontSize = 12;
            msgCard.FontFamily = new System.Windows.Media.FontFamily("Calibri");

            ChooseAlignment();

            msgCard.Content = textBlock;

            panel = new StackPanel();
            panel.Children.Add(msgCard);

            
            packIcon.Kind = PackIconKind.NetworkStrength4;
            packIcon.Width = 10;
            packIcon.Height = 10;
            packIcon.Margin = new System.Windows.Thickness(-4, -9, 0, 0);
            packIcon.Foreground = Brushes.White;           

            panel.Children.Add(packIcon);

            this.AddChild(panel);
            this.Background = Brushes.Transparent;           
        }

        private void ChooseAlignment() {
            if (MyTurn) {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                msgCard.Background = new SolidColorBrush(Color.FromRgb(239, 253, 222));
                packIcon.Foreground = new SolidColorBrush(Color.FromRgb(239, 253, 222));
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