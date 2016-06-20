using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portugal_Insurance___PayPal.PayPal;

namespace Portugal_Insurance___PayPal.Controllers
{
    public class PayPalController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            ViewBag.result = PDTHolder.Success(Request.QueryString.Get("tx"));
            return View("Success");
        }
	}
}