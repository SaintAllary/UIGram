using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChangeUserDataDialog.xaml
    /// </summary>
    public partial class ChangeUserDataDialog : Window
    {
        private string currentDate;

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value;
                NameTextBox.Text = value;    
            }
        }
        private string userPhone;

        public string UserPhone
        {
            get { return userPhone; }
            set { userPhone = value;
                NumberTextBox.Text = value;
            }
        }





        public ChangeUserDataDialog()
        {
            InitializeComponent();
        }

        public string CurrentPathToPict { get; set; }
        public bool DoexExecuted { get; set; }

        private void FindPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
                CurrentPathToPict = openFileDialog.FileName;
        }

        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(NumberTextBox.Text, "^[0-9]{0,3}[0-9]{3,10}$") && Regex.IsMatch(NameTextBox.Text, @"^[^.]{0,25}$"))
            {
                DoexExecuted = true;
                Close();
            }
            else
            {
                MessageBox.Show("Check your number, it must be like : 380952425161 and name less than 25 symbols", "Invalid value", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetNewTime(object sender, RoutedEventArgs e)
        {
                //DataPicker
        }
    }
}
