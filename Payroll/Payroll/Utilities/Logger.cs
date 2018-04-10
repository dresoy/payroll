using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public static async Task<List<LogResponseObject>> GetLogs()
        {
            HttpClient client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("http://silogger.azurewebsites.net/json/Historial?email=" + mail);

                var readResponse = await response.Content.ReadAsStringAsync();

                var jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LogResponseObject>>(readResponse);


                var result = (from t in jObject
                              orderby t.Date descending
                              select t).ToList();

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public class LogResponseObject
        {
            public string __type { get; set; }
            public DateTime Date { get; set; }
            public string Email { get; set; }
            public int Level { get; set; }
            public string Message { get; set; }
            public object Metadata { get; set; }
        }


        const string mail = "Dresoyravelo@gmail.com";
    }
}