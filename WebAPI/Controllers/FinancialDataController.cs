using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Context;
using DataAccess.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinancialDataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FinancialData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialData>>> GetFinancialData()
        {
          if (_context.CompanyFinancialData == null)
          {
              return NotFound();
          }
            return await _context.CompanyFinancialData.ToListAsync();
        }

        // GET: api/FinancialData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialData>> GetFinancialData(int id)
        {
          if (_context.CompanyFinancialData == null)
          {
              return NotFound();
          }
            var financialData = await _context.CompanyFinancialData.FindAsync(id);

            if (financialData == null)
            {
                return NotFound();
            }

            return financialData;
        }

        // PUT: api/FinancialData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancialData(int id, FinancialData financialData)
        {
            if (id != financialData.Id)
            {
                return BadRequest();
            }

            _context.Entry(financialData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FinancialData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinancialData>> PostFinancialData(FinancialData financialData)
        {
          if (_context.CompanyFinancialData == null)
          {
              return Problem("Entity set 'WebAPIContext.FinancialData'  is null.");
          }
            _context.CompanyFinancialData.Add(financialData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancialData", new { id = financialData.Id }, financialData);
        }

        // DELETE: api/FinancialData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialData(int id)
        {
            if (_context.CompanyFinancialData == null)
            {
                return NotFound();
            }
            var financialData = await _context.CompanyFinancialData.FindAsync(id);
            if (financialData == null)
            {
                return NotFound();
            }

            _context.CompanyFinancialData.Remove(financialData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialDataExists(int id)
        {
            return (_context.CompanyFinancialData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
