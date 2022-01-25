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
    public class SchemesController : Controller
    {
        // GET: Schemes
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Scheme
        public ActionResult Index()
        {
            return View(db.SchemeMasters.ToList());
        }

        // GET: Scheme/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchemeMaster schemeMaster = db.SchemeMasters.Find(id);
            if (schemeMaster == null)
            {
                return HttpNotFound();
            }
            return View(schemeMaster);
        }

        // GET: Scheme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scheme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchemeID,SchemeName,Createdby,Createddate,schemebit")] SchemeMaster schemeMaster)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                int year = Convert.ToInt32(date[0]);
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);

                DateTime Newdt = new DateTime(year, month, day);
                schemeMaster.Createddate = Newdt;

                db.SchemeMasters.Add(schemeMaster);
                db.SaveChanges();
                TempData["Message"] = "Scheme Create Successfully.";
                return RedirectToAction("Index");
            }

            return View(schemeMaster);
        }

        // GET: Scheme/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchemeMaster schemeMaster = db.SchemeMasters.Find(id);
            if (schemeMaster == null)
            {
                return HttpNotFound();
            }
            return View(schemeMaster);
        }

        // POST: Scheme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchemeID,SchemeName,Createdby,Createddate,schemebit")] SchemeMaster schemeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schemeMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schemeMaster);
        }

        // GET: Scheme/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchemeMaster schemeMaster = db.SchemeMasters.Find(id);
            if (schemeMaster == null)
            {
                return HttpNotFound();
            }
            return View(schemeMaster);
        }

        // POST: Scheme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchemeMaster schemeMaster = db.SchemeMasters.Find(id);
            db.SchemeMasters.Remove(schemeMaster);
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