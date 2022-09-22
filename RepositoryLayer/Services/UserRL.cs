using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration Configuration;
        public UserRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection (connectionstring);
        public UserDetailsModel Register(UserDetailsModel userModel)
        {
            try
            {
                using(connection)
                {
                    SqlCommand command = new SqlCommand("UserRegistration", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FullName", userModel.FullName);
                    command.Parameters.AddWithValue("@Email", userModel.Email);
                    command.Parameters.AddWithValue("@Password", userModel.Password);
                    command.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return userModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
