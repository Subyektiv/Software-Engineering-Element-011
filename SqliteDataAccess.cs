using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BudgetBuddy
{
     public class SqliteDataAccess
    {
        //Connection string to database.
        private static string LoadConnectionString(string id = "Default")
        {
            return "Data Source =./database.db; Version = 3;";
        }

        //Loading all the data from income table
        public static List<IncomeModel> LoadIncome()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                
                var output = cnn.Query<IncomeModel>("Select * from Income", new DynamicParameters());
                return output.ToList();
            }
        }

        //Saving the data to the income table.
        public static void SaveIncome(IncomeModel income)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into Income (Category, Description, Amount, Date) values (@Category, @Description, @Amount, @Date)", income);
            }
        }


        //Loading all the data from Expense table
        public static List<ExpenseModel> LoadExpense()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ExpenseModel>("SELECT * FROM Expense", new DynamicParameters());
                return output.ToList();
            }
        }

        //Saving the data to the expense table.
        public static void SaveExpense(ExpenseModel expense)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Insert into Expense (Category, Description, Amount, Date) values (@Category, @Description, @Amount, @Date)", expense);
            }
        }

        //Delete Income based on ID
        public static void DeleteIncome(int incomeId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM Income WHERE ID = @incomeId", new { incomeId });
            }
        }

        //Search income by ID
        public static List<IncomeModel> SearchIncomeById(int incomeId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IncomeModel>("SELECT * FROM Income WHERE ID = @incomeId", new { incomeId });
                return output.ToList();
            }
        }

        //Search income by category
        public static List<IncomeModel> SearchIncomeByCategory(string category)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Use LIKE with % wildcard for partial matching
                string query = "SELECT * FROM Income WHERE Category LIKE @category";

                // Add % to both ends of the category for a partial match
                var output = cnn.Query<IncomeModel>(query, new { category = $"%{category}%" }).ToList();
                return output;
            }
        }

        //Delete expense based on ID
        public static void DeleteExpense(int expenseId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM Expense WHERE ID = @expenseId", new { expenseId });
            }
        }

        //Search expense by ID
        public static List<IncomeModel> SearchExpenseById(int expenseId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<IncomeModel>("SELECT * FROM Expense WHERE ID = @expenseId", new { expenseId });
                return output.ToList();
            }
        }

        //Search Expense by category
        public static List<IncomeModel> SearchExpenseByCategory(string category)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Use LIKE with % wildcard for partial matching
                string query = "SELECT * FROM Expense WHERE Category LIKE @category";

                // Add % to both ends of the category for a partial match
                var output = cnn.Query<IncomeModel>(query, new { category = $"%{category}%" }).ToList();
                return output;
            }
        }

        /* 
         * Querries for Analytics page Starts here
         * */

        // Calculate total income amount for the current month
        public static float CalculateTotalIncomeForCurrentMonth()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current month
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // Get the last day of the current month
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                // Query incomes within the current month
                var incomes = cnn.Query<IncomeModel>("SELECT * FROM Income WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfMonth.ToString("dd/MM/yyyy"), lastDay = lastDayOfMonth.ToString("dd/MM/yyyy") });

                // Calculate and return the total income amount
                return incomes.Sum(income => income.Amount);
            }
        }

        // Calculate total income amount for the current year
        public static float CalculateTotalIncomeForCurrentYear()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current year
                DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                // Get the last day of the current year
                DateTime lastDayOfYear = firstDayOfYear.AddYears(1).AddDays(-1);

                // Query incomes within the current year
                var incomes = cnn.Query<IncomeModel>("SELECT * FROM Income WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfYear.ToString("dd/MM/yyyy"), lastDay = lastDayOfYear.ToString("dd/MM/yyyy") });

                // Calculate and return the total income amount
                return incomes.Sum(income => income.Amount);
            }
        }

        //Calculate total Expense for current month
        public static float CalculateTotalExpenseForCurrentMonth()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current month
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // Get the last day of the current month
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                // Query expenses within the current month
                var expenses = cnn.Query<ExpenseModel>("SELECT * FROM Expense WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfMonth.ToString("dd/MM/yyyy"), lastDay = lastDayOfMonth.ToString("dd/MM/yyyy") });

                // Calculate and return the total expense amount
                return expenses.Sum(expense => expense.Amount);
            }
        }

        //calculate total expense for current year
        public static float CalculateTotalExpenseForCurrentYear()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current year
                DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                // Get the last day of the current year
                DateTime lastDayOfYear = firstDayOfYear.AddYears(1).AddDays(-1);

                // Query expenses within the current year
                var expenses = cnn.Query<ExpenseModel>("SELECT * FROM Expense WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfYear.ToString("dd/MM/yyyy"), lastDay = lastDayOfYear.ToString("dd/MM/yyyy") });

                // Calculate and return the total expense amount
                return expenses.Sum(expense => expense.Amount);
            }
        }

        //calculate the total entries made in current month for both income and expense table
        public static int CalculateTotalEntriesForCurrentMonth()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current month
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                // Get the last day of the current month
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                // Query both incomes and expenses within the current month
                var incomes = cnn.Query<IncomeModel>("SELECT * FROM Income WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfMonth.ToString("dd/MM/yyyy"), lastDay = lastDayOfMonth.ToString("dd/MM/yyyy") });

                var expenses = cnn.Query<ExpenseModel>("SELECT * FROM Expense WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfMonth.ToString("dd/MM/yyyy"), lastDay = lastDayOfMonth.ToString("dd/MM/yyyy") });

                // Calculate and return the total number of entries
                return incomes.Count() + expenses.Count();
            }
        }

        //calculate the total entries made in current year for both income and expense table
        public static int CalculateTotalEntriesForCurrentYear()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Get the first day of the current year
                DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1);

                // Get the last day of the current year
                DateTime lastDayOfYear = firstDayOfYear.AddYears(1).AddDays(-1);

                // Query both incomes and expenses within the current year
                var incomes = cnn.Query<IncomeModel>("SELECT * FROM Income WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfYear.ToString("dd/MM/yyyy"), lastDay = lastDayOfYear.ToString("dd/MM/yyyy") });

                var expenses = cnn.Query<ExpenseModel>("SELECT * FROM Expense WHERE Date BETWEEN @firstDay AND @lastDay",
                    new { firstDay = firstDayOfYear.ToString("dd/MM/yyyy"), lastDay = lastDayOfYear.ToString("dd/MM/yyyy") });

                // Calculate and return the total number of entries
                return incomes.Count() + expenses.Count();
            }
        }


        //check for password in database at the time of login
        public static bool CheckPassword(string enteredPassword)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Query the admin user's password from the database
                var adminPassword = cnn.ExecuteScalar<string>("SELECT Password FROM Users WHERE Username = 'admin'");

                // Check if the entered password matches the stored admin password
                return (adminPassword != null) && (adminPassword == enteredPassword);
            }
        }


        //Update or change the admin password
        public static void UpdatePassword(string newPassword)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                // Update the admin user's password in the database
                cnn.Execute("UPDATE Users SET Password = @newPassword WHERE Username = 'admin'", new { newPassword });
            }
        }


    }
}
