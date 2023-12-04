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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetBuddy
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            if (oldPassTxt != null && newPassTxt != null &&
                !string.IsNullOrWhiteSpace(oldPassTxt.Text) && !string.IsNullOrWhiteSpace(newPassTxt.Text))
            {
                if (SqliteDataAccess.CheckPassword(oldPassTxt.Text))
                {
                    SqliteDataAccess.UpdatePassword(newPassTxt.Text);
                    MessageBox.Show("Password changed successfully!", "Success");
                }
                else
                {
                    MessageBox.Show("Invalid Old Password. Please input a correct password!", "Error");
                }
            }
            else
            {
                MessageBox.Show("Invalid Old or New Password. Please input a valid or correct password!", "Error");
            }
        }
    }
}
