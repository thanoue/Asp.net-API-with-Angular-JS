using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.Commons
{
    public class NumbericUpDown
    {
        public NumbericUpDown() { }
        public int Value { get; set; } = 1;
        public int Min { get; set; } = 1;
        public int Max { get; set; } = 9;
        public int Step { get; set; } = 1;
    }
}