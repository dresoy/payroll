using Payroll.Models.DbModels;
using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.DAO
{
    public static partial class Paysheet
    {

        public static async System.Threading.Tasks.Task<Tbl_Payroll> ToPayrollModelAsync(string role, string section, string name, string lastName, string hours, string amount)
        {
            try
            {
                Tbl_Payroll n = new Tbl_Payroll();

                n.Role = role.Trim();
                n.Section = short.Parse(section, System.Globalization.NumberStyles.Integer);
                n.Name = StringManager.UpperOnlyFirstLetter(name.Trim());
                n.LastName = StringManager.UpperOnlyFirstLetter(lastName.Trim());
                n.Hours = decimal.Parse(hours);
                n.Amount = decimal.Parse(amount);

                return n;

            }
            catch (Exception ex)
            {
                await Logger.Log("Error al convertir a Payroll " + ex.Message, Logger.LogTypes.Error, ex);
                throw ex;
            }

        }

        static int ParseInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception)
            {

                var n = value.Replace(".", ",");

                return int.Parse(n);
            }
        }

    }
}