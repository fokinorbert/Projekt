using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Ratings
    {
        public Users User { get; set; }
        public virtual Movies Movies { get; set; }
        [Key]
        [Column("rating_id")]
        public int Rating_id { get; set; }
        [ForeignKey("User")]
        [Column("user_id")]
        public int User_id { get; set; }
        [ForeignKey("Movies")]
        [Column("movie_id")]
        public int Movie_id { get; set; }
        public int Rating { get; set; }

        [NotMapped]
        public string MovieTitle { get; set; }


    }
}