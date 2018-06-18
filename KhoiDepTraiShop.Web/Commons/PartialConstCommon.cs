using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Commons
{
    public  class PartialConstCommon
    {
        //main partial view
        private const string RootPartial = "~/Views/Partial/";
        public const string FooterPartial = RootPartial + "_Footer.cshtml";
        public const string HeaderPartial = RootPartial + "_Header.cshtml";
        public const string CategoriesPartial = RootPartial + "_Categories.cshtml";
        public const string ProdutItemPartial = RootPartial + "_ProductPartial.cshtml";
        public const string SmallProducListPartial = RootPartial + "_SmallProductListPartial.cshtml";
        public const string SpecialProductPartial = RootPartial + "_SpecialProductListPartial.cshtml";
        public const string FilterZonePartial = RootPartial + "_FilterZonePartial.cshtml";
        public const string ProductListPartial = RootPartial + "_ProductListPartial.cshtml";
        public const string ProductListNotIncludinhPagingPartial = RootPartial + "_ProductListNotIncludingPagingPartial.cshtml";

        private const string RootEditorTemplatePartial = "~/Views/Shared/EditorTemplates/";
        public const string SelectViewItem = RootEditorTemplatePartial + "SelecViewItemViewModel.cshtml";
        public const string NumbericUpDown = RootEditorTemplatePartial + "NumbericUpDown.cshtml";

        private const string RootDisplayTemplatePartial = "~/Views/Shared/DisplayTemplates/";
        public const string VNCurrency = RootDisplayTemplatePartial + "VNCurrency.cshtml";
        public const string ProductRating = RootDisplayTemplatePartial + "RatingScore.cshtml";


        private const string RootPopupPartial = "~/Views/Popup/";
        public const string RegisterPopup = RootPopupPartial + "RegisterPartial.cshtml";
        public const string LoginPopup = RootPopupPartial + "LoginPartial.cshtml";


    }
}