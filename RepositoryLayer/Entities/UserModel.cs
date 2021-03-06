using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int userId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string phoneNo { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cpassword { get; set; }
        public DateTime registeredDate { get; set; }
        public DateTime modifiedDate { get; set; }


        public virtual ICollection<Note> Note { get; set; }
        public virtual ICollection<Label> Label { get; set; }
        //public virtual ICollection<Collabarator> Collab { get; set; }
        public virtual ICollection<UserAddress> Addresses { get; set; }
    }
}
