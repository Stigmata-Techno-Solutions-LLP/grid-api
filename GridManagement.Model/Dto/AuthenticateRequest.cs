using System.ComponentModel.DataAnnotations;

namespace GridManagement.Model.Dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string token {get;set;}
    }

    public class ForgotPasswordRequest
    {
        public string emailId {get;set;}
    }
}