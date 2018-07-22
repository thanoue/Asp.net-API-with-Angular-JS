using Microsoft.VisualStudio.TestTools.UnitTesting;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Model.Models;
using System.Linq;

namespace KhoiDepTraiShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class ProductCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IProductCategoryRepository productCategoryRepository;
        IProductRepository productRepository;

        IUnitOfWork unitOfWork;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            productCategoryRepository = new ProductCategoryRepository(dbFactory);
            productRepository = new ProductRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory); 
        }
        [TestMethod]
        public void test2()
        {
            var list = productRepository.GetAll().ToList();
            Assert.IsNotNull(list);
        }
        [TestMethod]
        public void ProductCategory_Repository_create()
        {
            ProductCategory cate = new ProductCategory();
            cate.Name = "Test_1_name";
            cate.Alias = "Test_1_alias";
            cate.Status = true;
            var result = productCategoryRepository.Add(cate);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Product pr = new Product();
            pr.Name = "aa";
            pr.Alias = "aa";
            pr.CategoryId = 2;
            pr.Status = true;
            var res = productRepository.Add(pr);
            unitOfWork.Commit();
            Assert.IsNotNull(res);
            Assert.AreEqual(1, res.Id);
        }
    }
}