using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookResponseModel
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        public double Rating { get; set; }

        public int RatingCount { get; set; }
        public string BookImage { get; set; }
    }
}
