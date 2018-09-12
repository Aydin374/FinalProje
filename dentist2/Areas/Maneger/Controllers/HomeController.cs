using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using dentist2.Filter;
using dentist2.Models;

namespace dentist2.Areas.Maneger.Controllers
{
  
    public class HomeController : Controller
    {
        medicalEntities db = new medicalEntities();
        // GET: Maneger/Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            Admin adm = db.Admins.FirstOrDefault(a => a.Email == Email);
            if (adm != null)
            {
                if (Crypto.VerifyHashedPassword(adm.Password, Password))
                {
                    Session["Login"] = true;
                    Session["IsAdmin"] = adm.isAdmin;
                    Session["IsNotAdminError"] = null;
                    return RedirectToAction("index", "dashboard");
                }
            }

            Session["LoginError"] = "E-poct ve ya wifre yanliwdir";
            return RedirectToAction("index");


            //Admin admin = new Admin();
            //admin.Email = Email;
            //admin.Password = Crypto.HashPassword(Password);
            //db.Admins.Add(admin);
            //db.SaveChanges();
            //return RedirectToAction("index");
        }
    }
}