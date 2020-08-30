using System;
using GridManagement.domain.Models;
using GridManagement.Model.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;

using System.Threading.Tasks;

namespace GridManagement.repository
{

    public class UserRepository: IUserRepository
    {
        private readonly gridManagementContext _context;
        private readonly IMapper _mapper;

        public UserRepository(gridManagementContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> ValidateUser(AuthenticateRequest userReq)
        {
           Users user  = await _context.Users.FirstOrDefaultAsync(x => x.Email == userReq.Username && x.Password == userReq.Password);
           return _mapper.Map<AuthenticateResponse>(user);          
        }


        public bool InsertNewUser(AddUser userReq)
        {
            try
            {
                var data = _context.Users.Where(x => x.Email == userReq.email).FirstOrDefault();

                if (_context.Users.Where(x => x.Email == userReq.email).Count()>0)
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
            var data =   _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
