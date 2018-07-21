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
        IEnumerable<ProductRating> GetAllByProduct(int productId);
        IEnumerable<ProductRating> GetAllPopulatedByProduct(int productId);
        int GetRatingAverage(int productId);
        void SaveChanges();
        bool RatingAblilityChecked(string userId, int productId);
        bool UserRatedOrNot(string userId, int productId);
        void SubmitRating(int productId, string userId, string title, string content, int score);
        ProductRating UpdateStatus(ProductRatingStatus productRatingStatus, int productId,string userId);
        ProductRating GetSingle(string userId, int productId);

       void MultiUpdate(ProductRatingStatus productRatingStatus, IList<ProductRating> productRatings);

      

        List<ProductRating> GetAllByStatus(ProductRatingStatus productRatingStatus);
    }
    public class ProductRatingService : IProductRatingService
    {
        IProductRatingRepository _productRatingRepository;
        IUnitOfWork _unitOfWork;
        IOrderDetailRepository _orderDetailRepository;
        IOrderRepository _orderRepository;
        public ProductRatingService(IProductRatingRepository productRatingRepository,IUnitOfWork unitOfWork,IOrderRepository orderRepository,IOrderDetailRepository orderDetailRepository)
        {
            _productRatingRepository = productRatingRepository;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }
        public ProductRating Add(ProductRating productRating)
        {
            _productRatingRepository.Add(productRating);
            _unitOfWork.Commit();
            return productRating;
        }

        public ProductRating Delete(int id)
        {
            return _productRatingRepository.Delete(id);
        }

        public IEnumerable<ProductRating> GetAllByProduct(int productId)
        {
            var ratings = _productRatingRepository.GetMulti(p => p.RatedProductId == productId).ToList();
            return ratings;
        }

        public int GetRatingAverage(int productId)
        {
            //var ratingList = _productRatingRepository.GetAll().Where(p => p.RatedProductId == productId).ToList();
            //int sum = 0;
            //if (ratingList.Count() == 0)
            //    return 0;
            //foreach(var  rating  in ratingList)
            //{
            //    sum += rating.RatingScore ?? 0;
            //}
            //return sum / ratingList.Count();
            return _productRatingRepository.GetRatingAverage(productId);
        }

        public bool RatingAblilityChecked(string userId, int productId)
        {
            var rating = _productRatingRepository.GetSingle(userId, productId);
            if (rating != null)
                return false;
            var order = _orderRepository.GetOrderListByProductAndUser(productId, userId);
            return order != null && order.Count() >0  ? true : false;
          
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductRating product)
        {
            _productRatingRepository.Update(product);
            _unitOfWork.Commit();
        }

        public void SubmitRating(int productId, string userId, string title, string content, int score)
        {
            var rating = new ProductRating()
            {
                RatedProductId = productId,
                RatingContent = content,
                RatingScore = score,
                Status = ProductRatingStatus.Waiting,
                RatingTime = DateTime.Now,
                RatingTitle = title,
                UserId = userId
            };

            _productRatingRepository.Add(rating);
            _unitOfWork.Commit();
        }

        public bool UserRatedOrNot(string userId, int productId)
        {
            var rating = _productRatingRepository.GetSingle(userId, productId);
            if (rating == null)
                return false;
            return true;
        }

        public List<ProductRating> GetAllByStatus(ProductRatingStatus productRatingStatus)
        {
            var ratings = _productRatingRepository.GetMulti(p => p.Status == productRatingStatus).ToList();
            return ratings;
        }

        public ProductRating UpdateStatus(ProductRatingStatus productRatingStatus, int productId, string userId)
        {
            var rating = _productRatingRepository.GetSingle(userId, productId);
            rating.Status = productRatingStatus;
            _unitOfWork.Commit();
            return rating;
        }

        public ProductRating GetSingle(string userId, int productId)
        {
            return _productRatingRepository.GetSingle(userId, productId);
        }

        public void MultiUpdate(ProductRatingStatus productRatingStatus, IList<ProductRating> productRatings)
        {
           foreach(var item in productRatings)
            {
                item.Status = productRatingStatus;
                _unitOfWork.Commit();
            }
        }

        public IEnumerable<ProductRating> GetAllPopulatedByProduct(int productId)
        {
            var ratings = _productRatingRepository.GetMulti(p => p.RatedProductId == productId && p.Status  == ProductRatingStatus.Public).ToList();
            return ratings;
        }
    }
}
