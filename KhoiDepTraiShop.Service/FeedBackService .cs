using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Data.Repositories;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Service
{
    public interface IFeedBackService
    {
        FeedBack CreateFeedBack(FeedBack error);
        IEnumerable<FeedBack> GetAllByType(FeedBackStatus status);
        IEnumerable<FeedBack> GetAll();
        void Save();
    }
    public class FeedBackService : IFeedBackService
    {
        IFeedBackRepository _feedBackRepository;
        IUnitOfWork _UnitOfWork;

        public FeedBackService()
        {
        }

        public FeedBackService(IFeedBackRepository feedBackRepository, IUnitOfWork unitOfWork)
        {
            this._feedBackRepository = feedBackRepository;
            this._UnitOfWork = unitOfWork;
        }

        public FeedBack CreateFeedBack(FeedBack feedBack)
        {
            return _feedBackRepository.Add(feedBack);
        }

        public IEnumerable<FeedBack> GetAll()
        {
            return _feedBackRepository.GetAll().ToList();
        }

        public IEnumerable<FeedBack> GetAllByType(FeedBackStatus status )
        {
            return _feedBackRepository.GetMulti(x => x.Status == status).ToList();
        }

        public void Save()
        {
            _UnitOfWork.Commit();
        }
    }
}
