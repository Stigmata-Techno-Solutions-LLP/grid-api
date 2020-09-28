using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;

namespace GridManagement.repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public AuthRepository(gridManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthenticateResponse ValidateUser(AuthenticateRequest userReq)
        {
            AuthenticateResponse result = null;
            Users user = _context.Users.Where(x => x.Username == userReq.Username && x.Password == userReq.Password && x.IsActive==true && x.IsDelete== false).FirstOrDefault();
            if (user != null)
            {
                result = new AuthenticateResponse
                {
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = Convert.ToBoolean(user.IsActive),
                    PhoneNumber = user.Phoneno,
                    RoleId = Convert.ToInt32(user.RoleId)
                };
            }
            return result;
        }

        public ResponseMessageForgotPassword ForgotPassword(string emailId)
        {
            ResponseMessageForgotPassword responseMessage = new ResponseMessageForgotPassword();
            try
            {
                Users user = _context.Users.Where(x => x.Email.ToLower() == emailId.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    return responseMessage = new ResponseMessageForgotPassword()
                    {
                        Message = "Password sent via the email. Kindly check email.",
                        IsValid = true,
                        Password = user.Password,
                        EmailId = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };
                }
                else
                {
                    return responseMessage = new ResponseMessageForgotPassword()
                    {
                        Message = "No User found for this Email",
                        IsValid = false,
                        Password = string.Empty,
                        EmailId = string.Empty,
                        FirstName = string.Empty,
                        LastName = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                return responseMessage = new ResponseMessageForgotPassword()
                {
                    Message = "Error in reseting the Password. Kindly contact Administrator. Error : " + ex.Message,
                    IsValid = false,
                    Password = string.Empty,
                    EmailId = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty
                };
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
