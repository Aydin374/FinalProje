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
    public class SlidesController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/slides
        public ActionResult Index()
        {
            return View(db.slides.ToList());
        }

        // GET: Maneger/slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Maneger/slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,text,subtext")] slide slide,HttpPostedFileBase photo)
        {
            if(photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
            {
                return Content("fayl duz deyil");
            }
            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + photo.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                photo.SaveAs(myfile);
                slide.photo = filname;
                db.slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Maneger/slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Maneger/slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,photo,title,text,subtext")] slide slide,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
          
                if (photo != null)
                {
                    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + photo.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                    photo.SaveAs(myfile);
                    slide.photo = filname;
                }
                else
                {
                    db.Entry(slide).Property(s => s.photo).IsModified = false;
                }
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Maneger/slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            slide slide = db.slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Maneger/slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            slide slide = db.slides.Find(id);
            db.slides.Remove(slide);
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
