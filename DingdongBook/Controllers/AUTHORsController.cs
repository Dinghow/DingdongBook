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
    public class AUTHORsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: AUTHORs
        public ActionResult Index()
        {
            return View(db.AUTHOR.ToList());
        }

        // GET: AUTHORs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHOR.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // GET: AUTHORs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AUTHORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME")] AUTHOR aUTHOR)
        {
            if (ModelState.IsValid)
            {
                db.AUTHOR.Add(aUTHOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aUTHOR);
        }

        // GET: AUTHORs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHOR.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: AUTHORs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME")] AUTHOR aUTHOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aUTHOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aUTHOR);
        }

        // GET: AUTHORs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTHOR aUTHOR = db.AUTHOR.Find(id);
            if (aUTHOR == null)
            {
                return HttpNotFound();
            }
            return View(aUTHOR);
        }

        // POST: AUTHORs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            AUTHOR aUTHOR = db.AUTHOR.Find(id);
            db.AUTHOR.Remove(aUTHOR);
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
