namespace Application.Data.Migrations
{
    using Application.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Application.Data.ApplicationDbContext>
    {
        static Random rand = new Random();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Application.Data.ApplicationDbContext context)
        {
            this.AddInitialCategories(context);
            this.AddInitialSubCategories(context);
        }

        private void AddInitialSubCategories(ApplicationDbContext context)
        {
            if (!context.SubCategories.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    var newCategory = new SubCategory
                    {
                        Name = "SubCategory " + i,
                        CategoryId = rand.Next(1, 5)
                    };
                    context.SubCategories.Add(newCategory);
                }
                context.SaveChanges();
            }
        }

        private void AddInitialCategories(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    var newCategory = new Category
                    {
                        Name = "Category " + i
                    };
                    context.Categories.Add(newCategory);
                }
                context.SaveChanges();
            }
        }
    }
}
