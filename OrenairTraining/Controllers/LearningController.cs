using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class LearningController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();
        //
        // GET: /Learning/

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            ViewBag.TreeList = My_Classes.TreeStructure.GenerateTreeToList();            
            return View();
        }

        public ActionResult Partial()
        {
            ViewBag.Message = "This is a partial view";
            return PartialView();
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
