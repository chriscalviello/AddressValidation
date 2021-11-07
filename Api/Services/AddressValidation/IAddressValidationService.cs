using Api.Models;

namespace Api.Services.AddressValidation
{
    public interface IAddressValidationService
    {
        public bool IsValid(Address address);
    }
}
