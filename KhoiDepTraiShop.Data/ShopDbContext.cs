﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using KhoiDepTraiShop.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;
using MySql.Data.Entity;

namespace KhoiDepTraiShop.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDbContext() : base("ShopConnectionMysql")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
   
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
    
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
    

        public DbSet<Tag> Tags { set; get; }

     

        public DbSet<Error> Errors { set; get; }

        public DbSet<ProductRating> ProductRatings { set; get; }

        public DbSet<ContactDetail> ContactDetails { set; get; }

        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public DbSet<AddressProvince> AddressProvinces { get; set; }
        public DbSet<AddressDistrict> AddressDistricts { get; set; }
        public DbSet<AddressWard> AddressWards { get; set; }

        public static ShopDbContext Create()
        {
            return new ShopDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");

            builder.Entity<ProductRating>().HasKey(i => new { i.UserId, i.RatedProductId });
            builder.Configurations.AddFromAssembly(assembly: Assembly.GetExecutingAssembly());

        }
    }
}
