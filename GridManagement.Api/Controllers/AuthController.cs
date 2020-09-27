using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using GridManagement.common;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger _loggerService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _loggerService = new LoggerConfiguration().WriteTo.File("logs\\Authentication.txt", rollingInterval:RollingInterval.Day).CreateLogger();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _authService.Authenticate(model);
                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect", isAPIValid = false });
                return Ok(response);
            }
            catch (Exception e)
            {
                Util.LogError(e); 
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
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
                return Ok(new { response = response.Message, code = 200 });
            }
             catch (Exception e)
            {
                _loggerService.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
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
                return Ok(new { message = response.Message, code = 200 });
            }
            catch (Exception e)
            {
                _loggerService.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

    }
}
