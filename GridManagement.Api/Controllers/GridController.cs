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


#region Grid API endpoints 

        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]        
        [Route("AddGrid")]
        public IActionResult AddGrid(AddGrid model)
        {
            try
            {
                var response = _gridService.AddGrid(model);
                if (response == false) return BadRequest(new { message = "Grid no. already exists" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                // return StatusCode InternalServerError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


      

        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [HttpGet]
        [Route("GridNoList")]
        public async Task<ActionResult<List<GridNo>>> GetGridNoList()
        {
            dynamic response = null;
           return Ok(await response);         
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]        
        [Route("GridList")]
        public async Task<ActionResult<List<AddGrid>>> GetGridList(gridFilter gridFilter)
        {
            dynamic response = null;
           return Ok(await response);         
        }



        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("DeleteGrid/{id}")]
        public async Task<IActionResult> DeleteGrid(int id)
        {
            dynamic response = null;
           return Ok(await response);         
        }


         [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Route("updateGrid/{id}")]
        public async Task<IActionResult> EditGrid(AddGrid gridReq)
        {
            dynamic response = null;
           return Ok(await response);         
        }

#endregion


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
