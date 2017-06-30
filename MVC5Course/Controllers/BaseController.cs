using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModel;
using MVC5Course.ActionFilter;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public abstract class BaseController : Controller // 抽象類別 不會被執行
    {
        protected FabricsEntities db = new FabricsEntities();

        [LocalOnly]
        public ActionResult Debug()
        {
            return Content("HelloWorld！");
        }

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    // POST到找不到的ACTION 強迫回到首頁 <不建議此方法應用 除非有需求找不到導頁>
        //    this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
        //    // 直接ExecuteResult結果
        //    //
        //}
    }
}