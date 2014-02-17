using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class SessionController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /Session/

        public ActionResult Index()
        {
            var session = db.session.Include(s => s.testconfig).Include(s => s.user);
            return View(session.ToList());
        }

        /// <summary>
        /// Для просмотра своей статистики
        /// </summary>
        /// <returns></returns>
        public ActionResult MyResults()
        {
            var session = db.session.Include(s => s.testconfig).Include(s => s.user);
            return View(session.Where(s => s.user.user_name == User.Identity.Name).ToList());
        }

        //
        // GET: /Session/Details/5

        public ActionResult Details(int id = 0)
        {
            session session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // GET: /Session/Create

        public ActionResult Create()
        {
            ViewBag.testconfig_id = new SelectList(db.testconfig, "testconf_id", "testconf_name");
            ViewBag.user_id = new SelectList(db.user, "user_id", "user_name");
            return View();
        }

        //
        // POST: /Session/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(session session)
        {
            if (ModelState.IsValid)
            {
                db.session.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.testconfig_id = new SelectList(db.testconfig, "testconf_id", "testconf_name", session.testconfig_id);
            ViewBag.user_id = new SelectList(db.user, "user_id", "user_name", session.user_id);
            return View(session);
        }

        //
        // GET: /Session/Edit/5

        public ActionResult Edit(int id = 0)
        {
            session session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.testconfig_id = new SelectList(db.testconfig, "testconf_id", "testconf_name", session.testconfig_id);
            ViewBag.user_id = new SelectList(db.user, "user_id", "user_name", session.user_id);
            return View(session);
        }

        //
        // POST: /Session/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.testconfig_id = new SelectList(db.testconfig, "testconf_id", "testconf_name", session.testconfig_id);
            ViewBag.user_id = new SelectList(db.user, "user_id", "user_name", session.user_id);
            return View(session);
        }

        //
        // GET: /Session/Delete/5

        public ActionResult Delete(int id = 0)
        {
            session session = db.session.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // POST: /Session/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            session session = db.session.Find(id);
            db.session.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}