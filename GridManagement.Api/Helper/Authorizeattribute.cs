using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Serilog;
using GridManagement.Model.Dto;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Web;
using GridManagement.service;
using GridManagement.domain.Models;
using GridManagement.repository;
using Microsoft.AspNetCore.Http;
using  Microsoft.Extensions.DependencyInjection;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    
            IConfiguration Configuration{get;}
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        


        string controllerName = context.RouteData.Values["Controller"].ToString();
        string actionName = context.RouteData.Values["Action"].ToString();

      //  context.R
        
    List<PageRoleAccess> pageRoleAccesslst = new List<PageRoleAccess>  
    {  
        new PageRoleAccess { ControllerName = "Grid", ActionName = "AddGrid",  Operation = "Add", PageFormId=1 },
        new PageRoleAccess { ControllerName = "Grid", ActionName = "GetGridList",  Operation = "View", PageFormId=1 },
        new PageRoleAccess { ControllerName = "Grid", ActionName = "GetGridByID",  Operation = "View", PageFormId=1 },
        new PageRoleAccess { ControllerName = "Grid", ActionName = "DeleteGrid",  Operation = "Delete", PageFormId=1 }, 
        new PageRoleAccess { ControllerName = "Grid", ActionName = "EditGrid",  Operation = "Edit", PageFormId=1 }, 

        new PageRoleAccess { ControllerName = "Layer", ActionName = "AddLayer",  Operation = "Add", PageFormId=2 },
        new PageRoleAccess { ControllerName = "Layer", ActionName = "GetLayerList",  Operation = "View", PageFormId=2 },
        new PageRoleAccess { ControllerName = "Layer", ActionName = "UploadLayer",  Operation = "Add", PageFormId=2 },

        new PageRoleAccess { ControllerName = "SubContractor", ActionName = "AddSubCont",  Operation = "Add", PageFormId=3 },
        new PageRoleAccess { ControllerName = "SubContractor", ActionName = "UpdateSubCont",  Operation = "Edit", PageFormId=3 }, 
        new PageRoleAccess { ControllerName = "SubContractor", ActionName = "GetSubContractorList",  Operation = "View", PageFormId=3 }, 
        new PageRoleAccess { ControllerName = "SubContractor", ActionName = "GetSubContractorNoList",  Operation = "View", PageFormId=3 }, 
        new PageRoleAccess { ControllerName = "SubContractor", ActionName = "DeleteSubCont",  Operation = "Delete", PageFormId=3 },

        new PageRoleAccess { ControllerName = "User", ActionName = "AddUser",  Operation = "Add", PageFormId=4 },
        new PageRoleAccess { ControllerName = "User", ActionName = "UpdateUser",  Operation = "Edit", PageFormId=4 }, 
        new PageRoleAccess { ControllerName = "User", ActionName = "GetUser",  Operation = "View", PageFormId=4 }, 
        new PageRoleAccess { ControllerName = "User", ActionName = "GetUserById",  Operation = "View", PageFormId=4 }, 
        new PageRoleAccess { ControllerName = "User", ActionName = "DeleteUser",  Operation = "Delete", PageFormId=4 },  
        new PageRoleAccess { ControllerName = "User", ActionName = "ChangePassword",  Operation = "Edit", PageFormId=4 }, 

        new PageRoleAccess { ControllerName = "PageAccess", ActionName = "GetPageAccess",  Operation = "View", PageFormId=5 },
        new PageRoleAccess { ControllerName = "PageAccess", ActionName = "GetPageAccessBasedOnRoleId",  Operation = "View", PageFormId=5 },
        new PageRoleAccess { ControllerName = "PageAccess", ActionName = "UpdatePageAccess",  Operation = "Edit", PageFormId=5 }, 

        new PageRoleAccess { ControllerName = "Client", ActionName = "ClientBilling",  Operation = "Add", PageFormId=6 },  
        new PageRoleAccess { ControllerName = "Client", ActionName = "GetLayerNoforBilling",  Operation = "View", PageFormId=6 },

        new PageRoleAccess { ControllerName = "Report", ActionName = "GetMasterReport",  Operation = "View", PageFormId=7 },
        new PageRoleAccess { ControllerName = "Report", ActionName = "SubContReport",  Operation = "View", PageFormId=7 },
        
        new PageRoleAccess { ControllerName = "Dashboard", ActionName = "LayerMonthlyDashboard",  Operation = "View", PageFormId=8 },
        new PageRoleAccess { ControllerName = "Dashboard", ActionName = "DashboardSummary",  Operation = "Edit", PageFormId=8 }, 
        new PageRoleAccess { ControllerName = "Dashboard", ActionName = "GetGridProgressMap",  Operation = "Edit", PageFormId=8 }, 


        new PageRoleAccess { ControllerName = "Grid", ActionName = "CreateCleaningGrubing",  Operation = "Add", PageFormId=9 },

        new PageRoleAccess { ControllerName = "Layer", ActionName = "GetLayerList",  Operation = "View", PageFormId=10 }, 

        new PageRoleAccess { ControllerName = "Layer", ActionName = "GetLayerList",  Operation = "View", PageFormId=11 },
        new PageRoleAccess { ControllerName = "Layer", ActionName = "ApproveLayer",  Operation = "Add", PageFormId=11 }, 

        new PageRoleAccess { ControllerName = "PageAccess", ActionName = "GetRoles",  Operation = "Common" },
        new PageRoleAccess { ControllerName = "Layer", ActionName = "GetLayerNoList",  Operation = "Common"},
        new PageRoleAccess { ControllerName = "Grid", ActionName = "GetCompletedCountbyGridNo",  Operation = "Common" }, 
        new PageRoleAccess { ControllerName = "Grid", ActionName = "GetGridNoList",  Operation = "Common" },

    };  


        HttpContext httpContext = context.HttpContext;
       // httpContext.RouteData.DataTokens["area"]

        
          //  var appSettingsSection = Configuration.GetSection("AppSettings");
         //   var appSettings = appSettingsSection.Get<GridManagement.Model.Dto.AppSettings>();

        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("8Zz5tw0Ionm3XPZZfN0NOml3z9FMfmpgXwovR9fp6ryDIoGRM8EPHAB6iHsc0fb");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "unique_name").Value);
            if (userId <= 0)
            {
                context.Result = new JsonResult(new { message = "Unauthorized", isAPIValid = false }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            PageRoleAccess pageAcc = pageRoleAccesslst.Where(x=>x.ActionName == actionName && x.ControllerName==controllerName  && x.Operation != "Common").FirstOrDefault();
  
            if (pageAcc != null) {
              //  RolesApplicationforms res = t4.CheckRoleWiseAccess(pageAcc.PageFormId,userId);
// //pageAcc.PageFormId
// if ((pageAcc.Operation == "Add" && res.IsAdd == true) || (pageAcc.Operation == "Edit" && res.IsUpdate == true) || (pageAcc.Operation == "Delete" && res.IsDelete == true) || (pageAcc.Operation == "View" && res.IsView == true)) {
// }
//  else {
//                 context.Result = new JsonResult(new { message = "User dont have access", isAPIValid = false }) { StatusCode = StatusCodes.Status403Forbidden };

//  }
            }

        }
        catch (Exception ex)
        {
            Log.Logger.Error("Error in validation : " + ex.Message);
            context.Result = new JsonResult(new { message = "Something went wrong" + ex.Message, isAPIValid = false }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
// public class AccountHandler : AuthorizeAttribute
// {
//     protected override  HandleRequirementAsync(
//         AuthorizationHandlerContext context,
//         AccountRequirement requirement)
//     {
//         // Your logic here... or anything else you need to do.
//         if (context.User.IsInRole("fooBar"))
//         {
//             // Call 'Succeed' to mark current requirement as passed
//             context.Succeed(requirement);
//         }

//         return Task.CompletedTask;
//     }
// }
}