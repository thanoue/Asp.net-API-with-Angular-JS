using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Infrastructure.Core
{
    public class PaginationSet <T>
    {
        public int Page { get; set; }
        public int Count
        {
            get
            {
                return Items != null ? Items.Count() : 0;
            }
        }
        public int TotalPages { set; get; }
        public int TotalRow { set; get; }
        public IEnumerable<T>Items { get; set; }
    }
}