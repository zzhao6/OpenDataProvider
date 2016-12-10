using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenDataProvider;

namespace OpenData.Tests
{
    [TestFixture(Category = "Unit")]
    public class OpenDataOptionUnitTests
    {
        private readonly OpenDataOption odOption;
        public OpenDataOptionUnitTests()
        {
            odOption = new OpenDataOption();
        }

        [Test]
        public async Task ShouldGetOptionExpiries()
        {
            var res = await odOption.GetExpiries("AAPL");
            Assert.IsTrue(IsValidExpiries(res));
        }

        [Test]
        public async Task ShouldGetOptionQuotes()
        {
            var expiriesJson = await odOption.GetExpiries("AAPL");
            var idx1 = expiriesJson.IndexOf('[');
            var idx2 = expiriesJson.IndexOf(',');
            var expiry = expiriesJson.Substring(idx1 + 1, idx2 - idx1 - 1);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expiry));
            DateTime expiryDateTime = dateTimeOffset.UtcDateTime;

            var res = await odOption.GetQuote("AAPL", expiryDateTime);

            Assert.IsTrue(res.Contains(expiry));
            Assert.IsFalse(res.Contains("\"calls\":[]"));
            Assert.IsFalse(res.Contains("\"puts\":[]"));
        }

        private bool IsValidExpiries(string res)
        {
            const int firstPartConstIdx = 19;
            var firstPart = res.Substring(0, firstPartConstIdx);
            if (firstPart != "{\"expirationDates\":")
            {
                return false;
            }
            var secondPart = res.Substring(firstPartConstIdx);
            var validChar = new List<char>()
            {
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '[',
                ']',
                '}',
                ','
            };
            foreach (var c in secondPart)
            {
                if (!validChar.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
