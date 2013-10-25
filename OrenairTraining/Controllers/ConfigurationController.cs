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
    public class ConfigurationController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();
                
        //
        // GET: /Configuration/

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            var tests = db.testconfig.Where(c => c.deleted == false).ToList();
            return View(tests);
        }

        //
        // GET: /Configuration/

        
        //
        // GET: /Configuration/Details/5

        public ActionResult Details(int id)
        {
            testconfig testconfig = db.testconfig.Find(id);

            var list = My_Classes.Parser.ParseTestConfig(testconfig.themes, testconfig.questions);
            var themesList = new List<string>();
            var questionsList = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                themesList.Add(db.container.Find(Convert.ToInt32(list[i])).container_name);
                i++;
                questionsList.Add(list[i]);
            }

            ViewBag.Themescount = themesList.Count;
            ViewBag.Themes = themesList;
            ViewBag.Questions = questionsList;

            if (testconfig == null)
            {
                return HttpNotFound();
            }
            return View(testconfig);
        }

        //
        // GET: /Configuration/Create

        public ActionResult Create(int themescount)
        {
            SelectList themes = new SelectList(db.container.Where(c => c.type_id == 2).ToList(), "container_id", "container_name");
            ViewBag.Themes = themes;
            ViewBag.Themescount = themescount;
            return View();
        }

        //
        // POST: /Configuration/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(testconfig testconfig, List<string> themes, List<string> questions)
        {
            int themescount = themes.Count;
            testconfig.themes = testconfig.questions = "";
            testconfig.deleted = false;
            for (int i = 0; i < themescount; i++)
            {
                if (questions[i] != "")
                {
                    testconfig.themes += themes[i] + "|";
                    testconfig.questions += questions[i] + "|";
                }
            }
            if (ModelState.IsValid)
            {
                db.testconfig.Add(testconfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testconfig);
        }

        //
        // GET: /Configuration/Edit/5

        public ActionResult Edit(int id)
        {
            testconfig testconfig = db.testconfig.Find(id);

            var list = My_Classes.Parser.ParseTestConfig(testconfig.themes, testconfig.questions);
            var themesList = new List<string>();
            var questionsList = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                themesList.Add(db.container.Find(Convert.ToInt32(list[i])).container_name);
                i++;
                questionsList.Add(list[i]);
            }

            ViewBag.Themescount = themesList.Count;
            ViewBag.Themes = themesList;
            ViewBag.Questions = questionsList;
            ViewBag.SThemes = new SelectList(db.container.Where(t => t.type_id == 2).ToList(), "container_id", "container_name");

            if (testconfig == null)
            {
                return HttpNotFound();
            }
            return View(testconfig);
        }

        //
        // POST: /Configuration/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(testconfig testconfig, List<string> themes, List<string> questions)
        {
            testconfig.themes = testconfig.questions = "";
            for (int i = 0; i < themes.Count; i++)
            {
                if (questions[i] != "")
                {
                    testconfig.themes += themes[i] + "|";
                    testconfig.questions += questions[i] + "|";
                }
            }
            testconfig.deleted = true;

            if (ModelState.IsValid)
            {                
                try
                {
                    db.Entry(testconfig).State = EntityState.Modified;
                    testconfig conf = new testconfig() { testconf_name = testconfig.testconf_name, themes = testconfig.themes, questions = testconfig.questions, deleted = false, time = testconfig.time };
                    db.testconfig.Add(conf);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }                
                return RedirectToAction("Index");
            }
            return View(testconfig);
        }

        //
        // GET: /Configuration/Delete/5

        public ActionResult Delete(int id)
        {
            testconfig testconfig = db.testconfig.Find(id);
            if (testconfig == null)
            {
                return HttpNotFound();
            }
            return View(testconfig);
        }

        //
        // POST: /Configuration/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            testconfig testconfig = db.testconfig.Find(id);
            testconfig.deleted = true;
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