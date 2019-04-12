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
    public class SiparisController : Controller
    {
        private ETICARETEntities db = new ETICARETEntities();

        // GET: Siparis
        public ActionResult Index()
        {
            var siparis = db.Siparis.Include(s => s.AspNetUser);
            return View(siparis.ToList());
        }

        // GET: Siparis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sipari sipari = db.Siparis.Find(id);
            if (sipari == null)
            {
                return HttpNotFound();
            }
            return View(sipari);
        }

        // GET: Siparis/Create
        public ActionResult Create()
        {
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Siparis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiparisID,RefKulID,Ad,Soyad,Adres,Telefon,TCKimlik,Tarih")] Sipari sipari)
        {
            if (ModelState.IsValid)
            {
                db.Siparis.Add(sipari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sipari.RefKulID);
            return View(sipari);
        }

        // GET: Siparis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sipari sipari = db.Siparis.Find(id);
            if (sipari == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sipari.RefKulID);
            return View(sipari);
        }

        // POST: Siparis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiparisID,RefKulID,Ad,Soyad,Adres,Telefon,TCKimlik,Tarih")] Sipari sipari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sipari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefKulID = new SelectList(db.AspNetUsers, "Id", "Email", sipari.RefKulID);
            return View(sipari);
        }

        // GET: Siparis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sipari sipari = db.Siparis.Find(id);
            if (sipari == null)
            {
                return HttpNotFound();
            }
            return View(sipari);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sipari sipari = db.Siparis.Find(id);
            db.Siparis.Remove(sipari);
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
