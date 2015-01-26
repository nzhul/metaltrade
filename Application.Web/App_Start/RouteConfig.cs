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

            routes.MapMvcAttributeRoutes();

                routes.MapRoute(
                    name: "Pages",
                    url: "pages/{id}",
                    defaults: new { controller = "Pages", action = "Index", id = UrlParameter.Optional },
                    namespaces: new[] { "Application.Web.Controllers" });

                routes.MapRoute(
                name: "Home",
                url: "Home/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { "Application.Web.Controllers" }
                );

                routes.MapRoute(
                name: "Products",
                url: "products/{Slug}-{Id}",
                defaults: new { controller = "Products", action = "Details" },
                namespaces: new[] { "Application.Web.Controllers" }
                );

                routes.MapRoute(
                name: "SubCategories",
                url: "category/{CategoryId}/{CategorySlug}/{SubCategoryId}/{SubCategorySlug}",
                defaults: new { controller = "Products", action = "Index" },
                namespaces: new[] { "Application.Web.Controllers" }
                );

                routes.MapRoute(
                name: "Categories",
                url: "category/{CategoryId}/{CategorySlug}",
                defaults: new { controller = "Products", action = "Index" },
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
