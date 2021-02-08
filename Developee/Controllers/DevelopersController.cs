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
    public class DevelopersController : Controller
    {
        private Database1Entities9 db = new Database1Entities9();

        // GET: Developers

        // GET: Developers

        // GET: Developers
        public ActionResult Index()
        {
            return View(db.Developers.ToList());
        }



        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string user, string pass)
        {
            int i = db.Developers.Where(x => x.Username == user && x.Password == pass).Count();
            if (i > 0)
            {
                this.Session.Add("username", user);
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("SignIn");
        }

        public ActionResult Dashboard()
        {

            return View();
        }


        public ActionResult DevProfile()
        {

            string username = this.Session["username"].ToString();
            var result = from d in db.Developers
                         select d;

            var developer = result.FirstOrDefault(x => x.Username == username);
            if (developer != null)
            {
                int id = developer.Dev_Id;
                Developer dev = db.Developers.Find(id);
                if (dev == null)
                {
                    return HttpNotFound();
                }
                return View(dev);
            }
            return View();
        }


        //my projects
        public ActionResult MyProjects()
        {
            Project project;
            string username = this.Session["username"].ToString();
            ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Dev_Id");
            var result = from d in db.Developers
                         select d;
            var result2 = from p in db.Projects
                          select p;


            var developer = result.FirstOrDefault(x => x.Username == username);


            if (developer != null)
            {
                int id = developer.Dev_Id;
                var proj = result2.FirstOrDefault(x => x.Dev_Id == id);
                int proj_id = proj.Id;
                project = db.Projects.Find(proj_id);
                if (project == null)
                {
                    return HttpNotFound();
                }

                /* var projects = db.Projects.Include(p => p.BA).Include(p => p.Customer).Include(p => p.Developer);
                 return View(projects.ToList());*/

                return View(project);

            }
            // new SelectList(db.Developers, "Dev_Id", "Username", project.Dev_Id);
            ViewBag["msg"] = "no proj assigned";
            return View();
        }


        public ActionResult developerList()
        {

            

            return View(db.Developers.ToList());
        }

      /*  [HttpGet]
        public ActionResult developerList([Bind(Include = "Dev_Id,Username,Password,Name,Address,Email,Mobile_no,No_Of_Exp,Company_Name,PanCard_no,Skills")] Developer developer)
        {
            return View();
        }
      */

        public ActionResult SelectedDeveloper(int? id)
        {

            return View();
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dev_Id,Username,Password,Name,Address,Email,Mobile_no,No_Of_Exp,Company_Name,PanCard_no,Skills")] Developer developer)
        {

            if (ModelState.IsValid)
            {
                this.Session.Add("username", developer.Username);
                db.Developers.Add(developer);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }

            return View(developer);
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dev_Id,Username,Password,Name,Address,Email,Mobile_no,No_Of_Exp,Company_Name,PanCard_no,Skills")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(developer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developer);
        }


        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }


        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developer developer = db.Developers.Find(id);
            db.Developers.Remove(developer);
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


        /*
        public ActionResult Index()
        {
            var developers = db.Developers.Include(d => d.Projects);
            return View(developers.ToList());
        }

        //SignIn Page
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string user, string pass)
        {
            int i = db.Developers.Where(x => x.Username == user && x.Password == pass).Count();
            if (i > 0)
            {
                this.Session.Add("username", user);
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("SignIn");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        //my projects

        public ActionResult developerList()
        {
           
            List<Developer> dlist = db.Developers.ToList();
            
            return View(dlist);
        }
        
        [HttpPost]
        public ActionResult developerList(int? id)
        {
            return View();
        }

        public ActionResult MyProjects()
        {
            Project project;
            string username = this.Session["username"].ToString();
            //ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Dev_Id");
            var result = from d in db.Developers
                         select d;
            var result2 = from p in db.Projects
                          select p;


            var developer = result.FirstOrDefault(x => x.Username == username);


            if (developer != null)
            {
                int id = developer.Dev_Id;
                var proj = result2.FirstOrDefault(x => x.Dev_Id == id);
                int proj_id = proj.Id;
                project = db.Projects.Find(proj_id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
            // new SelectList(db.Developers, "Dev_Id", "Username", project.Dev_Id);
            ViewBag["msg"] = "no proj assigned";
            return View();
        }

        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            List<Developer> dlist = db.Developers.ToList();

            return View(dlist);
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            //ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title");
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dev_Id,Username,Password,Name,Address,Email,Mobile_no,No_of_exp,Company_name,Pancard_no")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                this.Session.Add("username", developer.Username);
                db.Developers.Add(developer);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }

           // ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", developer.Proj_Id);
            return View(developer);
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", developer.Proj_Id);
            return View(developer);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dev_Id,Username,Password,Name,Address,Email,Mobile_no,No_of_exp,Company_name,Pancard_no,Proj_Id")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(developer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", developer.Proj_Id);
            return View(developer);
        }

        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developer developer = db.Developers.Find(id);
            db.Developers.Remove(developer);
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
    */
    }
}
