using System;
using System.Threading.Tasks;

namespace OpenDataProvider
{
    // get data from open source libraries
    // return json formatted strings

    // Yahoo Finance API:
    // stock quote, stock history
    // option quote

    // fixer.io (ECB):
    // currency quote
    // currency history

    public interface IOpenData
    {
        Task<string> GetQuote(string symbol);
        Task<string> GetHistory(string symbol, DateTime asOfDate);
        Task<string> GetHistory(string symbol, DateTime startDate, DateTime endDate);
    }

    public interface IOpenDataOption
    {
        Task<string> GetExpiries(string symbol);
        Task<string> GetQuote(string symbol, DateTime expiry);
        // For future implementation
        Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime asOfDate);
        Task<string> GetHistory(string symbol, DateTime expiry, double strike, DateTime startDate, DateTime endDate);
    }
}
