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
    [Authorize]
   // [ValidateAntiForgeryToken]

    public class ClientController : ControllerBase
    {


        private readonly IGridService _gridService;

        public ClientController(IGridService gridService)
        {
            _gridService = gridService;
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(201)]
        [Route("CreateClientBilling")]
        public IActionResult ClientBilling(AddClientBilling model)
        {
            try
            {
                var response = _gridService.CreateClientBilling(model);
               
              //  return Ok(response);
                  return StatusCode(StatusCodes.Status201Created, (new { message = "Client Billing Generated successfully",code =201}));
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
        [Route("LayerNoforBilling")]
        public async Task<ActionResult<List<LayerNo>>> GetLayerNoforBilling([FromQuery]  layerNoFilter layerNoFilter )
        {
        try {
           var response =  _gridService.ClientBillingLayersNo(layerNoFilter);
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