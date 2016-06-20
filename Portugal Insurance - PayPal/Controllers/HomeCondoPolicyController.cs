using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portugal_Insurance___PayPal.Models;
using Portugal_Insurance___PayPal.DAL;

namespace Portugal_Insurance___PayPal.Controllers
{
    public class HomeCondoPolicyController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();

        // GET: /HomeCondoPolicy/
        public ActionResult Index()
        {
            return View(db.HomeCondoPolicies.ToList());
        }

        // GET: /HomeCondoPolicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeCondoPolicy homecondopolicy = db.HomeCondoPolicies.Find(id);
            if (homecondopolicy == null)
            {
                return HttpNotFound();
            }
            return View(homecondopolicy);
        }

        // GET: /HomeCondoPolicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /HomeCondoPolicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="policyQuoteID,homeOwnerInsurance,condoInsurance,customerName,email,customerUsaAddress,zipCode,city,phoneUsa,cellPhone,insuredBuildingLocation,floors,typeOfConstruction,wall,roof,buildingsValue,contentsValue,burlglaryTheft,brokenGlass,familyCivilLiability,others,annualPremium,earthquake")] HomeCondoPolicy homecondopolicy)
        {
            if (ModelState.IsValid)
            {
                db.HomeCondoPolicies.Add(homecondopolicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homecondopolicy);
        }

        // GET: /HomeCondoPolicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeCondoPolicy homecondopolicy = db.HomeCondoPolicies.Find(id);
            if (homecondopolicy == null)
            {
                return HttpNotFound();
            }
            return View(homecondopolicy);
        }

        // POST: /HomeCondoPolicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="policyQuoteID,homeOwnerInsurance,condoInsurance,customerName,email,customerUsaAddress,zipCode,city,phoneUsa,cellPhone,insuredBuildingLocation,floors,typeOfConstruction,wall,roof,buildingsValue,contentsValue,burlglaryTheft,brokenGlass,familyCivilLiability,others,annualPremium,earthquake")] HomeCondoPolicy homecondopolicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homecondopolicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homecondopolicy);
        }

        // GET: /HomeCondoPolicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeCondoPolicy homecondopolicy = db.HomeCondoPolicies.Find(id);
            if (homecondopolicy == null)
            {
                return HttpNotFound();
            }
            return View(homecondopolicy);
        }

        // POST: /HomeCondoPolicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeCondoPolicy homecondopolicy = db.HomeCondoPolicies.Find(id);
            db.HomeCondoPolicies.Remove(homecondopolicy);
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
