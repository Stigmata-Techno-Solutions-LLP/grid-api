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
    public class ReportsController : ControllerBase
    {
   private readonly IGridService _gridService;  
    private readonly ISubContService _subContService;

        public ReportsController(IGridService gridService, ISubContService subContService)
        {
            _gridService = gridService;
            _subContService = subContService;
        }
        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("MasterReport")]
        public async Task<ActionResult<List<MasterReport>>> GetMasterReport( [FromQuery] FilterReport filterReport)
        {
        try {
           var response =  _gridService.MasterReport(filterReport);
           return Ok(response); 
            }
             catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }          
        }


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("SubContracorReport")]
        public  ActionResult<List<SubContractorReport>> SubSontReport([FromQuery] FilterReport filterReport)
        {
            try {
           var response = _subContService.SubContReport(filterReport);
           return Ok(response); 
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }  
        }


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("LayerMonthlyDashboard")]
        public async Task<ActionResult<List<LayerMonthWiseDashboard>>> LayerMonthlyDashboard([FromQuery] FilterDashboard filter)
        {
        try {
           LayerMonthWiseDashboard response =  _gridService.LayerMonthDashboard(filter);
           return Ok(response); 
            }
             catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }          
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("DashboardSummary")]
        public async Task<ActionResult<DashboardSummary>> DashboardSummary([FromQuery] FilterDashboard filter)
        {
        try {
           DashboardSummary response =  _gridService.dashboardSummary(filter);
           return Ok(response); 
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