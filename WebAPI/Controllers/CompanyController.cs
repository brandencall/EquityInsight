using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
         
            var company = await _context.Companies.Include(c => c.FinancialDatas).FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }
        // GET api/Company/ticker/AAPL
        [HttpGet("ticker/{ticker}")]
        public async Task<ActionResult<Company>> GetCompanyByTicker(string ticker)
        {

            var company = await _context.Companies.Include(c => c.FinancialDatas).FirstOrDefaultAsync(c => c.Ticker == ticker);
            

            if (company == null)
            {
                return NotFound();
            }

            company.SortFinancialDataByDate();

            return company;
        }
        // GET api/Company/Name/Apple
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Company>> GetCompanyByName(string name)
        {

            var company = await _context.Companies.Include(c => c.FinancialDatas).FirstOrDefaultAsync(c => c.Name == name);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }
        // GET api/Company/CIK/123456789
        [HttpGet("cik/{cik}")]
        public async Task<ActionResult<Company>> GetCompanyByCIK(string cik)
        {

            var company = await _context.Companies.Include(c => c.FinancialDatas).FirstOrDefaultAsync(c => c.CIK == cik);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Company/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
          if (_context.Companies == null)
          {
              return Problem("Entity set 'WebAPIContext.Company'  is null.");
          }
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
