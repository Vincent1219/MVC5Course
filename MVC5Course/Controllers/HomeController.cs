using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [SharedViewBag]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            throw new ArgumentException("Error");
            return View();
        }

        [SharedViewBag]
        public ActionResult PartialAbout()
        {

            //ViewBag.Message = "Your application description page.";

            // $.get('/Home/PartialAbout', function(data) { alert (data); })

            if ( Request.IsAjaxRequest()) { // 擴充方法 MVC專用 判斷AJAX 
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult SomeAction()
        {
            // return Content("<script>alert('建立成功！');</script>");
            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult GetFile()
        {
            // 第三個屬性 強迫下載
            return File(Server.MapPath("~/Content/wannacry.png"), "image/png", "NewName.png");
        }

        public ActionResult GetJson() // JSON 預設不接受GET
        {
            // $.get('/Home/GetJson', function(data) { Console.log(data); })
            db.Configuration.LazyLoadingEnabled = false; // 關閉延遲載入<循環參考>
            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Unknown()
        {
            return View();
        }

        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;
            return View();
        }

        public ActionResult RazorTest()
        {
            ViewData.Model = new int[] { 1, 2, 3, 4, 5 };
            return PartialView();
        }
    }
}