﻿using Portugal_Insurance___PayPal.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Portugal_Insurance___PayPal.PayPal;
//using PayPal.Api;

namespace Portugal_Insurance___PayPal.Controllers
{
    public class PayPalController : Controller
    {
        //GET: /Paypal/
        
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

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "http://orquidea-001-site3.dtempurl.com/Paypal/PaymentWithPayPal?";

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