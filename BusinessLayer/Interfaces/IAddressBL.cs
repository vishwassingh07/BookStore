using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public string AddAddress(int UserId, AddressPostModel addressModel);
        public string DeleteAddress(int UserId, int AddressId);
        public string UpdateAddress(int UserId, AddressPostModel addressModel);
    }
}
