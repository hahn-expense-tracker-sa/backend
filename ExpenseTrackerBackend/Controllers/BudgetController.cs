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
    public class BudgetController : ControllerBase
    {
        private readonly ExpenseTrackerContext _context;

        public BudgetController(ExpenseTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Budget>> GetBudget()
        {
            return await _context.Budgets.FirstOrDefaultAsync();
        }

       [HttpPost]
        public async Task<ActionResult<Budget>> SetBudget(Budget budget)
        {
            if (budget == null)
            {
                return BadRequest("Budget cannot be null.");
            }

            // Fetch the list of budgets and find the existing budget for the current month and year
            var existingBudget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Month.Month == budget.Month.Month && b.Month.Year == budget.Month.Year);

            // If the budget for the current month already exists, update it
            if (existingBudget != null)
            {
                existingBudget.Amount = budget.Amount; 
               
                _context.Budgets.Update(existingBudget);  
            }
            else
            {
               
                _context.Budgets.Add(budget); 
            }

            await _context.SaveChangesAsync();  
            return Ok(existingBudget ?? budget);  
        }



            }

}