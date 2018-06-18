
using KhoiDepTraiShop.Web.Commons;
using KhoiDepTraiShop.Web.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class CartItemViewModel
    {
        public CartItemViewModel()
        {
            Quantity = new NumbericUpDown();
        }        
        public string ProductImage { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [UIHint(TemplateConst.VNCurrencyDisPlay)]
        public decimal Price { get; set; }

      //  public int Quantity { get; set; }
        public string AddToCartTime { get; set; }

        [UIHint(TemplateConst.VNCurrencyDisPlay)]
        public decimal SubTotal
        {
            get
            {
                return Price * Quantity.Value;
            }            
        }

        public NumbericUpDown Quantity { get; set; }

    }
}