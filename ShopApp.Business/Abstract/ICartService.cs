using ShopApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string UserId);
        Cart GetCartByUserId(string userId);
    }
}
