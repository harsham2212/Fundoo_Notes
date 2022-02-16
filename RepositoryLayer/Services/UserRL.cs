using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
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
                user.email = userPostModel.email; ;
                user.password = userPostModel.password;
                user.cpassword = userPostModel.cpassword;
                StringCipher.Encrypt(userPostModel.password);
                user.password = StringCipher.Encrypt(userPostModel.password);

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
                var result = dbContext.Users.Where(x => x.email == userLogin.email && x.cpassword == userLogin.cpassword).FirstOrDefault();
                if (result != null)
                    return GenerateJWTToken(userLogin.email, user.userId);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string GenerateToken(string Email)
        {
            if (Email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
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

        private static string GenerateJWTToken(string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email.ToString()),
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
                var checkemail = dbContext.Users.FirstOrDefault(e => e.email == email);
              
                if (checkemail != null)
                {
                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\FundooQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\FundooQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(email, checkemail.userId);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.sendMail(email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;

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

        public List<UserModel> GetAllUsers()
        {
            try
            {
                var result = dbContext.Users.ToList();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.sendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }
    }
}