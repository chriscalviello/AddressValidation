using System;
using Api.Models;
using Xunit;

namespace ApiUnitTesting.Models
{
    public class RegexAddressFormatTest
    {
        public RegexAddressFormatTest()
        {
        }

        [Fact]
        public void WhenCreate_ShouldSetValues()
        {
            var sut = new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i");

            Assert.Equal("NL", sut.Country);
            Assert.Equal("/^([^0-9]*)$/", sut.RegexCity);
            Assert.Equal("^[0-9]", sut.RegexHouseNumber);
            Assert.Equal("/^([^0-9]*)$/", sut.RegexStreet);
            Assert.Equal("/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i", sut.RegexZipcode);
        }

        [Fact]
        public void GivenInvalidRegexCityPattern_WhenCreate_ShouldSetValues()
        {
            Assert.Throws<Exception>(() => new RegexAddressFormat("NL", "[", "^[0-9]", "/^([^0-9]*)$/", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i"));
        }

        [Fact]
        public void GivenInvalidRegexHouseNumberPattern_WhenCreate_ShouldSetValues()
        {
            Assert.Throws<Exception>(() => new RegexAddressFormat("NL", "/^([^0-9]*)$/", "[", "/^([^0-9]*)$/", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i"));
        }

        [Fact]
        public void GivenInvalidRegexStreetPattern_WhenCreate_ShouldSetValues()
        {
            Assert.Throws<Exception>(() => new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", "[", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i"));
        }

        [Fact]
        public void GivenInvalidRegexZipCodePatternPattern_WhenCreate_ShouldSetValues()
        {
            Assert.Throws<Exception>(() => new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", "["));
        }


    }
}
