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
    public class rangesController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/ranges
        public ActionResult Index()
        {
            return View(db.ranges.ToList());
        }

        // GET: Maneger/ranges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            range range = db.ranges.Find(id);
            if (range == null)
            {
                return HttpNotFound();
            }
            return View(range);
        }

        // GET: Maneger/ranges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/ranges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,text")] range range, HttpPostedFileBase icon)
        {
            if (icon.ContentType != "image/jpeg" && icon.ContentType != "image/png" && icon.ContentType != "image/gif")
            {
                return Content("Fayl qebul olunmur");
            }


            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + icon.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                icon.SaveAs(myfile);
                range.icon = filname;
                db.ranges.Add(range);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(range);
        }

        // GET: Maneger/ranges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            range range = db.ranges.Find(id);
            if (range == null)
            {
                return HttpNotFound();
            }
            return View(range);
        }

        // POST: Maneger/ranges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,icon,number,text")] range range,HttpPostedFileBase icon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(range).State = EntityState.Modified;
                if (icon != null)
                {
                    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + icon.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                    icon.SaveAs(myfile);
                    range.icon = filname;
                }
                else
                {
                    db.Entry(range).Property(r => r.icon).IsModified = false;
                }
                db.Entry(range).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(range);
        }

        // GET: Maneger/ranges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            range range = db.ranges.Find(id);
            if (range == null)
            {
                return HttpNotFound();
            }
            return View(range);
        }

        // POST: Maneger/ranges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            range range = db.ranges.Find(id);
            db.ranges.Remove(range);
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
