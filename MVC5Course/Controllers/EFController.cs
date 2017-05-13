using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        // GET: EF
        public ActionResult Index()
        {
            var data = db.Product.AsQueryable().
                       Where(p => p.Is刪除 == false &&
                       p.Active == true).
                       OrderByDescending(p => p.ProductId).Take(20);

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

        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.product WHERE ProductId = @p0", id).FirstOrDefault();
            return View(data);
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

            // db.OrderLine.RemoveRange(product.OrderLine);

            
            product.Is刪除 = true;

            // db.Product.Remove(product);

            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveAll()
        {
            db.Database.ExecuteSqlCommand("DELETE FORM dbo.product");

            return View();
        }
    }
}