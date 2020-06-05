using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Do_an_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // sigin

            routes.MapRoute(
              name: "signin",
              url: "Đăng-nhập",
              defaults: new { controller = "Account", action = "signin", id = UrlParameter.Optional }
          );

            //Đăng ký 
            routes.MapRoute(
            name: "signup",
            url: "Đăng-ký",
            defaults: new { controller = "Account", action = "signup", id = UrlParameter.Optional }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
