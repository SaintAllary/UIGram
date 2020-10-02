using System.Windows;
using System.Windows.Input;

namespace RuslanMessager
{
    /// <summary>
    /// Interaction logic for EditMessageDialog.xaml
    /// </summary>
    public partial class EditMessageDialog : Window
    {
        public bool DoesExecuted { get; set; }

        public string MessageContentUrl { get; set; }

        public EditMessageDialog() {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            if (this.MsgDatePicker.SelectedDate.HasValue && this.MsgTimePicker.SelectedTime.HasValue && this.MsgTextBox.Text.Trim().Length > 0) {
                this.DoesExecuted = true;
                Close();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            Close();
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}