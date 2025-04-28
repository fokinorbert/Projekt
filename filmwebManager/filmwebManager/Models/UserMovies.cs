using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using filmwebManager.Models; 
using Newtonsoft.Json;

namespace filmwebManager.Models
{
    public class UserMovies
    {

        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public int User_id { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Movie")]
        public int Movie_id { get; set; }


        [Key, Column(Order = 2)] 
        public string Status { get; set; }

        [JsonIgnore]
        public virtual Users User { get; set; }

        [JsonIgnore]
        public virtual Movies Movie { get; set; }
    }
}