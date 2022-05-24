using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDok.Models;

namespace DataDok.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.Uzytkownicy.ToList());
            }

        }

        public ActionResult Wyloguj()
        {
            if (Session["Username"] != null)
            {
                Session.Remove("Username");
                Session.RemoveAll();
            }
            return View("~/Views/Home/index.cshtml");

        }
        public ActionResult Rejestracja()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Rejestracja(Uzytkownicy uzytkownik)
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
                ViewBag.Message = uzytkownik.Login + " pomyslnie zarejestrowany";
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uzytkownicy user)
        {

            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.Uzytkownicy.Where(u => u.Login == user.Login && u.Haslo == user.Haslo).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = usr.Uzytkownik_id.ToString();
                    Session["Username"] = usr.Login.ToString();
                    return RedirectToAction("Zalogowany");
                }
                else
                {

                    ModelState.AddModelError("", "Niepoprawny login badz hasło");
                }
            }
            return View();
        }
        public ActionResult Zalogowany()
        {
            if (Session["UserId"] != null)
            {
                return View("~/Views/Home/index.cshtml");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Admin()
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
                    var uzytkownicy = db.Uzytkownicy.SqlQuery("SELECT * FROM Uzytkownicies").ToList();
                    return View("~/Views/Admin/index.cshtml");

                }
            }
            else
            {
                TempData["komunikat"] = "Musisz byc zalogowany";
                return RedirectToAction("../Account/Login");
            }
        }
        
    }

}
