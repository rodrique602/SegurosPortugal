using Portugal_Insurance___PayPal.Models;
using Portugal_Insurance___PayPal.Models.ViewModels;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Rotativa;
using Newtonsoft.Json.Linq;
using Rotativa.Options;
using System.Net.Mime;

//using Portugal_Insurance___PayPal.PayPal;
//using PayPal.Api;

namespace Portugal_Insurance___PayPal.Controllers
{
    public class PayPalController : Controller
    {
        //GET: /Paypal/
        private Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithCreditCard()
        {
            //create an item for witch you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to insantiate the object
            Item item = new Item();
            item.name = "Demo Item";
            item.currency = "USD";
            item.price = "5";
            item.quantity = "1";
            item.sku = "sku";

            //Now make a list of Items and add the above item to it
            //you can create as many items as you want and add them to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address fo the payment
            Address billingAddress = new Address();
            billingAddress.city = "NewYork";
            billingAddress.country_code = "US";
            billingAddress.line1 = "23rd street kew gardens";
            billingAddress.postal_code = "43210";
            billingAddress.state = "NY";


            //Now Create a credit card object of type credit card and add above details to it
            //Please replace your credit card details over here which you got from PayPal
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = "874"; //card cvv2 number;
            crdtCard.expire_month = 1; //card expire date
            crdtCard.expire_year = 2020; //card expire year
            crdtCard.first_name = "Aman";
            crdtCard.last_name = "Thakur";
            crdtCard.number = "123456789123456"; //enter your credit card number here
            crdtCard.type = "visa"; //credit card type here paypal allows 4 types

            //Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = "5";
            details.tax = "1";

            //Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            //Total shipping tax + subtotal.
            amnt.total = "7";
            amnt.details = details;

            //Now make a transaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Description about the payment amount.";
            tran.item_list = itemList;
            tran.invoice_number = "your invoice number which you are generating";

            //Now we have to make a list of transaction and add the transactions object
            //to this list. You can create one or more objects as per your requirements.

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            //Now we need to specify the FundingInstrument of the Payer
            //for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            //The Payment creation API requires a list of FundingInstrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            //Now create a Payer object and assign the fundingInstrument List to the object

            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit card";

            //finally create the payment object and assign the payer object & transaction lit to it
            Payment pymnt = new Payment();
            pymnt.intent = "Sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from PayPal
                //basically we are sending the clientID and clientSecret key in this function
                //to the get the context from the PayPal API to make the payment
                //for which we have created the objecct above.

                //Code for the configuration class is provided next


                //Basically, the apiContext object has a accesstoken which is sent by PayPal
                //to authenticate the payment to a factilitator account.
                //An access token could be an alphanumeric string

                APIContext apiContext = Configuration.GetAPIContext();

                //Create is a Payment class function which actually sends the payment details
                //to the PayPal API for the payment. The function is passed with the ApiContext
                //which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.state is "approved" it means the payment was successfull else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }
            }
            catch (PayPal.PayPalException ex)
            {
                //log4net.Repository.Hierarchy.Logger.Log("Error: " + ex.Message);
                Logger.Log("Error: " + ex.Message);
                return View("FailureView");
            }

            return View("SuccessView");
        }

        public ActionResult SuccessView()
        {
            String authToken = WebConfigurationManager.AppSettings["PDTToken"];

            //read in txn token from querystring
            String txToken = Request.QueryString.Get("tx");
            PDTHolder confirmacion = PDTHolder.RequestPDTToPayPal(txToken);
            string[] splitCustom = {};
            int vehicleValue = 0;
            int totalPolicyCoverageDays = 0;
            string vinNumber = "", carYear = "", carMake = "", carModel = "", coverageType = "", payPalTransactionId = "", vehicleType = "", vehiclePlate = "";
            decimal policyTotalCost = 0;
            DateTime startDate = DateTime.Now, endDate = DateTime.Now;
            AutomobilePolicy poliza = null;
            if (confirmacion != null)
            {
                //var poliza = db.AutomobilePolicies.Find(db.AutomobilePolicies.Where( pol => pol.clientID == null));
                ////AutomobilePolicy poliza = db.AutomobilePolicies.SingleOrDefault(pol => pol.Id == null);
                //if (poliza.payPalTransactionId != confirmacion.TransactionId)
                //{
                   
                //}
                poliza = db.AutomobilePolicies.FirstOrDefault(pol => pol.Id == null);
                if (poliza != null)
                {
                    //var currentUserId = User.Identity.GetUserId()
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new Portugal_Insurance___PayPalContextDB()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());
                    String customList = confirmacion.Custom.ToString();
                    splitCustom = customList.Split(',');

                    VMAutoPolicyInfo policyInfo = new VMAutoPolicyInfo(splitCustom);
                    ViewBag.policyInfo = policyInfo;

                    for (int i = 0; i < splitCustom.Length; i++)
                    {
                        vehicleValue = int.Parse(splitCustom[0]);
                        vinNumber = splitCustom[1].ToString();
                        carYear = splitCustom[2].ToString();
                        carMake = splitCustom[3].ToString();
                        carModel = splitCustom[4].ToString();
                        startDate = DateTime.Parse(splitCustom[5]);
                        endDate = DateTime.Parse(splitCustom[6]);
                        vehicleType = splitCustom[7].ToString();
                        vehiclePlate = splitCustom[8].ToString();
                        //totalPolicyCoverageDays = int.Parse(splitCustom[9]);
                        //coverageType = confirmacion.ItemName;

                    }
                    totalPolicyCoverageDays = (int)endDate.Subtract(startDate).TotalDays;
                    coverageType = confirmacion.ItemName;//We get converage type from paypal response

                    policyTotalCost = (decimal)confirmacion.GrossTotal;//We get GrossTotal from paypal response
                    payPalTransactionId = confirmacion.TransactionId;
                    //Se le asigna a una poliza el cliente logeado que acaba de pagar
                    //poliza.Id = manager.FindById(User.Identity.GetUserId().ToString()).Id;
                    //poliza.Id = User.Identity.GetUserId().ToString();
                    poliza.Id = currentUser.Id.ToString();
                    poliza.carMake = carMake;
                    poliza.carModel = carModel;
                    poliza.carYear = carYear;
                    poliza.policyEndingDate = endDate;
                    poliza.policySold = true;
                    poliza.policySoldDate = DateTime.Now;
                    poliza.policyStartingDate = startDate;
                    poliza.policyCoverageDays = totalPolicyCoverageDays;
                    poliza.vehicleValue = vehicleValue;
                    poliza.vehicleVin = vinNumber;
                    poliza.vehicleType = vehicleType;
                    poliza.vehiclePlate = vehiclePlate;
                    poliza.coverageType = coverageType;
                    poliza.policyTotalCost = policyTotalCost;
                    poliza.payPalTransactionId = payPalTransactionId;
                    poliza.paymentStatus = confirmacion.PaymentStatus;
                    poliza.paymentFee = (decimal)confirmacion.PaymentFee;
                    poliza.payerEmail = confirmacion.PayerEmail;
                    db.Entry(poliza).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                }
                else
                {
                    ViewBag.error = "Please call us at: (###)###-####, there was a problem processing your order. /n" +
                        "We apologize for the inconvinience.";
                }
            }
            /*
            String query = string.Format("cmd=_notify-synch&tx={0}&at={1}",
                                  txToken, authToken);

            // Create the request back
            string url = WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;
            */
            // Write the request back IPN strings
            /* StreamWriter stOut = new StreamWriter(req.GetRequestStream(),
                                      System.Text.Encoding.ASCII);
             stOut.Write(query);
             stOut.Close();*/

            // Do the request to PayPal and get the response
            /*StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            String strResponse = stIn.ReadToEnd();
            stIn.Close();*/
            SendPDF(poliza.automobilePolicyID); 
            return View(poliza);
        }
        public ActionResult ViewPolicy(int policyID)
        {
            AutomobilePolicy poliza = db.AutomobilePolicies.Find(policyID);
            return View(poliza); 
        }
        //DOWNLOAD PDF DEMO TO GENERATE PDF FROM VIEWS ETC. USING ROTATIVA
        /// <summary>
        /// Action Method using viewAsPdf class to create view as pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult SendPDF(int policyID)
        {
            try
            {
                AutomobilePolicy poliza = db.AutomobilePolicies.Find(policyID);
                var a = new ViewAsPdf();
                a.ViewName = "ViewPolicy";
                a.Model = poliza;
                var pdfBytes = a.BuildPdf(ControllerContext);

                // Optionally save the PDF to server in a proper IIS location.
                var fileName = string.Format("policy_folio_{0}.pdf", poliza.policyFolio);
                var path = Server.MapPath("~/App_Data/" + fileName);
                System.IO.File.WriteAllBytes(path, pdfBytes);

                // return ActionResult
                MemoryStream ms = new MemoryStream(pdfBytes);
                try
                {
                    var mensaje = new MailMessage();
                    var smtp = new SmtpClient();
                    mensaje.From = new MailAddress("deisyeliii@gmail.com");
                    mensaje.To.Add("rodriquez602@gmail.com, deisyeliii@gmail.com"/*poliza.ApplicationUser.emailAddress*/);
                    mensaje.Subject = "Seguros Portugal Insurance";
                    mensaje.IsBodyHtml = true;
                    mensaje.Body = "This is a test email, confirming the mock purchase of a insurance policy";
                    mensaje.Attachments.Add(new Attachment(path));
                    smtp.Host = "smtp.gmail.com";
                    //smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("deisyeliii@gmail.com", "Hnbjm@6381075459");
                    smtp.EnableSsl = true;
                    smtp.Send(mensaje);
                    Response.Write("Email Sent Succesfully");

                }
                catch(Exception ex)
                {
                    Response.Write(ex.Message); 
                }

                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult DownloadPDF(int policyID)
        {
            try
            {
                AutomobilePolicy poliza = db.AutomobilePolicies.Find(policyID);
                
                //THIS IS TO DOWNLOAD THE PDF DIRECTLY TO CLIENTS PC WHEN THEY CLICK ON DOWNLOADPDF LINK
                //Use ViewAsPdf Class to generate pdf using GeneratePDF.cshtml view
                return new Rotativa.ViewAsPdf("ViewPolicy", poliza)
                {
                    FileName = string.Format("policy_folio_{0}.pdf", poliza.policyFolio)

            };
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        //<summaroy>
        //This method is using ActionAsPdf class to generate pdf
        //</summary>
        //<returns></returns>
        public ActionResult DownloadActionAsPDF(int polizaID)
{
    try
    {
        //will take ActionMethod and generate the pdf
                return new Rotativa.ActionAsPdf("ViewPolicy")
                {
                    FileName = "SecondPdf.pdf"
                };
    }
    catch (Exception ex)
    {

        throw;
    }
}

        /// <summary>
        /// Action method to return view as pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneratePDF()
{
    try
    {
        var model = new GeneratePDFModel();
        //Your data from db

        //hard coded value for test purpose

        var content = "<h2>PDF Created<h2>" +
        "<p>Ohh This is very easy to generate pdf using Rotativa<p>";
        var logoFile = @"/Images/logo.png";

        model.PDFContent = content;
        model.PDFLogo = Server.MapPath(logoFile);

        //FileName = "Fee_Challan.pdf",
        //        PageOrientation = Orientation.Landscape,
        //        PageSize = Size.A4,
        //        PageMargins = { Left = 10, Right = 10, Top = 15, Bottom = 22 },
        //        MinimumFontSize = 7,
        //        PageHeight = 40

                return View(model);
    }
    catch (Exception ex)
    {
        throw;
    }
}

        /// <summary>
        /// using partial view for pdf generation
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadPartialViewPDF()
{
    var model = new GeneratePDFModel();

    //hard coded value for test purpose

    var content = "<h2>PDF Created<h2>" +
    "<p>Ohh This is very easy to generate pdf using Rotativa<p>";
    var logoFile = @"/Images/logo.png";

    model.PDFContent = content;
    model.PDFLogo = Server.MapPath(logoFile);

    return new Rotativa.PartialViewAsPdf("_PartialViewTest", model) { FileName = "partialViewAsPdf.pdf" };
}

        /// <summary>
        /// The method will geneate ulr content in pdf doc
        /// </summary>
        /// <returns></returns>
        public ActionResult UrlAsPDF()
{

            //this will generate google home page to in a pdf doc

            //THIS IS WORKING SO FAR BUT IT GENERATES THE WRONG VIEW IT GENERATES: http://segurosportugal-001-site1.atempurl.com/PayPal/iNDEX
            //string url = new Uri(HttpContext.Request.Url.AbsoluteUri).ToString();
            //string path = url.Substring(0, url.LastIndexOf("/"));
            //return new Rotativa.UrlAsPdf(path)

            string url = new Uri(HttpContext.Request.Url.AbsoluteUri).ToString();
            string path = url.Substring(0, url.LastIndexOf("/"));
            //string path = url.Substring(0, url.Length);
            //Value of HttpContext.Current.Request.Url.AbsoluteUri
            return new Rotativa.UrlAsPdf(path)
            {
        FileName = "urltest.pdf",
        PageOrientation = Rotativa.Options.Orientation.Landscape,
        PageSize = Rotativa.Options.Size.Letter

    };
}

public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create funcion call of the payment class

                    //Creating a payment
                    //baseURL is the url on which PayPal sends back the data.
                    //So we have provided URL of this controller only

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected to for PayPal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from PayPal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the PayPal redirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    //saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    //This section is executed when we have received all the payments parameters
                    //From the previous call to the function create

                    //Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                //log4net.Repository.Hierarchy.Logger.Log("Error" + ex.Message);
                Logger.Log("Error: " + ex.Message);
                return View("FailureView");
            }
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //similar to credit card itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Item Name",
                currency = "USD",
                price = "5",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            //Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            //similar as we did for credit card, do here and create details objecct
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "5"
            };

            //similar as we did for credit card, we do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "7", //Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transcation description.",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            //Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

    }
}