using ShopApp.Entities.Entities;


namespace ShopApp.DataAccess.Abstract
{
    public interface ICategoryDal : IRepository<Category>
    {
        Category GetByWithProducts(int id);
    }
}
