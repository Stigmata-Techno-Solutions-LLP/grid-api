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
using GridManagement.common;
using Microsoft.AspNetCore.Cors;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [EnableCors("AllowAll")]
   // [Authorize]
    [ApiController]

    [Route("api/[controller]")]
    public class PageAccessController : ControllerBase
    {
        private readonly IPageAccessService _pageAccessService;

        public PageAccessController(IPageAccessService pageAccessService)
        {
            _pageAccessService = pageAccessService;
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
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

         [HttpGet("getpageaccess/{id}")]
        public IActionResult GetPageAccessBasedOnRoleId(int id)
        {
            try
            {
                var response = _pageAccessService.GetPageAccessBasedonRoleId(id);
              //  if (response == null)
                //    return BadRequest(new { message = "No records found", isAPIValid = false });
                return Ok(response);
            }
             catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
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
                // return Ok(new { response = response, isAPIVaid = true });
                return StatusCode(StatusCodes.Status201Created, (new { message = response.Message,code =204}));
            }
             catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }
    }
}
