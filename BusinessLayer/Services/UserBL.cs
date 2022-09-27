using BusinessLayer.Interfaces;
using CommonLayer.UserModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserDetailsModel Register(UserDetailsModel userModel)
        {
            try
            {
                return userRL.Register(userModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UserLogin(UserLoginModel loginModel)
        {
            try
            {
                return userRL.UserLogin(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
