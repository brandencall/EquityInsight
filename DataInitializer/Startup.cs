using Microsoft.EntityFrameworkCore;
using DataAccess.Context;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataInitializer.Services.Interfaces;
using DataInitializer.Services;

namespace DataInitializer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of the different services used such as API calls and database insertions
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WebAPIContext")));
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IEdgarApiService, EdgarApiService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<CompanyDataImporterService>();
            services.AddScoped<IFinancialDataRepository, FinancialDataRepository>();
            services.AddScoped<FinancialDataImporterService>();

            // Other service configurations...
        }
    }
}
