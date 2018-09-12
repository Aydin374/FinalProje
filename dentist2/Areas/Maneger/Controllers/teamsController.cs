using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dentist2.Models;

namespace dentist2.Areas.Maneger.Controllers
{
    public class teamsController : Controller
    {
        private medicalEntities db = new medicalEntities();

        // GET: Maneger/teams
        public ActionResult Index()
        {
            return View(db.teams.ToList());
        }

        // GET: Maneger/teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Maneger/teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maneger/teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,properties,text")] team team,HttpPostedFileBase photo)
        {
            if (photo.ContentType != "image/jpeg" && photo.ContentType != "image/png" && photo.ContentType != "image/gif")
            {
                return Content("Fayl qebul olunmur");
            }


            if (ModelState.IsValid)
            {
                string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + photo.FileName;
                var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                photo.SaveAs(myfile);
                team.photo = filname;
                db.teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Maneger/teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Maneger/teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,properties,text,photo")] team team,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                
                if (photo != null)
                {
                    string filname = DateTime.Now.ToString("yyyyMMddHHmmss") + photo.FileName;
                    var myfile = System.IO.Path.Combine(Server.MapPath("~/Uploads"), filname);
                    photo.SaveAs(myfile);
                    team.photo = filname;
                }
                else
                {
                    db.Entry(team).Property(t => t.photo).IsModified = false;
                }
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Maneger/teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Maneger/teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            team team = db.teams.Find(id);
            db.teams.Remove(team);
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
