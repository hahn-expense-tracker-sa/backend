using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ExpenseTrackerBackend.models
{
   using Microsoft.EntityFrameworkCore;

    public class ExpenseTrackerContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options)
            : base(options) { }
    }
}