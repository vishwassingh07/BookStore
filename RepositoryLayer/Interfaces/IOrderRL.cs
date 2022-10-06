using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public string AddOrder(int UserId, OrderPostModel orderModel);
        public List<OrderRetrieveModel> RetrieveOrder(int UserId);
    }
}
