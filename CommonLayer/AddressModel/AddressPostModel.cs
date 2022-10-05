using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.AddressModel
{
    public class AddressPostModel
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int AddressTypeId { get; set; }
    }
}
