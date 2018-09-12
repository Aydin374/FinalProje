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
    public class subscribesController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/subscribes
        public ActionResult Index()
        {
            return View(db.subscribes.ToList());
        }

        // GET: Maneger/subscribes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscribe subscribe = db.subscribes.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // GET: Maneger/subscribes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/subscribes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email")] subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                db.subscribes.Add(subscribe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscribe);
        }

        // GET: Maneger/subscribes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscribe subscribe = db.subscribes.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // POST: Maneger/subscribes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email")] subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscribe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscribe);
        }

        // GET: Maneger/subscribes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscribe subscribe = db.subscribes.Find(id);
            if (subscribe == null)
            {
                return HttpNotFound();
            }
            return View(subscribe);
        }

        // POST: Maneger/subscribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subscribe subscribe = db.subscribes.Find(id);
            db.subscribes.Remove(subscribe);
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
