using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class MovieActors
    {
 

        [Key]
        [ForeignKey("Movies")]
        public int Movie_id { get; set; }
        [ForeignKey("Persons")]
        public int Person_id { get; set; }

      
        public Movies Movies { get; set; }
        public Persons Persons { get; set; }
    }
}