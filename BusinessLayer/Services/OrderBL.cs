using BusinessLayer.Interfaces;
using CommonLayer.OrderModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public string AddOrder(int UserId, OrderPostModel orderModel)
        {
            try
            {
                return orderRL.AddOrder(UserId, orderModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
