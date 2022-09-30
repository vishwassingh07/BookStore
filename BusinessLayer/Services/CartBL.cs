using BusinessLayer.Interfaces;
using CommonLayer.CartModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public string AddToCart(int UserId, CartPostModel cartModel)
        {
            try
            {
                return cartRL.AddToCart(UserId, cartModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteFromCart(int CartId, int UserId)
        {
            try
            {
                return cartRL.DeleteFromCart(CartId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
