using BusinessLayer.Interfaces;
using CommonLayer.AddressModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(int UserId, AddressPostModel addressModel)
        {
            try
            {
                return addressRL.AddAddress(UserId, addressModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string DeleteAddress(int UserId, int AddressId)
        {
            try
            {
                return addressRL.DeleteAddress(UserId, AddressId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateAddress(int UserId, AddressPostModel addressModel)
        {
            try
            {
                return addressRL.UpdateAddress(UserId, addressModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
