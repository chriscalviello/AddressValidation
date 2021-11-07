using System.Collections.Generic;
using Api.Models;

namespace Api.Services.Config
{
    public interface IConfigService
    {
        public List<RegexAddressFormat> GetRegexAddressesFormat();
        public RegexAddressFormat GetRegexAddressFormat(string country);
        public void AddRegexAddressFormat(RegexAddressFormat regexAddressFormat);
        public void EditRegexAddressFormat(RegexAddressFormat regexAddressFormat);
        public void DeleteRegexAddressFormat(string countryCode);
    }
}
