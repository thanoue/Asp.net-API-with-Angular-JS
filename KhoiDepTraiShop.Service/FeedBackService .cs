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
        FeedBack CreateFeedBack(FeedBack feedBack);
        IEnumerable<FeedBack> GetAllByType(FeedBackStatus status);
        IEnumerable<FeedBack> GetAll();
        FeedBack UpdateStatus(FeedBackStatus status, int feedbackId);

        void UpdateMultiFeedbacks(FeedBackStatus status, IList<int> ids);

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

        public void UpdateMultiFeedbacks(FeedBackStatus status, IList<int> ids)
        {
            foreach(var id in ids)
            {
                var feedback = _feedBackRepository.GetSingleById(id);
                feedback.Status = status;
                _UnitOfWork.Commit();
            }

        }

        public FeedBack UpdateStatus(FeedBackStatus status, int feedbackId)
        {
            var feedback = _feedBackRepository.GetSingleById(feedbackId);
            feedback.Status = status;
            _UnitOfWork.Commit();
            return feedback;
        }
    }
}
