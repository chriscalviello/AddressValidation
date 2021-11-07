using System;
using Api.Models;
using Api.Services.AddressValidation;
using Api.Services.Config;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IConfigService configService;
        private readonly IAddressValidationService addressValidationService;

        public AddressController()
        {
            configService = new JsonConfigService("config/AddressesFormat.json");
            addressValidationService = new AddressValidationService(configService);
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
