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
    public class BAsController : Controller
    {
        private Database1Entities9 db = new Database1Entities9();

        // GET: BAs
        public ActionResult Index()
        {
            var bAs = db.BAs.Include(b => b.Customer).Include(b => b.Developer).Include(b => b.Project);
            return View(bAs.ToList());
        }

        // GET: BAs/Details/5

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult BAProfile()
        {

            string username = this.Session["username"].ToString();
            var result = from d in db.BAs
                         select d;

            var BA = result.FirstOrDefault(x => x.Username == username);
            if (BA != null)
            {
                int id = BA.BA_Id;
                BA ba = db.BAs.Find(id);
                if (ba == null)
                {
                    return HttpNotFound();
                }
                return View(ba);
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BA bA = db.BAs.Find(id);
            if (bA == null)
            {
                return HttpNotFound();
            }
            return View(bA);
        }

        // GET: BAs/Create
        public ActionResult Create()
        {
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name");
            ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Username");
            ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title");
            return View();
        }

        // POST: BAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BA_Id,Name,Username,Password,Cust_Id,Dev_Id,Proj_Id")] BA bA)
        {
            if (ModelState.IsValid)
            {
                db.BAs.Add(bA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", bA.Cust_Id);
            ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Username", bA.Dev_Id);
            ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", bA.Proj_Id);
            return View(bA);
        }

        // GET: BAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BA bA = db.BAs.Find(id);
            if (bA == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", bA.Cust_Id);
            ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Username", bA.Dev_Id);
            ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", bA.Proj_Id);
            return View(bA);
        }

        // POST: BAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BA_Id,Name,Username,Password,Cust_Id,Dev_Id,Proj_Id")] BA bA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cust_Id = new SelectList(db.Customers, "Id", "Name", bA.Cust_Id);
            ViewBag.Dev_Id = new SelectList(db.Developers, "Dev_Id", "Username", bA.Dev_Id);
            ViewBag.Proj_Id = new SelectList(db.Projects, "Id", "Title", bA.Proj_Id);
            return View(bA);
        }

        // GET: BAs/Delete/5

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            List<BA> blist = db.BAs.ToList();

            foreach (BA ba in blist)
            {
                if (ba.Username == userName && ba.Password == password)
                {
                    Session.Add("bid", ba.BA_Id);
                    Session.Add("buname", ba.Username);
                    //  return this.RedirectToAction("Index", "Projects");
                    return this.RedirectToAction("Dashboard");
                }
                
            }

            return this.RedirectToAction("Index", "Home");

        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BA bA = db.BAs.Find(id);
            if (bA == null)
            {
                return HttpNotFound();
            }
            return View(bA);
        }

        // POST: BAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BA bA = db.BAs.Find(id);
            db.BAs.Remove(bA);
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
    }
}
