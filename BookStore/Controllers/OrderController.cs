using BusinessLayer.Interfaces;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        private readonly ILogger<OrderController> _logger;
        private readonly IDistributedCache distributedCache;
        public OrderController(IOrderBL orderBL, ILogger<OrderController> Logger, IDistributedCache distributedCache)
        {
            this.orderBL = orderBL;
            this._logger = Logger;
            this.distributedCache = distributedCache;
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllOrdersUsingRedisCache()
        {
            var cacheKey = "OrderList";
            string serializedOrderList = String.Empty;
            var orderList = new List<OrderRetrieveModel>();
            var redisOrderList = await distributedCache.GetAsync(cacheKey);
            if (redisOrderList != null)
            {
                serializedOrderList = Encoding.UTF8.GetString(redisOrderList);
                orderList = JsonConvert.DeserializeObject<List<OrderRetrieveModel>>(serializedOrderList);
            }
            else
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                orderList = orderBL.RetrieveOrder(UserID);

                serializedOrderList = JsonConvert.SerializeObject(orderList);
                redisOrderList = Encoding.UTF8.GetBytes(serializedOrderList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await distributedCache.SetAsync(cacheKey, redisOrderList, options);
            }
            return Ok(orderList);
        }
    }
}