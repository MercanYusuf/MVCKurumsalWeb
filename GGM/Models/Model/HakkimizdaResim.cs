using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("HakkımızdaResim")]
    public class HakkimizdaResim
    {
        [Key]
        public int HakkimizdaResimId { get; set; }
        public string ResimURL { get; set; }
    }
}