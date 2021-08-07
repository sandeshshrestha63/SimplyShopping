﻿using e_commerce.DAL;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment

        SimplyShoppingEntities db = new SimplyShoppingEntities();
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Payment/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }

            //on successful payment, show success page to user.
            return RedirectToAction("SuccessView");
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

            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            //Adding Item Details like name, currency, price etc
            List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);
            foreach (var item in cart )
            {
                itemList.items.Add(new Item()
                {
                    name = item.Product.ProductName.ToString(),
                    currency = "USD",
                    price = item.Product.Price.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
            }
            

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = Session["SessTotal"].ToString()
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = Session["SessTotal"].ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };
            var invoice = Convert.ToString((new Random()).Next(100000));
             invoice = "#" + invoice.ToString();
            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = invoice , //Generate an Invoice No
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

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
        public ActionResult SuccessView()
        {
            try
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);
                tbl_OrdKey ordkey = new tbl_OrdKey();
                Tbl_Members member =(Tbl_Members)(Session["UserName"]);
                ordkey.CustName = member.FristName + " "+member.LastName;
                ordkey.CustPhone = member.contact;
                ordkey.OrdDate = DateTime.Now;
                ordkey.IsDelivered = false;
                ordkey.OrdTot = Convert.ToDecimal(Session["SessTotal"].ToString());
                db.tbl_OrdKey.Add(ordkey);
                //db.SaveChanges();
                var ordid = db.tbl_OrdKey.OrderByDescending(x=>x.OrdID).ToList();
                var id = ordid.FirstOrDefault().OrdID;
                foreach (var item in cart)
                {
                    tbl_OrdHolder ordHolder = new tbl_OrdHolder();
                    ordHolder.ItemType = item.Product.ProductName;
                    ordHolder.ItemDescription = "";
                    ordHolder.Price = item.Product.Price* item.Quantity;
                    ordHolder.Quantity = item.Quantity;
                    //ordHolder.OrdId = Convert.ToInt64(id);
                    db.tbl_OrdHolder.Add(ordHolder);
                    //db.SaveChanges();
                }
                db.SaveChanges();
                Session.Remove("cart");
            }
            catch(Exception ex)
            {

            }
            return View();
        }
    }
}