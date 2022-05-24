using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace DataDok.Models
{
    
    public class Uzytkownicy
    {
        [Key]
        public int Uzytkownik_id { get; set;}

        [Required(ErrorMessage="Imie jest wymagane.")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Login jest wymagany.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Haslo jest wymagane.")]

        public string Haslo { get; set; }

    }

}