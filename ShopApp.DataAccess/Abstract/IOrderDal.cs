using System.Collections.Generic;
using ShopApp.Entities.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
