using Api.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiUnitTesting.Models
{
    public class AddressTest
    {
        private readonly AddressValidator _validator = new AddressValidator();

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("I")]
        [InlineData("ITA")]
        public void GivenInvalidCountry_ShouldHaveValidationError(string countryCode)
        {
            var sut = new Address(countryCode, "CITY", "HOUSENUMBER", "STREET", "ZIPCODE");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }
    }
}
