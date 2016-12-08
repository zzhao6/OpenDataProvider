using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenDataProvider;

namespace OpenData.Tests
{
    [TestFixture(Category = "Unit")]
    public class OpenDataStockUnitTests
    {
        private readonly OpenDataStock odStk;
        public OpenDataStockUnitTests()
        {
            odStk = new OpenDataStock();
        }

        [Test]
        public async Task ShouldGetStockQuote()
        {
            var response = await odStk.GetQuote("MSFT");
            Assert.IsFalse(response.Contains("\"LastTradePriceOnly\":null"));

            response = await odStk.GetQuote("XXXXX");
            Assert.IsTrue(response.Contains("\"LastTradePriceOnly\":null"));
        }

        [Test]
        public async Task ShouldGetStockHist()
        {
            var startDate = new DateTime(2001, 1, 1);
            var endDate = new DateTime(2002, 1, 1);
            var sym = "SPY";
            var response = await odStk.GetHistory(sym, startDate, endDate);
            Assert.IsFalse(response.Contains("\"count\":0"));

            sym = "XXXXX";
            response = await odStk.GetHistory(sym, startDate, endDate);
            Assert.IsTrue(response.Contains("\"count\":0"));
        }
    }
}
