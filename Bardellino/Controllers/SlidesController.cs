using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bardellino.Models;
using System.IO;
using System.Web.Helpers;

namespace Bardellino.Controllers
{
    public class SlidesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slides
        public ActionResult Index()
        {
            return View(db.Slides.ToList());
        }

        // GET: Slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slides/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Slide_Id,Posizione,Testo,Immagine")] Slide slide, HttpPostedFileBase file)
        {
            if(file != null)
            {
                int num = db.Slides.OrderByDescending(u => u.Posizione).Select(s=>s.Posizione).FirstOrDefault();
                int nuovo = num + 1;
                slide.Immagine = nuovo + ".jpg";
                slide.Posizione =nuovo;
                if (ModelState.IsValid)
                {
                    if (file != null)
                    { 
                        try
                        {
                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Slide/"), nuovo + ".jpg");
                            WebImage img = new WebImage(file.InputStream);
                            var larghezza = img.Width;
                            var altezza = img.Height;
                            var rapportoO = larghezza / altezza;
                            var rapportoV = altezza / larghezza;
                            if (altezza > 1900 | larghezza > 1900)
                            {
                                if (rapportoO >= 1)
                                {
                                    ViewBag.Message = "Attendi la fine del download...";
                                    img.Resize(1900, 1900 / rapportoO);
                                    img.Save(path);
                                    ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                                }
                                else
                                {
                                    img.Resize(800 / rapportoV, 800);
                                    img.Save(path);
                                    ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                                }
                            }
                            else
                            {
                                if (rapportoO >= 1)
                                {
                                    ViewBag.Message = "Attendi la fine del download...";
                                    img.Save(path);
                                    ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                                }
                                else
                                {
                                    ViewBag.Message = "Attendi la fine del download...";
                                    img.Save(path);
                                    ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        }
                        db.Slides.Add(slide);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
           else
            {
                ViewBag.Message = "Devi scegliere un file";
            }

            return View(slide);
        }

        // GET: Slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Slide_Id,Posizione,Testo,Immagine")] Slide slide, HttpPostedFileBase file)
        {
            if(file != null)
            {
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini/Slide/"), slide.Immagine);
                    WebImage img = new WebImage(file.InputStream);
                    var larghezza = img.Width;
                    var altezza = img.Height;
                    var rapportoO = larghezza / altezza;
                    var rapportoV = altezza / larghezza;
                    if (altezza > 1900 | larghezza > 1900)
                    {
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(1900, 1900 / rapportoO);
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            img.Resize(800 / rapportoV, 800);
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }
                    else
                    {
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            var path = Path.Combine(Server.MapPath("~/Content/Immagini/Slide/"), slide.Immagine);
            System.IO.File.Delete(path);
            db.Slides.Remove(slide);
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
