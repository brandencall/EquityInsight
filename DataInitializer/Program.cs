
using DataInitializer.Services;
using DataAccess.Models;
using DataInitializer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using DataInitializer;

class Program
{
    static async Task Main(string[] args)
    {
        // Create host builder
        var host = CreateHostBuilder(args).Build();

        // Create a new scope
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                /*var importer = services.GetRequiredService<CompanyDataImporterService>();
                importer.ImportCompanyData().Wait();*/
                var companyImporter = services.GetRequiredService<CompanyDataImporterService>();
                var financialImporter = services.GetRequiredService<FinancialDataImporterService>();

                List<Company> companies = await companyImporter.GetListOfCompanies();
                foreach (var company in companies)
                {
                    await financialImporter.ImportFinancialData(company);
                }
            }
            catch (Exception ex)
            {
                // Logging or handling the exception
                throw;
            }
        }

        // Run the web application.
        host.Run();

    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var startup = new Startup(hostContext.Configuration);
                startup.ConfigureServices(services);
            });
}