using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Serilog;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        HttpContext httpContext = context.HttpContext;
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
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
            if(userId <= 0){
                context.Result = new JsonResult(new { message = "Unauthorized", isAPIValid= false }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
        catch(Exception ex)
        {
            Log.Logger.Error("Error in validation : " + ex.Message);
                context.Result = new JsonResult(new { message = "Something went wrong", isAPIValid = false }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}