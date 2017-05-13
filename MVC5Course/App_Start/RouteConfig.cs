using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); // 什麼情況不要走路由
            //routes.IgnoreRoute("{resource}.aspx/{*pathInfo}"); // 什麼情況不要走路由 aspx混進MVC

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", //{controller}/{action}/{*id} 吃ID後所有任何資料 不包含?後面
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional // 可有可無
                }
            );
        }
    }
}
