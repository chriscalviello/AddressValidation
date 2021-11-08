using System;
using Api.Models.User;
using Api.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Add new credentials and get a jwt token
        /// </summary>
        [HttpPost]
        [Route("signup")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(LoggedUser))]
        public IActionResult Signup([FromBody] Credentials credentials)
        {
            try
            {
                return Ok(new { IsValid = authenticationService.Signup(credentials) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Login and get a jwt token
        /// </summary>
        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(LoggedUser))]
        public IActionResult Signin([FromBody] Credentials credentials)
        {
            try
            {
                return Ok(new { IsValid = authenticationService.Signin(credentials) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
