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
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridManagement.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
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
                return Ok(response);
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.StackTrace);
                // return StatusCode InternalServerError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]        
        [Route("CreateCG/{id}")]
        public IActionResult CreateCleaningGrubing(AddCG_RFI model, int id)
        {
            try
            {
                var response = _gridService.CleaningGrubbingEntry(model, id);

           return  StatusCode(201);   
            }
             catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                // return StatusCode InternalServerError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
             

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("GridList")]
        public async Task<ActionResult<List<AddGrid>>> GetGridList([FromQuery]  gridFilter filterReq)
        {
              try {
           var response =  _gridService.GetGridList(filterReq);
           return Ok(response); 
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
            catch (Exception ex)
            {
                Log.Logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }        
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("DeleteGrid/{id}")]
        public async Task<IActionResult> DeleteGrid(int id)
        {
             try {
           var response = _gridService.DeleteGrid(id);
           
           return  StatusCode(204);                  
             }

            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return  StatusCode(204);
            }
            catch(ValueNotFoundException e) {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorClass() { code= StatusCodes.Status400BadRequest.ToString(), message=e.Message});
            }
            catch (Exception ex)
            {
    
                Log.Logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }     
        }



    }


    [ApiController]
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
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]
        [Route("AddLayer")]
        public IActionResult AddLayer(AddLayer model)
        {
            try
            {
                var response = _gridService.AddLayer(model);
               
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                // return StatusCode InternalServerError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("GetLayerNoList")]
        public async Task<ActionResult<List<LayerNo>>> GetLayerNoList()
        {
            dynamic response = null;
           return Ok(await response);         
        }


        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("LayerList")]
        public async Task<ActionResult<List<AddLayer>>> GetLayerList(layerFilter gridFilter)
        {
            dynamic response = null;
           return Ok(await response);         
        }
       

    #endregion
    }



}
