using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Entities;


namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {
    }
}
