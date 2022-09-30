using CommonLayer.WishListModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        private readonly string connectionString;
        public WishListRL(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("BookStoreConnection");
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);
        public string AddToWishList(int UserId, WishListPostModel postModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddToWishlList", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookId", postModel.BookId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if(result != 0)
                    {
                        return "Added To WishList";
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
        public string DeleteFromWishList(int UserId, int WishListId)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spDeleteFromWishList", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@WishListId", WishListId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Deleted From WishList";
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
        public List<WishListResponseModel> GetWishListByUserId(int UserId)
        {
            List<WishListResponseModel> wishListedItems = new List<WishListResponseModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetWishList", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        WishListResponseModel wishListModel = new WishListResponseModel();
                        wishListModel.WishListId = reader["WishListId"] == DBNull.Value ? default : reader.GetInt32("WishListId");
                        wishListModel.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        wishListModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        wishListModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        wishListModel.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        wishListModel.Price = (reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price"));
                        wishListModel.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        wishListModel.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        wishListedItems.Add(wishListModel);
                    }
                    return wishListedItems;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
