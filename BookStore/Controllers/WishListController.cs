using BusinessLayer.Interfaces;
using CommonLayer.WishListModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }
        [HttpPost("AddToWishList")]
        public ActionResult AddToWishList(WishListPostModel postModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = wishListBL.AddToWishList(UserID, postModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Added To WishList Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could Not Be Added To WishList" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
