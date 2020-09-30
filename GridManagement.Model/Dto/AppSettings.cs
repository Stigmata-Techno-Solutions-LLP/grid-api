using System;
namespace GridManagement.Model.Dto
{
    public class AppSettings
    {
        public string Secret {get; set;}
        public string FromEmail {get;set;}
        public string Server {get;set;}
        public int Port {get;set;}
        public string Username {get;set;}
        public string Password {get;set;} 
        public string DBConn{get;set;}
    }
}
