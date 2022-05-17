using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDok.Models
{
    public class Grupy
    {
           
            public int GrupyID {get; set; }
            public bool Ksiegowosc { get; set; }
            public bool Kierownictwo { get; set; }
            public bool Administracja { get; set; }
            public bool Szefostwo { get; set; }
            public bool Obsluga_klienta { get; set; }
            public bool Admin { get; set; }
            [ForeignKey("Uzytkownicy")]
            public int Uzytkownik_id { get; set; }
            public Uzytkownicy Uzytkownicy { get; set; }



    }
}