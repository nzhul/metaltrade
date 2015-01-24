using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RiaLibrary.Web;

namespace Application.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoutes();

            routes.MapRoute(
                name: "Pages",
                url: "Pages/{id}",
                defaults: new { controller = "Pages", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Application.Web.Controllers" });

                routes.MapRoute(
                name: "Products",
                url: "{Slug}-{Id}",
                defaults: new { controller = "Products", action = "Details" },
                namespaces: new[] { "Application.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Application.Web.Controllers" });
        }
    }
}
