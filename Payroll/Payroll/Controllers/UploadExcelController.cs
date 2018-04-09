using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class UploadExcelController : Controller
    {
        //// GET: UploadExcell
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult UploadExcelFile()
        {
            ViewBag.loading = false;
            ViewBag.hasError = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadExcelFile(HttpPostedFileBase file)
        {
            ViewBag.loading = true;
            try
            {
                if (file.ContentLength > 0)
                {
                    //string _FileName = Path.GetFileName(file.FileName);
                    //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    //file.SaveAs(_path);
                    DataSet ExcelDS = await Utilities.ExcelManager.ExcelReaderAsync(file.InputStream);



                }
                ViewBag.Message = "File Uploaded Successfully!!";

                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                ViewBag.hasError = true;
                ViewBag.loading = false;
                return View();
            }
        }



    }
}