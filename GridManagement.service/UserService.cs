using System;
using GridManagement.Model.Dto;
using System.Collections.Generic;
using GridManagement.repository;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Serilog;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using GridManagement.common;

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

        public UserDetails getUserById(int id)
        {
            UserDetails user = _userRepository.getUserById(id);
            if(user == null) return null;
            return user;
        }

        public ResponseMessage AddUser(UserDetails userDetails)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            string pwd = Util.CreateRandomPassword(10);
            userDetails.password = Cryptography.Encrypt(_appSettings.SecretKeyPwd,pwd);
            responseMessage = _userRepository.AddUser(userDetails);
           
            bool isEmailSent = Util.SendMail("Password for L & T project", "<h1>Password for the user : " + userDetails.firstName + " " + userDetails.lastName + " </h1><br /><br /><p>Your Username is " + userDetails.userName + "</p><br /><p>Your Password is " + pwd + "</p>", userDetails.email, _appSettings.FromEmail, _appSettings.Password,_appSettings.Server,_appSettings.Port,_appSettings.Username);
            if (isEmailSent)
                return responseMessage;
            else
            {
                return new ResponseMessage()
                {
                    Message = "User Created. Error in sending the email. check the email.",

                };
            }
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
            changePassword.newPassword = Cryptography.Encrypt(_appSettings.SecretKeyPwd, Util.Base64Decode(changePassword.newPassword));
            changePassword.currentPassword = Cryptography.Encrypt(_appSettings.SecretKeyPwd, Util.Base64Decode(changePassword.currentPassword));

            responseMessage = _userRepository.ChangePassword(changePassword);
            return responseMessage;
        }

      
        public Task Execute(string apiKey, string subject, string message, List<string> emails)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_appSettings.FromEmail, "L&T GridManagement"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            foreach (var email in emails)
            {
                msg.AddTo(new EmailAddress(email));
            }

            Task response = client.SendEmailAsync(msg);
            return response;
        }
    }
}
