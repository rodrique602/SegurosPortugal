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
    public class PreciosController : Controller
    {
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();
        // GET: /Precios/
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
        public ActionResult Details(String vinNumberTB = "", decimal vehicleValueTB = 0, DateTime? startingDateTB = null, DateTime? endingDateTB = null, int? diasDeCobertura = null, int caryears = 0, String carmakes = "", String carmodels = "", String coverageType = "", decimal policyTot = 0)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            if (vehicleValueTB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (startingDateTB == null)
            {
                startingDateTB = DateTime.Now;
            }

            //Solo es una variable de prueba
            //PolicyTotal = policyTotalTB;

            //ViewBag.PolicyTotalVB = PolicyFinalTotalVar;

            //ViewBag.PolicyTotalVB = policyTotalTB;


            ViewBag.VehicleCost = vehicleValueTB;
            ViewBag.VinNumber = vinNumberTB;
            ViewBag.CarYear = caryears;
            ViewBag.CarMake = carmakes;
            ViewBag.CarModel = carmodels;

            ViewBag.StartingDate = startingDateTB;

            ViewBag.TipoDeCobertura = coverageType;

            //Aqui obtenemos los dias de las fechas y calculamos la diferiencia entre los dos
            string numdias = Request["Coverage_Days"];
            int numDiasCobertura = int.Parse(numdias);
            DateTime date1 = System.Convert.ToDateTime(startingDateTB);
            DateTime date2 = date1.AddDays(numDiasCobertura);
            ViewBag.EndingDate = date2;

            var days = date2.Subtract(date1).TotalDays;
            var days2 = date1.Subtract(date1).TotalDays;

            ViewBag.days = days;
            int day = Convert.ToInt32(days);

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


            //Busqueda de precios 1. FULL COVERAGE y Liability Only Por DIA y por filtro VehicleValueTB
            Precios preciosFCPorDia;
            Precios preciosliabilityOnlyPorDia;

            if (numDiasCobertura > 0)
            {
                preciosFCPorDia = db.Precios.FirstOrDefault(pre => pre.valorMinimo <= vehicleValueTB && pre.valorMaximo >= vehicleValueTB && pre.dias == numDiasCobertura && pre.coverageType == "Full Coverage Per Day");

                ViewBag.PreciosFCPorDia = preciosFCPorDia.total;


                preciosliabilityOnlyPorDia = db.Precios.FirstOrDefault(pre => pre.dias == numDiasCobertura && pre.coverageType == "Liability Only per day");

                ViewBag.PreciosliabilityOnlyPorDia = preciosliabilityOnlyPorDia.total;

                //int diasCobertura = date1 + numdias;
                //Manda el resultado de la busqueda para ser mostrado en la vista
                return View(preciosFCPorDia);

            }


            //Busqueda de precios 3. FULL COVERAGE Por Año y por filtro VehicleValueTB
            Precios preciosFCPorAnio;
            Precios preciosFCPorAnioWTow;
            Precios preciosLiabilityVehiclePorAnio;
            Precios preciosLiabilityVehiclePorAnioWTow;
            Precios preciosLiability1LicensePorAnio;
            Precios preciosLiability2LicensePorAnio;
            
            if (days <= 0)
            {
                preciosFCPorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= vehicleValueTB && precio.valorMaximo >= vehicleValueTB && precio.coverageType == "Full Coverage Annual");

                ViewBag.PreciosFCPorAnio = preciosFCPorAnio.total;


                preciosFCPorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= vehicleValueTB && precio.valorMaximo >= vehicleValueTB && precio.coverageType == "Full Coverage Annual W Trailer or Tow");

                ViewBag.PreciosFCPorAnioWTow = preciosFCPorAnioWTow.total;



                preciosLiability1LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 1 License");

                ViewBag.PreciosLiability1LicensePorAnio = preciosLiability1LicensePorAnio.total;


                preciosLiability2LicensePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability 2 Licence");

                ViewBag.PreciosLiability2LicensePorAnio = preciosLiability2LicensePorAnio.total;



                preciosLiabilityVehiclePorAnio = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle per Year");

                ViewBag.PreciosLiabilityVehiclePorAnio = preciosLiabilityVehiclePorAnio.total;


                preciosLiabilityVehiclePorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo == 0 && precio.valorMaximo == 0 && precio.dias == 0 && precio.coverageType == "Liability Vehicle W Trailer or Tow");

                ViewBag.PreciosLiabilityVehiclePorAnioWTow = preciosLiabilityVehiclePorAnioWTow.total;



                return View(preciosFCPorAnio);            
            //}

            //Busqueda de precios 4. FULL COVERAGE Por Año With TRAILER or TOW
            //Precios preciosFCPorAnioWTow;
            //if (days <= 0)
            //{
                //preciosFCPorAnioWTow = db.Precios.FirstOrDefault(precio => precio.valorMinimo <= vehicleValueTB && precio.valorMaximo >= vehicleValueTB && precio.coverageType == "Full Coverage Annual W Trailer or tow");

                //ViewBag.PreciosFCPorAnioWTow = preciosFCPorAnioWTow.total;

                //return View(preciosFCPorAnioWTow);
            //}


            //Busqueda de precios 5. Liability Vehicle Por Año
            //Precios preciosLiabilityVehiclePorAnio;
            //if (days <= 0)
            //{
                //preciosLiabilityVehiclePorAnio = db.Precios.FirstOrDefault(pre => pre.dias == days && pre.coverageType == "Liability Vehicle per Year");

                //ViewBag.PreciosLiabilityVehiclePorAnio = preciosLiabilityVehiclePorAnio.total;

                //return View(preciosLiabilityVehiclePorAnio);
            //}

            //Busqueda de precios 6. Liability Vehicle Por Año With Trailer or Tow
            //Precios preciosLiabilityVehiclePorAnioWTow;
            //if (days <= 0)
            //{
                //preciosLiabilityVehiclePorAnioWTow = db.Precios.FirstOrDefault(pre => pre.dias == days && pre.coverageType == "Liability Vehicle W Trailer or tow");

                //ViewBag.PreciosLiabilityVehiclePorAnioWTow = preciosLiabilityVehiclePorAnioWTow.total;

                //return View(preciosLiabilityVehiclePorAnioWTow);
            //}



            //Precios precios = db.Precios.Find(id);
            //if (preciosFCPorAnio == null)
            //{
                //return HttpNotFound();
            //}


            //Busqueda de precios 7. Liability 1 License Por Año
            //Precios preciosLiability1LicensePorAnio;
            //if (days <= 0)
            //{
                //preciosLiability1LicensePorAnio = db.Precios.FirstOrDefault(pre => pre.dias == days && pre.coverageType == "Liability 1 License");

                //ViewBag.PreciosLiability1LicensePorAnio = preciosLiability1LicensePorAnio.total;

                //return View(preciosLiability1LicensePorAnio);
            //}

            //Busqueda de precios 8. Liability 2 License Por Año
            //Precios preciosLiability2LicensePorAnio;
            //if (days <= 0)
            //{
                //preciosLiability2LicensePorAnio = db.Precios.FirstOrDefault(pre => pre.dias == days && pre.coverageType == "Liability 2 Licence");

                //ViewBag.PreciosLiability2LicensePorAnio = preciosLiability2LicensePorAnio.total;

                //return View(preciosLiability2LicensePorAnio);
            }


            //ViewBag.EndingDate = endingDateTB.Value.ToShortDateString();

            //Aqui obtenemos los dias de las fechas y calculamos la diferiencia entre los dos
            //DateTime date1 = System.Convert.ToDateTime(startingDateTB);
            //DateTime date2 = System.Convert.ToDateTime(endingDateTB);

           //var days = date2.Subtract(date1).TotalDays;
            //var days2 = date1.Subtract(date1).TotalDays;

           // ViewBag.days = days;
            //int day = Convert.ToInt32(days);

            //QUERY DE COTIZACION POR NUMERO DE DIAS algo me falta
            //Precios preciosCotizacionPorDia = db.Precios.FirstOrDefault(pre => pre.valorMinimo <= vehicleValueTB && pre.valorMaximo >= vehicleValueTB && pre.dias == days);

            //ViewBag.Days = precios.days;

            //int day = Convert.ToInt32(days);

            return View(ViewBag.PreciosFCPorDia, ViewBag.PreciosliabilityOnlyPorDia,ViewBag.PolicyTotalTB);
            

            }


        // GET: /Precios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Precios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
