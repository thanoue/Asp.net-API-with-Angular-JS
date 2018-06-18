using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models.Applications
{
    public class ApplicationGroupViewModel
    {
        public ApplicationGroupViewModel() { }
        public int ID { set; get; }     
        public string Name { set; get; }
        public string Description { set; get; }
        public IEnumerable<ApplicationRoleViewModel> Roles { set; get; }
    }
    public static class ApplicationGroupViewModelEmm
    {
        public static ApplicationGroupViewModel ToViewModel( this ApplicationGroup entity)
        {
            var vm = new ApplicationGroupViewModel()
            {
                ID = entity.ID,
                Description = entity.Description,
                Name = entity.Name
            };
            return vm;
        } 

        public static List<ApplicationGroupViewModel> ToViewModelList(this IList<ApplicationGroup> entities)
        {
            var vm = new List<ApplicationGroupViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static ApplicationGroup ToEntity(this ApplicationGroupViewModel model)
        {
            var entity = new ApplicationGroup()
            {
                Description = model.Description,
                ID = model.ID,
                Name = model.Name
            };
            return entity;
        }
    }
}