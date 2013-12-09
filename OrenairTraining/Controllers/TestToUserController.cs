using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class TestToUserController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /TestToUser/

        public ActionResult Index()
        {
            ViewBag.Configs = db.testconfig.ToList();
            ViewBag.Users = db.user.ToList();
            return View(db.testtouser.Where(ttu => ttu.is_completed!=true && ttu.deleted!=true).ToList());
        }

        //
        // GET: /TestToUser/Details/5

        public ActionResult Details(int id = 0)
        {
            testtouser testtouser = db.testtouser.Find(id);
            if (testtouser == null)
            {
                return HttpNotFound();
            }
            return View(testtouser);
        }

        //
        // GET: /TestToUser/Create
        [Authorize(Roles="moderator,admin")]
        public ActionResult Create()
        {
            ViewBag.Configs = new SelectList(db.testconfig.Where(c=>c.deleted!=true).ToList(), "testconf_id", "testconf_name");
            ViewBag.Departments = new SelectList(db.department.Where(d=>d.deleted!=true).ToList(), "department_id", "department_name");
            ViewBag.Users = new SelectList(db.user.Where(u => u.deleted != true).ToList(), "user_id", "fio");
            return View();
        }

        //
        // POST: /TestToUser/Create

        [HttpPost]
        public ActionResult Create(TestToUserModel testtouser)
        {
            if (ModelState.IsValid)
            {
                foreach (var id in testtouser.user_ids)
                {
                    db.testtouser.Add(new testtouser { user_id = id,
                                    testconf_id=testtouser.testconf_id,
                                    is_completed=false,
                                    deleted = false,
                                    date_of_assignment=DateTime.Now
                    });
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testtouser);
        }

        //
        // GET: /TestToUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            testtouser testtouser = db.testtouser.Find(id);
            if (testtouser == null)
            {
                return HttpNotFound();
            }
            return View(testtouser);
        }

        //
        // POST: /TestToUser/Edit/5

        [HttpPost]
        public ActionResult Edit(testtouser testtouser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testtouser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testtouser);
        }

        //
        // GET: /TestToUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            testtouser testtouser = db.testtouser.Find(id);
            if (testtouser == null)
            {
                return HttpNotFound();
            }
            return View(testtouser);
        }

        //
        // POST: /TestToUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            testtouser testtouser = db.testtouser.Find(id);
            db.testtouser.Remove(testtouser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUsersForDepartment(string id)
        {
            // get the products from the repository 

            SelectList users;

            if (id == null)
            {
                users = new SelectList(db.user.Where(u => u.deleted != true).ToList(), "user_id", "fio");
            }
            else
            {
                var dep_id = Convert.ToInt32(id);
                users = new SelectList(db.user.Where(u => u.department_id == dep_id).ToList(), "user_id", "fio");
            }

            return new JavaScriptSerializer().Serialize(users);
        }
    }
}