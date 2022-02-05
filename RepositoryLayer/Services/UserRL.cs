using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Class
{
    public class UserRL : IUserRL
    {
        FundooDBContext dbContext;
        public UserRL(FundooDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                UserModel user = new UserModel();
                user.userId = new UserModel().userId;
                user.fname = userPostModel.fname;
                user.lname = userPostModel.lname;
                user.phoneNo = userPostModel.phoneNo;
                user.address = userPostModel.address;
                user.email = userPostModel.email;
                user.password = userPostModel.password;
                user.cpassword = userPostModel.cpassword;

                user.registeredDate = DateTime.Now;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Login(UserLogin userLogin)
        {
            try
            {
                UserModel user = new UserModel();
                var result = dbContext.Users.Where(x => x.email == userLogin.email && x.password == userLogin.password).FirstOrDefault();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
