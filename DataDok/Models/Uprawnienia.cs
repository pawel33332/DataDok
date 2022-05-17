using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataDok.Models
{
    public class Uprawnienia
    {
        public int UprawnieniaID { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }

        public bool Update { get; set; }
        public bool Delete { get; set; }
        [ForeignKey("Grupy")]
        public int GrupyID { get; set; }
        public Grupy Grupy { get; set; }

    }
}