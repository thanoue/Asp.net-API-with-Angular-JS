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
        void Add(ProductCategory productcategody);
        void Update(ProductCategory productcategody);
        void Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAllByParentId(int parentid);
        ProductCategory GetById(int id);
        void SaveChanges();
    }
    public class ProductCategoryServicee :IProductCategodyService
    {
        IProductCategoryRepository _productCategoryRepository;
        IUnitOfWork _UnitOfWork;
        public ProductCategoryServicee(IProductCategoryRepository productCategoryRepository,IUnitOfWork UnitOfWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _UnitOfWork = UnitOfWork;
        }

        public void Add(ProductCategory productcategody)
        {
            _productCategoryRepository.Add(productcategody);
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Delete(id);
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
