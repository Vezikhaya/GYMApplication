namespace GYMApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanMaster")]
    public partial class PlanMaster
    {
        [Key]
        public int PlanID { get; set; }

        [StringLength(50)]
        public string PlanName { get; set; }

        public decimal PlanAmount { get; set; }

        public decimal ServicetaxAmout { get; set; }

        [StringLength(50)]
        public string ServiceTax { get; set; }

        public DateTime CreateDate { get; set; }



        public DateTime ModifyDate { get; set; }

 

        [StringLength(1)]
        public string RecStatus { get; set; }

        public int SchemeID { get; set; }
        public virtual SchemeMaster SchemeMaster { get; set; }



        public decimal TotalAmout { get; set; }

        [StringLength(50)]
        public string ServicetaxNo { get; set; }
    }
}
