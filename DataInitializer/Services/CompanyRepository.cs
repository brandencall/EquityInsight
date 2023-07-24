using DataAccess.Context;
using DataAccess.Models;
using DataInitializer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services
{
    /// <summary>
    /// This class is a service  for the CompanyDataImporterService. It is responsible for accessing the database 
    /// company data via _context using Dependency Injection.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a company to the Company table
        /// </summary>
        /// <param name="company">A company object</param>
        /// <returns></returns>
        public async Task AddCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks whether a company exists in the database based on the CIK
        /// </summary>
        /// <param name="cik">The companies CIK</param>
        /// <returns>Returns true if the company is already in the database. Returns false if it is not</returns>
        public async Task<bool> CompanyExists(string cik)
        {
            return await _context.Companies.AnyAsync(c => c.CIK == cik);
        }
        /// <summary>
        /// Gets the full list of companies in the Company database table
        /// </summary>
        /// <returns>List of Company objects</returns>
        public async Task<List<Company>> GetListOfCompanies()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
