using System;
using Microsoft.AspNetCore.Mvc;
using GridManagement.service;
using GridManagement.Model.Dto;
using Serilog;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{
    [Authorize]
    [EnableCors("AllowCors")]
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

         [HttpGet("getpageaccess/{id}")]
        public IActionResult GetPageAccessBasedOnRoleId(int id)
        {
            try
            {
                var response = _pageAccessService.GetPageAccessBasedonRoleId(id);
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

        [HttpPut("updatepageaccess")]
        public IActionResult UpdatePageAccess(PageAccessDetail pageAccessDetail)
        {
            try
            {
                var response = _pageAccessService.UpdatePageAccess(pageAccessDetail.pageAccessDetails);
                if (response == null)
                    return BadRequest(new { message = "Error in updating the page Access", isAPIValid = false });
                return Ok(new { response = response, isAPIVaid = true });
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return BadRequest(new { message = "Something went wrong", isAPIValid = false });
            }
        }
    }
}
