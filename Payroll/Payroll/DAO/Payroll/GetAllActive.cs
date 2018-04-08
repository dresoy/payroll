using Payroll.Models.DbModels;
using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.DAO
{
    public class Payroll
    {
        public static async Task<List<Tbl_Payroll>> GetAllActive()
        {
            await Logger.Log("Consultando registros de Nomina", Logger.LogTypes.Information);
            try
            {
                using (var db = new Models.dataContext())
                {
                    var result = await Task.Run(() => (from t in db.Tbl_Payroll
                                                       where t.Deleted == false
                                                       select t).ToList());

                    if (result.Count > 0)
                    {
                        return result;
                    }
                    else { return new List<Tbl_Payroll>(); }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error al consultar datos de Nomina " + ex.Message;
                await Logger.Log(errorMessage, Logger.LogTypes.Error);
                throw new Exception("Esta Transaccion no puede ser realizada en el momento");
            }
        }
    }
}