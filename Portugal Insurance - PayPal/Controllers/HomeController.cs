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
    public class HomeController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();

        public ActionResult Index()
        {

            //we use this to connect to the database
            Portugal_Insurance___PayPalContextDB db1 = new Portugal_Insurance___PayPalContextDB();
            List<Precios> precioss = db.Precios.Where(precio => precio.dias <= 15).ToList();

            var coverageTypes = (from cov in db1.Precios select cov.coverageType).Distinct();

            ViewBag.CoverageTypes = new SelectList(coverageTypes, "coverageType");

            //ViewBag.CoverageTypes = new SelectList(db.Precios, "precioID", "coverageType");

            //TEST CODE FOR DROPDOWN LIST FOR DIAS
            


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your Services page.";

            return View();
        }
    }
}