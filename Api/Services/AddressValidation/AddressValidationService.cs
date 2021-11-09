using System;
using System.Text.RegularExpressions;
using Api.Models;
using Api.Services.Config;

namespace Api.Services.AddressValidation
{
    public class AddressValidationService : IAddressValidationService
    {
        private readonly IConfigService configService;

        public AddressValidationService(IConfigService configService)
        {
            this.configService = configService;
        }

        public bool IsValid(Address address)
        {
            if(address == null || string.IsNullOrEmpty(address.Country))
            {
                throw new Exception("Invalid address");
            }

            var addressFormat = configService.GetRegexAddressFormat(address.Country);

            return IsValid(address, addressFormat);
        }

        private static bool IsValid(Address address, RegexAddressFormat addressFormat)
        {
            return IsValueOk(addressFormat.RegexCity, address.City)
                && IsValueOk(addressFormat.RegexHouseNumber, address.HouseNumber)
                && IsValueOk(addressFormat.RegexStreet, address.Street)
                && IsValueOk(addressFormat.RegexZipcode, address.ZipCode);
        }

        private static bool IsValueOk(string regexPattern, string value)
        {
            return string.IsNullOrEmpty(regexPattern) || new Regex(regexPattern).IsMatch(value);
        }

    }
}
