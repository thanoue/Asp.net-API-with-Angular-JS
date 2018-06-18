using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.Applications
{
    public class ApplicationRoleViewModel
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
    }
    public static class ApplicationRoleViewModelEmm
    {
        public static ApplicationRoleViewModel ToViewModel(this ApplicationRole entity)
        {
            var vm = new ApplicationRoleViewModel()
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };
            return vm;
        }

        public static List<ApplicationRoleViewModel> ToViewModelList(this IList<ApplicationRole> entities)
        {
            var vm = new List<ApplicationRoleViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static ApplicationRole ToEntity(this ApplicationRoleViewModel model, string method = "")
        {
            var entity = new ApplicationRole()
            {
                Description = model.Description,

                Name = model.Name
            };
            if (method == "update")
                entity.Id = model.Id;
            else
                entity.Id = Guid.NewGuid().ToString();
            return entity;
        }
    }
}