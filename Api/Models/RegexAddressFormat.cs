using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Api.Models
{
    public class RegexAddressFormat
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }
        public string RegexCity { get; set; }
        public string RegexHouseNumber { get; set; }
        public string RegexStreet { get; set; }
        public string RegexZipcode { get; set; }

        public RegexAddressFormat(string country, string regexCity, string regexHouseNumber, string regexStreet, string regexZipcode)
        {
            Country = country;
            RegexCity = regexCity;
            RegexHouseNumber = regexHouseNumber;
            RegexStreet = regexStreet;
            RegexZipcode = regexZipcode;
        }
    }

    public class RegexAddressFormatValidator : AbstractValidator<RegexAddressFormat>
    {
        public RegexAddressFormatValidator()
        {
            RuleFor(x => x.Country)
                .NotNull().WithMessage("Country is required")
                .Length(2).WithMessage("Country must be a two characters string");

            RuleFor(x => x.RegexCity).Custom((x, context) =>
            {
                if (!IsValidRegex(x))
                {
                    context.AddFailure("RegexCity is not a valid regex pattern");
                }
            });

            RuleFor(x => x.RegexHouseNumber).Custom((x, context) =>
            {
                if (!IsValidRegex(x))
                {
                    context.AddFailure("RegexHouseNumber is not a valid regex pattern");
                }
            });

            RuleFor(x => x.RegexStreet).Custom((x, context) =>
            {
                if (!IsValidRegex(x))
                {
                    context.AddFailure("RegexStreet is not a valid regex pattern");
                }
            });

            RuleFor(x => x.RegexZipcode).Custom((x, context) =>
            {
                if (!IsValidRegex(x))
                {
                    context.AddFailure("RegexZipCode is not a valid regex pattern");
                }
            });

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