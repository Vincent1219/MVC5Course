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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        //[HttpPost]
        [OutputCache(Duration = 5, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index(bool Active = true)
        {
            //var data = db.Product.OrderByDescending(p => p.ProductId).Take(10);
            var data = repo.取得Product列表頁所有資料( Active, showAll : false );

            //  ViewData.Model = data;
            // 相同View(data);

            //ViewData["productsdata"] = data;
            //ViewBag.productsdata = data;

            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id) // 資料寫入ACTION裡 => 模型係結
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost] // 區分上方同名的Action用
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Products/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, FormCollection form)
        //{
        //    // [Bind(Include = "ProductId,ProductName,Price,Active,Stock")]
        //    var product = repo.Get單筆資料ByProductId(id);
        //    // TryUpdateModel過程產生(ModelState.IsValid)
        //    if (TryUpdateModel<Product>(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" })) // 延遲驗證 - 拿MODEL來更新的話可以使用這方式 
        //                                          // 前端若無送進要修改的欄位 就會自動帶原先的欄位資料近來 不會為欄位類型預設值
        //    {
        //        //db.Entry(product).State = EntityState.Modified;
        //        //db.SaveChanges();
        //        repo.UnitOfWork.Commit();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductId(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //關閉驗證
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListProducts(SearchProducts searchCondition)
        {
            GetListProducts(searchCondition);
            return View();
        }

        private void GetListProducts(SearchProducts search)
        {
            var data = repo.取得Product列表頁所有資料(true);
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(search.productName))
                {
                    data = data.Where(x => x.ProductName.Contains(search.productName));
                }
                if (search.StockStart.HasValue && search.stockEnd.HasValue)
                {
                    data = data.Where(x => x.Stock >= search.StockStart && x.Stock <= search.stockEnd);
                }
            }
            ViewData.Model = data.
                             Select(p => new ProductLiteVM
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 Price = p.Price,
                                 Stock = p.Stock
                             }).Take(10);
        }

        [HttpPost]
        public ActionResult BatchUpdate(SearchProducts search, ProductBatchUpdateVM[] items)
        {
           if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var prod = db.Product.Find(item.ProductId);
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                return RedirectToAction("ListProducts");
            }
            //GetListProducts(search);
            return View("ListProducts");
        }

        public ActionResult CreateProducts()
        {
            return View();
        }

        [HttpPost] // Bind Include 只接收設定的屬性資料，其餘預設值，但 IsValid 會因為無Include 但該來屬性又有Required => 造成false
        public ActionResult CreateProducts([Bind(Include = "ProductName,Price,Stock")] ProductLiteVM data)
        {
            if (ModelState.IsValid) {
                // 驗鄭成功後 藏信訊在TempData傳給ListProducts的View做顯示
                TempData["CreateProduct_Result"] = "商品新增成功";
                return RedirectToAction("ListProducts");
            }
            return View(data);
        }
    }
}
