﻿using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        public string AddAddress(int UserId, AddressPostModel addressModel);
    }
}