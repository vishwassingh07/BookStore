using CommonLayer.FeedBackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedBackRL
    {
        public string AddFeedBack(int UserId, FeedBackPostModel feedbackModel);
    }
}
