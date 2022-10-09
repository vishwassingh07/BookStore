using CommonLayer.FeedBackModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedBackRL : IFeedBackRL
    {
        private readonly string connectionString;
        public FeedBackRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreConnection");
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);

        public string AddFeedBack(int UserId, FeedBackPostModel feedbackModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddFeedback", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    command.Parameters.AddWithValue("@Comments", feedbackModel.Comments);
                    command.Parameters.AddWithValue("@TotalRating", feedbackModel.TotalRating);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result > 1)
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
            finally
            {
                connection.Close();
            }
        }
    }
}
