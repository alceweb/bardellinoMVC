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
using System.IO;

namespace Bardellino.Controllers
{
    public class ServizisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Servizis
        public ActionResult Index()
        {
            var servizi = db.Servizis.ToList();
            ViewBag.ServiziCount = servizi.Count();
            return View(servizi);
        }

        public ActionResult IndexUt(InfoViewModels contatti)
        {
            var servizi = db.Servizis.Where(s => s.Pubblica == true).ToList();
            ViewBag.Servizi = servizi;
            return View(servizi);
        }

        // GET: Servizis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // GET: Servizis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servizis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Servizo_Id,Servizio,Pubblica", Exclude ="Descrizione")] Servizi servizi)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            servizi.Descrizione = collection["Descrizione"];

            if (ModelState.IsValid)
            {
                db.Servizis.Add(servizi);
                db.SaveChanges();
                Directory.CreateDirectory(Server.MapPath("~/Content/Immagini/Servizi/" + servizi.Servizo_Id));
                return RedirectToAction("IndexUt");
            }

            return View(servizi);
        }

        // GET: Servizis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Servizo_Id,Servizio,Pubblica", Exclude ="Descrizione")] Servizi servizi)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            servizi.Descrizione = collection["Descrizione"];

            if (ModelState.IsValid)
            {
                db.Entry(servizi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexUt");
            }
            return View(servizi);
        }

        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        [HttpPost]
        public ActionResult EditImg(IEnumerable<HttpPostedFileBase> files, int? id)
        {
            foreach (var file in files)
            {
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Servizi/" + id + "/"), fileName);
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
                                ViewBag.Message = "Attendi la fine del download...";
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
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                }
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/" + id + "/"));
            ViewBag.Immagini = immagini.ToList();
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);

        }

        public ActionResult EditImgP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/"));
            ViewBag.Immagini = immagini.ToList();
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        [HttpPost]
        public ActionResult EditImgP(HttpPostedFileBase file, int? id)
        {
            if (file != null)
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini/Servizi/"), id + ".jpg");
                    WebImage img = new WebImage(file.InputStream);
                    var larghezza = img.Width;
                    var altezza = img.Height;
                    var rapportoO = larghezza / altezza;
                    var rapportoV = altezza / larghezza;
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(100, 100 / rapportoO);
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            img.Resize(50 / rapportoV, 50);
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Devi scegliere un file";
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/"));
            ViewBag.Immagini = immagini.ToList();
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);

        }

        public ActionResult EditImgTot()
        {
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/Tot/"));
            ViewBag.Immagini = immagini.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult EditImgTot(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Servizi/Tot/"), fileName);
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
                                ViewBag.Message = "Attendi la fine del download...";
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
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                }
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/Tot/"));
            ViewBag.Immagini = immagini.ToList();
            return View();

        }

        [Authorize(Users ="cascinabardellino@gmail.com")]
        public ActionResult EditImgCel()
        {
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/Tot/"));
            ViewBag.Immagini = immagini.ToList();
            return View();
        }

        public ActionResult ImgRotateD(string file)
        {
            string path = Server.MapPath("~/Content/Immagini/Servizi/" + file);
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipXY);
            img.Save(path);
            img.Dispose();
            return RedirectToAction("Galleria", "Servizis");
        }
        [HttpPost]
        public ActionResult EditImgCel(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Servizi/Tot/"), fileName);
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
                                ViewBag.Message = "Attendi la fine del download...";
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
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                }
            }
            var immagini = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/Tot/"));
            ViewBag.Immagini = immagini.ToList();
            return View();

        }

        public ActionResult DeleteImgTot()
        {
            ViewBag.File = Request.QueryString["file"];
            return View();
        }
        [HttpPost, ActionName("DeleteImgTot")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImgTotConfirmed()
        {
            var file = "~/Content/Immagini/Servizi/Tot/" + Request.QueryString["file"];
            System.IO.File.Delete(Server.MapPath(file));
            return RedirectToAction("EditImgTot", "Servizis");

        }

        public ActionResult DeleteImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.File = Request.QueryString["file"];
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        [HttpPost, ActionName("DeleteImg")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImgConfirmed(int id)
        {
            var file = "~/Content/Immagini/Servizi/" + id + "/" + Request.QueryString["file"];
            System.IO.File.Delete(Server.MapPath(file));
            return RedirectToAction("EditImg", "Servizis", new { id = id });

        }

        public ActionResult Galleria()
        {
            var imgTot = Directory.GetFiles(Server.MapPath("/Content/Immagini/Servizi/Tot"));
            ViewBag.Immagini = imgTot.ToList();

            var servizi = db.Servizis.OrderBy(s => s.Servizio).ToList();
            return View(servizi);
        }
        // GET: Servizis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var files = Directory.EnumerateFiles(Server.MapPath("/Content/Immagini/Servizi/" + id));
            ViewBag.Files = files;
            ViewBag.FilesCount = files.Count();
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servizi servizi = db.Servizis.Find(id);
            db.Servizis.Remove(servizi);
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
