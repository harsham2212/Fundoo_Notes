using CommonLayer.User;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public string Login(UserLogin userLogin)
        {
            try
            {
                UserModel user = new UserModel();
                var result = dbContext.Users.Where(x => x.email == userLogin.email && x.password == userLogin.password).FirstOrDefault();
                if (result != null)
                    return GenerateJWTToken(userLogin.email, user.userId);
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Generating Token
        private static string GenerateJWTToken(string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool ForgetPassword(string email)
        {
            try
            {
                UserModel user = new UserModel();
                var result = dbContext.Users.Where(x => x.email == email).FirstOrDefault();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResetPassword(string email, string password, string cpassword)
        {
            try
            {
                UserModel user = new UserModel();
                var result = dbContext.Users.FirstOrDefault(x => x.email == email);
                if (result != null)
                {
                    result.password = password;
                    result.cpassword = cpassword;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
