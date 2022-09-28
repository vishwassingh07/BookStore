using BusinessLayer.Interfaces;
using CommonLayer.AdminModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(AdminLoginModel adminLogin)
        {
            try
            {
                return adminRL.AdminLogin(adminLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
