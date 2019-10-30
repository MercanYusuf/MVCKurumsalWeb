using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("İletişim")]
    public class Iletisim
    {
        [Key] public int IletisimId { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır.")]

        public string Adres { get; set; }
        [StringLength(250, ErrorMessage = "250 karakter olmalıdır.")]
        public string Telefon { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }


    }
}