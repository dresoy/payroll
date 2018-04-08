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

        public static async Task<List<Tbl_Payroll>> AddCollectionAsync(List<Tbl_Payroll> payrollCollection)
        {
            var db = new Models.dataContext();
            List<Tbl_Payroll> newList = new List<Tbl_Payroll>();

            foreach (var item in payrollCollection)
            {
                try
                {
                    newList.Add(await ValidateRow(db, item));
                }
                catch (Exception)
                {
                    await Logger.Log("Saltando registro " + item.Name + item.LastName, Logger.LogTypes.Information);
                }
            }

            await db.SaveChangesAsync();

            db.Dispose();

            return newList;
        }


        static async Task<Tbl_Payroll> ValidateRow(Models.dataContext db, Tbl_Payroll row)
        {

            var thisMoment = DateTime.Now;

            try
            {
                var result = await Task.Run(() => (from t in db.Tbl_Payroll
                                                   where t.Name == row.Name
                                                   where t.LastName == row.LastName
                                                   select t).ToList());

                if (result.Count == 0)
                {

                    row.Created = thisMoment;
                    row.Modified = thisMoment;
                    row.Deleted = false;
                    row.Name = StringManager.UpperOnlyFirstLetter(row.Name);
                    row.LastName = StringManager.UpperOnlyFirstLetter(row.LastName);

                    db.Tbl_Payroll.Add(row);
                }
                else if (result.Count > 1)
                {
                    throw new Exception("Existe man de un registro igual al momento de guardar");
                }
                else
                {
                    var data = result.Single();
                    //result.Single().Modified = thisMoment;
                    //result.Single().Deleted = false;
                    data.Modified = thisMoment;
                    data.Deleted = false;
                    data.Name = row.Name.ToUpperInvariant();

                }

                return result.Single();

            }
            catch (Exception ex)
            {
                await Logger.Log("ValidateRow " + ex.Message, Logger.LogTypes.Information);
                throw ex;
            }



        }

        //static 
    }
}