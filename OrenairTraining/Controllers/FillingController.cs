using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class FillingController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();
        //
        // GET: /Filling/

        [Authorize(Roles="admin, moderator")]
        public ActionResult Index(int? id)
        {
            ViewBag.TreeList = My_Classes.TreeStructure.GenerateTreeToList();
            ViewBag.ContainerId = id;
            return View();
        }

        //
        // GET: /Filling/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Filling/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Filling/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Filling/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Filling/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Filling/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Filling/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RenderQuestionsInRightSide(int id)
        {
            return PartialView("ContentQuestions", db.question.ToList().Where(q => q.container_id == id));
        }

        public ActionResult RenderMaterialsInRightSide(int id)
        {
            return PartialView("ContentMaterials", db.material.ToList().Where(m => m.container_id == id));
        }
    }
}
