using Bardellino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace Bardellino.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //seleziono le promo da visualizzare
            var adesso = DateTime.Today;
            var promo = db.Promos.Where(p=>p.Attiva == true & p.DataI <= adesso & p.DataF >= adesso).ToList();
            ViewBag.PromoCount = promo.Count();
            //Creo la lista di immagini di sfondo
            var immagini = db.Slides;
            ViewBag.Immagini = immagini.OrderBy(s=>s.Posizione).ToList();

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
            ViewBag.Cultura = new HtmlString(Thread.CurrentThread.CurrentCulture.Name);
            ViewBag.UICultura = new HtmlString(Thread.CurrentThread.CurrentUICulture.Name);
            Console.WriteLine("The current UI culture is {0}",
            Thread.CurrentThread.CurrentUICulture.Name);

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Contact(InfoViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage(
                    "webservice@cascinabardellino.it",
                    "cesare@cr-consult.eu, cascinabardellino@gmail.com",
                    "Richiesta informazioni dal sito cascinabardellino.it",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.cascinabardellino.itg<hr/>Richiesta: <strong>" +
                    contatti.Messaggio +
                    "</strong></li>"
                    );
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return RedirectToAction("FormOk", "Home");
            }
            return View(contatti);
        }

        public ActionResult FormOk()
        {
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