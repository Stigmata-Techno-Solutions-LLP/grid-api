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
        IAuthRepository _authRepository;

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {

            AuthenticateResponse user = _authRepository.ValidateUser(model);
            // return null if user not found
            if (user == null) return null;

            user.Token = generateJwtToken(user.Id.ToString());
            user.RefreshToken = generateRefreshToken(user.Id.ToString());
            return user;
        }


        private string generateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedBy = userId
                };
            }
        }

        public bool insertNewUser(AddUser model)
        {
            try
            {
                string encrypted = Cryptography.Encrypt("admin@123", "testpass");
                model.password = encrypted;
                return _authRepository.InsertNewUser(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool chkUserId(int id)
        {
            return _authRepository.chkUserId(id);
        }
    }
}
