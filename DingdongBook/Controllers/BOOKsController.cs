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
    public class BOOKsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: BOOKs
        public ActionResult Index()
        {
            return View(db.BOOK.ToList());
        }

        // GET: BOOKs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // GET: BOOKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BOOKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ISBN,NAME,PRICE,IMAGE,CATEGORY")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.BOOK.Add(bOOK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bOOK);
        }

        // GET: BOOKs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // POST: BOOKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ISBN,NAME,PRICE,IMAGE,CATEGORY")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bOOK);
        }

        // GET: BOOKs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // POST: BOOKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BOOK bOOK = db.BOOK.Find(id);
            db.BOOK.Remove(bOOK);
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
