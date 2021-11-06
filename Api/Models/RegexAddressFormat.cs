using System;
using System.Text.RegularExpressions;

namespace Api.Models
{
    public class RegexAddressFormat
    {
        public string Country { get; set; }
        public string RegexCity { get; set; }
        public string RegexHouseNumber { get; set; }
        public string RegexStreet { get; set; }
        public string RegexZipcode { get; set; }

        public RegexAddressFormat(string country, string regexCity, string regexHouseNumber, string regexStreet, string regexZipcode)
        {
            if (!IsValidRegex(regexCity) || !IsValidRegex(regexHouseNumber) || !IsValidRegex(regexStreet) || !IsValidRegex(regexZipcode))
            {
                throw new Exception("Wrong format error");
            }

            Country = country;
            RegexCity = regexCity;
            RegexHouseNumber = regexHouseNumber;
            RegexStreet = regexStreet;
            RegexZipcode = regexZipcode;
        }

        private static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return false;

            try
            {
                Regex.Match("", pattern);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}