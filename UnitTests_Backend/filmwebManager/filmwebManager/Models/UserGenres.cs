using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class UserGenres
    {

        [ForeignKey("User")]
        [Column("user_id")]
        public int User_id { get; set; }
        [ForeignKey("Genres")]
        [Column("genre_id")]
        public int Genre_id { get; set; }

        public Users User { get; set; }
        public Genres Genres { get; set; }

    }
}