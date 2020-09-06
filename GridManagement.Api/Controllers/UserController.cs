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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            try
            {
                var response = _userService.getUser();
                if (response == null)
                    return BadRequest(new { message = "No records found", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpPost("adduser")]
        public IActionResult AddUser(UserDetails userDetails)
        {
            try
            {
                var response = _userService.AddUser(userDetails);
                if (response == null)
                    return BadRequest(new { message = "Error in adding the user", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpPut("updateuser/{id}")]
        public IActionResult UpdateUser(UserDetails userDetails, int id)
        {
            try
            {
                var response = _userService.UpdateUser(userDetails, id);
                if (response == null)
                    return BadRequest(new { message = "Error in updating the user", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var response = _userService.DeleteUser(id);
                if (response == null)
                    return BadRequest(new { message = "Error in deleting the user", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var response = _userService.ChangePassword(changePassword);
                if (response == null)
                    return BadRequest(new { message = "Error in changing the password.", isAPIValid = false });
                return Ok(new { response = response, isAPIValid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }
    }
}
