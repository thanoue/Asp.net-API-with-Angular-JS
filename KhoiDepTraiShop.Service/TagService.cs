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
    public interface ITagService
    {
        Tag GetById(string tagId);
    }
    public class TagService : ITagService
    {
        IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository ;
        }
        public Tag GetById(string tagId)
        {
            return _tagRepository.GetByStringId(tagId);   
        }
    }
}
