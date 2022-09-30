using CommonLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        public string AddToWishList(int UserId, WishListPostModel postModel);
        public string DeleteFromWishList(int UserId, int WishListId);
    }
}
