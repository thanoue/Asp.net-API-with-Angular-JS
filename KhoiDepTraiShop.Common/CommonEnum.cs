using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Common
{
    public class CommonEnum
    {

    }
    public enum ProductRatingStatus
    {
        Waiting =0 ,
        Public,
        Deleted
    }

    public enum TagType
    {
        Product =0,
        Post
    }

    public enum FeedBackStatus
    {
        Wating =0,
        Seen,
        Responsed
    }
}
