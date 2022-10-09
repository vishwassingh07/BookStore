using CommonLayer.FeedBackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedBackBL
    {
        public string AddFeedBack(int UserId, FeedBackPostModel feedbackModel);
    }
}
