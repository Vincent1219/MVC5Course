using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {

        public override IQueryable<Product> All()
        {
            return base.All().Where(x => !x.Is刪除); //回傳統一呼叫的All 都會排除刪除資料
        }

        public IQueryable<Product> All(bool showAll)
        {
            if (showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public Product Get單筆資料ByProductId(int id)
        {
            return this.All().FirstOrDefault(x => x.ProductId == id);
        }

        public IQueryable<Product> 取得Product列表頁所有資料(bool active, bool showAll = false)
        {
            IQueryable<Product> all = this.All();

            if (showAll) {
                all = base.All();
            }

            return all.
                   Where(p => p.Active.HasValue && p.Active.Value == active).
                   OrderByDescending(p => p.ProductId).
                   Take(10);
        }

        public void Update(Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }

        public override void Delete(Product entity)
        {
            entity.Is刪除 = true;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}