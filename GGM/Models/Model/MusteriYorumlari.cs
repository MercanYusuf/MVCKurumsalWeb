using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GGM.Models.Model
{
    [Table("Müşteri Yorumları")]
    public class MusteriYorumlari
    {
        public int MusteriYorumlariId { get; set; }
        public string MusteriYorum { get; set; }
        public string MusteriAd { get; set; }
        public string ResimURL { get; set; }
        public string Link { get; set; }
    }
}