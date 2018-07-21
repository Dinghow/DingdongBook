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
    public class ORDERSController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: ORDERS
        public ActionResult Index()
        {
            var oRDERS = db.ORDERS.Include(o => o.ADDRESS).Include(o => o.USERS);
            return View(oRDERS.ToList());
        }

        // GET: ORDERS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERS oRDERS = db.ORDERS.Find(id);
            if (oRDERS == null)
            {
                return HttpNotFound();
            }
            return View(oRDERS);
        }

        // GET: ORDERS/Create
        public ActionResult Create()
        {
            ViewBag.ADDRESS_ID = new SelectList(db.ADDRESS, "ID", "NAME");
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: ORDERS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_ID,ADDRESS_ID,QUANTITY,PRICE,REMARK,TIME_START,TIME_GET,STATUS")] ORDERS oRDERS)
        {
            if (ModelState.IsValid)
            {
                db.ORDERS.Add(oRDERS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ADDRESS_ID = new SelectList(db.ADDRESS, "ID", "NAME", oRDERS.ADDRESS_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", oRDERS.USER_ID);
            return View(oRDERS);
        }

        // GET: ORDERS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERS oRDERS = db.ORDERS.Find(id);
            if (oRDERS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ADDRESS_ID = new SelectList(db.ADDRESS, "ID", "NAME", oRDERS.ADDRESS_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", oRDERS.USER_ID);
            return View(oRDERS);
        }

        // POST: ORDERS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_ID,ADDRESS_ID,QUANTITY,PRICE,REMARK,TIME_START,TIME_GET,STATUS")] ORDERS oRDERS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDERS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ADDRESS_ID = new SelectList(db.ADDRESS, "ID", "NAME", oRDERS.ADDRESS_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", oRDERS.USER_ID);
            return View(oRDERS);
        }

        // GET: ORDERS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDERS oRDERS = db.ORDERS.Find(id);
            if (oRDERS == null)
            {
                return HttpNotFound();
            }
            return View(oRDERS);
        }

        // POST: ORDERS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ORDERS oRDERS = db.ORDERS.Find(id);
            db.ORDERS.Remove(oRDERS);
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
