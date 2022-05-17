using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataDok.Models
{
    public class Dokument
    {
        public int DokumentID { get; set; }
        public string nazwa_dokumentu { get; set; }
        public byte[] zawartosc { get; set; }
        [ForeignKey("Uzytkownicy")]
        public int Uzytkownik_id { get; set; }
        public Uzytkownicy Uzytkownicy { get; set; }


    }
}