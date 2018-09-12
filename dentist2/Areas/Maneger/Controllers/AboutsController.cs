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
    public class AboutsController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/Abouts
        public ActionResult Index()
        {
            return View(db.Abouts.ToList());
        }

        // GET: Maneger/Abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: Maneger/Abouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/Abouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,video,title,text")] About about)
        {
            //if(video.ContentType != "video/3gpp" && video.ContentType != "video/3gpp2" && video.ContentType != "video/x-msvideo")
            //{
            //    return Content("fayl duz deyil");
            //}
            if (ModelState.IsValid)
            {
                //string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + video.FileName;
                //var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                //video.SaveAs(myfile);
                //about.video = filname;
                db.Abouts.Add(about);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(about);
        }

        // GET: Maneger/Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Maneger/Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,text,video")] About about)
        {
            if (ModelState.IsValid)
            {
                db.Entry(about).State = EntityState.Modified;
                //if (video != null)
                //{
                //    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + video.FileName;
                //    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                //    video.SaveAs(myfile);
                //}
                //else
                //{
                //    db.Entry(about).Property(a => a.video).IsModified = false;
                //}
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }

        // GET: Maneger/Abouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Maneger/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = db.Abouts.Find(id);
            db.Abouts.Remove(about);
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
