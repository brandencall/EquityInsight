using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services.Interfaces
{
    interface IEdgarApiService
    {
        Task<List<Company>> GetCompaniesAsync();
        // TODO: Implement the financial Data in the edgar interface
        Task<List<FinancialData>> GetFinancialDataAsync(Company company);
    }
}
