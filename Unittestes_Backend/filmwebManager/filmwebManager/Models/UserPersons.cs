using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class UserPersons
    {
        public Users User { get; set; }
        public Persons Persons { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }
        [ForeignKey("Persons")]
        public int Person_id { get; set; }
    }
}