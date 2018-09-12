using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dentist2.Filter;
using dentist2.Models;

namespace dentist2.Areas.Maneger.Controllers
{
    [Auth]
    public class openinghoursController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/openinghours
        public ActionResult Index()
        {
            return View(db.openinghours.ToList());
        }

        // GET: Maneger/openinghours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            openinghour openinghour = db.openinghours.Find(id);
            if (openinghour == null)
            {
                return HttpNotFound();
            }
            return View(openinghour);
        }

        // GET: Maneger/openinghours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/openinghours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,startday,andday,starttime,andtime")] openinghour openinghour)
        {
            if (ModelState.IsValid)
            {
                db.openinghours.Add(openinghour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(openinghour);
        }

        // GET: Maneger/openinghours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            openinghour openinghour = db.openinghours.Find(id);
            if (openinghour == null)
            {
                return HttpNotFound();
            }
            return View(openinghour);
        }

        // POST: Maneger/openinghours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,startday,andday,starttime,andtime")] openinghour openinghour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openinghour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(openinghour);
        }

        // GET: Maneger/openinghours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            openinghour openinghour = db.openinghours.Find(id);
            if (openinghour == null)
            {
                return HttpNotFound();
            }
            return View(openinghour);
        }

        // POST: Maneger/openinghours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            openinghour openinghour = db.openinghours.Find(id);
            db.openinghours.Remove(openinghour);
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
