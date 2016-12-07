using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataProvider
{
    public class OpenDataFX : IOpenData
    {
        private readonly WebClient wcWebClient;
        private const string fxApiBaseUrl = "http://api.fixer.io/";

        OpenDataFX()
        {
            wcWebClient = new WebClient();
        }

        // will get quote for a specific currency symbol
        public async Task<string> GetQuote(string symbol)
        {
            string url = "";
            switch (symbol.Length)
            {
                // download list of fx rates for one base symbol
                case 3:
                {
                    url = fxApiBaseUrl + $"latest?base={symbol}";
                    break;
                }
                // download rate for one pair only
                case 6:
                {
                    var sym1 = symbol.Substring(0, 3);
                    var sym2 = symbol.Substring(3, 3);
                    url = fxApiBaseUrl + $"latest?symbols={sym1},{sym2}";
                    break;
                }
                default:
                    throw new ArgumentException($"Invalid symbol: {symbol}");
            }

            var response = await wcWebClient.DownloadStringTaskAsync(url);
            return response;
        }

        public async Task<string> GetHistory(string symbol, DateTime asOfDate)
        {
            string url = "";
            var asOfDateStr = asOfDate.ToString("yyyy-MM-dd");
            switch (symbol.Length)
            {
                // download list of fx rates for one base symbol
                case 3:
                {
                    url = fxApiBaseUrl + $"{asOfDateStr}?base={symbol}";
                    break;
                }
                // download rate for one pair only
                case 6:
                {
                    var sym1 = symbol.Substring(0, 3);
                    var sym2 = symbol.Substring(3, 3);
                    url = fxApiBaseUrl + $"{asOfDateStr}?symbols={sym1},{sym2}";
                    break;
                }
                default:
                    throw new ArgumentException($"Invalid symbol: {symbol}");
            }

            var response = await wcWebClient.DownloadStringTaskAsync(url);
            return response;
        }

        public async Task<string> GetHistory(string symbol, DateTime startDate, DateTime endDate)
        {
            if ((endDate - startDate).TotalDays > 366)
            {
                throw new ArgumentException("Only support 1 year maximum difference");
            }

            //string url = "";
            //string sym1 = "";
            //string sym2 = null;
            //switch (symbol.Length)
            //{
            //    // download list of fx rates for one base symbol
            //    case 3:
            //    {
            //        sym1 = symbol;
            //        break;
            //    }
            //    // download rate for one pair only
            //    case 6:
            //    {
            //        sym1 = symbol.Substring(0, 3);
            //        sym2 = symbol.Substring(3, 3);
            //        break;
            //    }
            //    default:
            //        throw new ArgumentException($"Invalid symbol: {symbol}");
            //}

            var response = "";
            for (var dt = startDate; dt <= endDate; dt.AddDays(1))
            {
                var asOfDateStr = dt.ToString("yyyy-MM-dd");
                response += await GetHistory(symbol, dt);
            }
            return response;
        }
    }
}
