using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDok.Models;

namespace DataDok.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            if (Session["UserId"] != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    var a = Convert.ToInt32(Session["UserId"]);
                    var uprawnienia = db.Grupy.SqlQuery("SELECT * FROM Grupies  WHERE Uzytkownik_id={0}", a).FirstOrDefault();
                    var uprawnienie_administracja = uprawnienia.Administracja;
                    var uprawnienie_admin = uprawnienia.Admin;
                    if (uprawnienie_administracja == false && uprawnienie_admin == false)
                    {
                        TempData["komunikat"] = "Nie posiadasz uprawnień";
                        return RedirectToAction("../Account/Zalogowany");
                    }


                }
                using (OurDbContext db = new OurDbContext())
                {
                    return View("~/Views/Admin/users.cshtml");

                }
            }
            else
            {
                TempData["komunikat"] = "Musisz byc zalogowany";
                return RedirectToAction("../Account/Login");
            }

        }
        public ActionResult Dodaj()
        {
            return View("~/Views/Admin/Users/Dodaj.cshtml");
        }

        [HttpPost]
        public ActionResult Dodaj(Uzytkownicy uzytkownik)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {

                    db.Uzytkownicy.Add(uzytkownik);
                    db.SaveChanges();
                }
                using (OurDbContext db = new OurDbContext())
                {
                    var group = new Grupy { Ksiegowosc = false, Kierownictwo = false, Obsluga_klienta = false, Administracja = false, Admin = false, Szefostwo = false, Uzytkownik_id = uzytkownik.Uzytkownik_id };
                    db.Grupy.Add(group);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = uzytkownik.Login + " pomyslnie dodany";
            }
            return View("~/Views/Admin/Users/Dodaj.cshtml");
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
                    if (uprawnienie_administracja == false && uprawnienie_admin == false)
                    {
                        TempData["komunikat"] = "Nie posiadasz uprawnień";
                        return RedirectToAction("../Account/Zalgowany");
                    }


                }
                using (OurDbContext db = new OurDbContext())
                {
                    var uzytkownicy = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies").ToList();
                    return View(uzytkownicy);

                }
            }
            else
            {
                TempData["komunikat"] = "Musisz byc zalogowany";
                return RedirectToAction("../Account/Login");
            }

        }
        public ActionResult Edytuj_uzytkownika(int id)
        {
            TempData["id_uzytkownika"] = id;

            using (OurDbContext db = new OurDbContext())
            {
                var grupa = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies WHERE Uzytkownik_id={0}", id).FirstOrDefault();
                if (grupa != null)
                {
                    ViewBag.ID = grupa.Uzytkownik_id;
                    ViewBag.imie = grupa.Imie;
                    ViewBag.nazwisko = grupa.Nazwisko;
                    ViewBag.email = grupa.Email;
                    ViewBag.login = grupa.Login;
                    ViewBag.haslo = grupa.Haslo;
                }

                return View(grupa);

            }

        }
        [HttpPost]
        public ActionResult Zapisz(int id_uzyt, string imie, string nazwisko, string email, string login, string haslo)
        {
            imie = imie;
            nazwisko = nazwisko;
            email = email;
            login = login;
            haslo = haslo;



            using (OurDbContext db = new OurDbContext())
            {
                var users = db.Database.ExecuteSqlCommand("Update Uzytkownicies SET Imie={0}, Nazwisko={1},Email={2}," +
                "Login={3}, Haslo={4} WHERE Uzytkownik_id={5}", imie, nazwisko,
                email, login, haslo, id_uzyt);
                db.SaveChanges();
                var user = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies WHERE Uzytkownik_id={0}", id_uzyt).FirstOrDefault();
                ViewBag.imie = user.Imie;
                ViewBag.nazwisko = user.Nazwisko;
                ViewBag.email = user.Email;
                ViewBag.login = user.Login;
                ViewBag.haslo = user.Haslo;

            }


            TempData["zapisano"] = "Pomyslnie zapisano zmiany";
            return RedirectToAction("Wyswietl_Uzytkownikow");
        }

        public ActionResult U_Wyswietl_Uzytkownikow()
        {
            if (Session["UserId"] != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    var a = Convert.ToInt32(Session["UserId"]);
                    var uprawnienia = db.Grupy.SqlQuery("SELECT * FROM Grupies  WHERE Uzytkownik_id={0}", a).FirstOrDefault();
                    var uprawnienie_administracja = uprawnienia.Administracja;
                    var uprawnienie_admin = uprawnienia.Admin;
                    if (uprawnienie_administracja == false && uprawnienie_admin == false)
                    {
                        TempData["komunikat"] = "Nie posiadasz uprawnień";
                        return RedirectToAction("../Account/Zalgowany");
                    }


                }
                using (OurDbContext db = new OurDbContext())
                {
                    var uzytkownicy = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies").ToList();
                    return View(uzytkownicy);

                }
            }
            else
            {
                TempData["komunikat"] = "Musisz byc zalogowany";
                return RedirectToAction("../Account/Login");
            }

        }
        public ActionResult Usun(int id_uzyt)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var users = db.Database.ExecuteSqlCommand("DELETE FROM Uzytkownicies WHERE Uzytkownik_id={0}", id_uzyt);
                db.SaveChanges();
            }


            TempData["usunieto"] = "Pomyslnie usunięto użytkownika";
            return RedirectToAction("U_Wyswietl_Uzytkownikow");
        }

        public ActionResult Usun_uzytkownika(int id)
        {
            TempData["id_uzytkownika"] = id;

            using (OurDbContext db = new OurDbContext())
            {
                var grupa = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies WHERE Uzytkownik_id={0}", id).FirstOrDefault();
                if (grupa != null)
                {
                    ViewBag.ID = grupa.Uzytkownik_id;
                    ViewBag.imie = grupa.Imie;
                    ViewBag.nazwisko = grupa.Nazwisko;
                    ViewBag.email = grupa.Email;
                    ViewBag.login = grupa.Login;
                    ViewBag.haslo = grupa.Haslo;
                }

                return View(grupa);

            }
        }
    }
}