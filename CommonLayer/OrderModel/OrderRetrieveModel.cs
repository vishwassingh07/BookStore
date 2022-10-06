﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.OrderModel
{
    public class OrderRetrieveModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookImage { get; set; }
    }
}
