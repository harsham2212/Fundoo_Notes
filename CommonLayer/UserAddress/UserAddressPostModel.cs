using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.UserAddress
{
    public class UserAddressPostModel
    {
        [Required]
        public string AddressType { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }    
    }
}
