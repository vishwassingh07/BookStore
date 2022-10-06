using BusinessLayer.Interfaces;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [HttpPost("Add")]
        public ActionResult AddOrder(OrderPostModel orderModel)
        {
            try
            {

                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = orderBL.AddOrder(UserID, orderModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Order Placed Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Order Could Not Be Placed" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
