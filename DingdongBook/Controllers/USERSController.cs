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
    public class USERSController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: USERS
        public ActionResult Index()
        {
            var uSERS = db.USERS.Include(u => u.ACCOUNTS);
            return View(uSERS.ToList());
        }

        // GET: USERS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS uSERS = db.USERS.Find(id);
            if (uSERS == null)
            {
                return HttpNotFound();
            }
            return View(uSERS);
        }

        // GET: USERS/Create
        public ActionResult Create()
        {
            ViewBag.USER_ID = new SelectList(db.ACCOUNTS, "ID", "PASSWORD");
            return View();
        }

        // POST: USERS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,NAME,GENDER,EMAIL")] USERS uSERS)
        {
            if (ModelState.IsValid)
            {
                db.USERS.Add(uSERS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.ACCOUNTS, "ID", "PASSWORD", uSERS.USER_ID);
            return View(uSERS);
        }

        // GET: USERS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS uSERS = db.USERS.Find(id);
            if (uSERS == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.ACCOUNTS, "ID", "PASSWORD", uSERS.USER_ID);
            return View(uSERS);
        }

        // POST: USERS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,NAME,GENDER,EMAIL")] USERS uSERS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSERS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.ACCOUNTS, "ID", "PASSWORD", uSERS.USER_ID);
            return View(uSERS);
        }

        // GET: USERS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERS uSERS = db.USERS.Find(id);
            if (uSERS == null)
            {
                return HttpNotFound();
            }
            return View(uSERS);
        }

        // POST: USERS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            USERS uSERS = db.USERS.Find(id);
            db.USERS.Remove(uSERS);
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
