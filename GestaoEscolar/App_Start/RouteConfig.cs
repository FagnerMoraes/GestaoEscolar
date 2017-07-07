using System.Web.Mvc;
using System.Web.Routing;

namespace GestaoEscolar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Matricula",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Matricula", action = "GerenciarMatriculas", id = UrlParameter.Optional }
                );
        }
    }
}