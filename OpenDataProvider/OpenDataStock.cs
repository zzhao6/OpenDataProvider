using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace OpenDataProvider
{
    public class OpenDataStock : IOpenData
    {
        private readonly WebClient wcWebClient;
        // TODO: move this to config file
        //private readonly DateTime sDate = new DateTime(1990, 1, 1); // default start date
        //private readonly DateTime eDate = new DateTime(2016, 10, 1);    // default end date

        private string yahooStockQuoteBaseUrl1 =
            $"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22";

        private string yahooStockQuoteBaseUrl2 = "%22)&format=json&diagnostics=true&&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

        public OpenDataStock()
        {
            wcWebClient = new WebClient();
        }

        public async Task<string> GetQuote(string symbol)
        {
            var url = yahooStockQuoteBaseUrl1 + symbol + yahooStockQuoteBaseUrl2;
            var response = await wcWebClient.DownloadStringTaskAsync(url);
            return response;
        }

        public async Task<string> GetHistory(string symbol, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetHistory(string symbol, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetSplit(string symbol, DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetDividend(string symbol, DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}
