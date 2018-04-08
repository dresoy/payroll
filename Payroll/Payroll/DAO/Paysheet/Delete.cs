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

        public static async Task<Tbl_Payroll> DeleteAsync(Guid Id)
        {
            await Logger.Log("Consultando registros de Nomina", Logger.LogTypes.Information);
            try
            {
                using (var db = new Models.dataContext())
                {
                    var result = await Task.Run(() => (from t in db.Tbl_Payroll
                                                       where t.Id == Id
                                                       select t).ToList());

                    if (result.Count == 1)
                    {
                        
                        result.Single().Deleted = true;
                        result.Single().Modified = DateTime.Now;
                        await db.SaveChangesAsync();
                        return result.Single();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error al consultar el registro de Nomina " + Id + " " + ex.Message;
                await Logger.Log(errorMessage, Logger.LogTypes.Error);
                throw new Exception("Esta Transaccion no puede ser realizada en el momento");
            }
        }

    }
}