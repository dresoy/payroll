using Payroll.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public async Task<ActionResult> IndexAsync()
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
                Task log = Logger.Log("No se puede mostrar el reporte", Logger.LogTypes.Error, ex);
                ViewBag.hasError = true;
            }
            finally { ViewBag.loading = false; }

            return View(r);
        }

        public async Task<ActionResult> LogsAsync()
        {
            var r = new List<Logger.LogResponseObject>();
            ViewBag.loading = true;
            ViewBag.hasError = false;

            try
            {
                r = await Logger.GetLogs();
            }
            catch (Exception ex)
            {
                ViewBag.hasError = true;

                Task log = Logger.Log("Could not get the logs", Logger.LogTypes.Error, ex);

            }
            finally { ViewBag.loading = false; }

            return View(r);
        }

    }
}