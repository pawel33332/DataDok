using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DataDok.Models
{
    public class Potwierdzenia
    {
        [Key]
        [ForeignKey("Dokument")]
        public int DokumentID { get; set; }
        public Dokument Dokument { get; set; }
        public bool Ksiegowosc { get; set; }
        public bool Kierownictwo { get; set; }
        public bool Administracja { get; set; }
        public bool Szefostwo { get; set; }
        public bool Obsluga_klienta { get; set; }
        public bool Admin { get; set; }

    }
}