namespace KhoiDepTraiShop.Data.Migrations
{
    using KhoiDepTraiShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KhoiDepTraiShop.Data.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KhoiDepTraiShop.Data.ShopDbContext context)
        {

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tedu",
            //    Email = "tedu.international@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            //CreateProductCategorySample(context);
            //deleteProductCategory(context);
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Products', RESEED, 0)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ProductCategories', RESEED, 0)");

            // SetRandomViewCount(context);
           SetroductRating(context);

            //var ratings = context.ProductRatings.ToList();
            //context.ProductRatings.RemoveRange(ratings);
            //context.SaveChanges();
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ProductRatings', RESEED, 0)");
        }
        private void CreateProductCategorySample(KhoiDepTraiShop.Data.ShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }
        private void deleteProduct(ShopDbContext context)
        {
            var products = context.Products.ToList();
            context.Products.RemoveRange(products);
            context.SaveChanges();
        }

        private void deleteProductCategory(ShopDbContext context)
        {
            var products = context.ProductCategories.ToList();
            context.ProductCategories.RemoveRange(products);
            context.SaveChanges();
        }

        private void SetRandomViewCount(ShopDbContext context)
        {
            var products = context.Products.ToList().Where(p => p.Id >= 50);
            foreach (var product in products)
            {
                Random a = new Random();
                product.ViewCount = a.Next(9999, 15000);
                context.SaveChanges();
            }
        }

        private void SetPrice(ShopDbContext context)
        {
            var products = context.Products.ToList();
            foreach (var product in products)
            {
                Random a = new Random();
                if (product.Price == 0)
                    product.Price = Convert.ToDecimal(a.Next(20000, 100000));
                product.PromotionPrice = product.Price - product.Price * Convert.ToDecimal((Convert.ToDecimal(a.Next(1, 9)) / 11));
                context.SaveChanges();
            }

        }
        private void SetroductRating(ShopDbContext context)
        {
            var products = context.Products.Where(p=>p.Id % 3 ==0 || p.Id % 5 ==0).ToList();
            var Rand = new Random();
            foreach (var product in products)
            {
                List<ProductRating> Ratings = new List<ProductRating>()
                {
                   new ProductRating{ProductId = product.Id,RatingScore = Rand.Next(1,6),RatingPeopleName = "Tran Kha",RatingContent ="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",RatingTime =DateTime.Now,Status = Common.ProductRatingStatus.Waiting},
                   new ProductRating{ProductId = product.Id,RatingScore = Rand.Next(1,6),RatingPeopleName = "Tran Khoi",RatingContent ="sssssssssssssssssssssssssssssssssssssssssss",RatingTime =DateTime.Now,Status = Common.ProductRatingStatus.Waiting},
                   new ProductRating{ProductId = product.Id,RatingScore = Rand.Next(1,6),RatingPeopleName = "Tran Nug",RatingContent ="ddddddddddddddddddddddddddddddddddddddddddddddddd",RatingTime =DateTime.Now,Status = Common.ProductRatingStatus.Waiting},
                   new ProductRating{ProductId = product.Id,RatingScore = Rand.Next(1,6),RatingPeopleName = "Tran aha",RatingContent ="ddddddddddddddddddddddddddddddddddddddddddddddddddddd",RatingTime =DateTime.Now,Status = Common.ProductRatingStatus.Waiting},
                  
                };
                context.ProductRatings.AddRange(Ratings);
              
            }
            context.SaveChanges();
        }

    }
}
