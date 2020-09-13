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
                // if (response == null)
                //     return BadRequest(new { message = "No records found", isAPIValid = false });
                return Ok(response);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpPost("adduser")]
        public IActionResult AddUser(UserDetails userDetails)
        {
            try
            {
                var response = _userService.AddUser(userDetails);
                // if (response == null)
                //     return BadRequest(new { message = "Error in adding the user", isAPIValid = false });
               //  return Ok(new { response = response, isAPIVaid = true });
              return StatusCode(StatusCodes.Status201Created, (new { message = response.Message,code =201}));    
            }
             catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }
              catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpPut("updateuser/{id}")]
        public IActionResult UpdateUser(UserDetails userDetails, int id)
        {
            try
            {
                var response = _userService.UpdateUser(userDetails, id);
                // if (response == null)
                //     return BadRequest(new { message = "Error in updating the user", isAPIValid = false });
                return Ok(new { message = response.Message,code =204});
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }            
            catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpDelete("deleteuser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var response = _userService.DeleteUser(id);
                    return Ok(new { message = response.Message,code =204});
               // return Ok(new { response = response, code =204});
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }            
            catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
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
                return Ok(new { response = response, isAPIValid = true });
                  return Ok(new { message = response.Message,code =204});
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }            
            catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }
    }
}
