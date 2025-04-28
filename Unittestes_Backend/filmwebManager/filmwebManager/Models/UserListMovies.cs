using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class UserlistMovies
    {
        public int list_id { get; set; }

        public int movie_id { get; set; }
    }
}