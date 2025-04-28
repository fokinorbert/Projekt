using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Users
    {
        [Key]
        [Column("user_id")]
        public int User_id { get; set; }
        public string UserName { get; set; }
        [Column("password_hash")]
        public byte [] Password_hash { get; set; }
        [Column("password_salt")]
        public byte [] Password_salt { get; set; }
        public string Email { get; set; }



    }
}