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
    public class COMMENTSController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: COMMENTS
        public ActionResult Index()
        {
            var cOMMENTS = db.COMMENTS.Include(c => c.BOOK).Include(c => c.USERS);
            return View(cOMMENTS.ToList());
        }

        // GET: COMMENTS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENTS cOMMENTS = db.COMMENTS.Find(id);
            if (cOMMENTS == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENTS);
        }

        // GET: COMMENTS/Create
        public ActionResult Create()
        {
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN");
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: COMMENTS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_ID,BOOK_ID,TITLE,CONTENT,TIME,SCORE,TOTAL_LIKE,TOTAL_DISLIKE")] COMMENTS cOMMENTS)
        {
            if (ModelState.IsValid)
            {
                db.COMMENTS.Add(cOMMENTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cOMMENTS.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENTS.USER_ID);
            return View(cOMMENTS);
        }

        // GET: COMMENTS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENTS cOMMENTS = db.COMMENTS.Find(id);
            if (cOMMENTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cOMMENTS.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENTS.USER_ID);
            return View(cOMMENTS);
        }

        // POST: COMMENTS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_ID,BOOK_ID,TITLE,CONTENT,TIME,SCORE,TOTAL_LIKE,TOTAL_DISLIKE")] COMMENTS cOMMENTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMENTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOOK_ID = new SelectList(db.BOOK, "ID", "ISBN", cOMMENTS.BOOK_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENTS.USER_ID);
            return View(cOMMENTS);
        }

        // GET: COMMENTS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENTS cOMMENTS = db.COMMENTS.Find(id);
            if (cOMMENTS == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENTS);
        }

        // POST: COMMENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            COMMENTS cOMMENTS = db.COMMENTS.Find(id);
            db.COMMENTS.Remove(cOMMENTS);
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
