using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerBackend.models
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Month { get; set; }
    }
}