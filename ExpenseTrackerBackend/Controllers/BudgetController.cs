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

            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
            return Ok(budget);
        }

    }

}