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
    /// The service responsible for populating the database table with company data
    /// </summary>
    class CompanyDataImporterService
    {
        //Uses dependency injection for the company repository (helper class) and for the edgar api calls.
        private readonly ICompanyRepository _companyRepository;
        private readonly IEdgarApiService _edgarApiService;
        public CompanyDataImporterService(ICompanyRepository companyRepository, IEdgarApiService edgarApiService) 
        {
            _companyRepository = companyRepository;
            _edgarApiService = edgarApiService;
        }
        /// <summary>
        /// Imports the company data into the database that comes from the EDGAR API.
        /// Validates if the company already exists in the database
        /// </summary>
        /// <returns></returns>
        public async Task ImportCompanyData()
        {
            
            var companies = await _edgarApiService.GetCompaniesAsync();
            Console.WriteLine(companies.Count);

            foreach (var company in companies)
            {
                
                if(!await _companyRepository.CompanyExists(company.CIK))
                {
                    Console.WriteLine(company.Name);
                    await _companyRepository.AddCompany(company);
                }
                else 
                {
                    Console.WriteLine(company.Name + " already exists");
                }
                
            }
        }

        /// <summary>
        /// Gets the list of companies that exists in the database
        /// </summary>
        /// <returns>Returns the list of companies</returns>
        public async Task<List<Company>> GetListOfCompanies()
        {
            return await _companyRepository.GetListOfCompanies();
        }
    }
}
