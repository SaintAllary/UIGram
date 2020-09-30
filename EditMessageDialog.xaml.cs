using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RuslanMessager
{
    /// <summary>
    /// Interaction logic for EditMessageDialog.xaml
    /// </summary>
    public partial class EditMessageDialog : Window
    {
        public bool DoesExecuted { get; set; }

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
