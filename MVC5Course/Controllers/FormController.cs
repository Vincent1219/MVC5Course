using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class FormController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: Form
        public ActionResult Index()
        {
            return View(db.Product.Where(x => !x.Is刪除).Take(10));
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = db.Product.Find(id);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            // model binding的ModelState永遠優先權最高
            var product = db.Product.Find(id);
            if (TryUpdateModel( // TryUpdateModel也有作model binding
                product,
                includeProperties: new string[] { "ProductName" })) // 只對ProductName作binding
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            product = db.Product.Find(id); // 就算這裡重新查詢資料也會被model binding的ModelState覆蓋回去
            return View(product);
        }
    }
}