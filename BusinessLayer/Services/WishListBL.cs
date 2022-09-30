using BusinessLayer.Interfaces;
using CommonLayer.WishListModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public string AddToWishList(int UserId, WishListPostModel postModel)
        {
            try
            {
                return wishListRL.AddToWishList(UserId, postModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
