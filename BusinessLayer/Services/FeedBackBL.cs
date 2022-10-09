using BusinessLayer.Interfaces;
using CommonLayer.FeedBackModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL : IFeedBackBL
    {
        private readonly IFeedBackRL feedBackRL;
        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            this.feedBackRL = feedBackRL;
        }
        public string AddFeedBack(int UserId, FeedBackPostModel feedbackModel)
        {
            try
            {
                return feedBackRL.AddFeedBack(UserId, feedbackModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
