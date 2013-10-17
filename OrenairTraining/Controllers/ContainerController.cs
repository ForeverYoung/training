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
    public class ContainerController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /Container/

        public ActionResult Index()
        {
            return View(db.container.ToList());
        }

        //
        // GET: /Container/Details/5

        public ActionResult Details(int id = 0)
        {
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        //
        // GET: /Container/Create

        public ActionResult Create(int? id)
        {
            if (id != 0)
                ViewBag.Ancestors = new SelectList(db.container.Where(c => c.deleted == false), "container_id", "container_name", id);
            else
                ViewBag.Ancestors = new SelectList(db.container.Where(c => c.container_id == 0), "container_id", "container_name");
            ViewBag.ContainerTypes = new SelectList(db.containertype, "containertype_id", "containertype_name");
            ViewBag.Departments = new SelectList(db.department, "department_id", "department_name");
            return View();
        }

        //
        // POST: /Container/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(container container)
        {
            container.deleted = false;
            if (ModelState.IsValid)
            {
                db.container.Add(container);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(container);
        }

        //
        // GET: /Container/Edit/5

        public ActionResult Edit(int id = 0)
        {
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        //
        // POST: /Container/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(container container)
        {
            if (ModelState.IsValid)
            {
                db.Entry(container).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(container);
        }

        //
        // GET: /Container/Delete/5

        public ActionResult Delete(int id = 0)
        {
            container container = db.container.Find(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        //
        // POST: /Container/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.container.Find(id).deleted = true;
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