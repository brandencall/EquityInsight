using DataAccess.Models;
using DataInitializer.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInitializer.Services
{
    /// <summary>
    /// Service responsible for make calls to EDGAR API.
    /// </summary>
    class EdgarApiService : IEdgarApiService
    {
        //HttpClient that calls the EDGAR API
        private readonly HttpClient _client;
        //Global variable for keeping track of calls make to the EDGAR API
        private int _requestCounter = 0;
        //Vairable responsible of keep track of how much time has passed to reset the requestCounter
        private DateTime _nextResetTime;
        //List of finacial properties to get for a company
        private static List<string> properties = new List<string>
        {
            "Assets",
            "Cash",
            "Revenues",
            "NetIncomeLoss",
            "LiabilitiesAndStockholdersEquity",
            "StockholdersEquity",
            "GrossProfit",
            "OperatingIncomeLoss",
            "EarningsPerShareBasic",
            "EarningsPerShareDiluted",
            "SalesRevenueNet",
            "CashAndCashEquivalentsAtCarryingValue",
            "NoncurrentAssets",
            "ShortTermInvestments",
            "Liabilities",
            "LongTermDebt",
            "LongTermDebtNoncurrent",
        };
        
        public EdgarApiService() 
        {
            //EDGAR api requires to add headers to the request.
            _client = new HttpClient();
            var headers = new
            {
                UserAgent = "BrandenCall (mailto:brandencall@live.com)",
                Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                Referer = "https://www.sec.gov/edgar/searchedgar/companysearch.html"
            };
            _client.DefaultRequestHeaders.Add("User-Agent", headers.UserAgent);
            _client.DefaultRequestHeaders.Add("Accept", headers.Accept);
            _client.DefaultRequestHeaders.Add("Referer", headers.Referer);

            _nextResetTime = DateTime.Now.AddSeconds(1);

        }

        /// <summary>
        /// Gets a list of companies from the EDGAR API
        /// </summary>
        /// <returns>List of Company objects</returns>
        public async Task<List<Company>> GetCompaniesAsync()
        {
            var url = "https://www.sec.gov/files/company_tickers.json";
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var companyDict = JsonConvert.DeserializeObject<Dictionary<string, Company>>(content);

            return companyDict.Values.ToList();
        }

        /// <summary>
        /// Gets the financial data for a company
        /// </summary>
        /// <param name="company">Company object</param>
        /// <returns>List of FinancialData objects for a company</returns>
        public async Task<List<FinancialData>> GetFinancialDataAsync(Company company)
        {
            EnsureRateLimit();
            string CIK = company.CIK.PadLeft(10, '0');
            Console.WriteLine(CIK);
            Console.WriteLine(company.Id);
            var url = "https://data.sec.gov/api/xbrl/companyfacts/CIK" + CIK + ".json";
            var response = await _client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Requested resource not found.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    Console.WriteLine("Internal Server Error. Try again later.");
                }
                return new List<FinancialData>() { };
            }
            
            var content = await response.Content.ReadAsStringAsync();
            

            return ParseJson(content, company.Id);
        }
        /// <summary>
        /// Ensures that the rate limit for the EDGAR API is not reached by using 
        /// 'leaky bucket' algorithm. The EDGAR API only lets you call 10 times per second.
        /// </summary>
        private void EnsureRateLimit()
        {
            if (_requestCounter >= 10)
            {
                var delayTime = _nextResetTime - DateTime.Now;

                if (delayTime > TimeSpan.Zero)
                {
                    Task.Delay(delayTime).Wait(); // Wait for the remaining time
                }

                // Reset the counter and the next reset time
                _requestCounter = 0;
                _nextResetTime = DateTime.Now.AddSeconds(1);
            }
        }

        /// <summary>
        /// Helper method to Parse the finacial json given by the EDGAR API.
        /// It will group the Financial Data properties by there 'endDate'
        /// </summary>
        /// <param name="content">The Json returned from the EDGAR API</param>
        /// <param name="companyId">The ID of the Company object that you are getting the finacial data for </param>
        /// <returns>List of Financial Data</returns>
        private static List<FinancialData> ParseJson(string content, int companyId)
        {
            JObject data = JObject.Parse(content);
            if (!data.Properties().Any())
            {
                Console.WriteLine("JObject is empty");
                return new List<FinancialData>(); ;
            }

            JObject facts = new JObject();

            if((JObject)data["facts"]["us-gaap"] != null)
            {
                facts = (JObject)data["facts"]["us-gaap"];
            }
            else
            {
                Console.WriteLine("Doesn't have 'us-gaap'! Skipping for now!");
                return new List<FinancialData>();
            }

            var groupedData = new Dictionary<string, FinancialData>();

            foreach (var property in properties)
            {
                var category = facts.Property(property);

                if (category != null)
                {
                    var units = (JObject)category.Value["units"];
                    foreach (var unit in units.Properties())
                    {
                        var entries = unit.Value;

                        foreach (JObject entry in entries)
                        {
                            if (entry["end"] == null)
                            {
                                continue;
                            }

                            string? endDate = entry["end"].ToString();
                            string key = endDate.Substring(0, 7);

                            if (!groupedData.ContainsKey(key))
                            {
                                groupedData[key] = new FinancialData
                                {
                                    CompanyId = companyId,
                                    EndDate = entry["end"].ToString(),
                                };
                            }

                            setProperty(property, groupedData[key], (decimal)entry["val"]);
                        }
                    }
                }
            }

            return groupedData.Values.ToList();

        }

        /// <summary>
        /// Dynamic Property Assignment for the given Financial Data with the given value
        /// </summary>
        /// <param name="property">Financial Data property</param>
        /// <param name="financialData">Finacial Data object</param>
        /// <param name="value">Value for the property</param>
        private static void setProperty(string property, FinancialData financialData, decimal value)
        {
            var propInfo = financialData.GetType().GetProperty(property);
            if (propInfo != null && propInfo.CanWrite)
            {
                propInfo.SetValue(financialData, value, null);
            }
        }
    }
}
