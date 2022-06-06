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
            return View();
        }
        public ActionResult Wyswietl_dokumenty()
        {
            return View();
        }
    }
}