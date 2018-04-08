using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Utilities
{
    public static class Logger
    {

        public enum LogTypes
        {
            Information,
            Warning,
            Error
        }

        public static async Task Log(string Message, LogTypes LogType)
        {
            //Write something in here;


        }
    }
}