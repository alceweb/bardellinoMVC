using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bardellino.Models;
using System.Web.Helpers;

namespace Bardellino.Controllers
{
    public class PromoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promoes
        public ActionResult Index()
        {
            var promo = db.Promos.OrderBy(p=>p.DataI).ToList();
            ViewBag.PromoCount = promo.Count();
            return View(promo);
        }

        // GET: Promoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // GET: Promoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Promo_Id,DataI,DataF,Nome,Attiva", Exclude ="Descrizione")] Promo promo)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            promo.Descrizione = collection["Descrizione"];

            if (ModelState.IsValid)
            {
                db.Promos.Add(promo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promo);
        }

        // GET: Promoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // POST: Promoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Promo_Id,DataI,DataF,Nome,Attiva", Exclude = "Descrizione")] Promo promo)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            promo.Descrizione = collection["Descrizione"];

            if (ModelState.IsValid)
            {
                db.Entry(promo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promo);
        }

        // GET: Promoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promo promo = db.Promos.Find(id);
            if (promo == null)
            {
                return HttpNotFound();
            }
            return View(promo);
        }

        // POST: Promoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promo promo = db.Promos.Find(id);
            db.Promos.Remove(promo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
