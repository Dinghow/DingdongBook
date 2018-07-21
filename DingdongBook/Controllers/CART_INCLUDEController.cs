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
    public class CART_INCLUDEController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: CART_INCLUDE
        public ActionResult Index()
        {
            var cART_INCLUDE = db.CART_INCLUDE.Include(c => c.BOOK).Include(c => c.USERS);
            return View(cART_INCLUDE.ToList());
        }

        // GET: CART_INCLUDE/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART_INCLUDE cART_INCLUDE = db.CART_INCLUDE.Find(id);
            if (cART_INCLUDE == null)
            {
                return HttpNotFound();
            }
            return View(cART_INCLUDE);
        }

        // GET: CART_INCLUDE/Create
        public ActionResult Create()
        {
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN");
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: CART_INCLUDE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,BOOK_ID,QUANTITY,PRICE")] CART_INCLUDE cART_INCLUDE)
        {
            if (ModelState.IsValid)
            {
                db.CART_INCLUDE.Add(cART_INCLUDE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART_INCLUDE.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART_INCLUDE.USER_ID);
            return View(cART_INCLUDE);
        }

        // GET: CART_INCLUDE/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART_INCLUDE cART_INCLUDE = db.CART_INCLUDE.Find(id);
            if (cART_INCLUDE == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART_INCLUDE.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART_INCLUDE.USER_ID);
            return View(cART_INCLUDE);
        }

        // POST: CART_INCLUDE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,BOOK_ID,QUANTITY,PRICE")] CART_INCLUDE cART_INCLUDE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cART_INCLUDE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cART_INCLUDE.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cART_INCLUDE.USER_ID);
            return View(cART_INCLUDE);
        }

        // GET: CART_INCLUDE/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CART_INCLUDE cART_INCLUDE = db.CART_INCLUDE.Find(id);
            if (cART_INCLUDE == null)
            {
                return HttpNotFound();
            }
            return View(cART_INCLUDE);
        }

        // POST: CART_INCLUDE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CART_INCLUDE cART_INCLUDE = db.CART_INCLUDE.Find(id);
            db.CART_INCLUDE.Remove(cART_INCLUDE);
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
