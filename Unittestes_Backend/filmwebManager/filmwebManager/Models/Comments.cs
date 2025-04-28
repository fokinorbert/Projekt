using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Comments
    {
        public Users User { get; set; }
        public Movies Movies { get; set; }

        [Key]
        public int Comment_id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int User_id { get; set; }

        [ForeignKey("Movies")]
        [Column("movie_id")]
        public int Movie_id { get; set; }

        public string Comment { get; set; }
        public DateTime Created_At { get; set; }

        [NotMapped]
        public string ImgUrl { get; set; }


    }
}