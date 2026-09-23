using System;
using System.Collections.Generic;
using System.Text;

namespace TCSA.Study.Linq.Models.Rows
{
    public class StockActivityReportRow
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public int TradeCount { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalValue { get; set; }
        public decimal HighestTradePrice { get; set; }
        public DateTime LatestTradeDate { get; set; }
    }
}
