using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using GridManagement.common;


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
            try {
            AuthenticateResponse result = null;
            Users user = _context.Users.Where(x => x.Username == userReq.Username && x.Password == userReq.Password && x.IsActive==true && x.IsDelete== false).FirstOrDefault();
            if (user == null)  throw new ValueNotFoundException("Username or password is incorrect");
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
            
            return result;
            } catch(Exception ex) {
                throw ex;
            }
        }

        public ResponseMessageForgotPassword ForgotPassword(string emailId)
        {
            ResponseMessageForgotPassword responseMessage = new ResponseMessageForgotPassword();
            try
            {
                Users user = _context.Users.Where(x => x.Email.ToLower() == emailId.ToLower() && x.IsDelete == false).FirstOrDefault();
                if (user == null)  throw new ValueNotFoundException("EmailId doesn't exist");
                
                    return responseMessage = new ResponseMessageForgotPassword()
                    {
                        Message = "Password sent via the email. Kindly check email.",
                        IsValid = true,
                        EmailId = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Password = user.Password,
                        UserId = user.Id

                    };                            
            }
            catch (Exception ex)
            {
               throw ex;
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

          public void AudtitLog(AuditLogs audit) {
            _context.AuditLogs.Add(audit);
            _context.SaveChanges();
        }
    }
}
