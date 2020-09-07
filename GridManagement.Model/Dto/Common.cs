using System;
namespace GridManagement.Model.Dto
{
    public class ResponseMessage
    {
        public string Message {get;set;}
        public bool IsValid {get;set;}
    }

    public class ResponseMessageForgotPassword
    {
        public string Message {get;set;}
        public bool IsValid {get;set;}
        public string Password {get;set;}
        public string EmailId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
    }
}
