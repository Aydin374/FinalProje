using System.Web.Mvc;

namespace dentist2.Areas.Maneger
{
    public class ManegerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Maneger";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Maneger_default",
                "maneger/{controller}/{action}/{id}",
                new { controller = "Home", action ="Index", id = UrlParameter.Optional } ,
                new[] { "dentist2.Areas.Maneger.Controllers" }
            );
        }
    }
}