using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Movies
    {

        [Key]
        public int Movie_id { get; set; }
        public string Title { get; set; }
        [ForeignKey("Genres")]
        public int Genre_id { get; set; }
        [Column("release_year")]
        public int Relase_Year { get; set; }

        [Column("director_id")]
        public int? Director_id { get; set; }
        [Column("img_url")]
        public string Img_Url { get; set; }

        public virtual Genres Genres { get; set; }

        public int? Duration { get; set; }

        [Column("description")]
        public string Description { get; set; } 
    }
}