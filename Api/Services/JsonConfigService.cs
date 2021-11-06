using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Api.Models;

namespace Api.Services
{
    public class JsonConfigService : IConfigService
    {
        private string jsonPath { get; set; }

        public JsonConfigService(string jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        public void AddRegexAddressFormat(RegexAddressFormat regexAddressFormat)
        {
            var addressesFormat = GetRegexAddressesFormat();
            if (addressesFormat.Any(x => x.Country == regexAddressFormat.Country))
            {
                throw new Exception(regexAddressFormat.Country + " already existing");
            }

            addressesFormat.Add(regexAddressFormat);

            try
            {
                var jsonString = JsonSerializer.Serialize(addressesFormat);
                System.IO.File.WriteAllText(jsonPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when writing the json file. " + ex.Message);
            }
        }

        public void DeleteRegexAddressFormat(string countryCode)
        {
            var addressesFormat = GetRegexAddressesFormat();

            if (!addressesFormat.Any(x => x.Country == countryCode))
            {
                throw new Exception("Can't find " + countryCode);
            }

            try
            {
                var jsonString = JsonSerializer.Serialize(addressesFormat.Where(x => x.Country != countryCode));
                System.IO.File.WriteAllText(jsonPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when writing the json file. " + ex.Message);
            }
        }

        public void EditRegexAddressFormat(RegexAddressFormat regexAddressFormat)
        {
            if (regexAddressFormat == null)
            {
                throw new Exception("Invalid argument");
            }

            var addressesFormat = GetRegexAddressesFormat();

            if (!addressesFormat.Any(x => x.Country == regexAddressFormat.Country))
            {
                throw new Exception("Can't find " + regexAddressFormat.Country);
            }

            var idx = addressesFormat.FindIndex(x => x.Country == regexAddressFormat.Country);
            addressesFormat[idx] = new RegexAddressFormat(regexAddressFormat.Country, regexAddressFormat.RegexCity, regexAddressFormat.RegexHouseNumber, regexAddressFormat.RegexStreet, regexAddressFormat.RegexZipcode);

            try
            {
                var jsonString = JsonSerializer.Serialize(addressesFormat);
                System.IO.File.WriteAllText(jsonPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when writing the json file. " + ex.Message);
            }
        }

        public List<RegexAddressFormat> GetRegexAddressesFormat()
        {
            List<RegexAddressFormat> result;
            try
            {
                var jsonString = System.IO.File.ReadAllText(jsonPath);
                result = JsonSerializer.Deserialize<List<RegexAddressFormat>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when reading the json file. " + ex.Message);
            }
            return result;
        }

        public RegexAddressFormat GetRegexAddressFormat(string country)
        {
            var list = GetRegexAddressesFormat() ?? new List<RegexAddressFormat>();
            return list.FirstOrDefault(x => x.Country == country);
        }
    }
}
