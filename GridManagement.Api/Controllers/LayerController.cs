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
   
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class LayerController : ControllerBase
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
        public IActionResult AddLayer(AddLayer model)
        {
            try
            {
                var response = _gridService.AddLayer(model);
                // return Ok(response);   
                return StatusCode(StatusCodes.Status201Created, (new { message = "Grid Layer updated successfully",code =201}));             
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
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("LayerNoList")]
        public async Task<ActionResult<List<LayerNo>>> GetLayerNoList( )
        {
        try {
           var response =  _gridService.GetLayerNoList();
           return Ok(response); 
            }
             catch (Exception e)
            {
                Log.Logger.Error(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorClass() { code= StatusCodes.Status500InternalServerError.ToString(), message="Something went wrong"});
            }      
        }
    #endregion
    }



}