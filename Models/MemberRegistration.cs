namespace GYMApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("MemberRegistration")]
    public partial class MemberRegistration
    {
        [Key]
        public long MemID { get; set; }

        [StringLength(20)]
        public string MemberNo { get; set; }

        [StringLength(100)]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public string MemberFName { get; set; }

        [StringLength(100)]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        public string MemberLName { get; set; }

        [StringLength(100)]
        [DisplayName("Middle Name")]
        [Required(ErrorMessage = "Please enter Middle Name")]
        public string MemberMName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Please select Birth Date")]
        public DateTime? DOB { get; set; }

        [StringLength(10)]
        public string Age { get; set; }

        [StringLength(10)]
        public string Contactno { get; set; }

        [StringLength(30)]
        public string EmailID { get; set; }

        [StringLength(30)]
        [DisplayName("Please select Gender")]
        public string Gender { get; set; }

        [DisplayName("Please selectWorkouttype")]

        public int PlanID { get; set; }
        public virtual PlanMaster planmaster { get; set; }
        
        public long PeriodID { get; set; }

        public DateTime CreatedDate { get; set; }

      

        [DisplayName("Joining Date")]
        [Required(ErrorMessage = "Please select Joining Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }
        public double Amount { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        public string MainMemberID { get; set; }

    
        [NotMapped]
        public string PaymentID { get; set; }

        public void SendEmail(string Email)
        {
            var email = new Email();
            email.To = Email;
            email.Body = "Dear Gym Customer <br/>We have recieved your Gym Membership payment Welcome to the Famous Vee Gym<br/> <br/><br/>Regurds<br/><br/>VEE GYM";
            email.Subject = "Membership payment Received";
            email.Sendmail();
        }


        ApplicationDbContext db = new ApplicationDbContext();



        public class ValidWorkouttypeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0)
                    return false;
                else
                    return true;
            }


        }


        public class ValidPlanAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

       

    }

}