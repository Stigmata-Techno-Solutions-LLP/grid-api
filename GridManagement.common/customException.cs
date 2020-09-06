using System;
namespace GridManagement.common
{
    public class customException
    {
     
    }
        public class ErrorClass {
        public string code {get;set;}
        public string message { get;set;}

        
    }
    public class ValueNotFoundException : Exception
{   

    public ValueNotFoundException(string message)
        : base(message)
    {
    }

    public ValueNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
}