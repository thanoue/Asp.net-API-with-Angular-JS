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
        IEnumerable<ProductCategory> GetAllByParentId(int parentid);
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

        public IEnumerable<ProductCategory> GetAllByParentId(int parentid)
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentId == parentid);
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
