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
    public class ORDER_INCLUDEController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: ORDER_INCLUDE
        public ActionResult Index()
        {
            var oRDER_INCLUDE = db.ORDER_INCLUDE.Include(o => o.BOOK).Include(o => o.ORDERS);
            return View(oRDER_INCLUDE.ToList());
        }

        // GET: ORDER_INCLUDE/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_INCLUDE oRDER_INCLUDE = db.ORDER_INCLUDE.Find(id);
            if (oRDER_INCLUDE == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_INCLUDE);
        }

        // GET: ORDER_INCLUDE/Create
        public ActionResult Create()
        {
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN");
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "REMARK");
            return View();
        }

        // POST: ORDER_INCLUDE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDER_ID,BOOK_ID,QUANTITY,PRICE")] ORDER_INCLUDE oRDER_INCLUDE)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_INCLUDE.Add(oRDER_INCLUDE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", oRDER_INCLUDE.BOOK_ID);
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "REMARK", oRDER_INCLUDE.ORDER_ID);
            return View(oRDER_INCLUDE);
        }

        // GET: ORDER_INCLUDE/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_INCLUDE oRDER_INCLUDE = db.ORDER_INCLUDE.Find(id);
            if (oRDER_INCLUDE == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", oRDER_INCLUDE.BOOK_ID);
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "REMARK", oRDER_INCLUDE.ORDER_ID);
            return View(oRDER_INCLUDE);
        }

        // POST: ORDER_INCLUDE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ORDER_ID,BOOK_ID,QUANTITY,PRICE")] ORDER_INCLUDE oRDER_INCLUDE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER_INCLUDE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", oRDER_INCLUDE.BOOK_ID);
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "REMARK", oRDER_INCLUDE.ORDER_ID);
            return View(oRDER_INCLUDE);
        }

        // GET: ORDER_INCLUDE/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_INCLUDE oRDER_INCLUDE = db.ORDER_INCLUDE.Find(id);
            if (oRDER_INCLUDE == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_INCLUDE);
        }

        // POST: ORDER_INCLUDE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ORDER_INCLUDE oRDER_INCLUDE = db.ORDER_INCLUDE.Find(id);
            db.ORDER_INCLUDE.Remove(oRDER_INCLUDE);
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
