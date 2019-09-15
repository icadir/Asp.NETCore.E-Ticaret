using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Entities;
using System.Linq;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete form ProductCategory where ProductId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlCommand(cmd, productId, categoryId);
            }
        }

        public Category GetByWithProducts(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductCategories)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault();
            }
        }
    }
}
