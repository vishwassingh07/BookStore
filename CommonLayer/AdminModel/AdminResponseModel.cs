using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.AdminModel
{
    public class AdminResponseModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}
