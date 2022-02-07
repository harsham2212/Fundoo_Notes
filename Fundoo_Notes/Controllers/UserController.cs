﻿using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundooDBContext fundooDBContext;
        IUserBL userBL;
        public UserController(IUserBL userBL, FundooDBContext fundooDB)
        {
            this.userBL = userBL;
            this.fundooDBContext = fundooDB;
        }
        [HttpPost("register")]
        public ActionResult registerUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful{userPostModel.email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("login")]
        public ActionResult Login(UserLogin login)
        {
            try
            {
                string result = this.userBL.Login(login);
                return this.Ok(new { success = true, message = $"Login Successful{login.email},token={result}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("forgetpassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                this.userBL.ForgetPassword(email);
                return this.Ok(new { success = true, message = $"Forget Password{email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }   
    }
}
