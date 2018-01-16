using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class ProductTagViewModel
    {      
        public int ProductId { set; get; }      
        public string TagId { set; get; }      
        public virtual ProductViewModel Product { set; get; }
        public virtual TagViewModel Tag { set; get; }
    }
}