using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class FinancialData
    {
        [Key]
        [Required]
        public int Id { get; set; }
        // Foreign key for Company
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [JsonProperty("end")]
        public string EndDate { get; set; }
        [JsonProperty("Assets")]
        public decimal Assets { get; set; }
        [JsonProperty("Cash")]
        public decimal Cash { get; set; }
        [JsonProperty("Revenues")]
        public decimal Revenues { get; set; }
        [JsonProperty("NetIncomeLoss")]
        public decimal NetIncomeLoss { get; set; }
        [JsonProperty("LiabilitiesAndStockholdersEquity")]
        public decimal LiabilitiesAndStockholdersEquity { get; set; }
        [JsonProperty("StockholdersEquity")]
        public decimal StockHoldersEquity { get; set; }
        [JsonProperty("GrossProfit")]
        public decimal GrossProfit { get; set; }
        [JsonProperty("OperatingIncomeLoss")]
        public decimal OperatingIncomeLoss { get; set; }
        [JsonProperty("EarningsPerShareBasic")]
        public decimal EarningsPerShareBasic { get; set; }
        [JsonProperty("EarningsPerShareDiluted")]
        public decimal EarningsPerShareDiluted { get; set; }
        [JsonProperty("SalesRevenueNet")]
        public decimal SalesRevenueNet { get; set; }
        [JsonProperty("CashAndCashEquivalentsAtCarryingValue")]
        public decimal CashAndCashEquivalentsAtCarryingValue { get; set; }
        [JsonProperty("NoncurrentAssets")]
        public decimal NoncurrentAssets { get; set; }

        [JsonProperty("ShortTermInvestments")]
        public decimal ShortTermInvestments { get; set; }
        [JsonProperty("Liabilities")]
        public decimal Liabilities { get; set; }
        [JsonProperty("LongTermDebt")]
        public decimal LongTermDebt { get; set; }
        [JsonProperty("LongTermDebtNoncurrent")]
        public decimal LongTermDebtNoncurrent { get; set; }


        /// <summary>
        /// Override the ToString() method to be able to show all of the financial properties
        /// </summary>
        /// <returns>Returns a formatted string of the Financial properties</returns>
        public override string ToString()
        {


            return 
                   "Assest: " + Assets.ToString() + "\n" +
                   "Cash: " + Cash.ToString() + "\n" +
                   "Revenues: " + Revenues.ToString() + "\n" +
                   "NetIncome: " + NetIncomeLoss.ToString() + "\n" +
                   "LiabilitiesAndStockholdersEquity: " + LiabilitiesAndStockholdersEquity.ToString() + "\n" +
                   "GrossProfit: " + GrossProfit.ToString() + "\n" +
                   "OperatingIncomeLoss: " + OperatingIncomeLoss.ToString() + "\n" +
                   "EarningsPerShareBasic: " + EarningsPerShareBasic.ToString() + "\n" +
                   "EarningsPerShareDiluted: " + EarningsPerShareDiluted.ToString() + "\n" +
                   "SalesRevenueNet: " + SalesRevenueNet.ToString() + "\n" +
                   "CashAndCashEquivalentsAtCarryingValue: " + CashAndCashEquivalentsAtCarryingValue.ToString() + "\n" +
                   "NoncurrentAssets: " + NoncurrentAssets.ToString() + "\n" +
                   "ShortTermInvestments: " + ShortTermInvestments.ToString() + "\n" +
                   "Liabilities: " + Liabilities.ToString() + "\n" +
                   "LongTermDebt: " + LongTermDebt.ToString() + "\n" +
                   "LongTermDebtNoncurrent: " + LongTermDebtNoncurrent.ToString() + "\n" +
                   "StockHoldersEquity: " + StockHoldersEquity.ToString() + "\n";


        }
    }
}
