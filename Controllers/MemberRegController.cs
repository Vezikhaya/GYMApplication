using GYMApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GYMApplication.Controllers
{
    public class MemberRegController : Controller
    {
        // GET: MemberReg

        private ApplicationDbContext db = new ApplicationDbContext();


      



        // GET: MemberReg
        public ActionResult Index()
        {
            return View(db.MemberRegistrations.ToList());
        }

        // GET: MemberReg/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistrations.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            return View(memberRegistration);
        }

        // GET: MemberReg/Create
        [Authorize]
        public ActionResult Create()
        {



         //   ViewBag.SchemeID = new SelectList(db.SchemeMasters, "SchemeID", "SchemeName");
            ViewBag.PlanID = new SelectList(db.PlanMasters, "PlanID", "PlanName");

            ViewData["SelectedPlan"] = string.Empty;



            return View();


        }

        // POST: MemberReg/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
   //     [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "MemID,MemberNo,MemberFName,MemberLName,MemberMName,DOB,Age,Contactno,EmailID,Gender,PlanID,PeriodID,CreatedDate,Amount,JoiningDate,Address,MainMemberID")] MemberRegistration memberRegistration)
        {
            if (ModelState.IsValid)
            {
                var Amount = (from v in db.PlanMasters
                            where v.PlanID == memberRegistration.PlanID
                            select v.TotalAmout).FirstOrDefault();
                memberRegistration.Amount = Convert.ToDouble( Amount);
                string[] parttime = memberRegistration.DOB.ToString().Split('-');
                int year1 = Convert.ToInt32(memberRegistration.DOB.Value.Year);
                int month1 = Convert.ToInt32(memberRegistration.DOB.Value.Month);
                int day1 = Convert.ToInt32(memberRegistration.DOB.Value.Day);
                DateTime birthdate = new DateTime(year1, month1, day1);
                memberRegistration.DOB = birthdate;

                string[] joing = memberRegistration.JoiningDate.ToString().Split('-');
                int year2 = Convert.ToInt32(memberRegistration.JoiningDate.Value.Year);
                int month2 = Convert.ToInt32(memberRegistration.JoiningDate.Value.Month);
                int day2 = Convert.ToInt32(memberRegistration.JoiningDate.Value.Day);
                DateTime joining = new DateTime(year2, month2, day2);
                memberRegistration.JoiningDate = joining;
                
                var ID = User.Identity.GetUserId();
                memberRegistration.MainMemberID = (ID);
                var est = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
                DateTime targetTime = TimeZoneInfo.ConvertTime(DateTime.Now, est);
                memberRegistration.CreatedDate = targetTime;

                var rec = new ReceiptDetail { Email = memberRegistration.EmailID, FirstName = memberRegistration.MemberFName, LastName = memberRegistration.MemberLName, PhoneNumber = memberRegistration.MemberLName, createddate = memberRegistration.JoiningDate, Memberid = memberRegistration.MemID };
                db.ReceiptDetails.Add(rec);


                memberRegistration.SendEmail(memberRegistration.EmailID);


                db.MemberRegistrations.Add(memberRegistration);
                db.SaveChanges();

              



                TempData["Message"] = "Member Created Successfully.";
         
              
            }
     //       ViewBag.SchemeID = new SelectList(db.SchemeMasters, "SchemeID", "SchemeName", memberRegistration.SchemeID);


           ViewBag.PlanID = new SelectList(db.PlanMasters, "PlanID", "PlanName", memberRegistration.PlanID);
            ViewData["SelectedPlan"] = memberRegistration.PlanID;
            string amount = (memberRegistration.Amount).ToString();
            string orderId = new Random().Next(1, 99999).ToString();
            string name = "Vee Gym";
            string description = "better Gym";


            string site = "";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the mmor live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10000100";
                merchant_key = "46f0cd694581a";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
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
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["returnurl"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
            str.Append("&amount=" + HttpUtility.UrlEncode(amount));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));

            // Redirect to PayFast
            Response.Redirect(site + str.ToString());

            return RedirectToAction("Index", "Account");
        }






        public ActionResult Return()
        {
            var plan = (int)Session["MemberReg"];
            return RedirectToAction("Confirm", new { ID = plan });
        }
        public void SendEmail(string Email)
        {
            var email = new Email();
            email.To = Email;
            email.Body = "Dear Customer<br/>Your application for your return has been successful. We will contact you shortly. <br/> <br/><br/>Regards<br/><br/>Vee Gym";
            email.Subject = "Membership";
            email.Sendmail();
        }

   
        public ActionResult SPayFast()
        {
            int convertKey = 0;
            if (Session["UserKey"] == null)
            {
                 convertKey = Convert.ToInt32(Session["UserKey"]);
            }

            //string key = Session["UserKey"].ToString();
            //var lblUserKey = key;

         //   int i = int.Parse(Session["UserKey"].ToString());

            var order = convertKey;
                var theorder = db.MemberRegistrations.Find(order);
                string amount = (theorder.Amount).ToString();
                string orderId = new Random().Next(1, 99999).ToString();
                string name = "Vee Gym";
                string description = "better Gym";


                string site = "";
                string merchant_id = "";
                string merchant_key = "";

                // Check if we are using the mmor live system
                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10000100";
                    merchant_key = "46f0cd694581a";
                }
                else if (paymentMode == "live")
                {
                    site = "https://www.payfast.co.za/eng/process?";
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

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                str.Append("&amount=" + HttpUtility.UrlEncode(amount));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                Response.Redirect(site + str.ToString());
           
            return View();
        }




        //public ActionResult SPayFast()
        //{
        //    try
        //    {


        //          var MemberRegistration = (int)Session["MemberRegistration"];
        //        var theorder = db.MemberRegistrations.Find(MemberRegistration);
        //        string amount = (theorder.Amount).ToString();
        //        string orderId = new Random().Next(1, 99999).ToString();
        //        string name = "Vezikhaya Gym";
        //        string description = "Better Gym";


        //        string site = "";
        //        string merchant_id = "";
        //        string merchant_key = "";

        //        // Check if we are using the mmor live system
        //        string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

        //        if (paymentMode == "test")
        //        {
        //            site = "https://sandbox.payfast.co.za/eng/process?";
        //            merchant_id = "10000100";
        //            merchant_key = "46f0cd694581a";
        //        }
        //        else if (paymentMode == "live")
        //        {
        //            site = "https://www.payfast.co.za/eng/process?";
        //            merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
        //            merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
        //        }
        //        // Build the query string for payment site

        //        StringBuilder str = new StringBuilder();
        //        str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
        //        str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
        //        str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
        //        str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
        //        //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

        //        str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
        //        str.Append("&amount=" + HttpUtility.UrlEncode(amount));
        //        str.Append("&Plan Package=" + HttpUtility.UrlEncode(name));
        //        str.Append("&item_description=" + HttpUtility.UrlEncode(description));

        //        // Redirect to PayFast
        //        Response.Redirect(site + str.ToString());

        //        return View();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        public ActionResult Confirm(int ID)
        {
            var Membership = db.MemberRegistrations.Find(ID);
            ViewBag.Message = "Payment Recieved";
            db.Entry(Membership).State = EntityState.Modified;
            db.SaveChanges();
            return View(Membership);
        }
        public ActionResult ViewOrder(int Id)
        {
            var membership = db.PlanMasters.Find(Id);
            return View("Confirm", membership);
        }



        // GET: MemberReg/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistrations.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            return View(memberRegistration);
        }

        // POST: MemberReg/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemID,MemberNo,MemberFName,MemberLName,MemberMName,DOB,Age,Contactno,EmailID,Gender,PlantypeID,WorkouttypeID,Createdby,CreatedDate,ModifiedDate,ModifiedBy,MemImagename,MemImagePath,JoiningDate,Address,MainMemberID")] MemberRegistration memberRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberRegistration);
        }

        // GET: MemberReg/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberRegistration memberRegistration = db.MemberRegistrations.Find(id);
            if (memberRegistration == null)
            {
                return HttpNotFound();
            }
            return View(memberRegistration);
        }

        // POST: MemberReg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MemberRegistration memberRegistration = db.MemberRegistrations.Find(id);
            db.MemberRegistrations.Remove(memberRegistration);
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
