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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetBuddy
{
    /// <summary>
    /// Interaction logic for Income.xaml
    /// </summary>
    public partial class Income : Page
    {
        public string[] categories { get; set; }
        public Income()
        {
            InitializeComponent();

            categories = new string[] {"Salary", "Business Income", "Freelance", "Rental", "Dividends", "Royalties", "Gift/Inheritence", "Alimony", "Pension", "Social Security", "Other" };

            DataContext = this;

            // incomeListView.ItemsSource = SqliteDataAccess.LoadIncome();

            Task.Run(() =>
            {
                // Load the income data on a separate thread
                var incomeData = SqliteDataAccess.LoadIncome();

                // Update the UI on the main (dispatcher) thread
                Application.Current.Dispatcher.Invoke(() =>
                {
                    incomeListView.ItemsSource = incomeData;
                });
            });

        }

        private void Amount_KeyDown(object sender, KeyEventArgs e)
        {
            // Get the Key enum value
            Key key = e.Key;

            // Check if the key represents a digit and is not Backspace (8) or Delete (46)
            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9) || key == Key.Back || key == Key.Delete)
            {
                e.Handled = false; // Allow the key press
            }
            else
            {
                e.Handled = true; // Block the key press
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            IncomeModel i = new IncomeModel();

            i.Category = this.categorySelected.Text.ToString();
            i.Description = this.DescriptionTxt.Text.ToString();

            // Convert the AmountTxt TextBox.Text to float
            if (float.TryParse(this.AmountTxt.Text, out float amount))
            {
                i.Amount = amount;
            }
            else
            {
                // Handle the case where the conversion fails (e.g., display an error message)
                MessageBox.Show("Invalid amount. Please enter a valid number.");
                return; // Stop further processing
            }

            // Convert the DatePicker date to the desired format (yyyy-MM-dd)
            if (DateTime.TryParse(this.datePicker.Text, out DateTime selectedDate))
            {
                i.Date = selectedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                // Handle the case where the date conversion fails
                MessageBox.Show("Invalid date. Please select a valid date.");
                return; // Stop further processing
            }

            try
            {
                SqliteDataAccess.SaveIncome(i);
                MessageBox.Show("Data submitted successfully.", "Success");
                incomeListView.ItemsSource = SqliteDataAccess.LoadIncome();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


        private void ClearFields()
        {
            // Clear fields in the entry section
            AmountTxt.Text = "";
            DescriptionTxt.Text = "";
            datePicker.SelectedDate = DateTime.Now; // Set it to the current date or another default value
            
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void clearn2Btn_Click(object sender, RoutedEventArgs e)
        {
            // Clear fields in the delete section
            inputID.Text = "";
        }

        private void inputID_KeyDown(object sender, KeyEventArgs e)
        {
            // Get the Key enum value
            Key key = e.Key;

            // Check if the key represents a digit and is not Backspace (8) or Delete (46)
            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9) || key == Key.Back || key == Key.Delete)
            {
                e.Handled = false; // Allow the key press
            }
            else
            {
                e.Handled = true; // Block the key press
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int incomeIdToDelete;

            if(int.TryParse(inputID.Text, out incomeIdToDelete))
            {
                try
                {
                    SqliteDataAccess.DeleteIncome(incomeIdToDelete);
                    MessageBox.Show($"Income with ID {incomeIdToDelete} has been deleted successfully.", "Success");
                    incomeListView.ItemsSource = SqliteDataAccess.LoadIncome();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID", "Failed");
            }
        }

        private void searchByID_KeyDown(object sender, KeyEventArgs e)
        {
            // Get the Key enum value
            Key key = e.Key;

            // Check if the key represents a digit and is not Backspace (8) or Delete (46)
            if ((key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9) || key == Key.Back || key == Key.Delete)
            {
                e.Handled = false; // Allow the key press
            }
            else
            {
                e.Handled = true; // Block the key press
            }
        }

        private void searchByID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(searchByID.Text, out int searchId))
            {
                incomeListView.ItemsSource = SqliteDataAccess.SearchIncomeById(searchId);
            }
            else if (string.IsNullOrWhiteSpace(searchByID.Text))
            {
                // If the search box is empty, reload all data
                incomeListView.ItemsSource = SqliteDataAccess.LoadIncome();
            }
            else
            {
                MessageBox.Show("Invalid ID", "Error");
            }
        }

        private void searchByCategory_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchCategory = searchByCategory.Text;

            if (!string.IsNullOrWhiteSpace(searchCategory))
            {
                incomeListView.ItemsSource = SqliteDataAccess.SearchIncomeByCategory(searchCategory);
            }
            else
            {
                // If the search box is empty, reload all data
                incomeListView.ItemsSource = SqliteDataAccess.LoadIncome();
            }
        }
    }
}
