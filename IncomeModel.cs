using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuddy
{
    public class IncomeModel
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string Date { get; set; }

        // Additional properties or methods can be added as needed
    }
}
