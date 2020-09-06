using GridManagement.Model.Dto;

namespace GridManagement.service
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        bool insertNewUser(AddUser model);
        bool chkUserId(int id);

    }
}
