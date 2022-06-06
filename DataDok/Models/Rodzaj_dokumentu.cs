using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataDok.Models
{
    public class Rodzaj_dokumentu
    {
        [Key]
        public int Rodzaj_ID { get; set; }

        [ForeignKey("ID_Rodzajow")]
        public int ID_Rodzajow_id { get; set; }
        public ID_Rodzajow ID_Rodzajow { get; set; }

        [ForeignKey("Wlasciwosci")]
        public int Wlasciwosc_id { get; set; }
        public Wlasciwosci Wlasciwosci { get; set; }
        public bool Umowa { get; set; }
        public bool Zlecenie { get; set; }
        public bool Faktura { get; set; }
        public bool Rachunek { get; set; }


    }
}