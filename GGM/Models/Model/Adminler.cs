using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Admin")]
    public class Adminler 
   
    { 
   
        [Key]
        public int AdminId { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 karakterden fazla girmeyiniz.")]
        public string Email { get; set; }
        [Required, StringLength(50, ErrorMessage = "40 karakterden fazla girmeyiniz.")]
        public string Sifre { get; set; }

        public string Yetki { get; set; }
    }
}