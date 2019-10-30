﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [DisplayName("Slider Başlık"), StringLength(40, ErrorMessage = "40 karakter olmalıdır.")]
        public string Baslik { get; set; }
        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "150 karakter olmalıdır.")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), StringLength(250, ErrorMessage = "250 karakter olmalıdır. ")]
        public string ResimURL { get; set; }
    }
}