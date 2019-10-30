using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
     [Table("Hizmet Anasayfa")]
    public class HizmetAna
    {
        [Key]
        public int HizmetAnaId { get; set; }

        public string Aciklama { get; set; }
        public string  ResimURL { get; set; }
    }
}