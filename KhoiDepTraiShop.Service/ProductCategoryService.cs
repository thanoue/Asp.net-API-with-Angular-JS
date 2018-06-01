using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Data.Infrastructure;
namespace KhoiDepTraiShop.Service
{
    public interface IProductCategodyService
    {
        ProductCategory Add(ProductCategory productcategody);
        void Update(ProductCategory productcategody);
        ProductCategory Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyWord);
        IEnumerable<ProductCategory> GetAllByParentId(int parentid);
        IEnumerable<ProductCategory> GetAllRootCategory();
        IEnumerable<ProductCategory> GetAllSubCategory();
        ProductCategory GetById(int id);
        void SaveChanges();
    }
    public class ProductCategoryService :IProductCategodyService
    {
        IProductCategoryRepository _productCategoryRepository;
        IUnitOfWork _UnitOfWork;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,IUnitOfWork UnitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _UnitOfWork = UnitOfWork;
        }

        public ProductCategory Add(ProductCategory productcategody)
        {
            return _productCategoryRepository.Add(productcategody);
        }

        public ProductCategory Delete(int id)
        {
              return  _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
           return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyWord)
        {
            if(!string.IsNullOrEmpty(keyWord))
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentid)
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentId == parentid);
        }

        public IEnumerable<ProductCategory> GetAllRootCategory()
        {
            return _productCategoryRepository.GetMulti(x=>x.ParentId ==null);
        }

        public IEnumerable<ProductCategory> GetAllSubCategory()
        {
            var hasParent = _productCategoryRepository.GetMulti(x => x.ParentId != null).ToList();
            var single = _productCategoryRepository.GetMulti(x => x.ParentId == null).ToList();
            var vm = new List<ProductCategory>();
            foreach(var item in single)
            {
                var category = hasParent.Where(x => x.ParentId == item.Id).FirstOrDefault();
                if (category == null)
                    vm.Add(item);

            }
            return vm;
        }

        public ProductCategory GetById(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(ProductCategory productcategody)
        {
            _productCategoryRepository.Update(productcategody);
        }
    }
}
