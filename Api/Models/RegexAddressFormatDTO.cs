using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Api.Models
{
    public class RegexAddressFormatDTO
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; private set; }
        public bool IsCityOptional { get; private set; }
        public bool IsHouseNumberOptional { get; private set; }
        public bool IsStreetOptional { get; private set; }
        public bool IsZipcodeOptional { get; private set; }

        public RegexAddressFormatDTO(RegexAddressFormat regexAddressFormat)
        {
            if(regexAddressFormat == null)
            {
                throw new Exception("Invalid argument");
            }

            Country = regexAddressFormat.Country;

            IsCityOptional = IsFieldOptional(regexAddressFormat.RegexCity);
            IsHouseNumberOptional = IsFieldOptional(regexAddressFormat.RegexHouseNumber);
            IsStreetOptional = IsFieldOptional(regexAddressFormat.RegexStreet);
            IsZipcodeOptional = IsFieldOptional(regexAddressFormat.RegexZipcode);
        }

        private static bool IsFieldOptional(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return true;

            try
            {
                return Regex.IsMatch("", pattern);
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
