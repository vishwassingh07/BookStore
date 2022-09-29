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
        public List<BookResponseModel> GetAllBooks()
        {
            List<BookResponseModel> lisOfBooks = new List<BookResponseModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetAllBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        BookResponseModel bookModel = new BookResponseModel();
                        bookModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        bookModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        bookModel.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        bookModel.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        bookModel.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        bookModel.Price = (reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price"));
                        bookModel.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        bookModel.Rating = (reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating"));
                        bookModel.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        bookModel.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        lisOfBooks.Add(bookModel);
                    };
                    return lisOfBooks;
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
        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spUpdateBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", bookModel.BookId);
                    command.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    command.Parameters.AddWithValue("@Author", bookModel.Author);
                    command.Parameters.AddWithValue("@Description", bookModel.Description);
                    command.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    command.Parameters.AddWithValue("@Price", bookModel.Price);
                    command.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                    command.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    command.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    command.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    var result = command.ExecuteNonQuery();
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
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spDeleteBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", BookId);

                    var result = command.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
