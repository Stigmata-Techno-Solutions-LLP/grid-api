using GridManagement.Model.Dto;

namespace GridManagement.service
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        
        RefreshResponse RefreshToken(string token);
        ResponseMessage ForgotPassword(string email);

    }
}
