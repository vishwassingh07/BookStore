using CommonLayer.AdminModel;
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
    public class AdminRL : IAdminRL
    {
        private readonly string connectionString;
        private readonly IConfiguration _AppSettings;
        public AdminRL(IConfiguration configuration, IConfiguration _AppSettings)
        {
            this.connectionString = configuration.GetConnectionString("BookStoreConnection");
            this._AppSettings = _AppSettings;
        }
        public static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly SqlConnection connection = new SqlConnection(connectionstring);
        public string AdminLogin(AdminLoginModel adminLogin)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spAdminLogin", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AdminEmail", adminLogin.AdminEmail);
                    command.Parameters.AddWithValue("@AdminPassword", adminLogin.AdminPassword);
                    command.ExecuteNonQuery();

                    SqlDataReader reader = command.ExecuteReader();
                    AdminResponseModel model = new AdminResponseModel();
                    if (reader.Read())
                    {
                        model.AdminId = Convert.ToInt32(reader["AdminId"] == DBNull.Value ? default : reader["AdminId"]);
                        model.AdminEmail = Convert.ToString(reader["AdminEmail"] == DBNull.Value ? default : reader["AdminEmail"]);
                        model.AdminPassword = Convert.ToString(reader["AdminPassword"] == DBNull.Value ? default : reader["AdminPassword"]);
                    }
                    var token = GenerateSecurityToken(model.AdminEmail, model.AdminId);
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
        private string GenerateSecurityToken(string AdminEmail, int AdminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._AppSettings[("JWT:key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("AdminEmail", AdminEmail),
                    new Claim("AdminId", AdminId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
