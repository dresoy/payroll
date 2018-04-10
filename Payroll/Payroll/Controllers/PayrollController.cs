using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Payroll.Models;
using Payroll.Models.DbModels;

namespace Payroll.Controllers
{
    public class PayrollController : Controller
    {
        private dataContext db = new dataContext();

        // GET: Payroll
        public async Task<ActionResult> Index()
        {
            List<Models.DbModels.Tbl_Payroll> payrolls = new List<Models.DbModels.Tbl_Payroll>();
            //return View(await db.Tbl_Payroll.ToListAsync());
            ViewBag.hasError = false;
            ViewBag.errorMessage = null;

            try
            {
                payrolls = await DAO.Paysheet.GetAllActiveAsync();
            }
            catch (Exception ex)
            {
                ViewBag.hasError = false;
                ViewBag.errorMessage = ex.Message;
            }



            return View(payrolls);
        }

        // GET: Payroll/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.hasError = false;
            ViewBag.errorMessage = null;
            Tbl_Payroll tbl_Payroll = null;

            try
            {

                tbl_Payroll = await DAO.Paysheet.GetActiveAsync((Guid)id);
                if (tbl_Payroll == null)
                {
                    throw new Exception("Registro no encontrado");
                }

            }
            catch (Exception ex)
            {
                ViewBag.hasError = false;
                ViewBag.errorMessage = ex.Message;
            }

            return View(tbl_Payroll);

        }

        // GET: Payroll/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payroll/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Role,Section,Name,LastName,Hours,Amount")] Tbl_Payroll tbl_Payroll)
        {

            if (ModelState.IsValid)
            {
                //    tbl_Payroll.Id = Guid.NewGuid();
                //    db.Tbl_Payroll.Add(tbl_Payroll);
                //    await db.SaveChangesAsync();
                //    return RedirectToAction("Index");


                List<Tbl_Payroll> newList = new List<Tbl_Payroll>();
                newList.Add(tbl_Payroll);

                try
                {
                    var result = await DAO.Paysheet.AddCollectionAsync(newList);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }



            return View(tbl_Payroll);
        }

        // GET: Payroll/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Payroll tbl_Payroll = await db.Tbl_Payroll.FindAsync(id);
            if (tbl_Payroll == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Payroll);
        }

        // POST: Payroll/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Role,Section,Name,LastName,Hours,Amount")] Tbl_Payroll tbl_Payroll)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(tbl_Payroll).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                try
                {
                    var result = await DAO.Paysheet.EditAsync(tbl_Payroll);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                }



            }
            return View(tbl_Payroll);
        }

        // GET: Payroll/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Tbl_Payroll tbl_Payroll = await db.Tbl_Payroll.FindAsync(id);
            //if (tbl_Payroll == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(tbl_Payroll);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.hasError = false;
            ViewBag.errorMessage = null;
            Tbl_Payroll tbl_Payroll = null;

            try
            {

                tbl_Payroll = await DAO.Paysheet.GetActiveAsync((Guid)id);
                if (tbl_Payroll == null)
                {
                    throw new Exception("Registro no encontrado");
                }

            }
            catch (Exception ex)
            {
                ViewBag.hasError = false;
                ViewBag.errorMessage = ex.Message;
            }

            return View(tbl_Payroll);
        }

        // POST: Payroll/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            //Tbl_Payroll tbl_Payroll = await db.Tbl_Payroll.FindAsync(id);
            //db.Tbl_Payroll.Remove(tbl_Payroll);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");

            ViewBag.hasError = false;
            ViewBag.errorMessage = null;

            try
            {
                var result = await DAO.Paysheet.DeleteAsync(id);

                if (result == null)
                {
                    throw new Exception("Registro no encontrado");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.hasError = false;
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
