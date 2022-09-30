using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        public string AddToCart(int UserId, CartPostModel cartModel);
        public string DeleteFromCart(int CartId, int UserId);
        public string UpdateCart(int UserId, CartUpdateModel cartUpdateModel);
        public List<CartRetrieveModel> GetAllCartItems(int UserId);
    }
}
