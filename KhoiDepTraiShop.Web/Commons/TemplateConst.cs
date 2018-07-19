using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Commons
{
    public class TemplateConst
    {
        public const string VNCurrencyDisPlay = "VNCurrency";
        public const string RatingStarDisplay = "RatingScore";
        public const string SpecialProductListPartial = "SpecialProductListPartial";
        public const string DropDownBox = "DropDownBox";

        private const string HTMLTemplates = "/Assets/Client/Templates/";
        public const string UserValidationMail = HTMLTemplates + "UserValidatitonTemplate.html";
        public const string OrderTemplate = HTMLTemplates + "OrderTemplate.html";

    }

    public class SessionConst
    {
        public const string CART_ITEMS = "**CART_ITEMS**";
    }
}