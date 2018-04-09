using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.DAO.Reports
{
    public static class Payroll
    {

        public static async Task<EmployeesByTypeModel> GetEmployeesByTypeAsync()
        {

            await Logger.Log("Consultando GetEmployeesByTypeAsync", Logger.LogTypes.Information, null);

            var db = new Models.dataContext();
            EmployeesByTypeModel result = new EmployeesByTypeModel();

            try
            {
                var selection = await Task.Run(() => (from t in db.Tbl_Payroll
                                                      where t.Deleted == false
                                                      group t by t.Role into g
                                                      select new EmployeesByTypeAverageModel()
                                                      {
                                                          Role = g.FirstOrDefault().Role,
                                                          TotalHours = g.Sum(s => s.Hours),
                                                          TotalAmount = g.Sum(s => s.Amount),
                                                          AverageHours = g.Average(s => s.Hours),
                                                          AverageAmount = g.Sum(s => s.Amount) / g.Sum(s => s.Hours),
                                                          Count = g.Count()
                                                      }).ToList());

                result.Group = selection;
                result.Count = selection.Count;
                result.AverageAmountPerHour = selection.Sum(s => s.TotalAmount) / selection.Sum(s => s.TotalHours);
            }
            catch (Exception ex)
            {
                await Logger.Log("Error consultando GetEmployeesByTypeAsync", Logger.LogTypes.Error, ex);
            }
            finally
            {
                db.Dispose();
            }


            return result;
        }

        public class EmployeesByTypeModel
        {

            public decimal AverageAmountPerHour { get; set; }
            public int Count { get; set; }
            public List<EmployeesByTypeAverageModel> Group { get; set; }
        }

        public class EmployeesByTypeAverageModel
        {
            /// <summary>
            /// Tipo
            /// </summary>
            public string Role { get; set; }
            /// <summary>
            /// Promedio de 
            /// </summary>
            public decimal TotalHours { get; set; }

            public decimal TotalAmount { get; set; }

            public decimal AverageHours { get; set; }

            public decimal AverageAmount { get; set; }

            public int Count { get; set; }
        }


    }
}