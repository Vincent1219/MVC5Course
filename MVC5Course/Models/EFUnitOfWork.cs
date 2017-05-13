using System.Data.Entity;

namespace MVC5Course.Models
{
    /// <summary>
    /// 實際資料庫存取
    /// </summary>
	public class EFUnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; set; }

		public EFUnitOfWork()
		{
			Context = new FabricsEntities();
		}

		public void Commit()
		{
			Context.SaveChanges();
		}
		
        /// <summary>
        /// 
        /// </summary>
		public bool LazyLoadingEnabled
		{
			get { return Context.Configuration.LazyLoadingEnabled; }
			set { Context.Configuration.LazyLoadingEnabled = value; }
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.Configuration.ProxyCreationEnabled; }
			set { Context.Configuration.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Database.Connection.ConnectionString; }
			set { Context.Database.Connection.ConnectionString = value; }
		}
	}
}
