using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserLogin
    {
        [Required(ErrorMessage="User Name is Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }
    }
}
