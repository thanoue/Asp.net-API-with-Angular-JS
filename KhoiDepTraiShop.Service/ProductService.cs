using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Common;
namespace KhoiDepTraiShop.Service
{
    public interface IProductService{
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyWord);
        IEnumerable<Product> GetAllByCategoryPaging(int categoryId,int page, int pagesize, out int totalrow);
        IEnumerable<Product> GetAllPaging(int page, int pagesize, out int totalrow);
        Product GetById(int id);
        IEnumerable<Product> GetAllByTagPaging(string tag,int page, int pagesize, out int totalrow);
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;

        IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productrepository,IProductTagRepository productTagRepository,ITagRepository tagRepository,IUnitOfWork unitofwork)
        {
            this._productRepository = productrepository;
            this._unitOfWork = unitofwork;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
        }
        public Product Add(Product Product)
        {

          var product  =   _productRepository.Add(Product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringUtility.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductId = product.Id;
                    productTag.TagId = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return product;
        }

        public Product Delete(int id)
        {
           return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll ()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                return _productRepository.GetMulti(p => p.Name.Contains(keyWord) || p.Description.Contains(keyWord));
            }
            return _productRepository.GetAll();
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
            
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringUtility.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.Id = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductId == product.Id);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductId = product.Id;
                    productTag.TagId = tagId;
                    _productTagRepository.Add(productTag);
                }

            }
            _productRepository.Update(product);
        }
    }
}
