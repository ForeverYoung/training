using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.Win32;
using OrenairTraining.Models;

namespace OrenairTraining.Controllers
{
    public class MaterialController : Controller
    {
        private OrenairTrainingEntities db = new OrenairTrainingEntities();

        //
        // GET: /Material/

        public ActionResult Index()
        {
            return View(db.material.ToList());
        }

        //
        // GET: /Material/Details/5

        public ActionResult Details(int id = 0)
        {
            material material = db.material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        //
        // GET: /Material/Create

        public ActionResult Create(int? id)
        {
            ViewBag.Themes = new SelectList(db.container.Where(c => c.type_id == 2), "container_id", "container_name",  id);
            ViewBag.CurrentContainerId = id;
            return View();
        }

        //
        // POST: /Material/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(material material, HttpPostedFileBase fileUpload)
        {
            material.material_content=new byte[fileUpload.ContentLength];
            fileUpload.InputStream.Read(material.material_content, 0, fileUpload.ContentLength);
            material.file_name = fileUpload.FileName;
            material.file_size = fileUpload.ContentLength;
            material.deleted = false;
            if (ModelState.IsValid)
            {                
                db.material.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(material);
        }

        //
        // GET: /Material/Edit/5

        public ActionResult Edit(int id = 0)
        {
            material material = db.material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        //
        // POST: /Material/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }

        //
        // GET: /Material/Delete/5

        public ActionResult Delete(int id = 0)
        {
            material material = db.material.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        //
        // POST: /Material/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            material material = db.material.Find(id);
            db.material.Remove(material);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult OpenFile(int id)
        {
            byte[] theData = db.material.Find(id).material_content;
            string filename = db.material.Find(id).file_name;
            string contentType = "";
            try
             {
               // get the registry classes root
               RegistryKey classes = Registry.ClassesRoot;

               // find the sub key based on the file extension
               RegistryKey fileClass = classes.OpenSubKey(Path.GetExtension(filename));
               contentType = fileClass.GetValue("Content Type").ToString();
             }
             catch { }

            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AddHeader("content-length", theData.Length.ToString());
            Response.AddHeader("content-disposition", "inline; filename=" + filename + "");
            Response.BinaryWrite(theData);
            Response.Flush();
            Response.End();

            return File(theData, contentType, filename);
        }
    }
}