using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using ExpenseTrackerBackend.models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseTrackerContext _context;

        public ExpensesController(ExpenseTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var expenses = await _context.Expenses.ToListAsync();

            // Calculate total expense for the current month
            var totalExpenses = expenses.Where(e => e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                                        .Sum(e => (decimal)e.Amount);

            // Get the budget for the current month
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Month.Month == DateTime.Now.Month && b.Month.Year == DateTime.Now.Year);

            if (budget != null)
            {
                var remainingBudget = budget.Amount - totalExpenses;
                return Ok(new { Expenses = expenses, TotalExpenses = totalExpenses, Budget = budget.Amount, RemainingBudget = remainingBudget });
            }
            
            return Ok(new { Expenses = expenses, TotalExpenses = totalExpenses, Budget = 0, RemainingBudget = 0 });
        }

        [HttpGet("getByCategory")]
public async Task<ActionResult> GetExpensesByCategory()
{
    // Group expenses by category and calculate the total amount for each category
    var groupedExpenses = await _context.Expenses
        .GroupBy(e => e.CategoryId) // Group by category ID
        .Select(g => new
        {
            // Fetch the category name once per group
            CategoryName = _context.Categories
                .Where(c => c.Id == g.Key)
                .Select(c => c.Name)
                .FirstOrDefault(),
            TotalAmount = g.Sum(e => e.Amount)  // Sum of expenses for the category
        })
        .ToListAsync();

    return Ok(groupedExpenses);
}


        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns validation errors
            }

            

            // Add the expense to the context and save changes
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}