using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDok.Models;

namespace DataDok.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dodaj_dokument()
        {
            @TempData["id_uzytkownika"] = Convert.ToInt32(Session["UserId"]);
            return View();
        }
        [HttpPost]
        public ActionResult Dodawanie_dokumentu(int id_uzyt, string nazwa_dokumentu, int typ, string data_dokumentu, string file,
         string ksiegowosc, string kierownictwo, string administracja, string szefostwo, string obsluga_klienta, string admin)
        {
            if (ksiegowosc == null)
            {
                ksiegowosc = "True";
            }
            else
            {
                ksiegowosc = "False";
            }
            if (kierownictwo == null)
            {
                kierownictwo = "True";
            }
            else
            {
                kierownictwo = "False";
            }
            if (administracja == null)
            {
                administracja = "True";
            }
            else
            {
                administracja = "False";
            }
            if (szefostwo == null)
            {
                szefostwo = "True";
            }
            else
            {
                szefostwo = "False";
            }
            if (obsluga_klienta == null)
            {
                obsluga_klienta = "True";
            }
            else
            {
                obsluga_klienta = "False";
            }
            if (admin == null)
            {
                admin = "True";
            }
            else
            {
                admin = "False";
            }

            using (OurDbContext db = new OurDbContext())
        {
            var dodawanie_dokumentu= db.Database.ExecuteSqlCommand("INSERT INTO Dokuments(nazwa_dokumentu, Uzytkownik_id,sciezka,data_dokumentu,ID_Rodzajow_id) " +
            "VALUES({0},{1},{2},{3}, {4})", nazwa_dokumentu, id_uzyt, file, data_dokumentu, typ);
            var id= db.Dokument.SqlQuery("SELECT * FROM Dokuments Order BY DokumentID DESC").FirstOrDefault();
            int dokument_id=id.DokumentID;
            var potwierdzanie_dokuemntu = db.Database.ExecuteSqlCommand("INSERT INTO Potwierdzenias(DokumentID, Ksiegowosc, Kierownictwo, Administracja, Szefostwo, Obsluga_klienta, Admin) " +
                "VALUES({0},{1},{2},{3},{4},{5},{6})",dokument_id,ksiegowosc,kierownictwo,administracja,szefostwo,obsluga_klienta,admin);
            db.SaveChanges();
        }
          
            return RedirectToAction("../Account/Login");
        }
        public ActionResult Wyswietl_dokumenty()
        {
            return View();
        }
    }
}