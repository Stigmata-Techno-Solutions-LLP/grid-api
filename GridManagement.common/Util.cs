using System;
using Serilog;

namespace GridManagement.common
{

    public static class Util
    {
        
        public static void LogError(Exception ex) {
Log.Logger.Error( ex.Message  + "\n"  +(ex.InnerException == null ? "": ex.InnerException.Message) + "\n" + ex.StackTrace);
        }
    }
}