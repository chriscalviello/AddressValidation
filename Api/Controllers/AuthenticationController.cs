using System;
using System.Collections.Generic;
using System.Data;
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
            catch (InvalidOperationException ex)
            {
                return StatusCode(409, new { Error = ex.Message });
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
                var loggedUser = authenticationService.Signin(credentials);

                if(loggedUser == null)
                {
                    return StatusCode(401, new { Error = "Invalid credentials" });
                }

                return Ok(new { loggedUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
