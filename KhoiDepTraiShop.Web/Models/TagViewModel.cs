using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;

namespace KhoiDepTraiShop.Web.Models
{
    public class TagViewModel
    {        
        public TagViewModel() { }
        public string Id { set; get; }
        public string Name { set; get; }
        public TagType Type { set; get; }
    }
    public static class TagViewModelEmm
    {
        public static TagViewModel ToModel(this Tag  entity)
        {
            var model = new TagViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type
            };
            return model;
        }
        public static List<TagViewModel> ToModelList(this IList<Tag> entities)
        {
            var vm = new List<TagViewModel>();
            vm.AddRange(entities.Select(p => p.ToModel()));
            return vm;
        }
       
    }
}