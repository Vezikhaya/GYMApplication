using GYMApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GYMApplication.Controllers
{
    public class PayFastController : Controller
    {
        // GET: PayFast
        // GET: Payfast
        ApplicationDbContext db = new ApplicationDbContext();
           MemberRegistration memberRegistration = new MemberRegistration();


        // GET: Payment

        public ActionResult SPayFast()
        {
            var email = User.Identity.GetUserName();
            var FindEmail = db.Users.Where(m => m.UserName == email);
            //var Del = db.DeliveryOptions.Count(m => m.UserName == email);
            //double delivery = 0;
            //if (Del > 0)
            //{
            //    delivery = 100;
            //}
            string EmailFound = "";
            foreach (var item in FindEmail)
            {
                EmailFound = item.Email;
            }

            var cart = memberRegistration.Amount;


            decimal amount = Convert.ToDecimal(cart);
            string name = "Lakke Lakke ";
          

         
                string site = "";
                string merchant_id = "";
                string merchant_key = "";

                // Check if we are using the mmor live system
                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process";
                    merchant_id = "10022846";
                    merchant_key = "dz03cbdzixw7t";
                }
                else if (paymentMode == "live")
                {
                    site = "https://www.payfast.co.za/eng/process";
                    merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                    merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
                }
                else
                {
                    throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
                }
                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&amount=" + HttpUtility.UrlEncode(Convert.ToString(amount)));

                str.Append("&item_name=" + HttpUtility.UrlEncode(name));

                str.Append("&item_price=" + HttpUtility.UrlEncode(Convert.ToString(amount)));


                // Redirect to PayFast
                Response.Redirect(site + str.ToString());
         
            return View();
        }

    }
}