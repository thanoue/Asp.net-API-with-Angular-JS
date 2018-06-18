using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.Commons
{
    public class ViewItemModel
    {
        public ViewItemModel()
        {
        }
        public ViewItemModel(long id, string name)
        {
            this.IdString = id.ToString();
            Name = name;
        }
        public ViewItemModel(int id, string name)
        {
            this.IdString = id.ToString();
            this.Name = name;
        }
        public ViewItemModel(int id, string name, string objectData)
        {
            this.IdString = id.ToString();
            this.Name = name;
            this.StringValue = objectData;
        }
        public ViewItemModel(string id, string name)
        {
            this.IdString = id;
            this.Name = name;
        }
        public ViewItemModel(int id, string name, bool flag) : this(id, name)
        {
            this.Flag = flag;
        }
        public ViewItemModel(int id, string name, int val) : this(id, name)
        {
            this.Value = val;
        }

        public ViewItemModel(int id, string name, bool flag, long longId) : this(id, name, flag)
        {
            LongId = longId;
        }

        public string Name { get; set; }
        public bool Flag { get; set; }
        public int Value { get; set; }
        public string StringValue { get; set; }
        public string IdString { get; set; }

        public long LongId { get; set; }
    }

}