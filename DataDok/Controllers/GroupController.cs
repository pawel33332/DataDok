using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDok.Models;
using System.Data.Entity;



namespace DataDok.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Wyswietl_Uzytkownikow()
        {
            if (Session["UserId"] != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    var a = Convert.ToInt32(Session["UserId"]);
                    var uprawnienia = db.Grupy.SqlQuery("SELECT * FROM Grupies  WHERE Uzytkownik_id={0}", a).FirstOrDefault();
                    var uprawnienie_administracja = uprawnienia.Administracja;
                    var uprawnienie_admin = uprawnienia.Admin;
                    if(uprawnienie_administracja==false && uprawnienie_admin==false)
                    {
                        TempData["komunikat"] = "Nie posiadasz uprawnień";
                        return RedirectToAction("../Account/Zalogowany"); 
                    }
                

                }
                using (OurDbContext db = new OurDbContext())
                {
                    var uzytkownicy = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies").ToList();
                    return View(uzytkownicy);

                }
            } else
            {
                TempData["komunikat"] = "Musisz byc zalogowany";
                return RedirectToAction("../Account/Login");
            }
          
        }
        public ActionResult Edytuj_grupe(int id)
        {
            TempData["id_uzytkownika"] = id;
         
            using (OurDbContext db = new OurDbContext())
            {
                var grupa = db.Grupy.SqlQuery("SELECT * FROM Grupies WHERE Uzytkownik_id={0}",id).FirstOrDefault();
                if(grupa != null)
                {
                ViewBag.ID = grupa.Uzytkownik_id;
                ViewBag.ksiegowosc = grupa.Ksiegowosc;
                ViewBag.kierownictwo = grupa.Kierownictwo;
                ViewBag.administracja = grupa.Administracja;
                ViewBag.szefostwo = grupa.Szefostwo;
                ViewBag.obsluga_klienta = grupa.Obsluga_klienta;
                }
                
                return View(grupa);

            }

        }
        [HttpPost]
        public ActionResult Zapisz(int id_uzyt,string ksiegowosc,string kierownictwo,string administracja, string szefostwo, string obsluga_klienta)
        {
            
            if(ksiegowosc==null)
            {
                ksiegowosc = "False";
            } else
            {
                ksiegowosc = "True";
            }
            if (kierownictwo == null)
            {
                kierownictwo = "False";
            }
            else
            {
                kierownictwo = "True";
            }
            if (administracja == null)
            {
               administracja = "False";
            }
            else
            {
                administracja = "True";
            }
            if (szefostwo == null)
            {
                szefostwo = "False";
            }
            else
            {
                szefostwo = "True";
            }
            if (obsluga_klienta == null)
            {
                obsluga_klienta= "False";
            }
            else
            {
                obsluga_klienta = "True";
            }
       
            
            using (OurDbContext db = new OurDbContext())
            {
                var grupy = db.Database.ExecuteSqlCommand("Update Grupies SET Ksiegowosc={0}, Kierownictwo={1},Administracja={2}," +
                "Szefostwo={3}, Obsluga_klienta={4} WHERE Uzytkownik_id={5}", ksiegowosc, kierownictwo,
                administracja, szefostwo,obsluga_klienta, id_uzyt);
                db.SaveChanges();
                var grupa = db.Grupy.SqlQuery("SELECT * FROM Grupies WHERE Uzytkownik_id={0}", id_uzyt).FirstOrDefault();
                ViewBag.ksiegowosc = grupa.Ksiegowosc;
                ViewBag.kierownictwo = grupa.Kierownictwo;
                ViewBag.administracja = grupa.Administracja;
                ViewBag.szefostwo = grupa.Szefostwo;
                ViewBag.obsluga_klienta = grupa.Obsluga_klienta;

            }


            TempData["zapisano"] = "Pomyslnie zapisano przynaleznosc uzytkownika do grupy";
            return RedirectToAction("Wyswietl_uzytkownikow");
        }
      
        }
}