using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDok.Models;
public class Params
{
    public int ID { get; set; }
    public string nazwa { get; set; }
    public DateTime data { get; set; }
    public string sciezka { get; set; }
    public int rodzaj { get; set; }
    public bool ksiegowosc { get; set; }
    public bool kierownictwo { get; set; }
    public bool administracja { get; set; }
    public bool szefostwo { get; set; }
    public bool obsluga_klienta { get; set; }
    public bool admin { get; set; }

}
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
            var potwierdzanie_dokumentu = db.Database.ExecuteSqlCommand("INSERT INTO Potwierdzenias(DokumentID, Ksiegowosc, Kierownictwo, Administracja, Szefostwo, Obsluga_klienta, Admin) " +
                "VALUES({0},{1},{2},{3},{4},{5},{6})",dokument_id,ksiegowosc,kierownictwo,administracja,szefostwo,obsluga_klienta,admin);
            db.SaveChanges();
        }
          
            return RedirectToAction("/Moje_dokumenty");
        }
        public ActionResult Wyswietl_dokumenty()
        {
            return View();
        }
        public ActionResult Moje_dokumenty()
        {
           
        int id_uzytkownika = Convert.ToInt32(Session["UserId"]);
            using (OurDbContext db = new OurDbContext())
            {
                //var dokumenty = db.Dokument.SqlQuery("SELECT * FROM Dokuments WHERE Uzytkownik_id={0}", id_uzytkownika).ToList();
                var dokumenty = (from d in db.Dokument
                                 join p in db.Potwierdzenia on d.DokumentID equals p.DokumentID
                                 where d.Uzytkownik_id == id_uzytkownika
                                 select new
                                 {
                                     ID = d.DokumentID,
                                     nazwa = d.nazwa_dokumentu,
                                     data = d.data_dokumentu,
                                     sciezka = d.sciezka,
                                     rodzaj = d.ID_Rodzajow_id,
                                     ksiegowosc = p.Ksiegowosc,
                                     kierownictwo = p.Kierownictwo,
                                     administracja = p.Administracja,
                                     szefostwo = p.Szefostwo,
                                     obsluga_klienta = p.Obsluga_klienta,
                                     admin = p.Admin
                                 }).ToList();
                ViewBag.dokumenty = dokumenty;
                foreach (var p in dokumenty)
                {
                    var suma = Convert.ToInt32(p.ksiegowosc) + Convert.ToInt32(p.kierownictwo) + Convert.ToInt32(p.administracja) + Convert.ToInt32(p.szefostwo)
                    + Convert.ToInt32(p.obsluga_klienta) + Convert.ToInt32(p.admin);
                    if (suma > 5)
                    {
                        TempData["status"] = "Zatwierdzony";
                    }
                    else
                    {
                        TempData["status"] = "Wymaga potwierdzenia przez: ";
                        if (p.ksiegowosc == false)
                        {
                            TempData["status"] += "ksiegowosc, ";
                        }
                        if (p.kierownictwo == false)
                        {
                            TempData["status"] += "kierownictwo, ";
                        }
                        if (p.administracja == false)
                        {
                            TempData["status"] += "administracja, ";
                        }
                        if (p.szefostwo == false)
                        {
                            TempData["status"] += "szefostwo, ";
                        }
                        if (p.obsluga_klienta == false)
                        {
                            TempData["status"] += "obsluge klienta, ";
                        }
                        if (p.admin == false)
                        {
                            TempData["status"] += "admina";
                        }
                    }
                }
                    /*
                    var suma = Convert.ToInt32(ksiegowosc) + Convert.ToInt32(kierownictwo)+Convert.ToInt32(administracja)+ Convert.ToInt32(szefostwo)
                        + Convert.ToInt32(obsluga_klienta) + Convert.ToInt32(admin); //jezeli dokument potwierdzony u wszystkich
                    */
               return View(ViewBag.dokumenty);
                }
            
            }
    }
}