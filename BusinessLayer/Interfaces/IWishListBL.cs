using CommonLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {
        public string AddToWishList(int UserId, WishListPostModel postModel);
        public string DeleteFromWishList(int UserId, int WishListId);
    }
}
