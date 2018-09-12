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
    public class iconsController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/icons
        public ActionResult Index()
        {
            return View(db.icons.ToList());
        }

        // GET: Maneger/icons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            icon icon = db.icons.Find(id);
            if (icon == null)
            {
                return HttpNotFound();
            }
            return View(icon);
        }

        // GET: Maneger/icons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/icons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,icon1,title,text")] icon icon,HttpPostedFileBase icon1)
        {
            if (icon1.ContentType != "image/jpeg" && icon1.ContentType != "image/png" && icon1.ContentType != "image/gif")
            {
                return Content("Fayl qebul olunmur");
            }
            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + icon1.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                icon1.SaveAs(myfile);
                icon.icon1 = filname;
                db.icons.Add(icon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(icon);
        }

        // GET: Maneger/icons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            icon icon = db.icons.Find(id);
            if (icon == null)
            {
                return HttpNotFound();
            }
            return View(icon);
        }

        // POST: Maneger/icons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,icon1,title,text")] icon icon,HttpPostedFileBase icon1)
        {
            if (ModelState.IsValid)
            {
                if(icon1 != null)
                {
                    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + icon1.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                    icon1.SaveAs(myfile);
                    icon.icon1 = filname;
                }
                else
                {
                    db.Entry(icon).Property(i => i.icon1).IsModified = false;
                }
                db.Entry(icon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(icon);
        }

        // GET: Maneger/icons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            icon icon = db.icons.Find(id);
            if (icon == null)
            {
                return HttpNotFound();
            }
            return View(icon);
        }

        // POST: Maneger/icons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            icon icon = db.icons.Find(id);
            db.icons.Remove(icon);
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
