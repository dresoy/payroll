using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            DAO.Reports.Payroll.EmployeesByTypeModel r = null;
            ViewBag.loading = true;
            ViewBag.hasError = false;
            try
            {
                r = await DAO.Reports.Payroll.GetEmployeesByTypeAsync();
                r.AverageAmountPerHour = Math.Round(r.AverageAmountPerHour, 2);
            }
            catch (Exception ex)
            {
                await Logger.Log("No se puede mostrar el reporte", Logger.LogTypes.Error, ex);
                ViewBag.hasError = true;
            }
            finally { ViewBag.loading = false; }

            return View(r);
        }
    }
}