using Application.Data.Repositories;
using Application.Models;
using Application.Models.Articles;
using Application.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class UoWData : IUoWData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public UoWData()
            : this(new ApplicationDbContext())
        {
        }

        public UoWData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }
        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }
        public IRepository<SubCategory> SubCategories
        {
            get { return this.GetRepository<SubCategory>(); }
        }

        public IRepository<Image> Images
        {
            get { return this.GetRepository<Image>(); }
        }

        public IRepository<Article> Articles
        {
            get { return this.GetRepository<Article>(); }
        }

        public IRepository<Page> Pages
        {
            get { return this.GetRepository<Page>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
