using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinChart.Models;
using System.Diagnostics;
using System.Net;

namespace CoinChart.Services
{

    public interface IAlphaVantageService
    {
        List<AlphaVantageHistoricalData> GetCoinHistory(string Symbol);
    }
    public class AlphaVantageService : IAlphaVantageService
    {
        readonly string API_KEY = "727E7VJ66CH7SQ2V";
        public List<AlphaVantageHistoricalData> GetCoinHistory(string Symbol) 
        {

            List<AlphaVantageHistoricalData> HistoricalList = new List<AlphaVantageHistoricalData>();
            Stopwatch s = new Stopwatch();
            s.Start();
            var UrlString = GetStringFromUrl(string.Format("https://www.alphavantage.co/query?function=DIGITAL_CURRENCY_DAILY&symbol={0}&market=USD&apikey={1}&datatype=csv", Symbol, API_KEY));

            Console.WriteLine("Getting the data from internet: " + s.ElapsedMilliseconds.ToString());
            s.Restart();
            string[] CsvLines = UrlString.Split('\n');
            HistoricalList = GetFromCsvToHistoricalData(CsvLines);
            Console.WriteLine("Serializing into a typed list " + s.ElapsedMilliseconds.ToString());
            return HistoricalList;

        }

        private List<AlphaVantageHistoricalData> GetFromCsvToHistoricalData(string[] Lines)
        {
            List<AlphaVantageHistoricalData> returnList = new List<AlphaVantageHistoricalData>();
            foreach (var line in Lines)
            {
                string[] thisLine = line.Split(",");

                try
                {
                    var DataPoint = new AlphaVantageHistoricalData()
                    {
                        date = DateTime.Parse(thisLine[0]),
                        open = decimal.Parse(thisLine[1]),
                        high = decimal.Parse(thisLine[2]),
                        low = decimal.Parse(thisLine[3]),
                        close = decimal.Parse(thisLine[4]),
                        volume = decimal.Parse(thisLine[9])
                    };
                    returnList.Add(DataPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " in " + line);
                }

            }

            return returnList;
        }
        private string GetStringFromUrl(string URL)
        {
            string htmlCode = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(URL);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return htmlCode;
        }
    }
}
