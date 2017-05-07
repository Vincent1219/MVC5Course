using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var data = db.Product.AsQueryable().
                       Where(p => p.Active == true).
                       OrderByDescending(p => p.ProductId);

            //var data1 = all.Where(p => p.ProductId == 1);
            //var data2 = all.FirstOrDefault(p => p.ProductId == 1);
            //var data3 = db.Product.Find(1);

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product p)
        {
            if (ModelState.IsValid)
            {
                var product = db.Product.Find(id);
                product.ProductName = p.ProductName;
                product.Price = p.Price;
                product.Stock = p.Stock;
                product.Active = p.Active;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}