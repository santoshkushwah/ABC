using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_Using_Angular
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("");
            routes.IgnoreRoute("EmployeeInfo/{*.}");
           // routes.MapPageRoute("Default", "{*.}", "~/index.html");
            routes.AppendTrailingSlash = true;
        }
    }
}
