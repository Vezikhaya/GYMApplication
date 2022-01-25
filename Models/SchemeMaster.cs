namespace GYMApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SchemeMaster")]
    public partial class SchemeMaster
    {
        [Key]
        public int SchemeID { get; set; }

        [StringLength(50)]
        public string SchemeName { get; set; }



        public DateTime? Createddate { get; set; }

        public bool? schemebit { get; set; }
    }
}
