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

namespace BudgetBuddy
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the entered username and password
            string enteredUsername = userName.Text;
            string enteredPassword = passTxt.Password;

            // Check if the entered credentials are valid
            if (enteredUsername == "admin" && SqliteDataAccess.CheckPassword(enteredPassword))
            {
                // Credentials are valid, open the new home window
                Home homeWindow = new Home();
                homeWindow.Show();

                // Close the current login window
                Close();
            }
            else
            {
                // Show an error message or handle invalid credentials
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
