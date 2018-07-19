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
        Reported,
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
        Responsed,
        Deleted
    }

    public enum OrderStatus
    {
         Sending = 0,
         Handling ,
         Delivering,
         Received,
         Deleted,

    }

    public enum PaymentMethod
    {
        InternetBanking,
        HandByHandPaying,
        Others
    }
}
