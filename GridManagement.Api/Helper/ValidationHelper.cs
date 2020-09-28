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
using Microsoft.AspNetCore.Mvc.Filters;

namespace GridManagement.Api.Helper
{
  public  class ValidateModelAttribute 
{
       public  BadRequestObjectResult CustomErrorResponse(ActionContext actionContext) {  
 
 return new BadRequestObjectResult(actionContext.ModelState  
  .Where(modelError => modelError.Value.Errors.Count > 0)  
  .Select(modelError => new Error {  
   code = modelError.Key,  
    Message = modelError.Value.Errors.FirstOrDefault().ErrorMessage  
  }).FirstOrDefault());  
} 
    }



    public class Error    
{    
    public string code { get; set; }    
    public string Message { get; set; }    
} 
}