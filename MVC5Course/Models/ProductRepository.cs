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
            return base.All().Where(x => !x.Is�R��); //�^�ǲΤ@�I�s��All ���|�ư��R�����
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

        public Product Get�浧���ByProductId(int id)
        {
            return this.All().FirstOrDefault(x => x.ProductId == id);
        }

        public IQueryable<Product> ���oProduct�C���Ҧ����(bool active, bool showAll = false)
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
            entity.Is�R�� = true;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}