using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDataProvider
{
    public class OpenDataOption : IOpenDataOption
    {
        public Task<string> GetExpiries(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetQuote(string symbol, DateTime expiry)
        {
            throw new NotImplementedException();
        }
        // For future implementation
        public Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime asOfDate)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime startDate,
            DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
