using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Developee;

namespace Developee.Controllers
{
    public class ProjectsController : Controller
    {
        private Database1Entities9 db = new Database1Entities9();
        Developer dev = new Developer();
        // GET: Projects
        public ActionResult Index()
        {
            
            var projects = db.Projects.Include(p => p.Customer);
            //Session.Add("id",projects.);
            //Session.Add("uname", customer.Username);
            return View();
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name");
            int id = (int)Session["id"];
            string uname=Session["uname"].ToString();
            ViewBag.Message = uname;
            ViewBag.id = id;
            //Console.WriteLine(id);
            //Console.ReadLine();
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Duration,Budget,Cust_Id,Start_Date,End_Date")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Cust_Id = (int)Session["id"];
               
                db.Projects.Add(project);
               
                db.SaveChanges();
                Session.Add("proj_id", project.Id);
                return RedirectToAction("option","Customers");
            }
            ViewBag.Cust_Id = Session["username"];
          //  ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", project.Cust_Id);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", project.Cust_Id);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Duration,Budget,Cust_Id,Start_Date,End_Date")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", project.Cust_Id);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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

        public ActionResult Success(int ? id)
        {
            int pid= (Int32)Session["proj_id"];
            Project proj= proj = db.Projects.Find(pid);
            proj.Dev_Id = id;
                
            

            db.Entry(proj).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }

        public ActionResult SuccessBA(int? id)
        {
            int pid = (Int32)Session["proj_id"];
            Project proj = proj = db.Projects.Find(pid);
            proj.BA_Id = id;



            db.Entry(proj).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }
    }
}
