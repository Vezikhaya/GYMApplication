using GYMApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GYMApplication.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        PlanMaster objIPlanMaster;
        SchemeMaster objscheme;


        public PlanController()
        {
            objIPlanMaster = new PlanMaster();
            objscheme = new SchemeMaster();
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Plan
    //    [Authorize()]
        public ActionResult Index()
        {


            return View(db.PlanMasters.ToList());
        }

        // GET: Plan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // GET: Plan/Create
        public ActionResult Create()
        {
            ViewBag.SchemeID = new SelectList(db.SchemeMasters, "SchemeID", "SchemeName");

            return View();
        }

        // POST: Plan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlanID,PlanName,PlanAmount,ServicetaxAmout,ServiceTax,CreateDate,CreateUserID,ModifyDate,ModifyUserID,RecStatus,SchemeID,PeriodID,TotalAmout,ServicetaxNo")] PlanMaster planMaster)
        {
            if (ModelState.IsValid)
            {

                var plan = planMaster.PlanAmount;
                var cost = double.Parse(planMaster.ServiceTax);
                var total = cost  * Convert.ToDouble(plan);
                var total_Cost = total + Convert.ToDouble(plan);
                planMaster.TotalAmout = Convert.ToDecimal(total_Cost);
                planMaster.ServicetaxAmout =Convert.ToDecimal(total);
                var Name = (from v in db.SchemeMasters
                            where v.SchemeID == planMaster.SchemeID
                            select v.SchemeID).FirstOrDefault();


              
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                int year = Convert.ToInt32(date[0]);
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);

                DateTime Newdt = new DateTime(year, month, day);
                planMaster.CreateDate = Newdt;
                planMaster.ModifyDate = Newdt;
                planMaster.RecStatus = "A";
                TempData["notice"] = "Plan Created Successfully";


                db.PlanMasters.Add(planMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchemeID = new SelectList(db.SchemeMasters, "SchemeID", "SchemeName", planMaster.SchemeID);
            return View(planMaster);
        }

        // GET: Plan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // POST: Plan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanID,PlanName,PlanAmount,ServicetaxAmout,ServiceTax,CreateDate,CreateUserID,ModifyDate,ModifyUserID,RecStatus,SchemeID,PeriodID,TotalAmout,ServicetaxNo")] PlanMaster planMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planMaster);
        }

        // GET: Plan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMaster planMaster = db.PlanMasters.Find(id);
            if (planMaster == null)
            {
                return HttpNotFound();
            }
            return View(planMaster);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanMaster planMaster = db.PlanMasters.Find(id);
            db.PlanMasters.Remove(planMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
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
