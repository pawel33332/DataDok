using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DataDok.Models
{
    public class OurDbContext : DbContext
    {
        public DbSet<Uzytkownicy> Uzytkownicy { get; set; }
        public DbSet<Grupy> Grupy { get; set; }
        public DbSet<Uprawnienia> Uprawnienia { get; set; }
        public DbSet<Dokument> Dokument { get; set; }
        public DbSet<Waznosc_dokumentu> Waznosc_dokumentu { get; set; }
    }
}