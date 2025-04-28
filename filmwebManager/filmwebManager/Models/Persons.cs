using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace filmwebManager.Models
{
    public class Persons
    {
        [Key]
        public int Person_id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        [Column("birth_date")]
        public DateTime Date {  get; set; }
        public string Birth_Place { get; set; }
    }
}