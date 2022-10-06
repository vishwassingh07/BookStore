using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public string AddOrder(int UserId, OrderPostModel orderModel);
    }
}
