using ExcelDataReader;
using Payroll.Models.DbModels;
using Payroll.Utilities;
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
            await Logger.Log("Iniciando lectura de archivo de Excel", Logger.LogTypes.Information);
            try
            {
                if (file.ContentLength > 0)
                {
                    //string _FileName = Path.GetFileName(file.FileName);
                    //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    //file.SaveAs(_path);
                    DataTableCollection fileTables = await Utilities.ExcelManager.ExcelReaderAsync(file.InputStream);

                    if (fileTables.Count == 0) { throw new Exception("Archivo sin hojas"); }

                    foreach (System.Data.DataTable table in fileTables)
                    {
                        List<Models.DbModels.Tbl_Payroll> newCollection = new List<Models.DbModels.Tbl_Payroll>();
                        for (int i = 1; i < table.Rows.Count; i++)
                        {
                            var row = table.Rows[i].ItemArray;


                            Tbl_Payroll n = await DAO.Converter.ToPayrollModelAsync(
                                     row[0].ToString(), row[1].ToString(), row[2].ToString(),
                                     row[3].ToString(), row[4].ToString(), row[5].ToString()
                                  );

                            newCollection.Add(n);
                        }

                        await DAO.Paysheet.AddCollectionAsync(newCollection);
                    }


                }
                //ViewBag.Message = "File Uploaded Successfully!!";

                //return View();
                return RedirectToAction("Index", "Payroll");
            }
            catch (Exception ex)
            {
                await Logger.Log("Subir archivo de Excel " + ex.Message, Logger.LogTypes.Error);
                ViewBag.Message = "Error al leer archivo, puede ser por tipo incorrecto o formato erroneo";
                ViewBag.hasError = true;
                ViewBag.loading = false;
                return View();
            }
        }



    }
}