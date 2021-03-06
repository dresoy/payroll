﻿using Payroll.Models.DbModels;
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
        public static async Task<Tbl_Payroll> GetActiveAsync(Guid Id)
        {
            try
            {

                Task log = Logger.Log("Consultando registros de Nomina", Logger.LogTypes.Information, Id);

                using (var db = new Models.dataContext())
                {
                    var result = await Task.Run(() => (from t in db.Tbl_Payroll
                                                       where t.Deleted == false
                                                       where t.Id == Id
                                                       select t).ToList());

                    if (result.Count == 1)
                    {
                        return result.FirstOrDefault();
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
                Task log = Logger.Log(errorMessage, Logger.LogTypes.Error, ex);
                throw new Exception("Esta Transaccion no puede ser realizada en el momento");
            }
        }
    }
}