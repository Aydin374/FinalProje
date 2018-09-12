using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dentist2.Filter
{
    public class AdminLevel:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isAdmin = Convert.ToBoolean(HttpContext.Current.Session["isAdmin"]);
            if (!isAdmin)
            {
                HttpContext.Current.Session["IsNotAdminError"] = "Siz bura gire bilmezsiniz";
                filterContext.Result = new RedirectResult("~/maneger/dashboard");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}