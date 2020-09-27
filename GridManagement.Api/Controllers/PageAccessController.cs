using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using System.Net;
using Microsoft.AspNetCore.Http;
using GridManagement.common;
using Microsoft.AspNetCore.Cors;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [EnableCors("AllowAll")]
   // [Authorize]
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PageAccessController : ControllerBase
    {
        private readonly IPageAccessService _pageAccessService;
        private readonly ILogger _loggerService;

        public PageAccessController(IPageAccessService pageAccessService)
        {
            _pageAccessService = pageAccessService;
            _loggerService = new LoggerConfiguration().WriteTo.File("logs\\PageAccessManagement.txt", rollingInterval:RollingInterval.Day).CreateLogger();
        }

         [HttpGet("getroles")]
        public IActionResult GetRoles()
        {
            try
            {
                var response = _pageAccessService.GetRoles();
                return Ok(response);
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpGet("getpageaccess")]
        public IActionResult GetPageAccess()
        {
            try
            {
                var response = _pageAccessService.GetPageAccess();
               // if (response == null)
                 //   return BadRequest(new { message = "No records found", isAPIValid = false });
                return Ok(response);
            }
            catch (Exception e)
            {
               Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

         [HttpGet("getpageaccess/{id}")]
        public IActionResult GetPageAccessBasedOnRoleId(int id)
        {
            try
            {
                var response = _pageAccessService.GetPageAccessBasedonRoleId(id);
                return Ok(response);
            }
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpPut("updatepageaccess")]
        public IActionResult UpdatePageAccess(PageAccessDetail pageAccessDetail)
        {
            try
            {
                var response = _pageAccessService.UpdatePageAccess(pageAccessDetail.pageAccessDetails);
                if (response == null)
                    return BadRequest(new { message = "Error in updating the page Access", isAPIValid = false });
                return StatusCode(StatusCodes.Status201Created, (new { message = response.Message,code =204}));
            }
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }
    }
}
