using System;
using Api.Models;
using Api.Services.AddressValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressValidationService addressValidationService;

        public AddressController(IAddressValidationService addressValidationService)
        {
            this.addressValidationService = addressValidationService;
        }

        [HttpPost]
        [Route("isvalid")]
        public IActionResult IsValid([FromBody] Address address)
        {
            try
            {
                return Ok(new { IsValid = addressValidationService.IsValid(address) });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message});
            }
        }
    }
}
