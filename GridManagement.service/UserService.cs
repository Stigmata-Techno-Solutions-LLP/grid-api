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
            responseMessage = _userRepository.AddUser(userDetails);
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
    }
}
