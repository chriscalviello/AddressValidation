using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;

namespace Api.Services.Config
{
    public class FakeConfigService : IConfigService
    {
        private List<RegexAddressFormat> RegexAddressesFormat { get; set; }

        public FakeConfigService(List<RegexAddressFormat> regexAddressesFormat)
        {
            RegexAddressesFormat = regexAddressesFormat;
        }

        public void AddRegexAddressFormat(RegexAddressFormat regexAddressFormat)
        {
            RegexAddressesFormat.Add(regexAddressFormat);
        }

        public void DeleteRegexAddressFormat(string countryCode)
        {
            RegexAddressesFormat = RegexAddressesFormat.Where(x => x.Country != countryCode).ToList();
        }

        public void EditRegexAddressFormat(RegexAddressFormat regexAddressFormat)
        {
            var idx = RegexAddressesFormat.FindIndex(x => x.Country == regexAddressFormat.Country);
            if(idx == -1)
            {
                throw new Exception("Can't find " + regexAddressFormat.Country);
            }

            RegexAddressesFormat[idx] = regexAddressFormat;
        }

        public List<RegexAddressFormat> GetRegexAddressesFormat()
        {
            return RegexAddressesFormat;
        }

        public RegexAddressFormat GetRegexAddressFormat(string country)
        {
            return RegexAddressesFormat.FirstOrDefault(x => x.Country == country);
        }
    }
}
