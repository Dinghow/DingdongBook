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
    public class COMMENT_FEEDBACKController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: COMMENT_FEEDBACK
        public ActionResult Index()
        {
            var cOMMENT_FEEDBACK = db.COMMENT_FEEDBACK.Include(c => c.COMMENTS).Include(c => c.USERS);
            return View(cOMMENT_FEEDBACK.ToList());
        }

        // GET: COMMENT_FEEDBACK/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT_FEEDBACK cOMMENT_FEEDBACK = db.COMMENT_FEEDBACK.Find(id);
            if (cOMMENT_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENT_FEEDBACK);
        }

        // GET: COMMENT_FEEDBACK/Create
        public ActionResult Create()
        {
            ViewBag.COMMENT_ID = new SelectList(db.COMMENTS, "ID", "TITLE");
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME");
            return View();
        }

        // POST: COMMENT_FEEDBACK/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,COMMENT_ID,ATTITUDE,TIME")] COMMENT_FEEDBACK cOMMENT_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.COMMENT_FEEDBACK.Add(cOMMENT_FEEDBACK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.COMMENT_ID = new SelectList(db.COMMENTS, "ID", "TITLE", cOMMENT_FEEDBACK.COMMENT_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENT_FEEDBACK.USER_ID);
            return View(cOMMENT_FEEDBACK);
        }

        // GET: COMMENT_FEEDBACK/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT_FEEDBACK cOMMENT_FEEDBACK = db.COMMENT_FEEDBACK.Find(id);
            if (cOMMENT_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            ViewBag.COMMENT_ID = new SelectList(db.COMMENTS, "ID", "TITLE", cOMMENT_FEEDBACK.COMMENT_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENT_FEEDBACK.USER_ID);
            return View(cOMMENT_FEEDBACK);
        }

        // POST: COMMENT_FEEDBACK/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,COMMENT_ID,ATTITUDE,TIME")] COMMENT_FEEDBACK cOMMENT_FEEDBACK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMENT_FEEDBACK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.COMMENT_ID = new SelectList(db.COMMENTS, "ID", "TITLE", cOMMENT_FEEDBACK.COMMENT_ID);
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "NAME", cOMMENT_FEEDBACK.USER_ID);
            return View(cOMMENT_FEEDBACK);
        }

        // GET: COMMENT_FEEDBACK/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT_FEEDBACK cOMMENT_FEEDBACK = db.COMMENT_FEEDBACK.Find(id);
            if (cOMMENT_FEEDBACK == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENT_FEEDBACK);
        }

        // POST: COMMENT_FEEDBACK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            COMMENT_FEEDBACK cOMMENT_FEEDBACK = db.COMMENT_FEEDBACK.Find(id);
            db.COMMENT_FEEDBACK.Remove(cOMMENT_FEEDBACK);
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
