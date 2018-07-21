using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;

namespace DingdongBook.Controllers
{
    public class ADDRESSesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: ADDRESSes
        public ActionResult Index()
        {
            return View(db.ADDRESS.ToList());
        }

        // GET: ADDRESSes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADDRESS aDDRESS = db.ADDRESS.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDDRESS);
        }

        // GET: ADDRESSes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADDRESSes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,PHONE,COUNTRY,PROVINCE,CITY,DISTRICT")] ADDRESS aDDRESS)
        {
            if (ModelState.IsValid)
            {
                db.ADDRESS.Add(aDDRESS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDDRESS);
        }

        // GET: ADDRESSes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADDRESS aDDRESS = db.ADDRESS.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDDRESS);
        }

        // POST: ADDRESSes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,PHONE,COUNTRY,PROVINCE,CITY,DISTRICT")] ADDRESS aDDRESS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDDRESS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDDRESS);
        }

        // GET: ADDRESSes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADDRESS aDDRESS = db.ADDRESS.Find(id);
            if (aDDRESS == null)
            {
                return HttpNotFound();
            }
            return View(aDDRESS);
        }

        // POST: ADDRESSes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ADDRESS aDDRESS = db.ADDRESS.Find(id);
            db.ADDRESS.Remove(aDDRESS);
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
