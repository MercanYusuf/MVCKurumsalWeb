using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GGM.Models.Model;

namespace GGM.Models.DataContext
{
    public class dataContext:DbContext
    {
        public dataContext():base("GGMM")
        {
            
        }
        public DbSet<Adminler> Adminler { get; set; }
       
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<SiteKimlik> SiteKimlik { get; set; }
        public DbSet<Yorum> Yorum { get; set; }
        public DbSet<Referanslar> Referanslar { get; set; }
        public DbSet<MusteriYorumlari> MusteriYorumlari { get; set; }

        public DbSet<HizmetAna> HizmetAna { get; set; }
        public DbSet<HakkimizdaResim> HakkimizdaResim { get; set; }




    }
}