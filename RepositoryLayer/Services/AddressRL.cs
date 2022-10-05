using CommonLayer.AddressModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly string connectionString;
        public AddressRL(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreConnection");
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);
        public string AddAddress(int UserId, AddressPostModel addressModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Address", addressModel.Address);
                    command.Parameters.AddWithValue("@City", addressModel.City);
                    command.Parameters.AddWithValue("@State", addressModel.State);
                    command.Parameters.AddWithValue("@AddressTypeId", addressModel.AddressTypeId);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfully Added !!!";
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
        public string DeleteAddress(int UserId, int AddressId)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spDeleteAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@AddressId", AddressId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfully Deleted !!!";
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
        public string UpdateAddress(int UserId, AddressPostModel addressModel)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                    command.Parameters.AddWithValue("@Address", addressModel.Address);
                    command.Parameters.AddWithValue("@City", addressModel.City);
                    command.Parameters.AddWithValue("@State", addressModel.State);
                    command.Parameters.AddWithValue("@AddressTypeId", addressModel.AddressTypeId);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfully Updated !!!";
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
        public List<AddressRetrieveModel> RetrieveAddress(int UserId)
        {
            List<AddressRetrieveModel> addressList = new List<AddressRetrieveModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spRetrieveAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        AddressRetrieveModel retrieveModel = new AddressRetrieveModel();
                        retrieveModel.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        retrieveModel.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        retrieveModel.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        retrieveModel.MobileNumber = reader["MobileNumber"] == DBNull.Value ? default : reader.GetInt64("MobileNumber");
                        retrieveModel.Address = reader["Address"] == DBNull.Value ? default : reader.GetString("Address");
                        retrieveModel.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                        retrieveModel.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                        addressList.Add(retrieveModel);
                    }
                    return addressList;
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
