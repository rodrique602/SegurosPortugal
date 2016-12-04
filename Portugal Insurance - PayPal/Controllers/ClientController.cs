using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portugal_Insurance___PayPal.Models;

namespace Portugal_Insurance___PayPal.Controllers
{
    public class ClientController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();

        // GET: /Client/
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /Client/Details/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser client = db.Users.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ApplicationUser client)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(client);
                db.SaveChanges();
               // ViewBag.registered = true;
                return Redirect(Request.UrlReferrer.ToString());
            }
            //ViewBag.registered = false;

            return View(client);
        }

        // GET: /Client/Edit/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser client = db.Users.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit([Bind(Include="clientID,fullName,addressLine1,addressLine2,city,state,zipCode,country,phoneNumber,emailAddress,licenseNumber1,licenseNumber2")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: /Client/Delete/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser client = db.Users.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationUser client = db.Users.Find(id);
            db.Users.Remove(client);
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
