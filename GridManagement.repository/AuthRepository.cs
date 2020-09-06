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
            AuthenticateResponse result = new AuthenticateResponse();
            Users user = _context.Users.Where(x => x.Username == userReq.Username && x.Password == userReq.Password).FirstOrDefault();
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
