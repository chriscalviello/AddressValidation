using System;
using Api.Models;
using Api.Services.Config;
using Xunit;

namespace ApiUnitTesting.Services
{
    public class JsonConfigServiceTest : IDisposable
    {
        private readonly JsonConfigService sut;
        private readonly string filePath = "config/AddressesFormatForTest.json";

        public JsonConfigServiceTest()
        {
            sut = new JsonConfigService(filePath);

            string json = @"[
                {
                ""Country"": ""NL"",
                ""RegexCity"": ""/^([^0-9]*)$/"",
                ""RegexHouseNumber"": ""^[0-9]"",
                ""RegexStreet"": ""/^([^0-9]*)$/"",
                ""RegexZipcode"": ""/^(?:NL-)?(\\d{4})\\s*([A-Z]{2})$/i""
                }
            ]";

            System.IO.File.WriteAllText(filePath, json);
        }

        public void Dispose()
        {
            System.IO.File.Delete(filePath);
        }

        [Fact]
        public void WhenGetRegexAddressesFormat_ShouldReturnValues()
        {
            var result = sut.GetRegexAddressesFormat();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void WhenGetRegexAddressesFormat_ShouldThrow()
        {
            var fakeSut = new JsonConfigService("config/fake.json");

            Assert.Throws<Exception>(() => fakeSut.GetRegexAddressesFormat());
        }

        [Fact]
        public void GivenExistingCountry_WhenGetRegexAddressFormat_ShouldReturnValues()
        {
            var result = sut.GetRegexAddressFormat("NL");

            Assert.NotNull(result);
            Assert.Equal("NL", result.Country);
        }

        [Fact]
        public void GivenNotExistingCountry_WhenGetRegexAddressFormat_ShouldReturnValues()
        {
            var result = sut.GetRegexAddressFormat("US");

            Assert.Null(result);
        }

        [Fact]
        public void GivenExistingCountry_WhenAddRegexAddressFormat_ShouldThrow()
        {
            var regexAddressFormat = new RegexAddressFormat("NL", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", "/^\\d{5}$/");

            Assert.Throws<Exception>(() => sut.AddRegexAddressFormat(regexAddressFormat));
        }

        [Fact]
        public void GivenNotExistingCountry_WhenAddRegexAddressFormat_ShouldAddValue()
        {
            var regexAddressFormat = new RegexAddressFormat("US", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", "^\\d{5}$");
            sut.AddRegexAddressFormat(regexAddressFormat);

            Assert.Equal(2, sut.GetRegexAddressesFormat().Count);

            var savedRegexAddressFormat = sut.GetRegexAddressFormat("US");

            Assert.Equal("US", savedRegexAddressFormat.Country);
            Assert.Equal("/^([^0-9]*)$/", savedRegexAddressFormat.RegexCity);
            Assert.Equal("^[0-9]", savedRegexAddressFormat.RegexHouseNumber);
            Assert.Equal("/^([^0-9]*)$/", savedRegexAddressFormat.RegexStreet);
            Assert.Equal("^\\d{5}$", savedRegexAddressFormat.RegexZipcode);
        }

        [Fact]
        public void GivenExistingCountry_WhenEditRegexAddressFormat_ShouldEditValue()
        {
            var regexAddressFormat = new RegexAddressFormat("NL", "/^([^0-8]*)$/", "^[0-8]", "/^([^0-8]*)$/", "^\\d{4}$");
            sut.EditRegexAddressFormat(regexAddressFormat);

            Assert.Single(sut.GetRegexAddressesFormat());

            var savedRegexAddressFormat = sut.GetRegexAddressFormat("NL");

            Assert.Equal("NL", savedRegexAddressFormat.Country);
            Assert.Equal("/^([^0-8]*)$/", savedRegexAddressFormat.RegexCity);
            Assert.Equal("^[0-8]", savedRegexAddressFormat.RegexHouseNumber);
            Assert.Equal("/^([^0-8]*)$/", savedRegexAddressFormat.RegexStreet);
            Assert.Equal("^\\d{4}$", savedRegexAddressFormat.RegexZipcode);
        }

        [Fact]
        public void GivenNotExistingCountry_WhenEditRegexAddressFormat_ShouldThrow()
        {
            var regexAddressFormat = new RegexAddressFormat("UK", "/^([^0-9]*)$/", "^[0-9]", "/^([^0-9]*)$/", "/^\\d{5}$/");

            Assert.Throws<Exception>(() => sut.EditRegexAddressFormat(regexAddressFormat));
        }

        [Fact]
        public void GivenNotExistingCountry_WhenDeleteRegexAddressFormat_ShouldThrow()
        {
            Assert.Throws<Exception>(() => sut.DeleteRegexAddressFormat("UK"));
        }

        [Fact]
        public void GivenExistingCountry_WhenDeleteRegexAddressFormat_ShouldDelete()
        {
            sut.DeleteRegexAddressFormat("NL");

            Assert.Empty(sut.GetRegexAddressesFormat());
        }
    }
}
