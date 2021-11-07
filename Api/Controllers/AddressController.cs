using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Services.AddressValidation;
using Api.Services.Config;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressValidationService addressValidationService;
        private readonly IConfigService configService;

        public AddressController(IAddressValidationService addressValidationService, IConfigService configService)
        {
            this.addressValidationService = addressValidationService;
            this.configService = configService;
        }

        /// <summary>
        /// Check if the given address is valid for a country
        /// </summary>
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

        /// <summary>
        /// Add support to a new country
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddAddressesFormat([FromBody] RegexAddressFormat regexAddressFormat)
        {
            try
            {
                configService.AddRegexAddressFormat(regexAddressFormat);   
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Edit address' configuration for a country
        /// </summary>
        [HttpPut]
        [Route("")]
        public IActionResult EditAddressesFormat([FromBody] RegexAddressFormat regexAddressFormat)
        {
            try
            {
                configService.EditRegexAddressFormat(regexAddressFormat);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Delete address' configuration for a country
        /// </summary>
        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromQuery] string countryCode)
        {
            try
            {
                configService.DeleteRegexAddressFormat(countryCode);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get supported configurations
        /// </summary>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RegexAddressFormatDTO>))]
        public IActionResult GetAddressesFormat([FromQuery] string countryCode)
        {
            try
            {
                if(string.IsNullOrEmpty(countryCode))
                {
                    return Ok(new { AddressesFormat = configService.GetRegexAddressesFormat().Select(x => new RegexAddressFormatDTO(x)) });
                }

                return Ok(new { AddressesFormat = new List<RegexAddressFormatDTO>() { new RegexAddressFormatDTO(configService.GetRegexAddressFormat(countryCode)) } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
