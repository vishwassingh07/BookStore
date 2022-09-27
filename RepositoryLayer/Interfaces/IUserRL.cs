using CommonLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserDetailsModel Register(UserDetailsModel userModel);
        public string UserLogin(UserLoginModel loginModel);
        public string ForgotPassword(string Email);
    }
}
