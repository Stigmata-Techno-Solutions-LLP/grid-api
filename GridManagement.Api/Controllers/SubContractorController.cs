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
    [Authorize]

   
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
           // [ValidateAntiForgeryToken]

        public IActionResult AddSubCont(AddSubContractorModel model)
        {
            try
            {
                var response = _subContService.AddSubCont(model);
                return StatusCode(StatusCodes.Status201Created, (new { message = "Sub-Contractor added successfully",code =201}));
            }
            catch(ValueNotFoundException e) {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
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
                // return  StatusCode(204);
                return Ok((new { message = "Updated subcontractor successfully",code =204}));
            }
           catch(ValueNotFoundException e) {
               Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("GetSubContractorList")]
        public  ActionResult<List<SubContractorDetails>> GetSubContractorList(int? subId)
        {
            try {
           var response = _subContService.GetSubContList(subId);
           return Ok(response); 
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
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
            
         //  return  StatusCode(204);  
          return Ok(new { message = "Deleted SubContractor successfully",code =204});                
             }
             catch(ValueNotFoundException e) {
                 Util.LogError(e);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }
            
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            } 
        }     


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("SubContractorNoList")]
        public async Task<ActionResult<List<SubContractorName>>> GetSubContractorNoList()
        {
        try {
           var response =  _subContService.GetSubContNoList();
           return Ok(response); 
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }     
        }

  

    }

}