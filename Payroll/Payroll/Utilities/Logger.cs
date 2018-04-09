using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

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

        public static async Task Log(string Message, LogTypes LogType, object Metadata)
        {

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("http://silogger.azurewebsites.net/json/Log",
                    new
                    {
                        email = mail,
                        message = Message,
                        level = (int)LogType,
                        metadata = Newtonsoft.Json.JsonConvert.SerializeObject(Metadata).ToString()
                    });

                var r = await response.Content.ReadAsStringAsync();



            }
            catch (Exception)
            {
                Console.WriteLine("Not even log is possible in this days.");
            }

        }

        public static async Task GetLogs()
        {
            HttpClient client = new HttpClient();
            try
            {

                HttpResponseMessage response = await client.GetAsync("http://silogger.azurewebsites.net/json/Historial?email=" + mail);


            }
            catch (Exception)
            {

                throw;
            }
        }

        const string mail = "Dresoyravelo@gmail.com";
    }
}