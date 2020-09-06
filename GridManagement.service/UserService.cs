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
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace GridManagement.service
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public List<UserDetails> getUser()
        {
            List<UserDetails> users = _userRepository.getUser();
            if (users == null || users.Count == 0) return null;
            return users;
        }

        public ResponseMessage AddUser(UserDetails userDetails)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            userDetails.password = CreateRandomPassword(10);
            responseMessage = _userRepository.AddUser(userDetails);
            SendMail("Password for L & T project", "<h1>Password for the user : " + userDetails.firstName + " " + userDetails.lastName + " </h1><br /><br /><p>Your Password is " + userDetails.password + "</p>", userDetails.email);
            return responseMessage;
        }

        public ResponseMessage UpdateUser(UserDetails userDetails, int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage = _userRepository.UpdateUser(userDetails, id);
            return responseMessage;
        }

        public ResponseMessage DeleteUser(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage = _userRepository.DeleteUser(id);
            return responseMessage;
        }

        public ResponseMessage ChangePassword(ChangePassword changePassword)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage = _userRepository.ChangePassword(changePassword);
            return responseMessage;
        }

        private string CreateRandomPassword(int length = 10)
        {
            try
            {
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                Random random = new Random();
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }
                return new string(chars);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return string.Empty;
            }
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
