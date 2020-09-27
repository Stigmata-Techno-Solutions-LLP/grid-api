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
using Newtonsoft.Json;
using GridManagement.common;
using GridManagement.Api.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{

    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    //[Authorize]
    //[ValidateAntiForgeryToken]


    public class GridController : ControllerBase
    {
        private readonly IGridService _gridService;

        public GridController(IGridService gridService)
        {
            _gridService = gridService;
        }


        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]        
        [Route("AddGrid")]
        public IActionResult AddGrid(AddGrid model)
        {
            try
            {
                var response = _gridService.AddGrid(model);              
                return StatusCode(StatusCodes.Status201Created, (new { message = "Grid added successfully",code =201}));
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
        [ProducesResponseType(201)]        
        [Route("CreateCG/{id}")]
        public IActionResult CreateCleaningGrubing([FromForm]AddCG_RFI model, int id)
        {
            try
            {
            if (model.uploadDocs != null) {
                                     if (model.uploadDocs.Select(x=>x.Length).Sum() > 50000000)   throw new ValueNotFoundException(" File size exceeded limit");

                  if (model.uploadDocs.Length > 5)  throw new ValueNotFoundException("Document count should not greater than 5"); 
                      foreach(IFormFile file in model.uploadDocs) {                
                     if ( constantVal.AllowedDocFileTypes.Where(x=>x.Contains(file.ContentType)).Count() == 0 && constantVal.AllowedIamgeFileTypes.Where(x=>x.Contains(file.ContentType)).Count() == 0 )  throw new ValueNotFoundException( string.Format("File Type {0} is not allowed", file.ContentType)); 
                      }
    } 
           var response = _gridService.CleaningGrubbingEntry(model, id);                
     
                
                //  foreach(IFormFile file in uploadDocs.uploadDocs.Count()) {

                //  }
              return Ok(new { message = "Cleaning & Grubbing added successfully",code =201});  
            }
             catch(ValueNotFoundException e) {
                Util.LogError(e);                 
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new ErrorClass() { code= StatusCodes.Status422UnprocessableEntity.ToString(), message=e.Message});
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message=e.Message});
            }
        }
             

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("GridList")]
        public async Task<ActionResult<List<GridDetails>>> GetGridList([FromQuery]  gridFilter filterReq)
        {
              try {
           var response =  _gridService.GetGridList(filterReq);
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
        [Route("GridDetailsById")]
        public async Task<ActionResult<GridDetails>> GetGridByID([FromQuery] int Id)
        {
              try {
           var response =  _gridService.GetGridDetails(Id);
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
        [Route("GridNoList")]
        public async Task<ActionResult<List<GridNo>>> GetGridNoList()
        {
              try {
           var response =  _gridService.GetGridNoList();
           return Ok(response); 
            }
            catch (Exception e)
            {
Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }    
        

        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("DeleteGrid/{id}")]
        public async Task<IActionResult> DeleteGrid(int id)
        {
             try {
           var response = _gridService.DeleteGrid(id);
           
            // return Ok(new { message = "Grid deleted successfully",code =204});     
             return Ok(new { message = "Grid deleted successfully",code =204});            
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
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("LayerCmplCountByGrid")]
        public async Task<ActionResult<string>> GetCompletedCountbyGridNo([FromQuery]int Id)
        {
              try {
           var response =  _gridService.GetCompletedLayerCountByGridNo(Id);
           return Ok(response); 
            }
            catch (Exception e)
            {
                Util.LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }    
        


        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("updateGrid/{id}")]
        public async Task<IActionResult> EditGrid(AddGrid gridReq, int id)
        {
           try
            {
                var response = _gridService.UpdateGrid(gridReq, id);
                // return StatusCode(StatusCodes.Status204NoContent, (new { message = "Grid updated successfully",code =204}));
                   return Ok(new { message = "Grid updated successfully",code =204});      
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

    }

}
