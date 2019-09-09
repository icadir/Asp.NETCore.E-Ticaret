using ShopApp.Entities.Entities;
using System.Collections.Generic;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        List<Product> GetProductsByCategory(string category);

        Product GetProductDetails(int id);
    }
}
