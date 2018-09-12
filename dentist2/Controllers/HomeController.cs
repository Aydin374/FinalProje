using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dentist2.Models;

namespace dentist2.Controllers
{
    public class HomeController : Controller
    {
        medicalEntities db = new medicalEntities();
        public ActionResult Index()
        {
            ViewHome model = new ViewHome();
            model.About = db.Abouts.FirstOrDefault();
            model.Slide = db.slides.ToList();
            model.icon = db.icons.ToList();
            model.service = db.Services.ToList();
            model.range = db.ranges.ToList();
            model.news = db.news.ToList();
            model.team = db.teams.ToList();
            model.gallery = db.galleries.ToList();
            model.helpdesk = db.helpdesks.FirstOrDefault();
            model.subscribe = db.subscribes.ToList();
            return View(model);
        }

       
    }
}