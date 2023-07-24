using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DataAccess.Models
{
    public class Company
    {
#pragma warning disable CS8618
        [Key]
        [Required]
        public int Id { get; set; }
        private string _cik;
        [Required]
        [JsonProperty("cik_str")]
        [StringLength(10)]
        public string CIK { get; set; }
        

        [Required]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        [Required]
        [JsonProperty("title")]
        public string Name { get; set; }
        public DateTime NextEarnings { get; set; }

        // Navigation property
        public ICollection<FinancialData>? FinancialDatas { get; set; } = new List<FinancialData>();
#pragma warning restore CS8618
        /// <summary>
        /// Sort the companies financial data by 'endData' in descending order.
        /// </summary>
        public void SortFinancialDataByDate()
        {
            if (FinancialDatas != null)
            {
                FinancialDatas = FinancialDatas.OrderByDescending(data => DateTime.Parse(data.EndDate)).ToList();
            }
           
        }
    }

    
}
