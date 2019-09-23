using ShopApp.Entities.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string UserId);
        Cart GetCartByUserId(string userId);
        void AddToCart(string userId, int productId, int quantity);
    }
}
