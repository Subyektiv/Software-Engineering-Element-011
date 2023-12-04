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
    /// Interaction logic for Analytics.xaml
    /// </summary>
    public partial class Analytics : Page
    {
        public Analytics()
        {
            InitializeComponent();

            float totalIncomeThisMonth;
            float totalIncomeThisYear;
            float totalExpenseThisMonth;
            float totalExpenseThisYear;
            float totalEntriesThisMonth;
            float totalEntriesThisYear;

            // Run a separate thread asynchronously
            Task.Run(() =>
            {
                totalIncomeThisMonth = SqliteDataAccess.CalculateTotalIncomeForCurrentMonth();

                // Use Dispatcher to update UI control from the main UI thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalIncomeAmount.Text = totalIncomeThisMonth.ToString();
                });

                totalIncomeThisYear = SqliteDataAccess.CalculateTotalIncomeForCurrentYear();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalIncomeAmountThisYear.Text = totalIncomeThisYear.ToString();
                });

                totalExpenseThisMonth = SqliteDataAccess.CalculateTotalExpenseForCurrentMonth();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalExpenseAmount.Text = totalExpenseThisMonth.ToString();
                });

                totalExpenseThisYear = SqliteDataAccess.CalculateTotalExpenseForCurrentYear();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalExpenseAmountThisYear.Text = totalExpenseThisYear.ToString();
                });

                totalEntriesThisMonth = SqliteDataAccess.CalculateTotalEntriesForCurrentMonth();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalEntriesMade.Text = totalEntriesThisMonth.ToString();
                });

                totalEntriesThisYear = SqliteDataAccess.CalculateTotalEntriesForCurrentYear();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    totalEntriesMadeThisYear.Text = totalEntriesThisYear.ToString();
                });

            });

        }
    }
}
