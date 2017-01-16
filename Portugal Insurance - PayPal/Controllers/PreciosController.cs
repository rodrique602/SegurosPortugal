using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Portugal_Insurance___PayPal.Models;
using Portugal_Insurance___PayPal.Models.ViewModels;


namespace Portugal_Insurance___PayPal.Controllers
{
    public class PreciosController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();
        // GET: /Precios/
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Index()
        {
            //we use this to connect to the database
            Portugal_Insurance___PayPalContextDB preciosContext = new Portugal_Insurance___PayPalContextDB();
            List<Precios> precioss = preciosContext.Precios.Where(precio => precio.dias <= 15).ToList();


            var coverageTypes = (from cov in db.Precios select cov.coverageType).Distinct();

            ViewBag.CoverageTypes = new SelectList(coverageTypes, "coverageType");



            //ViewBag.CoverageTypes = new SelectList(preciosContext.Precios, "precioID", "coverageType");

            //This is the one were working on to try and display a dropdown list of the coverage types available
            //listItem.Add(new SelectListItem() p = preciosContext.Precios.Where(p => p.coverageType <= 3).ToList();

            //p.precioID = 2;
            //p.coverageType = "Second Value";

            //listItem.Add(new SelectListItem() { Value = p.coverageType, Text = p.precioID.ToString() });

            //p.precioID = 3;
            //p.coverageType = "3rd Value";

            //listItem.Add(new SelectListItem() { Value = p.coverageType, Text = p.precioID.ToString() });

            //ViewBag.DropDownValues = new SelectList(listItem, "Text", "Value");

            //Ejemplo de una forma de generar DropDownList
            //ViewBag.DropDownValues = new SelectList(new[] {"First Value", "Second Value", "Third Value"});

            return View(precioss);
        }

        // GET: /Precios/Details/5
        public static decimal PolicyFinalTotalVar;
        //public ActionResult Details(String vinNumberTB = "", decimal info.vehicleValue = 0, DateTime? startingDateTB = null, DateTime? endingDateTB = null, int? diasDeCobertura = null, int caryears = 0, String carmakes = "", String carmodels = "", String coverageType = "", decimal policyTot = 0)
            public ActionResult Details(VMAutoPolicyInfo info, String numDiasDeCobertura/*, String vehicleType*/)
        {
            if (info.vehicleValue == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //By default the starting date is today
            if (info.startingDate == null)
            {
                info.startingDate = DateTime.Now;
            }
            
            ViewBag.info = info;
            int numDias = int.Parse(numDiasDeCobertura);
            //string vehicleKind = vehicleType;
            //Aqui obtenemos los dias de las fechas y calculamos la diferiencia entre los dos
            DateTime date1 = info.startingDate;
            DateTime date2 = date1.AddDays(numDias);

            info.endingDate = date2;

            ViewBag.days = numDias;

            if (date1 < DateTime.Now)
            {
                ViewBag.dateerror1 = "Error, Fecha Menor al dia de hoy";
            }
            if (date2 < date1)
            {
                ViewBag.dateerror2 = "Error, Fecha Menor al dia de Inicio";
            }

            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;

            //Busqueda de precios 1. FULL COVERAGE y Liability Only Por DIA y por filtro info.vehicleValue
            Precios preciosFCPorDia;
            Precios preciosliabilityOnlyPorDia;

            //Busqueda de precios 3. FULL COVERAGE Por Año y por filtro info.vehicleValue
            Precios preciosFCPorAnio;
            Precios preciosFCPorAnioWTow;
            Precios preciosLiabilityVehiclePorAnio;
            Precios preciosLiabilityVehiclePorAnioWTow;
            Precios preciosLiability1LicensePorAnio;
            Precios preciosLiability2LicensePorAnio;
            

            /*Annual Policies*/
            preciosFCPorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= info.vehicleValue && precio.valorMaximo >= info.vehicleValue && precio.coverageType == "Full Coverage Annual");

            ViewBag.PreciosFCPorAnio = preciosFCPorAnio.total;


            preciosFCPorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= info.vehicleValue && precio.valorMaximo >= info.vehicleValue && precio.coverageType == "Full Coverage Annual W Trailer or Tow");

            ViewBag.PreciosFCPorAnioWTow = preciosFCPorAnioWTow.total;


            preciosLiability1LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 1 License");

            ViewBag.PreciosLiability1LicensePorAnio = preciosLiability1LicensePorAnio.total;


            preciosLiability2LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 2 Licence");

            ViewBag.PreciosLiability2LicensePorAnio = preciosLiability2LicensePorAnio.total;


            preciosLiabilityVehiclePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle per Year");

            ViewBag.PreciosLiabilityVehiclePorAnio = preciosLiabilityVehiclePorAnio.total;


            preciosLiabilityVehiclePorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle W Trailer or Tow");

            ViewBag.PreciosLiabilityVehiclePorAnioWTow = preciosLiabilityVehiclePorAnioWTow.total;
            


            //For diarios policies
            if (numDias > 0)
            {
                //For Daily Policies
                preciosFCPorDia = db.Precios.FirstOrDefault(pre => pre.valorMinimo <= info.vehicleValue && pre.valorMaximo >= info.vehicleValue && pre.dias == numDias && pre.coverageType == "Full Coverage Per Day");

                ViewBag.PreciosFCPorDia = preciosFCPorDia.total;


                preciosliabilityOnlyPorDia = db.Precios.FirstOrDefault(pre => pre.dias == numDias && pre.coverageType == "Liability Only per day");

                ViewBag.PreciosliabilityOnlyPorDia = preciosliabilityOnlyPorDia.total;


                //preciosFCPorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= info.vehicleValue && precio.valorMaximo >= info.vehicleValue && precio.coverageType == "Full Coverage Annual");

                //ViewBag.PreciosFCPorAnio = preciosFCPorAnio.total;


                //preciosFCPorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= info.vehicleValue && precio.valorMaximo >= info.vehicleValue && precio.coverageType == "Full Coverage Annual W Trailer or Tow");

                //ViewBag.PreciosFCPorAnioWTow = preciosFCPorAnioWTow.total;


                //preciosLiability1LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 1 License");

                //ViewBag.PreciosLiability1LicensePorAnio = preciosLiability1LicensePorAnio.total;


                //preciosLiability2LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 2 Licence");

                //ViewBag.PreciosLiability2LicensePorAnio = preciosLiability2LicensePorAnio.total;


                //preciosLiabilityVehiclePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle per Year");

                //ViewBag.PreciosLiabilityVehiclePorAnio = preciosLiabilityVehiclePorAnio.total;


                //preciosLiabilityVehiclePorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle W Trailer or Tow");

                //ViewBag.PreciosLiabilityVehiclePorAnioWTow = preciosLiabilityVehiclePorAnioWTow.total;

                //int diasCobertura = date1 + numdias;
                //Manda el resultado de la busqueda para ser mostrado en la vista

            }

            //ViewBag.policyTotalTB = algo; /**PENDIENTE**/
            return View();
        }


        // GET: /Precios/Create
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Precios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Create([Bind(Include="precioID,type,coverageType,dias,valorMinimo,valorMaximo,total")] Precios precios)
        {
            if (ModelState.IsValid)
            {
                db.Precios.Add(precios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(precios);
        }

        // GET: /Precios/Edit/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit(int id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precios precios = db.Precios.Find(id);
            if (precios == null)
            {
                return HttpNotFound();
            }
            return View(precios);
        }

        // POST: /Precios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR + "," + AccountRolesNames.SALESMANAGER)]
        public ActionResult Edit([Bind(Include = "precioID,type,coverageType,dias,valorMinimo,valorMaximo,total")] Precios precios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(precios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(precios);
        }

        // GET: /Precios/Delete/5
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult Delete(int id = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precios precios = db.Precios.Find(id);
            if (precios == null)
            {
                return HttpNotFound();
            }
            return View(precios);
        }

        // POST: /Precios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccountRolesNames.ADMINISTRATOR)]
        public ActionResult DeleteConfirmed(int id)
        {
            Precios precios = db.Precios.Find(id);
            db.Precios.Remove(precios);
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
