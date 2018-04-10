using Payroll.Models.DbModels;
using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.DAO
{
    public partial class Paysheet
    {
        public static async Task<List<Tbl_Payroll>> GetAllActiveAsync()
        {
            try
            {
                Task log = Logger.Log("Consultando registros de Nomina", Logger.LogTypes.Information, null);

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
                Task log = Logger.Log(errorMessage, Logger.LogTypes.Error, ex);
                throw new Exception("Esta Transaccion no puede ser realizada en el momento");
            }
        }
    }
}