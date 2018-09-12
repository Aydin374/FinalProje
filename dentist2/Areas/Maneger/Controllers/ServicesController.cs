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
    public class ServicesController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: Maneger/Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Maneger/Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,text")] Service service,HttpPostedFileBase icon)
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
                service.icon = filname;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Maneger/Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Maneger/Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,icon,title,text")] Service service,HttpPostedFileBase icon)
        {
            if (ModelState.IsValid)
            {
                if (icon != null)
                {
                    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + icon.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                    icon.SaveAs(myfile);
                    service.icon = filname;
                }
                else
                {
                    db.Entry(service).Property(s => s.icon).IsModified = false;
                }
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Maneger/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Maneger/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
