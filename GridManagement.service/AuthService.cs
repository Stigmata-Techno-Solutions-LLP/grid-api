using System;
using GridManagement.Model.Dto;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using GridManagement.repository;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using GridManagement.common;
namespace GridManagement.service
{
  

    public class AuthService : IAuthService
    {
        IUserRepository _userRepo;
       

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings, IUserRepository userRepo)
        {
            _appSettings = appSettings.Value;
            _userRepo = userRepo;
        }

        public async Task<AuthenticateResponse>  Authenticate(AuthenticateRequest model)
        {

           AuthenticateResponse user = await _userRepo.ValidateUser(model);
            // return null if user not found
            if (user == null) return null;
           
            user.Token = generateJwtToken(user.Id.ToString());
            return user;
        }

        
        private string generateJwtToken(string userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool insertNewUser(AddUser model)
        {
            try
            {
                string encrypted = Cryptography.Encrypt("admin@123", "testpass");
                model.password = encrypted;
                return _userRepo.InsertNewUser(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool chkUserId(int id)
        {
            return _userRepo.chkUserId(id);
        }


    }


}
