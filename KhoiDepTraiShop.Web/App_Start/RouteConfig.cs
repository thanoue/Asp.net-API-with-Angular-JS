using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KhoiDepTraiShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            

            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Contact", action = "ContactPage" },
              namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

            );

            routes.MapRoute(
              name: "About",
              url: "gioi-thieu.html",
              defaults: new { controller = "Static", action = "About", id = UrlParameter.Optional },
              namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

            );

            routes.MapRoute(
                 name: "Login",
                 url: "dang-nhap",
                 defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                 namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

            );

            routes.MapRoute(
                 name: "Checkout",
                 url: "chitiet-giohang",
                 defaults: new { controller = "Order", action = "CheckOut", id = UrlParameter.Optional },
                 namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

            );

            routes.MapRoute(
                name: "ProductDetails",
                url: "chitiet-sanpham-{productId}",
                defaults: new { controller = "Product", action = "ProductDetail", productId = UrlParameter.Optional },
                namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

            );

            routes.MapRoute(
               name: "TagsDetails",
               url: "danhsach-sanpham-theotag-{tagId}",
               defaults: new { controller = "Product", action = "ProductListByTag", tagId = UrlParameter.Optional },
               namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

           );

            routes.MapRoute(
              name: "ProductCategoryDetail",
              url: "danhsach-sanpham-{categoryId}",
              defaults: new { controller = "Product", action = "CategoryDetail", categoryId = UrlParameter.Optional },
              namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }

          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",Id = UrlParameter.Optional},
                 namespaces: new string[] { "KhoiDepTraiShop.Web.Controllers" }
            );
        }
    }
}
