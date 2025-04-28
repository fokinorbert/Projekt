using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
	public class UserLists
	{
        [Key]
		public int list_id { get; set; }
        public Users user { get; set; } 
        [ForeignKey("user")] 
        public int user_id { get; set; }
        public string list_name { get; set; }
    }
}