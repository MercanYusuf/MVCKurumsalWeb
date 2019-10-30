using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("SiteKimlik")]
    public class SiteKimlik
    {
        [Key]
        public int KimlikId { get; set; }

        [DisplayName("Site Başlık")]
        [Required, StringLength(250, ErrorMessage = "250 karakterden fazla girmeyiniz")]
        public string Title { get; set; }
        [DisplayName("Site Anahtar Kelimeler")]
        [Required, StringLength(250, ErrorMessage = "300 karakterden fazla girmeyiniz")]
        public string Keywords { get; set; }
        [Required, StringLength(250, ErrorMessage = "300 karakterden fazla girmeyiniz")]
        [DisplayName("Site Açıklama")]
        public string Description { get; set; }
        [Required, StringLength(250, ErrorMessage = "300 karakterden fazla girmeyiniz")]

        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }

        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }
    }
}