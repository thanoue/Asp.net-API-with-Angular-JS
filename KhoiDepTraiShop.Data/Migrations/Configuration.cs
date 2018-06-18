namespace KhoiDepTraiShop.Data.Migrations
{
    using KhoiDepTraiShop.Common;
    using KhoiDepTraiShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<KhoiDepTraiShop.Data.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override   void Seed(KhoiDepTraiShop.Data.ShopDbContext context)
        {


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

            //  SetroductRating(context);
            //SetPrice(context);

            //var ratings = context.ProductRatings.ToList();
            //context.ProductRatings.RemoveRange(ratings);
            //context.SaveChanges();
            //deleteProduct(context);
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Products', RESEED, 0)");

            //var products = context.Products.ToList();
            //foreach (var pr in products)
            //{
            //    Random a = new Random();
            //    pr.Quantity = a.Next(0, pr.Id * (a.Next(1,50)));

            //}

            //var pros = context.Products.Where(p => p.Name.Contains("..."));
            //foreach (var pr in pros)
            //    pr.Name = pr.Name.Replace("...", "");
            //context.SaveChanges();

            //var cate1 = new ProductCategory()
            //{
            //    Name = "Rau-Củ-Quả",
            //    Alias = StringUtility.ToUnsignString("Rau-Củ-Quả"),
            //    CreatedDate = DateTime.Now,
            //    Status = true
            //};
            //var cate2 = new ProductCategory()
            //{
            //    Name = "Thực phẩm",
            //    Alias = StringUtility.ToUnsignString("Thực phẩm"),
            //    CreatedDate = DateTime.Now,
            //    Status = true
            //};
            //var cate3 = new ProductCategory()
            //{
            //    Name = "Trái cây",
            //    Alias = StringUtility.ToUnsignString("Trái cây"),
            //    CreatedDate = DateTime.Now,
            //    Status = true
            //};
            //var cate4 = new ProductCategory()
            //{
            //    Name = "Sản phẩm khác",
            //    Alias = StringUtility.ToUnsignString("Sản phẩm khác"),
            //    CreatedDate = DateTime.Now,
            //    Status = true
            //};
            //context.ProductCategories.AddRange(new List<ProductCategory>() { cate1, cate2, cate3, cate4 });
            //context.SaveChanges();

            //var cateList = context.ProductCategories.ToList();
            //foreach (var cate in cateList)
            //{
            //    if (cate.Id >= 15)
            //        cate.ParentId = null;

            //}
            //context.SaveChanges();

            //    var nongSanSach = new Tag() { Name = "nông sản sạch", Id = StringUtility.ToUnsignString("nông sản sạch"), Type = TagType.Product };
            //    var douong = new Tag() { Name = "đồ uống sạch", Id = StringUtility.ToUnsignString("đồ uống sạch"), Type = TagType.Product };
            //    var thucPhamAnNhanh = new Tag() { Name = "thực phẩm đóng hộp", Id = StringUtility.ToUnsignString("thực phẩm đóng hộp"), Type = TagType.Product };
            //    var ruou = new Tag() { Name = "rượu vang", Id = StringUtility.ToUnsignString("rượu vang"), Type = TagType.Product };
            //    var sanPhamKho = new Tag() { Name = "dản phẩm khô", Id = StringUtility.ToUnsignString("sản phẩm khô"), Type = TagType.Product };
            //    var thucPhamTuoiSong = new Tag() { Name = "thực phẩm tươi sống", Id = StringUtility.ToUnsignString("thực phẩm tươi sống"), Type = TagType.Product };
            //    var hangNhapKhau = new Tag() { Name = "hàng nhập khẩu", Id = StringUtility.ToUnsignString("hàng nhập khẩu"), Type = TagType.Product };
            //    var hangNoiDia = new Tag() { Name = "hàng nội địa", Id = StringUtility.ToUnsignString("hàng nội địa"), Type = TagType.Product };
            //    var dacSan = new Tag() { Name = "đặc sản", Id = StringUtility.ToUnsignString("đặc sản"), Type = TagType.Product };
            //    var sanPhamtrungBay = new Tag() { Name = "sản phẩm trưng bày", Id = StringUtility.ToUnsignString("sản phẩm trưng bày"), Type = TagType.Product };
            //    var sanPhamDongChai = new Tag() { Name = "sản phẩm đóng chai", Id = StringUtility.ToUnsignString("sản phẩm đóng chai"), Type = TagType.Product };

            ////    context.Tags.AddRange(new List<Tag>() { nongSanSach, douong, thucPhamAnNhanh, ruou, sanPhamKho, thucPhamTuoiSong, hangNhapKhau, hangNoiDia, sanPhamDongChai, dacSan, sanPhamtrungBay });
            // //   context.SaveChanges();

            //    var products = context.Products.ToList();
            //    foreach (var product in products)
            //    {
            //        if (product.CategoryId<=3)
            //        {
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = thucPhamTuoiSong.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = dacSan.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = hangNoiDia.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = nongSanSach.Id });
            //            product.Tags = "nông sản sạch,thực phẩm tươi sống,hàng nội địa";
            //            context.SaveChanges();
            //        }
            //        if (product.CategoryId == 14 || product.CategoryId == 5 || product.CategoryId == 6)
            //        {
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = sanPhamKho.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = dacSan.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = hangNoiDia.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = thucPhamAnNhanh.Id });
            //            product.Tags = sanPhamKho.Name + "," + dacSan.Name + "," + hangNoiDia.Name + "," + thucPhamAnNhanh.Name;
            //            context.SaveChanges();
            //        }
            //        if (product.CategoryId == 7 || product.CategoryId == 8 || product.CategoryId == 13)
            //        {
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = sanPhamDongChai.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = thucPhamAnNhanh.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = sanPhamKho.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = sanPhamtrungBay.Id });
            //            product.Tags = sanPhamDongChai.Name + "," + thucPhamAnNhanh.Name + "," + sanPhamKho.Name + "," + sanPhamtrungBay.Name;
            //            context.SaveChanges();
            //        }
            //        if (product.CategoryId == 11 || product.CategoryId == 13)
            //        {
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = hangNhapKhau.Id });
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = dacSan.Id });
            //            product.Tags = hangNhapKhau.Name + "," + dacSan.Name;
            //            context.SaveChanges();
            //        }
            //        if (product.CategoryId == 10 || product.CategoryId == 11)
            //        {
            //            context.ProductTags.Add(new ProductTag() { ProductId = product.Id, TagId = nongSanSach.Id });
            //            product.Tags = product.Tags + ',' + nongSanSach.Name;
            //            context.SaveChanges();
            //        }
            //    }


            //var contact1 = new ContactDetail()
            //{
            //    Name = "Cửa hàng thực phẩm SunFood chi nhánh 1",
            //    Address = "101 Dương Đình Hội, Phước Long B, Quận 9, Hồ Chí Minh, Vietnam",
            //    Email = "sunfoodawesome@gmai.com",
            //    Lat = 10.819767,
            //    Lng = 106.7775095,
            //    Phone = "+84 974 395 735",
            //    Status = true,
            //    Website = "sunfood.com.vn"
            //};
            //var contact2 = new ContactDetail()
            //{
            //    Name = "Cửa hàng thực phẩm SunFood chi nhánh trung tâm ",
            //    Address = "194 Pasteur, Phường 6, Quận 3, Hồ Chí Minh, Vietnam",
            //    Email = "sunfood_none@gmai.com",
            //    Lat = 10.7816092,
            //    Lng = 106.6924488,
            //    Phone = "+84 974 395 735",
            //    Status = true,
            //    Website = "sunfood.com.vn"
            //};
            //var contact3 = new ContactDetail()
            //{
            //    Name = "Cửa hàng thực phẩm SunFood ngoại thành",
            //    Address = "16A, Phú Châu, Phường Tam Phú, Quận Thủ Đức, Thành Phố Hồ Chí Minh, Thành Phố Hồ Chí Minh, Tam Phú, Thủ Đức, Hồ Chí Minh, Vietnam",
            //    Email = "sunfood_none@gmai.com",
            //    Lat = 10.8633753,
            //    Lng = 106.7434113,
            //    Phone = "+84 974 395 735",
            //    Status = true,
            //    Website = "sunfood.com.vn"
            //};
            //context.ContactDetails.AddRange(new List<ContactDetail> { contact1, contact2, contact3 });
            var products = context.Products.ToList();
            foreach(var product in products)
            {
                var a = new Random(); var rand = a.Next(7, 10);
                product.OriginalPrice = product.Price - product.Price / rand;
            }
            context.SaveChanges();
        }

        private async Task deleteUser(ShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = await manager.FindByEmailAsync("khoikhaguitar.vl@gmail.com");
            await manager.DeleteAsync(user);
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
            var products = context.Products.ToList().Where(p => p.ViewCount == null);
            foreach (var product in products)
            {
                Random a = new Random();
                product.ViewCount = a.Next(1000, 5000);
                context.SaveChanges();
            }
        }

        private void SetPrice(ShopDbContext context)
        {
            var products = context.Products.ToList();
            foreach (var product in products)
            {
                Random a = new Random();
                if (product.Price <= 0)
                    product.Price = Convert.ToDecimal(a.Next(20000, 100000));
                product.PromotionPrice = product.Price - product.Price * Convert.ToDecimal((Convert.ToDecimal(a.Next(1, 9)) / 11));
                context.SaveChanges();
            }

        }


    }
}
