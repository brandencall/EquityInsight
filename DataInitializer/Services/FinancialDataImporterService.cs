using DataInitializer.Services.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services
{
    class FinancialDataImporterService
    {
        //Uses dependency injection for the fincial data repository (helper class) and for the edgar api calls.
        private readonly IFinancialDataRepository _financialDataRepository;
        private readonly IEdgarApiService _edgarApiService;

        public FinancialDataImporterService(IFinancialDataRepository financialDataRepository, IEdgarApiService edgarApiService)
        {
            _financialDataRepository = financialDataRepository;
            _edgarApiService = edgarApiService;
        }   
        /// <summary>
        /// Imports the fincial data given a Company object.
        /// </summary>
        /// <param name="company">Company object</param>
        /// <returns></returns>
        public async Task ImportFinancialData(Company company)
        {
            List<FinancialData> finacialData = await _edgarApiService.GetFinancialDataAsync(company);
            await _financialDataRepository.AddFincialData(finacialData);

        }
    }
}
