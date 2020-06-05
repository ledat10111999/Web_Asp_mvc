namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("province")]
    public partial class province
    {
        public long id { get; set; }

        [Column("_name")]
        [StringLength(50)]
        public string C_name { get; set; }

        [Column("_code")]
        [StringLength(20)]
        public string C_code { get; set; }
    }
}
