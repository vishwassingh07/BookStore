using BusinessLayer.Interfaces;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderBL orderBL, ILogger<OrderController> Logger)
        {
            this.orderBL = orderBL;
            this._logger = Logger;
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("Retrieve")]
        public ActionResult RetrieveOrder()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = orderBL.RetrieveOrder(UserID);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Order Details Fetched Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Order Details Could Not Be Fetched" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
