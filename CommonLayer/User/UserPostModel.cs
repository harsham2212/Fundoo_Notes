using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        public string fname { get; set; }
        public string lname { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"(0|91)?[6-9][0-9]{9}",
        ErrorMessage = "Please enter correct Phone Number")]
        public string phoneNo { get; set; }
        public string address { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
        ErrorMessage = "Please enter correct Email Address")]
        public string email { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$",
        ErrorMessage = "Please enter correct format of Password")]
        public string password { get; set; }
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$",
        ErrorMessage = "Your Confirm Password should Match with Password")]
        public string cpassword { get; set; }
    }
}

// Password format : password between 8 to 15 characters which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.
// Email format : A-Z small capital both and @ and gmail.com
//phone number : starts with 0 or 91 then first number should start with 6 7 8 or 9 and rest nine numbers from 0-9
