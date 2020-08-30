using System;
using System.Threading.Tasks;
using GridManagement.Model.Dto;

namespace GridManagement.repository
{
    public interface IUserRepository
    {

        Task<AuthenticateResponse> ValidateUser(AuthenticateRequest userReq);
        bool InsertNewUser(AddUser userReq);

        public bool chkUserId(int id);
    }
}
