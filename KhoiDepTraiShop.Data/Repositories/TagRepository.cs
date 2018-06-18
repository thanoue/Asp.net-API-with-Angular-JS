using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag GetByStringId(string id);
    }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Tag GetByStringId(string id)
        {
            return DbContext.Tags.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
