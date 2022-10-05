using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.AddressModel
{
    public class AddressRetrieveModel
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
