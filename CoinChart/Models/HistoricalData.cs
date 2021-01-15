using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinChart.Models
{
    public class AlphaVantageHistoricalData
    {
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal volume { get; set; }

    }
}
