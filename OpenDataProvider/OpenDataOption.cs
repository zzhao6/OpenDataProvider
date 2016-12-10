using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataProvider
{
    public class OpenDataOption : IOpenDataOption
    {
        private readonly WebClient wcWebClient;
        private const string optionExpiryUrl = "https://query2.finance.yahoo.com/v7/finance/options/";
        private const string optionQuoteUrl = "https://query2.finance.yahoo.com/v7/finance/options/{0}?date={1}";

        public OpenDataOption()
        {
            wcWebClient = new WebClient();
        }

        public async Task<string> GetExpiries(string symbol)
        {
            var url = optionExpiryUrl + symbol;
            var response = await wcWebClient.DownloadStringTaskAsync(url);

            // extract substring of expiries
            var idxOfExpiry = response.IndexOf("\"expirationDates\":[");
            if (idxOfExpiry < 0)
            {
                return null;
            }

            var lsq = new char[] {'['};
            var rsq = new char[] {']'};
            var idxLsq = response.IndexOfAny(lsq, idxOfExpiry);
            var idxRsq = response.IndexOfAny(rsq, idxOfExpiry);
            
            // return a valid json string
            var expiryArrayJson = "{\"expirationDates\":" + response.Substring(idxLsq, idxRsq - idxLsq + 1) + "}";
            return expiryArrayJson;
        }

        public async Task<string> GetQuote(string symbol, DateTime expiry)
        {
            var dto = new DateTimeOffset(expiry, TimeSpan.Zero);
            var unixTimestamp = dto.ToUnixTimeSeconds();
            //Int32 unixTimestamp = (Int32)(expiry.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var url = string.Format(optionQuoteUrl, symbol, unixTimestamp);
            var response = await wcWebClient.DownloadStringTaskAsync(url);
            return response;
        }
        
        // For future implementation
        public async Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime startDate,
            DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
