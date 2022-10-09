using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.FeedBackModel
{
    public class FeedBackPostModel
    {
        public int BookId { get; set; }
        public string Comments { get; set; }
        public decimal TotalRating { get; set; }
    }
}
