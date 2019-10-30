using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Referanslar")]
    public class Referanslar
    {
        [Key]
        public int ReferansId { get; set; }


        public string ReferansAd { get; set; }

        public string ResimURL { get; set; }
    }
}