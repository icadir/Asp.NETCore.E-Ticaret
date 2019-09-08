using ShopApp.Entities.Entities;
using System.Collections.Generic;

namespace ShopApp.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetAll();
        List<Product> GetPopulerProducts();
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
