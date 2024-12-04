using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerBackend.models
{
   public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Expense name is required")]
        public string Name { get; set; }= string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public double Amount { get; set; }

        public int CategoryId { get; set; }  // Foreign key to the Category table

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
    }

}