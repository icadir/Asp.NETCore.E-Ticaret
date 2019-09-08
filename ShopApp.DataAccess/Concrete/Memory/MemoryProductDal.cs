using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete.Memory
{
    public class MemoryProductDal : IProductDal
    {
        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            var products = new List<Product>()
            {
                new Product(){Id=1,Name="Samgung s6",ImageUrl="1.jpg",Price=1000},
                new Product(){Id=2,Name="Samgung s7",ImageUrl="2.jpg",Price=2000},
                new Product(){Id=3,Name="Samgung s8",ImageUrl="3.jpg",Price=3000},
            };
            return products;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
