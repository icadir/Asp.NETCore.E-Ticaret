using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {

    }
}
