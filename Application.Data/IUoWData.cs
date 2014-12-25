using Application.Data.Repositories;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public interface IUoWData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<SubCategory> SubCategories { get; }
        IRepository<Image> Images { get; }

        int SaveChanges();
    }
}
