using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _authService.Authenticate(model);
                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken(RefreshTokenRequest refreshToken)
        {
            try
            {
                var response = _authService.RefreshToken(refreshToken.token);
                if (response == null)
                    return Unauthorized(new { message = "Invalid token" });
                return Ok(new { response = response, IsAPIValid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            try
            {
                var response = _authService.ForgotPassword(forgotPassword.emailId);
                if (response == null)
                    return BadRequest(new { message = "Error in sending the details", isAPIValid = false });
                return Ok(new { response = response, IsAPIValid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

    }
}
