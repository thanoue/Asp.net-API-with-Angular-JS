using System;

namespace KhoiDepTraiShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ShopDbContext Init();
    }
}