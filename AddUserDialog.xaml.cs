using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;

namespace RuslanMessager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public bool DoexExecuted { get; set; }
        public string CurrentPathToPict { get; set; }

        public AddUserDialog() {
            InitializeComponent();
        }

        private void CreateNewUser(object sender, RoutedEventArgs e) {
            if (Regex.IsMatch(NumberTextBox.Text, "^[0-9]{0,3}[0-9]{3,10}$") && Regex.IsMatch(NameTextBox.Text, @"^[^.]{0,25}$")) {
                DoexExecuted = true;
                Close();
            }
            else {
                MessageBox.Show("Check your number, it must be like : 380952425161 and name less than 25 symbols", "Invalid value", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindPicture(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
                CurrentPathToPict = openFileDialog.FileName;
        }
    }
}