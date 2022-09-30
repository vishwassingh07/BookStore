using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        public string AddToCart(int UserId, CartPostModel cartModel);
        public string DeleteFromCart(int CartId, int UserId);
        public string UpdateCart(int UserId, CartUpdateModel cartUpdateModel);
        public List<CartRetrieveModel> GetAllCartItems(int UserId);
    }
}
