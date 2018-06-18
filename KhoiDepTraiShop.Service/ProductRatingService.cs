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
    public interface IProductRatingService
    {
        ProductRating Add(ProductRating product);
        void Update(ProductRating product);
        ProductRating Delete(int id);
        IEnumerable<ProductRating> GetAll();
        int GetRatingAverage(int productId);
        void SaveChanges();
    }
    public class ProductRatingService : IProductRatingService
    {
        IProductRatingRepository _productRatingRepository;
        IUnitOfWork _unitOfWork;
        public ProductRatingService(IProductRatingRepository productRatingRepository,IUnitOfWork unitOfWork)
        {
            _productRatingRepository = productRatingRepository;
            _unitOfWork = unitOfWork;
        }
        public ProductRating Add(ProductRating productRating)
        {
            throw new NotImplementedException();
        }

        public ProductRating Delete(int id)
        {
            return _productRatingRepository.Delete(id);
        }

        public IEnumerable<ProductRating> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetRatingAverage(int productId)
        {
            var ratingList = _productRatingRepository.GetAll().Where(p => p.RatedProductId == productId).ToList();
            int sum = 0;
            if (ratingList.Count() == 0)
                return 0;
            foreach(var  rating  in ratingList)
            {
                sum += rating.RatingScore ?? 0;
            }
            return sum / ratingList.Count();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductRating product)
        {
            throw new NotImplementedException();
        }
    }
}
