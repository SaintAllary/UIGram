﻿using System;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public bool DoexExecuted { get; set; }
        public AddUserDialog()
        {
            InitializeComponent();
        }

        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
    
            if (Regex.IsMatch(NumberTextBox.Text, "[0-9]{0,3}[0-9]{6,10}$") && Regex.IsMatch(NameTextBox.Text, @"^[^.]{0,10}$"))
            {
                DoexExecuted = true;
                Close();
            }
            else
            {
            
                MessageBox.Show("Check your number, it must be like : 380952425161 and name less than 10 symbols","Invalid value",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
