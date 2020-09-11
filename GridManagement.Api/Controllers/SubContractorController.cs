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
namespace GridManagement.Api.Controllers
{

    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
   
    public class SubContController : ControllerBase
    {


private readonly ISubContService _subContService;

        public SubContController(ISubContService subContService)
        {
            _subContService = subContService;
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]
        [Route("AddSubcontractor")]
        public IActionResult AddSubCont(AddSubContractorModel model)
        {
            try
            {
                var response = _subContService.AddSubCont(model);
                
                return  StatusCode(201);
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
            }
            
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(204)]
        [Route("UpdateSubcontractor/{Id}")]
        public IActionResult UpdateSubCont(AddSubContractorModel model,int Id)
        {
             try
            {
                var response = _subContService.UpdateSubCont(model, Id);
                if (response == false) return BadRequest(new { message = "SubContractorId doesn't exists or SubcontractorCode alredy exists" });
                return  StatusCode(204);
            }
           catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
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
        [Route("GetSubContractorList")]
        public  ActionResult<List<SubContractorDetails>> GetSubContractorList()
        {
            try {
           var response = _subContService.GetSubContList();
           return Ok(response); 
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }        
        }


        
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("DeleteSubCont/{id}")]
        public async Task<IActionResult> DeleteSubCont(int id)
        {
            try {
           var response = _subContService.DeleteSubCont(id);
            
           return  StatusCode(204);                  
             }
             catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
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
        [Route("SubContractorNoList")]
        public async Task<ActionResult<List<SubContractorName>>> GetGridNoList()
        {
        try {
           var response =  _subContService.GetSubContNoList();
           return Ok(response); 
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }        
        }  

    }

}