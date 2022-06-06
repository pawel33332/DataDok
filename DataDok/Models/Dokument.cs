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
        public string sciezka { get; set; }
        public DateTime data_dokumentu { get; set;}
        [ForeignKey("Uzytkownicy")]
        public int Uzytkownik_id { get; set; }
        public Uzytkownicy Uzytkownicy { get; set; }

        [ForeignKey("ID_Rodzajow")]
        public int ID_Rodzajow_id{ get; set; }
        public ID_Rodzajow ID_Rodzajow{ get; set; }


    }
}