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
    }
}
