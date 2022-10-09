using BusinessLayer.Interfaces;
using CommonLayer.FeedBackModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackBL feedBackBL;
        public FeedBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;
        }

        [HttpPost("AddFeedBack")]
        public ActionResult AddFeedBack(FeedBackPostModel feedbackModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = feedBackBL.AddFeedBack(UserID, feedbackModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book FeedBack Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book FeedBack Already Exists" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
