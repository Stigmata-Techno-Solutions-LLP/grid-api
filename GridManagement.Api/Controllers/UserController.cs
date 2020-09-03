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
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]
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

         [HttpPut]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(204)]
        [Route("UpdateUser/{Id}")]
        public IActionResult UpdateSubCont(UpdateUser model)
        {
            try
            {
                return Ok(null);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

      


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("UsersList")]
        public async Task<ActionResult<List<UserDetails>>> GetUserList(UserFilter userFilterModel)
        {
            dynamic response = null;
           return Ok(await response);         
        }
       
        
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("DeActivateUser/{id}")]
        public async Task<IActionResult> DeActivateUser(int id)
        {
            dynamic response = null;
           return Ok(await response);         
        }   

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("CahngePassword")]
        public async Task<IActionResult> changePassword(ChangePassword chngePassword)
        {
            dynamic response = null;
           return Ok(await response);         
        }   

         [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPw)
        {
            dynamic response = null;
           return Ok(await response);         
        }   
        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("CheckUsernameAndMailId")]
         public async Task<ActionResult <Boolean>> CheckUsernameMailId(UsernameVerification userDetails)
        {
            dynamic response = null;
           return Ok(await response);         
        }
       
    }
}
