using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HW03_u20679484
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "CombinedIndex", id = UrlParameter.Optional }
            );
            routes.MapRoute(
       name: "MaintainDefaultAction",
       url: "Maintain/{action}/{id}",
       defaults: new { controller = "Maintain", action = "MaintainIndex", id = UrlParameter.Optional }
   );

        }
    }
}
