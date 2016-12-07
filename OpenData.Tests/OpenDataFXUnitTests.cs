using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenDataProvider;

namespace OpenData.Tests
{
    [TestFixture(Category = "Unit")]
    public class OpenDataFXUnitTests
    {
        private readonly OpenDataFX odFx;
        public OpenDataFXUnitTests()
        {
            odFx = new OpenDataFX();
        }

        [Test]
        public async Task ShouldGetFXQuoteForOneBase()
        {
            var res = await odFx.GetQuote("USD");
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Contains("error"));
            Assert.IsTrue(res.Contains("\"base\":\"USD\""));
        }

        [Test]
        public async Task ShouldGetFXQuoteForOnePair()
        {
            var res = await odFx.GetQuote("USDGBP");
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Contains("error"));
            Assert.IsTrue(res.Contains("\"base\":\"USD\""));
            Assert.IsTrue(res.Contains("\"rates\":{\"GBP\""));
        }

        [Test]
        public async Task ShouldGetFXHistoryOneBaseOneDate()
        {
            var asOfDate = new DateTime(2000, 1, 3);
            var res = await odFx.GetHistory("USD", asOfDate);
            Assert.IsTrue(res.Contains("\"base\":\"USD\""));
            Assert.IsTrue(res.Contains("\"date\":\"2000-01-03\""));
        }


        [Test]
        public async Task ShouldGetFXHistoryOneBaseMultiDates()
        {
            var startDate = new DateTime(2000, 1, 3);
            var endDate = new DateTime(2000, 1, 10);
            var res = await odFx.GetHistory("USD", startDate, endDate);
            Assert.IsTrue(res.Contains("\"base\":\"USD\""));
            Assert.IsTrue(res.Contains("\"date\":\"2000-01-03\""));
            Assert.IsTrue(res.Contains("\"date\":\"2000-01-10\""));
        }

        [Test]
        public async Task ShouldGetFXHistoryOnePairMultiDates()
        {
            var startDate = new DateTime(2000, 1, 3);
            var endDate = new DateTime(2000, 1, 10);
            var res = await odFx.GetHistory("USDGBP", startDate, endDate);
            Assert.IsTrue(res.Contains("\"base\":\"USD\""));
            Assert.IsTrue(res.Contains("\"rates\":{\"GBP\""));
            Assert.IsTrue(res.Contains("\"date\":\"2000-01-03\""));
            Assert.IsTrue(res.Contains("\"date\":\"2000-01-10\""));
        }
    }
}
