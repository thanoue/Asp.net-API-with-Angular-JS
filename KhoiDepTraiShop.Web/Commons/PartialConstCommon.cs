using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Commons
{
    public static class PartialConstCommon
    {
        //main partial view
        private static string RootPartial = "~/Views/Partial/";
        public static string FooterPartial = RootPartial + "_Footer.cshtml";
        public static string HeaderPartial = RootPartial + "_Header.cshtml";
        public static string CategoriesPartial = RootPartial + "_Categories.cshtml";
        public static string ProdutItemPartial = RootPartial + "_ProductPartial.cshtml";
        public static string SmallProducListPartial = RootPartial + "_SmallProductListPartial.cshtml";
        public static string SpecialProductPartial = RootPartial + "_SpecialProductListPartial.cshtml";
        public static string FilterZonePartial = RootPartial + "_FilterZonePartial.cshtml";
        public static string ProductListPartial = RootPartial + "_ProductListPartial.cshtml";
        public static string ProductListNotIncludinhPagingPartial = RootPartial + "_ProductListNotIncludingPagingPartial.cshtml";

    }
}