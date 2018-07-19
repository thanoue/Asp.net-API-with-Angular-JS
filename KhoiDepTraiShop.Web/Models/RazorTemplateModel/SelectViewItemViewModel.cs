using KhoiDepTraiShop.Web.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.RazorTemplateModel
{
    public class SelectViewItemViewModel
    {
        public string DefaultOption { get;  set; } = " ";

        public SelectViewItemViewModel()
        {
            ListData = new List<ViewItemModel>();
        }
        public SelectViewItemViewModel(IList<ViewItemModel> items)
        {
            if (items.Count > 0)
                ListData = items;
            else
                ListData = new List<ViewItemModel>();

        }
        public SelectViewItemViewModel(IList<ViewItemModel> items, string titleConfirm = "", string messageConfirm = "") : this(items)
        {
            this.MessageConfirm = messageConfirm;
            this.TitleConfirm = titleConfirm;
        }
        public SelectViewItemViewModel(IList<ViewItemModel> items, string name) : this(items)
        {

            this.Name = name;
        }

        public SelectViewItemViewModel(IList<ViewItemModel> items, string name, string titleConfirm = "", string messageConfirm = "") : this(items)
        {
            this.MessageConfirm = messageConfirm;
            this.TitleConfirm = titleConfirm;
            this.Name = name;
        }

        public SelectViewItemViewModel(string selected, IList<ViewItemModel> items) : this(items)
        {
            Selected = selected;
        }

        public SelectViewItemViewModel(string selected, IList<ViewItemModel> items, string name) : this(items)
        {
            Selected = selected;
            Name = name;
        }

        public SelectViewItemViewModel SetDefaultOption(string defaultOption)
        {
            this.DefaultOption = defaultOption;
            return this;
        }


        public string Selected { get; set; }
        public string TitleConfirm { get; set; }
        public string MessageConfirm { get; set; }
        public string Name { get; set; }
        public int SelectedInt
        {
            get
            {
                var intValue = 0;
                int.TryParse(Selected, out intValue);
                return intValue;
            }
            set
            {
                Selected = value.ToString();
            }
        }
        public int? SelectedNullableInt
        {
            get
            {
                if (SelectedInt == 0) return null;

                return SelectedInt;
            }
        }

        public IList<ViewItemModel> ListData { get; set; }
    }

    public enum DefaultOption
    {
        None = 0,
        OptionAll = 1,
        OptionNone = 2,
    }
}