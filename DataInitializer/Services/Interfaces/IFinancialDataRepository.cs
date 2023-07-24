using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services.Interfaces
{
    interface IFinancialDataRepository
    {
        Task AddFincialData(List<FinancialData> financialData);
    }
}
