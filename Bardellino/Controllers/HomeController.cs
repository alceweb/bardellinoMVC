using Bardellino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Bardellino.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var oggi = DateTime.Today;
            var promo = db.Promos.ToList();
            return View(promo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dove()
        {
            return View();
        }

        public ActionResult Galleria()
        {
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/"), "*.*", SearchOption.AllDirectories).ToList();
            ViewBag.Immagini = immagini.ToList();
            return View();
        }
    }
}