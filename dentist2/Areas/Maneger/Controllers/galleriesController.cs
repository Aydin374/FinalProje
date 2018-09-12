using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dentist2.Models;
using dentist2.Filter;

namespace dentist2.Areas.Maneger.Controllers
{
    [Auth]
    public class galleriesController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/galleries
        public ActionResult Index()
        {
            return View(db.galleries.ToList());
        }

        // GET: Maneger/galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Maneger/galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,phtosource")] gallery gallery, HttpPostedFileBase phtosource)
        {
            if (phtosource.ContentType != "image/jpeg" && phtosource.ContentType != "image/png" && phtosource.ContentType != "image/gif")
            {
                return Content("fayl duzgun deyil");
            }


            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + phtosource.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                phtosource.SaveAs(myfile);
                gallery.phtosource = filname;
                db.galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        [AdminLevel]
        // GET: Maneger/galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Maneger/galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminLevel]
        public ActionResult Edit([Bind(Include = "id,phtosource")] gallery gallery,HttpPostedFileBase photos)
        {
            if(photos.ContentType != "image/jpeg" && photos.ContentType != "image/png" && photos.ContentType != "image/gif")
            {
                return Content("fayl duzgun deyil");
            }
           
           
            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + photos.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), photos.FileName);
                photos.SaveAs(myfile);
                gallery.phtosource = photos.FileName;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Maneger/galleries/Delete/5
        [AdminLevel]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gallery gallery = db.galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Maneger/galleries/Delete/5
        [AdminLevel]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gallery gallery = db.galleries.Find(id);
            db.galleries.Remove(gallery);
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
