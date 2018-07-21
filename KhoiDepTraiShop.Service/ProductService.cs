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
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyWord);
        IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int page, int pagesize, out int totalrow);
        IEnumerable<Product> GetAllPaging(int page, int pagesize, out int totalrow);
        Product GetById(int id);
        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pagesize, out int totalrow);
        int GetMaxProductId();
        IEnumerable<Product> GetAllByCategoryIds(List<int> categories);
        IEnumerable<Product> GetHighViewCountProducts(int minViewCount);
        IEnumerable<Product> GetFilterByPrice(decimal min, decimal max, int? categoryId);
        IEnumerable<Product> GetByRatingRangeProductPaging(int ratingScore, int? categoryId, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetPriceFilterByPriceRangeProductPaging(decimal minPrice, decimal maxPrice, int? categoryId, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetPriceFilterByDiscountRangeProductPaging(decimal minDiscount, decimal maxDiscount, int? categoryId, int page, int pageSize, out int totalRow);
        IEnumerable<Product> GetByKeywordRangeProductPaging(string keyword, int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetRelativePrducts(Product product);
        IEnumerable<Tag> GetTagListByProductId(int productId);
        IEnumerable<Product> GetProductListByTag(string tagId, int page, int pageSize, out int totalRow);
        void IncreaseViewCount(int productId);
       
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IProductRatingRepository _productRatingRepository;

        IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productrepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitOfWork unitofwork,IProductRatingRepository productRatingRepository)
        {
            _productRatingRepository = productRatingRepository;
            this._productRepository = productrepository;
            this._unitOfWork = unitofwork;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
        }
        public Product Add(Product Product)
        {

            var product = _productRepository.Add(Product);
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
                       tag.Type = TagType.Product;
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

        public IEnumerable<Product> GetAll()
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

        public IEnumerable<Product> GetAllByCategoryIds(List<int> categories)
        {
            var vm = new List<Product>();
            foreach (int i in categories)
            {
                var products = _productRepository.GetAll().Where(p => p.CategoryId == i).ToList();
                if (products != null)
                    vm.AddRange(products);
            }
            return vm;
        }

        public IEnumerable<Product> GetAllByCategoryPaging(int categoryId, int page, int pagesize, out int totalrow)
        {
            var products = _productRepository.GetAll().Where(p => p.CategoryId == categoryId).ToList();
            totalrow = products.Count();
            return products.Skip((page - 1) * pagesize).Take(pagesize);
            //return _productRepository.GetMultiPaging(x => x.Status && x.CategoryId == categoryId, out totalrow, page, pagesize, new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pagesize, out int totalrow)
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

        public IEnumerable<Product> GetFilterByPrice(decimal min, decimal max, int? categoryId)
        {
            return _productRepository.GetFilterByPrice(min, max, categoryId);
        }

        public IEnumerable<Product> GetHighViewCountProducts(int minViewCount)
        {
            return _productRepository.GetAll().Where(p => p.ViewCount >= minViewCount);
        }

        public IEnumerable<Product> GetPriceFilterByPriceRangeProductPaging(decimal minPrice, decimal maxPrice, int? categoryId, int page, int pageSize, out int totalRow)
        {
            if (categoryId != null)
            {
                var query = _productRepository.GetMulti(x => x.Price >= minPrice && x.CategoryId == categoryId).OrderBy(p => p.Price).ToList();

                totalRow = query.Count();

                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            {
                var query = _productRepository.GetMulti(x => x.Price >= minPrice).OrderBy(p => p.Price).ToList();

                totalRow = query.Count();

                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }

        }

        public int GetMaxProductId()
        {
            return _productRepository.GetAll().Max(p => p.Id);
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
                        tag.Type = TagType.Product;
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

        public IEnumerable<Product> GetPriceFilterByDiscountRangeProductPaging(decimal minDiscount, decimal maxDiscount, int? categoryId, int page, int pageSize, out int totalRow)
        {
            var vm = new List<Product>();
            var products = new List<Product>();
            if (categoryId == null)
            {
                products = _productRepository.GetAll().ToList();
            }
            else
                products = _productRepository.GetMulti(p => p.CategoryId == categoryId).ToList();
            foreach(var product in products)
            {
                var disCountPercent =  ((product.Price - product.PromotionPrice)/product.Price)*100;
                if (disCountPercent >= minDiscount && disCountPercent <= maxDiscount) {
                    vm.Add(product);
                } ;
            }

            totalRow = vm.Count();
            return vm.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelativePrducts(Product product)
        {
            //TODO relative here
            var vm = _productRepository.GetMulti(p => p.Status && p.CategoryId == product.CategoryId);
            return vm.Take(10).ToList();
        }

        public IEnumerable<Tag> GetTagListByProductId(int productId)
        {
            return _productTagRepository.GetMulti(p => p.ProductId == productId, new string[] { "Tag" }).Select(t => t.Tag).ToList();
        }

        public IEnumerable<Product> GetProductListByTag(string tagId,int page, int pageSize, out int totalRow)
        {
           var list =_productTagRepository.GetMulti(x => x.TagId == tagId, new string[] { "Product" }).Select(t => t.Product).ToList();
            totalRow = list.Count();
            return list.Skip((page-1)*pageSize).Take(pageSize);
        }

        public void IncreaseViewCount(int productId)
        {
            var product = _productRepository.GetSingleById(productId);
            product.ViewCount = product.ViewCount!=null ?product.ViewCount +1 : 1;
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetByKeywordRangeProductPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            var list = _productRepository.GetMulti(p=>p.Name.Contains(keyword) || p.Description.Contains(keyword)).ToList();
            totalRow = list.Count();
            return list.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetByRatingRangeProductPaging(int ratingScore, int? categoryId, int page, int pageSize, out int totalRow)
        {
            var products = categoryId != null ? _productRepository.GetAll().Where(p => p.CategoryId == categoryId).ToList() : _productRepository.GetAll().ToList();
            foreach(var product in products.ToList())
            {
                if (_productRatingRepository.GetRatingAverage(product.Id) != ratingScore)
                    products.Remove(product);
            }
            totalRow = products.Count();
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
