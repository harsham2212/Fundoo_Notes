using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("User")]

    public class UserController : ControllerBase
    {
        FundooDBContext fundooDBContext;
        IUserBL userBL;
        public UserController(IUserBL userBL, FundooDBContext fundooDB)
        {
            this.userBL = userBL;
            this.fundooDBContext = fundooDB;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
        //[HttpPost("register/{users}")]
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful:,{userPostModel.email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost("login")]
        public ActionResult LogInUser(UserLogin userLogin)
        {
            try
            {
                string result = this.userBL.Login(userLogin);
                if (result != null)
                    return this.Ok(new { success = true, message = $"LogIn Successful: {userLogin.email}, Token = {result}" }); 
                else
                    return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });        
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("forgotpassword/{email}")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = fundooDBContext.Users.FirstOrDefault(x => x.email == email);
                if (result == null)
                {
                    return this.Ok(new { success = false, message = $"Email not registered" });
                }
                else
                {
                    this.userBL.ForgetPassword(email);
                    return this.BadRequest(new { success = true, message = $"Tokken sent for resetting Password" });
                }  
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("resetpassword/{password}/{cpassword}")]
        public ActionResult ResetPassword(string password, string cpassword)
        {
            try
            {
                if(password != cpassword){
                    return this.BadRequest(new { success = false, message = $"Confirm Password not match with Password" });
                }
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    if (UserEmailObject == null)
                    {
                        return this.BadRequest(new { success = false, message = $"Password not Reset" });
                    }
                    else
                    {
                        this.userBL.ResetPassword(UserEmailObject, password, cpassword);
                        return Ok(new { success = true, message = "Password Reset Sucessfully" });
                    }
                }
                return this.BadRequest(new { success = false, message = $"Password not Changed" });
            }
            
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Table data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}