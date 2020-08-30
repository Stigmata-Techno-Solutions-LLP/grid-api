using System;
using System.Threading.Tasks;
using GridManagement.Model.Dto;

namespace GridManagement.service
{
   
        public interface IAuthService
        {
            Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
             bool insertNewUser(AddUser model);
        bool chkUserId(int id);

        }



}
