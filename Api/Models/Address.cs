using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Api.Models
{
    public class Address
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public Address() { }

        public Address(string country, string city, string houseNumber, string street, string zipCode)
        {
            Country = country;
            City = city;
            HouseNumber = houseNumber;
            Street = street;
            ZipCode = zipCode;
        }
    }

    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Country)
                .NotNull().WithMessage("Country is required")
                .Length(2).WithMessage("Country must be a two characters string");
        }
    }
}
