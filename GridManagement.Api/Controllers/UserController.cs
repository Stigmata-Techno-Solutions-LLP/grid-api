using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using System.Net;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController (IAuthService authService)
        {
            _authService = authService;
        }
       
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _authService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }



       [HttpPost]
       [Route("AddUser")]
        public IActionResult AddUser(AddUser model)
        {
            try
            {
                var response = _authService.insertNewUser(model);
                if (response == false) return BadRequest(new { message = "Email already exists" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
               // return StatusCode InternalServerError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
