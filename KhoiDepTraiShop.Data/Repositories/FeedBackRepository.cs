﻿using KhoiDepTraiShop.Data.Infrastructure;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Data.Repositories
{
    public interface IFeedBackRepository : IRepository<FeedBack>
    {

    }
    public class FeedBackRepository : RepositoryBase<FeedBack>, IFeedBackRepository
    {
        public FeedBackRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        } 
    }
}
