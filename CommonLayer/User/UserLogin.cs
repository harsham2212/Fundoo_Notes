using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserLogin
    {
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
        ErrorMessage = "Please enter correct Email Address")]
        public string email { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$",
        ErrorMessage = "Please enter password in Correct format")]
        public string password { get; set; }
    }
}
// Password format : password between 8 to 15 characters which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.
// Email format : A-Z small capital both and @ and gmail.com