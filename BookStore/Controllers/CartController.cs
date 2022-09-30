using BusinessLayer.Interfaces;
using CommonLayer.CartModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize]
        [HttpPost("AddToCart")]
        public ActionResult AddToCart(CartPostModel cartModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = cartBL.AddToCart(UserID, cartModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Added To Cart Successfully", Data = result});
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Not Added To Cart" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("DeleteFromCart")]
        public ActionResult DeleteFromCart(int CartId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = cartBL.DeleteFromCart(CartId, UserID);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Deleted From Cart Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Could Not Deleted From Cart" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("UpdateCart")]
        public ActionResult UpdateCart(CartUpdateModel cartUpdateModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = cartBL.UpdateCart(UserID, cartUpdateModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Cart Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Cart Could Not Be Updated" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
