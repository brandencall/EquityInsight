using DataAccess.Context;
using DataAccess.Models;
using DataInitializer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services
{
    /// <summary>
    /// This class is a service  for the FinancialDataImporterService. It is responsible for accessing the database 
    /// finacial data via _context using Dependency Injection.
    /// </summary>
    public class FinancialDataRepository : IFinancialDataRepository
    {
        private readonly AppDbContext _context;

        public FinancialDataRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a fincial data object to the fincial data base table.
        /// May need to update so that it can update existing entries in the table
        /// </summary>
        /// <param name="fincialData">Fincial data object</param>
        /// <returns></returns>
        public async Task AddFincialData(List<FinancialData> fincialData)
        {
            foreach (var f in fincialData)
            {
                var existingData = _context.CompanyFinancialData
                    .FirstOrDefault(x => x.CompanyId == f.CompanyId
                                    && x.EndDate == f.EndDate);

                if (existingData == null)
                {
                    Console.WriteLine("Adding financial data! CompanyId: "
                        + f.CompanyId + " EndDate: " + f.EndDate);
                    _context.CompanyFinancialData.Add(f);
                }
                else
                {
                    Console.WriteLine("Financial Data already exists! CompanyId: " 
                        + f.CompanyId + " EndDate: " + f.EndDate);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 8115) //Error number for Arithmetic overflow error
                {
                    // Handle the arithmetic overflow error
                    Console.WriteLine("Arithmetic overflow occurred");

                    // Loop through all errors (a SQL command can produce multiple errors)
                    foreach (Microsoft.Data.SqlClient.SqlError err in ex.Errors)
                    {
                        Console.WriteLine($"Message: {err.Message} \nLineNumber: {err.LineNumber} \nSource: {err.Source} \nProcedure: {err.Procedure}");
                    }
                }
                else
                {
                    // Handle other SQL errors
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
