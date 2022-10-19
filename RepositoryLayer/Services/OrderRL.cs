using CommonLayer.OrderModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private readonly string connectionString;
        public OrderRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreConnection");
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);
        public string AddOrder(int UserId, OrderPostModel orderModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookId", orderModel.BookId);
                    command.Parameters.AddWithValue("@OrderQuantity", orderModel.Quantity);
                    command.Parameters.AddWithValue("@AddressId", orderModel.AddressId);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfull !!!";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<OrderRetrieveModel> RetrieveOrder(int UserId)
        {
            List<OrderRetrieveModel> orderList = new List<OrderRetrieveModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spRetrieveOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;
                     command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderRetrieveModel retrieveModel = new OrderRetrieveModel();
                        retrieveModel.OrderId = reader["OrderId"] == DBNull.Value ? default : reader.GetInt32("OrderId");
                        retrieveModel.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        retrieveModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        retrieveModel.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        retrieveModel.TotalPrice = reader["TotalPrice"] == DBNull.Value ? default : reader.GetDecimal("TotalPrice");
                         retrieveModel.Quantity = reader["OrderQuantity"] == DBNull.Value ? default : reader.GetInt32("OrderQuantity");
                        retrieveModel.OrderDate = reader["OrderDate"] == DBNull.Value ? default : reader.GetDateTime("OrderDate");
                        retrieveModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        retrieveModel.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        retrieveModel.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        orderList.Add(retrieveModel);
                    }
                    connection.Close();
                    if(orderList.Count > 0)
                    {
                        return orderList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
