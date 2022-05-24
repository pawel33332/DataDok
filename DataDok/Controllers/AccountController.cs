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
        public ActionResult Rejestracja()
        {
            return View();
        }

        public ActionResult Wyloguj()
        {
            if (Session["Username"]!=null)
            {
                Session.Remove("Username");
                Session.RemoveAll();
            }
            return View("~/Views/Home/index.cshtml");
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
            if(Session["UserId"]!=null)
            {
                return View("~/Views/Home/index.cshtml");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

       

    }
}