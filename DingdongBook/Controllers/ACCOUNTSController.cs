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
    public class ACCOUNTSController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: ACCOUNTS
        public ActionResult Index()
        {
            var aCCOUNTS = db.ACCOUNTS.Include(a => a.USERS);
            return View(aCCOUNTS.ToList());
        }

        // GET: ACCOUNTS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNTS aCCOUNTS = db.ACCOUNTS.Find(id);
            if (aCCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNTS);
        }

        // GET: ACCOUNTS/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: ACCOUNTS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PASSWORD,ACCOUNT,AUTHORITY")] ACCOUNTS aCCOUNTS)
        {
            if (ModelState.IsValid)
            {
                db.ACCOUNTS.Add(aCCOUNTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.USERS, "USER_ID", "NAME", aCCOUNTS.ID);
            return View(aCCOUNTS);
        }

        // GET: ACCOUNTS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNTS aCCOUNTS = db.ACCOUNTS.Find(id);
            if (aCCOUNTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.USERS, "USER_ID", "NAME", aCCOUNTS.ID);
            return View(aCCOUNTS);
        }

        // POST: ACCOUNTS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PASSWORD,ACCOUNT,AUTHORITY")] ACCOUNTS aCCOUNTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCOUNTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.USERS, "USER_ID", "NAME", aCCOUNTS.ID);
            return View(aCCOUNTS);
        }

        // GET: ACCOUNTS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNTS aCCOUNTS = db.ACCOUNTS.Find(id);
            if (aCCOUNTS == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNTS);
        }

        // POST: ACCOUNTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ACCOUNTS aCCOUNTS = db.ACCOUNTS.Find(id);
            db.ACCOUNTS.Remove(aCCOUNTS);
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
