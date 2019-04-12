using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class SepetsController : Controller
    {
        private ETICARETEntities db = new ETICARETEntities();

        // GET: Sepets
        public ActionResult Index()
        {
            var sepets = db.Sepets.Include(s => s.AspNetUser).Include(s => s.Urunler);
            return View(sepets.ToList());
        }
        public ActionResult SepeteEkle()
        {

            return RedirectToAction("Index");
        }
        // GET: Sepets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepets.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            return View(sepet);
        }

        // GET: Sepets/Create
        public ActionResult Create()
        {
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.RefUrunID = new SelectList(db.Urunlers, "UrunID", "UrunAdi");
            return View();
        }

        // POST: Sepets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SepetID,RefKulID,RefUrunID,Adet,Toplam")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                db.Sepets.Add(sepet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sepet.RefKulID);
            ViewBag.RefUrunID = new SelectList(db.Urunlers, "UrunID", "UrunAdi", sepet.RefUrunID);
            return View(sepet);
        }

        // GET: Sepets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepets.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sepet.RefKulID);
            ViewBag.RefUrunID = new SelectList(db.Urunlers, "UrunID", "UrunAdi", sepet.RefUrunID);
            return View(sepet);
        }

        // POST: Sepets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SepetID,RefKulID,RefUrunID,Adet,Toplam")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sepet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sepet.RefKulID);
            ViewBag.RefUrunID = new SelectList(db.Urunlers, "UrunID", "UrunAdi", sepet.RefUrunID);
            return View(sepet);
        }

        // GET: Sepets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepets.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            return View(sepet);
        }

        // POST: Sepets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sepet sepet = db.Sepets.Find(id);
            db.Sepets.Remove(sepet);
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
