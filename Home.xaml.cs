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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            MainFrame.Content = new Analytics();
        }

        private void AnalyticsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Analytics();
        }

        private void IncomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Income();
        }

        private void ExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Expense();
        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Help();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Settings();
        }
    }
}
