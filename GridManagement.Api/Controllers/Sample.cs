using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersssController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
 
        public UsersssController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _authService.Authenticate(model);
                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect", isAPIValid= false });
                return Ok(new {response=response, isAPIVaid= true});
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid= false });
            }
        }

        [Authorize]
        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            try
            {
                var response = _userService.getUser();
                if (response == null)
                    return BadRequest(new { message = "No records found", isAPIValid= false });
                return Ok(new {response=response, isAPIVaid= true});
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid= false });
            }
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
        public async Task<ActionResult<Boolean>> CheckUsernameMailId(UsernameVerification userDetails)
        {
            dynamic response = null;
            return Ok(await response);
        }

    }
}
