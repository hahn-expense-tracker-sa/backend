using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerBackend.models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public ICollection<Expense> expenses { get; } = new List<Expense>();
    }
}