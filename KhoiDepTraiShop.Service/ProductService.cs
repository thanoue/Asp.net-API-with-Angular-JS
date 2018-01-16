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
    public interface IProductService{
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetALL();
        IEnumerable<Product> GetAllByCategoryPaging(int categoryId,int page, int pagesize, out int totalrow);
        IEnumerable<Product> GetAllPaging(int page, int pagesize, out int totalrow);
        Product GetById(int id);
        IEnumerable<Product> GetAllByTagPaging(string tag,int page, int pagesize, out int totalrow);
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productrepository,IUnitOfWork unitofwork)
        {
            this._productRepository = productrepository;
            this._unitOfWork = unitofwork;
        }
        public Product Add(Product product)
        {
          return  _productRepository.Add(product);
        }

        public Product Delete(int id)
        {
           return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetALL()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int page, int pagesize, out int totalrow)
        {
            return _productRepository.GetMultiPaging(x => x.Status && x.CategoryId == categoryId, out totalrow, page, pagesize,new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag,int page, int pagesize, out int totalrow)
        {
            return _productRepository.GetAllByTag(tag, page, pagesize, out totalrow);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pagesize, out int totalrow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalrow, page, pagesize);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id); 
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
