using CommonLayer.BookModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly string connectionString;
        public BookRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreConnection");
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);
        public BookDetailsModel AddBook(BookDetailsModel bookModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddBook", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    command.Parameters.AddWithValue("@Author", bookModel.Author);
                    command.Parameters.AddWithValue("@Description", bookModel.Description);
                    command.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    command.Parameters.AddWithValue("@Price", bookModel.Price);
                    command.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                    command.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    command.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    command.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if(result != 0)
                    {
                        return bookModel;
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
