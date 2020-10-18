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
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;  
using Microsoft.AspNetCore.Http;  

namespace GridManagement.Api.Controllers
{
   
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
   [Authorize]
   // [ValidateAntiForgeryToken]

    public class LayerController :ControllerBase
    {


private readonly IGridService _gridService;

        public LayerController(IGridService gridService)
        {
            _gridService = gridService;
        }
#region Layer API endpoints

        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]
        [Route("AddLayer")]
        public IActionResult AddLayer([FromForm] AddLayer model)
        {
            try
            {
                List<LayerSubcontractor> layerSub  = JsonConvert.DeserializeObject<List<LayerSubcontractor>>(model.layerSubContractor1);
        
                model.layerSubContractor = layerSub;

                            if (model.uploadDocs != null) {
                  if (model.uploadDocs.Length > 5)  throw new ValueNotFoundException("Document count should not greater than 5"); 
                      foreach(IFormFile file in model.uploadDocs) {                
                     if ( constantVal.AllowedDocFileTypes.Where(x=>x.Contains(file.ContentType)).Count() == 0 )  throw new ValueNotFoundException( string.Format("File Type {0} is not allowed", file.ContentType)); 
                      }
                 if (model.uploadDocs.Select(x=>x.Length).Sum() > 50000000)   throw new ValueNotFoundException(" File size exceeded limit");
  
    }
                var response = _gridService.AddLayer(model);
                // return Ok(response);   
                return StatusCode(StatusCodes.Status201Created, (new { message = "Grid Layer updated successfully",code =201}));             
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
        [Route("LayerList")]
        public async Task<ActionResult<List<layerDtls>>> GetLayerList([FromQuery]layerFilter layerFilter)
        {
              try {
           var response =  _gridService.GetLayerList(layerFilter);
           return Ok(response); 
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
        [Route("LayerNoList")]
        public async Task<ActionResult<List<LayerNo>>> GetLayerNoList( [FromQuery] LayerNoFilterSkip layerNoFilter)
        {
        try {
           var response =  _gridService.GetLayerNoList(layerNoFilter);
           return Ok(response); 
            }
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }
   
   
   
        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]        
        [Route("ApproveLayer")]
        public  IActionResult ApproveLayer([FromQuery]  int layerDtlsId )
        {
        try {
               _gridService.ApproveLayer(layerDtlsId);
                // return StatusCode(StatusCodes.Status204NoContent, (new { message = "Grid updated successfully",code =204}));
                   return Ok(new { message = "Layer Approved successfully",code =204});      
            
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

        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(204)]
        [Route("UploadLayer")]
        public IActionResult UploadLayer([FromForm] UploadLayerImages model)
        {
            try
            {
                _gridService.UploadLayer(model);
                   return Ok(new { message = "Layer Uploaded successfully",code =204});      
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
        [Route("UserLayerList")]
        public async Task<ActionResult<List<LayerNo>>> GetUserLayerList()
        {
        try {
           var response =  _gridService.GetUserLayerList();
           return Ok(response); 
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
        [Route("ClientLayerList")]
        public async Task<ActionResult<List<LayerNo>>> GetClientLayerList()
        {
        try {
           var response =  _gridService.GetClientLayerList();
           return Ok(response); 
            }
             catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }
   
   
    #endregion
    }



}