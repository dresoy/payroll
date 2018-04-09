using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public static async Task Log(string Message, LogTypes LogType, object Metadata)
        {
            //Write something in here;
            HttpClient client = new HttpClient();

            //var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject();
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("http://silogger.azurewebsites.net/json/Log",
                    new
                    {
                        email = "test@test",
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

    }
}