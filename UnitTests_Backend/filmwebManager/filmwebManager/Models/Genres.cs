using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Genres
    {
        [Key]
        [Column("genre_id")]
        public int Genre_id { get; set; }
        [Column("genre_name")]
        public string Genre_name { get; set; }

    }
}