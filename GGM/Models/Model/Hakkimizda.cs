using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int HakkimizdaId { get; set; }
        [Required]
        [DisplayName("Hakkımızda Açıklama")]
        public string Aciklama { get; set; }
        [Required]
        [DisplayName("Kurumsal Açıklama")]
        public string Kurumsal { get; set; }
        [Required]
        [DisplayName("Vizyon Açıklama")]
        public string Vizyon { get; set; }
        [Required]
        [DisplayName("Misyon Açıklama")]
        public string Misyon { get; set; }
        [Required]
        [DisplayName("Kalite Politikamız")]
        public string KalitePolitikamiz { get; set; }
        [Required]
        [DisplayName("Değerlerimiz")]
        public string Degerlerimiz { get; set; }

        public string ResimURL { get; set; }
     






    }
}