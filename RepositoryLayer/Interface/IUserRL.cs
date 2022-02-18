using CommonLayer.User;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void RegisterUser(UserPostModel userPostModel);
        public string Login(UserLogin userLogin);
        public bool ForgetPassword(string email);
        public void ResetPassword(string email, string password, string cpassword);
        public string EncryptPassword(string password);
        List<UserModel> GetAllUsers();
    }
}
