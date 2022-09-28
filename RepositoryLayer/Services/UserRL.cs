using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration Configuration;
        private readonly IConfiguration _AppSettings;
        public UserRL(IConfiguration Configuration, IConfiguration _AppSettings)
        {
            this.Configuration = Configuration;
            this._AppSettings = _AppSettings;
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
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spUserLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", loginModel.Email);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);
                    command.ExecuteNonQuery();

                    SqlDataReader reader = command.ExecuteReader();
                    GetAllUserModel model = new GetAllUserModel();
                    if (reader.Read())
                    {
                        model.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                        model.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default :reader["Password"]);
                    }
                    var token = GenerateSecurityToken(model.Email, model.UserId);
                    return token;
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
        private string GenerateSecurityToken(string Email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._AppSettings[("JWT:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spForgotPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", Email);
                    SqlDataReader reader = command.ExecuteReader();
                    GetAllUserModel model = new GetAllUserModel();
                    if (reader.Read())
                    {
                        model.Email = Convert.ToString(reader["Email"] == DBNull.Value ? default : reader["Email"]);
                        model.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                    }
                    var token = GenerateSecurityToken(model.Email, model.UserId);
                    MSMQModel msmq = new MSMQModel();
                    msmq.sendData2Queue(token);
                    return "Mail Sent";
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
        public bool ResetPassword(string Email, string NewPassword, string ConfirmPassword)
        {
            try
            {
                if (NewPassword.Equals(ConfirmPassword))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spResetPassword", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", NewPassword);
                    int result = command.ExecuteNonQuery();
                    if(result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
