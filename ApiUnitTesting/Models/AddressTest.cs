using Api.Models;
using Xunit;

namespace ApiUnitTesting.Models
{
    public class AddressTest
    {
        public AddressTest()
        {
        }

        [Fact]
        public void WhenCreate_ShouldSetValues()
        {
            var sut = new Address("IT", "CITY", "HOUSENUMBER", "STREET", "ZIPCODE");

            Assert.Equal("IT", sut.Country);
            Assert.Equal("CITY", sut.City);
            Assert.Equal("HOUSENUMBER", sut.HouseNumber);
            Assert.Equal("STREET", sut.Street);
            Assert.Equal("ZIPCODE", sut.ZipCode);
        }
    }
}
