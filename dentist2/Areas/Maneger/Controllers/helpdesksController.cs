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
    public class helpdesksController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/helpdesks
        public ActionResult Index()
        {
            return View(db.helpdesks.ToList());
        }

        // GET: Maneger/helpdesks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            helpdesk helpdesk = db.helpdesks.Find(id);
            if (helpdesk == null)
            {
                return HttpNotFound();
            }
            return View(helpdesk);
        }

        // GET: Maneger/helpdesks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/helpdesks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,location,aboutext,fblink,instalink,twitlink,dentalemergancytext,mapsource")] helpdesk helpdesk,HttpPostedFileBase dentalemergancyphto, HttpPostedFileBase logo)
        {

            if (dentalemergancyphto.ContentType != "image/jpeg" && dentalemergancyphto.ContentType != "image/png" && dentalemergancyphto.ContentType != "image/gif")
            {
                return Content("Fayl qebul olunmur");
            }
            if (ModelState.IsValid)
            {

                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + dentalemergancyphto.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                dentalemergancyphto.SaveAs(myfile);
                helpdesk.dentalemergancyphto = filname;
                string finame = DateTime.Now.ToString("yyyyMMddHHmmss") + dentalemergancyphto.FileName;
                var mfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), finame);
                logo.SaveAs(mfile);
                helpdesk.logo = finame;
                db.helpdesks.Add(helpdesk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(helpdesk);
        }

        // GET: Maneger/helpdesks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            helpdesk helpdesk = db.helpdesks.Find(id);
            if (helpdesk == null)
            {
                return HttpNotFound();
            }
            return View(helpdesk);
        }

        // POST: Maneger/helpdesks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,location,logo,aboutext,fblink,instalink,twitlink,dentalemergancyphto,dentalemergancytext,mapsource")] helpdesk helpdesk,HttpPostedFileBase dentalemergancyphto, HttpPostedFileBase logo)
        {
            if (ModelState.IsValid)
            {

                if (dentalemergancyphto == null && logo == null)
                {
                    db.Entry(helpdesk).Property(h => h.dentalemergancyphto).IsModified = false;
                    db.Entry(helpdesk).Property(h => h.logo).IsModified = false;
                }
                else
                {
                    if (dentalemergancyphto != null)
                    {
                        string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + dentalemergancyphto.FileName;
                        var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                        dentalemergancyphto.SaveAs(myfile);
                        helpdesk.dentalemergancyphto = filname;
                    }
                    if (logo != null)
                    {
                        string finame = DateTime.Now.ToString("yyyyMMddHHmmss") + logo.FileName;
                        var mfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), finame);
                        logo.SaveAs(mfile);
                        helpdesk.logo = finame;
                    }
                 
                }
                db.Entry(helpdesk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(helpdesk);
        }

        // GET: Maneger/helpdesks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            helpdesk helpdesk = db.helpdesks.Find(id);
            if (helpdesk == null)
            {
                return HttpNotFound();
            }
            return View(helpdesk);
        }

        // POST: Maneger/helpdesks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            helpdesk helpdesk = db.helpdesks.Find(id);
            db.helpdesks.Remove(helpdesk);
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
