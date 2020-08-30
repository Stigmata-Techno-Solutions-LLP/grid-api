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

        [HttpPost]
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

        [HttpPost]
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
    }


}
