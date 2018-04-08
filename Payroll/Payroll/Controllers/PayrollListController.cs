using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class PayrollListController : Controller
    {
        // GET: PayrollList
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {

            List<Models.DbModels.Tbl_Payroll> payrolls = new List<Models.DbModels.Tbl_Payroll>();

            //pl.Add(new Models.DbModels.Tbl_Payroll
            //{
            //    Id = new Guid(),
            //    Name = "Some Name",
            //    LastName = "Any Last Name",

            //});

            ViewBag.hasError = false;
            ViewBag.errorMessage = null;

            try
            {
                payrolls = await DAO.Payroll.GetAllActive();
            }
            catch (Exception ex)
            {
                ViewBag.hasError = false;
                ViewBag.errorMessage = ex.Message;
            }
           


            return View(payrolls);
        }

    }
}
