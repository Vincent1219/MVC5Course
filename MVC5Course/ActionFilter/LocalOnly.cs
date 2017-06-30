using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilter
{
    public class LocalOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 非Local執行會回到首頁
            if ( !filterContext.RequestContext.HttpContext.Request.IsLocal) {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}