using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services.Interfaces
{
    interface ICompanyRepository
    {
        Task AddCompany(Company company);
        Task<bool> CompanyExists(string cik);
        Task<List<Company>> GetListOfCompanies();
    }
}
