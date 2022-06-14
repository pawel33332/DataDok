using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDok.Models
{
    public class Wlasciwosci
    {
        [Key]
        public int Wlasciwosci_ID { get; set; }

        [ForeignKey("Waznosc_dokumentu")]
        public int Waznosc_dokumentu_ID { get; set; }
        public Waznosc_dokumentu Waznosc_dokumentu{ get; set; }
    }
}