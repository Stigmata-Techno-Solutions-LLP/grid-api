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
            AuthenticateResponse result =  new AuthenticateResponse();
            Users user = _context.Users.Where(x => x.Username == userReq.Username && x.Password == userReq.Password).FirstOrDefault();
            if (user != null)
            {
                Userroles userRoles = _context.Userroles.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (userRoles != null)
                {
                    result = new AuthenticateResponse
                    {
                        FirstName = user.FirstName,
                        Id = user.Id,
                        LastName = user.LastName,
                        Email = user.Email,
                        IsActive = Convert.ToBoolean(user.IsActive),
                        PhoneNumber = user.Phoneno,
                        RoleId = userRoles.Id
                    };
                }
            }
            else{
                result = null;
            }

            return result;
        }


        public bool InsertNewUser(AddUser userReq)
        {
            try
            {
                var data = _context.Users.Where(x => x.Email == userReq.email).FirstOrDefault();

                if (_context.Users.Where(x => x.Email == userReq.email).Count() > 0)
                {
                    return false;
                }

                _context.Users.Add(_mapper.Map<Users>(userReq));
                _context.SaveChanges();
                return true;

                // .FirstOrDefaultAsync(x => x.email == userReq.Username && x.password == userReq.Password);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool chkUserId(int id)
        {
            var data = _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return data != null ? true : false;
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
