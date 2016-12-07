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
            var response = await odStk.GetQuote("MFST");
            Assert.IsNotNull(response);
        }
    }
}
