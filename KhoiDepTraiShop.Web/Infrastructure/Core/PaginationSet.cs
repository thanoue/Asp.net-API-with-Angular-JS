using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Infrastructure.Core
{
    public class PaginationSet <T>
    {
        public PaginationSet()
        {
            Items = new List<T>();
        }
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
        public IList<T>Items { get; set; }
        public int MaxPage { get;  set; }
    }
}