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
    public class QuestionController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /Question/

        public ActionResult Index()
        {
            return View(db.question.Where(q => q.deleted == false).ToList());
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id = 0)
        {
            question question = db.question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create(int? id)
        {
            SelectList types = new SelectList(db.questiontype, "questiontype_id", "questiontype_name");
            ViewBag.Types = types;
            SelectList themes = new SelectList(db.container.Where(c => c.type_id == 2), "container_id", "container_name", id);
            ViewBag.Themes = themes;
            SelectList materials = new SelectList(db.material, "material_id", "material_name");
            ViewBag.Materials = materials;
            ViewBag.Count = 3;
            return View();
        }

        //
        // POST: /Question/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(question question, List<string> answers, List<object> ichecked)
        {
            question.answer = "";
            int i = 0;
            if (answers.Count > 1)
                foreach (var answer in answers)
                {
                    question.answer += Convert.ToInt32(ichecked[0]) == i + 1 ? "@" : "";
                    question.answer += answer + "|";
                    i++;
                }
            question.answer.TrimEnd(new char[]{'|'});
            if (ModelState.IsValid)
            {
                question.deleted = false;
                db.question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        //
        // GET: /Question/Edit/5

        public ActionResult Edit(int id = 0)
        {
            question question = db.question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id = 0)
        {
            question question = db.question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            question question = db.question.Find(id);
            question.deleted = true;
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