using dentist2.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentist2.Areas.Maneger.Controllers
{
    [Auth]
    public class DashboardController : Controller
    {
        // GET: Maneger/Dashboard
        public ActionResult Index() 
        {
          
            return View();
        }
    }
}