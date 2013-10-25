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
    public class UserController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /User/

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            ViewBag.Roles = db.role.ToList();
            return View(db.user.ToList());
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            user user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /User/Create

        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SelectList roles = new SelectList(db.role, "role_id", "role_name");
            ViewBag.Roles = roles;
            SelectList departments = new SelectList(db.department, "department_id", "department_name");
            ViewBag.Departments = departments;
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(user user)
        {
            if (ModelState.IsValid)
            {
                if (db.user.First(u => u.user_name == user.user_name) == null)
                {
                    user.regdate = DateTime.Now;
                    db.user.Add(user);
                    db.SaveChanges();
                    return View(user);
                }                
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            user user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            user user = db.user.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.user.Find(id);
            db.user.Remove(user);
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