using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDok.Models
{
    public enum Waznosc_dok
    {
        Bardzo_wazny,
        Malo_wazny,
        Srednio_wazny
    }
    public class Waznosc_dokumentu
    {
        public int Waznosc_dokumentuID { get; set; }
        public Waznosc_dok waznosc  { get; set; }
        [ForeignKey("Dokument")]
        public int DokumentID { get; set; }
        public Dokument Dokument { get; set; }
    }
}