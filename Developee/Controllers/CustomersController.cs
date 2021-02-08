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
    public class CustomersController : Controller
    {
        private Database1Entities9 db = new Database1Entities9();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            List<Customer> clist=db.Customers.ToList();

            foreach(Customer cust in clist)
            {
                if (cust.Username==userName && cust.Password==password)
                {
                    Session.Add("id", cust.Id);
                    Session.Add("uname", cust.Username);
                    //  return this.RedirectToAction("Index", "Projects");
                    return this.RedirectToAction("Dashboard");
                }
                /*else
                    return this.RedirectToAction("Index","Home");*/
            }

            return this.RedirectToAction("Index", "Home");

        }

        public ActionResult option()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult CustomerProfile()
        {

            string username = this.Session["uname"].ToString();
            var result = from d in db.Customers
                         select d;

            var cust = result.FirstOrDefault(x => x.Username == username);
            if (cust != null)
            {
                int id = cust.Id;
                Customer c = db.Customers.Find(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                return View(c);
            }
            return View();
        }


        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        public ActionResult CustSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustSignUp([Bind(Include = "Id,Name,Username,Password,Mobile_no,Email_Id,Id_Proof,Org_Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                Session.Add("id",customer.Id);
                Session.Add("uname", customer.Username);
                return RedirectToAction("Dashboard");
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Username,Password,Mobile_no,Email_Id,Id_Proof")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Username,Password,Mobile_no,Email_Id,Id_Proof")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
