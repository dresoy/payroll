using Payroll.Models.DbModels;
using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.DAO
{
    public static partial class Paysheet
    {
        public static async Task<Tbl_Payroll> EditAsync(Tbl_Payroll Record)
        {
            try
            {
                Task log = Logger.Log("Editando registros de Nomina", Logger.LogTypes.Information, Record);

                using (var db = new Models.dataContext())
                {
                    var result = await Task.Run(() => (from t in db.Tbl_Payroll
                                                       where t.Deleted == false
                                                       where t.Id == Record.Id
                                                       select t).Single());


                    result.Role = Record.Role;
                    result.Section = Record.Section;
                    result.Name = StringManager.UpperOnlyFirstLetter(Record.Name);
                    result.LastName = StringManager.UpperOnlyFirstLetter(Record.LastName);
                    result.Hours = Record.Hours;
                    result.Amount = Record.Amount;
                    result.Modified = DateTime.Now;

                    await db.SaveChangesAsync();
                    return result;

                }
            }
            catch (Exception ex)
            {
                var errorMessage = "Error en la edicion de registro de Nomina " + Record.Id + " " + ex.Message;
                Task log = Logger.Log(errorMessage, Logger.LogTypes.Error, ex);
                throw new Exception("Esta Transaccion no puede ser realizada en el momento");
            }
        }
    }
}