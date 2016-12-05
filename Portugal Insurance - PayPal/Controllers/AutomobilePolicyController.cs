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
    public class AutomobilePolicyController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();

        // GET: /AutomobilePolicy/
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Index()
        {
            var automobilepolicies = db.AutomobilePolicies;
            return View(automobilepolicies.ToList());
        }

        // GET: /AutomobilePolicy/Details/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutomobilePolicy automobilepolicy = db.AutomobilePolicies.Find(id);
            if (automobilepolicy == null)
            {
                return HttpNotFound();
            }
            return View(automobilepolicy);
        }

        // GET: /AutomobilePolicy/Create
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.Users, "clientID", "fullName");
            return View();
        }

        // POST: /AutomobilePolicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Create([Bind(Include = "automobilePolicyID,vehicleValue,vehicleVin,carYear,carMake,carModel,policyFolio,policySold,policySoldDate,policyStartingDate,policyEndingDate,clientID")] AutomobilePolicy automobilepolicy)
        {
            if (ModelState.IsValid)
            {
                automobilepolicy.policyStartingDate = DateTime.Now;
                automobilepolicy.policyEndingDate = DateTime.Now; 
                db.AutomobilePolicies.Add(automobilepolicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(automobilepolicy);
        }

        // GET: /AutomobilePolicy/Edit/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutomobilePolicy automobilepolicy = db.AutomobilePolicies.Find(id);
            if (automobilepolicy == null)
            {
                return HttpNotFound();
            }
            return View(automobilepolicy);
        }

        // POST: /AutomobilePolicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit([Bind(Include="automobilePolicyID,vehicleValue,vehicleVin,carYear,carMake,carModel,policyFolio,policySold,policySoldDate,policyStartingDate,policyEndingDate,clientID")] AutomobilePolicy automobilepolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(automobilepolicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(automobilepolicy);
        }

        // GET: /AutomobilePolicy/Delete/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutomobilePolicy automobilepolicy = db.AutomobilePolicies.Find(id);
            if (automobilepolicy == null)
            {
                return HttpNotFound();
            }
            return View(automobilepolicy);
        }

        // POST: /AutomobilePolicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            AutomobilePolicy automobilepolicy = db.AutomobilePolicies.Find(id);
            db.AutomobilePolicies.Remove(automobilepolicy);
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
