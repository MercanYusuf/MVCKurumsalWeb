using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Hizmet")]
    public class Hizmet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HizmetId { get; set; }
        [DisplayName("Hizmet Mevzuat")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string MevzuatCalismasi { get; set; }

        [DisplayName("Hizmet Ithalat")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string IthalatIslemleri { get; set; }

        [DisplayName("Hizmet Ihracat")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string IhracatIslemleri { get; set; }

        [DisplayName("Hizmet DıgerGumruk")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string GumrukIsleri { get; set; }

        [DisplayName("Hizmet Destek")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string DestekHizmetleri { get; set; }
        [DisplayName("Hizmet Diger")]
        [StringLength(4000, ErrorMessage = "500 Karakter olmalıdır.")]
        public string DigerHizmetler { get; set; }
        [DisplayName("Hizmet Resim")]

        public string ResimURL { get; set; }
    }
}