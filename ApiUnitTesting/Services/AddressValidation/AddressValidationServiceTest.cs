using System;
using System.Collections.Generic;
using Api.Models;
using Api.Services.AddressValidation;
using Api.Services.Config;
using Xunit;

namespace ApiUnitTesting.Services.AddressValidation
{
    public class AddressValidationServiceTest
    {
        private readonly AddressValidationService sut;
        private readonly Address ValidDutchAddress = new Address("NL", "Amsterdam", "1", "Street", "1111 AA");
        private readonly Address ValidItalianAddress = new Address("IT", "Florence", "1", "Via Roma", "59100");

        public AddressValidationServiceTest()
        {
            var regexAddressesFormat = new List<RegexAddressFormat>() {
                new RegexAddressFormat("NL", "[\\p{L} ]+$", "^[0-9]", "[\\p{L} ]+$", "^[1-9][0-9]{3} ?(?!sa|sd|ss|SA|SD|SS)[A-Za-z]{2}$"),
                new RegexAddressFormat("IT", "[\\p{L} ]+$", "^[0-9]", "[\\p{L} ]+$", "^([0-9]{5})$"),
            };
            sut = new AddressValidationService(new FakeConfigService(regexAddressesFormat));
        }

        [Fact]
        public void GivenValidAddress_WhenIsValid_ShouldReturnTrue()
        {
            Assert.True(sut.IsValid(ValidDutchAddress));
            Assert.True(sut.IsValid(ValidItalianAddress));
        }

        [Fact]
        public void GivenNoAddress_WhenIsValid_ShouldThrow()
        {
            Assert.Throws<Exception>(() => sut.IsValid(null));
        }

        [Fact]
        public void GivenInvalidCity_WhenIsValid_ShouldReturnFalse()
        {
            var address = new Address(ValidDutchAddress.Country, "1", ValidDutchAddress.HouseNumber, ValidDutchAddress.Street, ValidDutchAddress.ZipCode);
            Assert.False(sut.IsValid(address));
        }

        [Fact]
        public void GivenInvalidHouseNumber_WhenIsValid_ShouldReturnFalse()
        {
            var address = new Address(ValidDutchAddress.Country, ValidDutchAddress.City, "AAA", ValidDutchAddress.Street, ValidDutchAddress.ZipCode);
            Assert.False(sut.IsValid(address));
        }

        [Fact]
        public void GivenInvalidStreet_WhenIsValid_ShouldReturnFalse()
        {
            var address = new Address(ValidDutchAddress.Country, ValidDutchAddress.City, ValidDutchAddress.HouseNumber, "1234", ValidDutchAddress.ZipCode);
            Assert.False(sut.IsValid(address));
        }

        [Fact]
        public void GivenInvalidZipCode_WhenIsValid_ShouldReturnFalse()
        {
            var address = new Address(ValidDutchAddress.Country, ValidDutchAddress.City, ValidDutchAddress.HouseNumber, ValidDutchAddress.Street, "1234");
            Assert.False(sut.IsValid(address));
        }
    }
}
