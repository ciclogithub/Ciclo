using System.Web.Mvc;

namespace Ciclo.Areas.Ciclo
{
    public class CicloAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ciclo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ciclo_default",
                "Ciclo/{controller}/{action}/{id}",
                new { controller = "Ciclo",  action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}