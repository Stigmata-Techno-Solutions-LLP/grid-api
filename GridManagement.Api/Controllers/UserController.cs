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
    [EnableCors("AllowAll")]
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _loggerService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _loggerService = new LoggerConfiguration().WriteTo.File("logs\\UserManagement.txt", rollingInterval:RollingInterval.Day).CreateLogger();
        }

        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            try
            {
                var response = _userService.getUser();
                return Ok(response);
            }
            catch (Exception e)
            {
               Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }

        [HttpGet("getuser/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var response = _userService.getUserById(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }

        [HttpPost("adduser")]
        public IActionResult AddUser(UserDetails userDetails)
        {
            try
            {
                var response = _userService.AddUser(userDetails);
                return StatusCode(StatusCodes.Status201Created, (new { message = response.Message, code = 201 }));
            }
            catch (ValueNotFoundException e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code = StatusCodes.Status422UnprocessableEntity.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                _loggerService.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }

        [HttpPut("updateuser/{id}")]
        public IActionResult UpdateUser(UserDetails userDetails, int id)
        {
            try
            {
                var response = _userService.UpdateUser(userDetails, id);
                return Ok(new { message = response.Message, code = 204 });
            }
            catch (ValueNotFoundException e)
            {
               Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code = StatusCodes.Status422UnprocessableEntity.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }

        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var response = _userService.DeleteUser(id);
                return Ok(new { message = response.Message, code = 204 });
            }
            catch (ValueNotFoundException e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code = StatusCodes.Status422UnprocessableEntity.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var response = _userService.ChangePassword(changePassword);
                if (response == null)
                    return BadRequest(new { message = "Error in changing the password.", code = 400 });
                return Ok(new { message = response.Message, code = 204 });
            }
            catch (ValueNotFoundException e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code = StatusCodes.Status422UnprocessableEntity.ToString(), message = e.Message });
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code = StatusCodes.Status500InternalServerError.ToString(), message = "Something went wrong" });
            }
        }
    }
}
