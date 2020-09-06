using System;
using System.Threading.Tasks;
using GridManagement.Model.Dto;

namespace GridManagement.repository
{
    public interface IAuthRepository
    {
        AuthenticateResponse ValidateUser(AuthenticateRequest userReq);
    }
}
