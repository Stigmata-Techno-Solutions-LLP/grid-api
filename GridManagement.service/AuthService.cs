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
using System.Linq;
using GridManagement.common;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace GridManagement.service
{
    public class AuthService : IAuthService
    {
        IAuthRepository _authRepository;
        IUserRepository _userRepository;

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings, IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }      

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
try {
            string decodedVal = Util.Base64Decode(model.Password);
            model.Password = Cryptography.Encrypt(_appSettings.SecretKeyPwd, decodedVal);            
            AuthenticateResponse user = _authRepository.ValidateUser(model);
            // return null if user not found
            if (user == null) return null;

            user.Token = generateJwtToken(user.Id.ToString());
            user.RefreshToken = generateRefreshToken(user.Id.ToString());
            return user;
            } catch(Exception ex) {
    throw ex;
}
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
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string generateRefreshToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshResponse RefreshToken(string token)
        {
            RefreshResponse refreshResponse = new RefreshResponse();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
                refreshResponse.Token = generateJwtToken(userId.ToString());
                refreshResponse.RefreshToken = generateRefreshToken(userId.ToString());
                refreshResponse.Message = "Refresh token regenerated.";
                refreshResponse.IsAPIValid = true;
                return refreshResponse;
            }
            catch (Exception ex)
            {
                return refreshResponse = new RefreshResponse()
                {
                    Message = "Token expired. Error : " + ex.Message,
                    IsAPIValid = false
                };
            }
        }

        public ResponseMessage ForgotPassword(string emailId)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                ResponseMessageForgotPassword responseMessageForgotPassword = new ResponseMessageForgotPassword();
                responseMessageForgotPassword = _authRepository.ForgotPassword(emailId);
                if (!string.IsNullOrEmpty(responseMessageForgotPassword.Password))
                {
                    string newPass = Util.CreateRandomPassword(10);
                   // responseMessageForgotPassword.Password = Cryptography.Encrypt(_appSettings.SecretKeyPwd,Util.CreateRandomPassword(10));
                ChangePassword chngePwd = new ChangePassword{
                     currentPassword = responseMessageForgotPassword.Password,
                     newPassword = Cryptography.Encrypt(_appSettings.SecretKeyPwd,newPass),
                     userId = responseMessageForgotPassword.UserId
                                     };
                _userRepository.ChangePassword(chngePwd);
                Util.SendMail("Reset Password for L & T project", "<h1>New Password for the user : " + responseMessageForgotPassword.FirstName + " " + responseMessageForgotPassword.LastName + " </h1><br /><p>Your Password is " + newPass + "</p>", responseMessageForgotPassword.EmailId, _appSettings.FromEmail, _appSettings.Password,_appSettings.Server,_appSettings.Port,_appSettings.Username);
                }
                responseMessage = new ResponseMessage()
                {
                    Message = "Password sent to the corresponding emailId"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseMessage;
        }

        public void SendMail(string subject, string bodyHtml, string toEmail)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_appSettings.FromEmail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = bodyHtml };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect(_appSettings.Server, Convert.ToInt32(_appSettings.Port), SecureSocketOptions.StartTls);
                smtp.Authenticate(_appSettings.Username, _appSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
    }
}
