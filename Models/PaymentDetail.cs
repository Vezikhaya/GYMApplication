namespace GYMApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PaymentDetail
    {
        [Key]
        public long PaymentID { get; set; }

        public int? PlanID { get; set; }

        public int? WorkouttypeID { get; set; }

        [StringLength(50)]
        public string Paymenttype { get; set; }

        public DateTime? PaymentFromdt { get; set; }

        public DateTime? PaymentTodt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PaymentAmount { get; set; }

        public DateTime? NextRenwalDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreateUserID { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? ModifyUserID { get; set; }

        [StringLength(1)]
        public string RecStatus { get; set; }

        public long? MemberID { get; set; }

        [StringLength(30)]
        public string MemberNo { get; set; }
    }
}
