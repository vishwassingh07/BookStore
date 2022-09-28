using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookDetailsModel
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int DiscountedPrice { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public int RatingCount { get; set; }
        [Required]
        public string BookImage { get; set; }
    }
}
