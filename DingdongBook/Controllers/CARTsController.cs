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
    public class CARTsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: CARTs
        public ActionResult Index()
        {
            var cART = db.CART.Include(c => c.BOOK).Include(c => c.USERS);
            return View(cART.ToList());
        }

        // GET: CARTs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CART.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            return View(cART);
        }

        // GET: CARTs/Create
        public ActionResult Create()
        {
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN");
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: CARTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,BOOK_ID,QUANTITY,TIME_START")] CART cART)
        {
            if (ModelState.IsValid)
            {
                db.CART.Add(cART);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART.USER_ID);
            return View(cART);
        }

        // GET: CARTs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CART.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART.USER_ID);
            return View(cART);
        }

        // POST: CARTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,BOOK_ID,QUANTITY,TIME_START")] CART cART)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cART).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART.USER_ID);
            return View(cART);
        }

        // GET: CARTs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART cART = db.CART.Find(id);
            if (cART == null)
            {
                return HttpNotFound();
            }
            return View(cART);
        }

        // POST: CARTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CART cART = db.CART.Find(id);
            db.CART.Remove(cART);
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
