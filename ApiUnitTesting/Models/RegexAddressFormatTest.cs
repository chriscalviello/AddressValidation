using System;
using Api.Models;
using FluentValidation.TestHelper;
using Xunit;

namespace ApiUnitTesting.Models
{
    public class RegexAddressFormatTest
    {
        private readonly RegexAddressFormatValidator _validator = new RegexAddressFormatValidator();

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("[")]
        public void GivenInvalidRegexCityPattern_ShouldHaveValidationError(string regexCity)
        {
            var sut = new RegexAddressFormat("NL", regexCity, "^[0-9]", "/^([^0-9]*)$/", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.RegexCity);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("[")]
        public void GivenInvalidRegexHouseNumberPattern_ShouldHaveValidationError(string regexHouseNumber)
        {
            var sut = new RegexAddressFormat("NL", "/^([^0-9]*)$/", regexHouseNumber, "/^([^0-9]*)$/", "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.RegexHouseNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("[")]
        public void GivenInvalidRegexStreetPattern_ShouldHaveValidationError(string regexStreet)
        {
            var sut = new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", regexStreet, "/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i");
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.RegexStreet);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("[")]
        public void GivenInvalidRegexZipCodePattern_ShouldHaveValidationError(string regexZipCode)
        {
            var sut = new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", regexZipCode);
            var result = _validator.TestValidate(sut);

            result.ShouldHaveValidationErrorFor(x => x.RegexZipcode);
        }
    }
}
