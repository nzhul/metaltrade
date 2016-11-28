using Application.Data.Migrations;
using Application.Models;
using Application.Models.Articles;
using Application.Models.Pages;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<SubCategory> SubCategories { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Page> Pages { get; set; }
	}
}
