using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GYMApplication.Models
{
    public class MembershipVM
    {


        int orderId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
      public string PlanType { get; set; }
        public double Amount { get; set; }
        public long PeriodID { get; set; }


        ApplicationDbContext db = new ApplicationDbContext();

        public int GetMembership()
        {
            return orderId;
        }
        public void Create()
        {
            var rec = new ReceiptDetail { Email = Email, FirstName = FirstName, LastName = LastName, PhoneNumber = PhoneNumber };
            db.ReceiptDetails.Add(rec);
            var membership = new MemberRegistration { Amount = Amount, CreatedDate = DateTime.Now, PaymentID =Convert.ToString( rec.ReceiptID) , PlanID = int.Parse( PlanType) };
    
            SendEmail(this.Email);
       
         
        }
        public void SendEmail(string Email)
        {
            var email = new Email();
            email.To = Email;
            email.Body = "Dear Customer<br/>We have recieved your order payment and it is being processed<br/> <br/><br/>Regurds<br/><br/>VEE GYM";
            email.Subject = "OrderPayment Received";
            email.Sendmail();
        }

      
    }
}