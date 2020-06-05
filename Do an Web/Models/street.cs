namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("street")]
    public partial class street
    {
        public long id { get; set; }

        [Column("_name")]
        [StringLength(100)]
        public string C_name { get; set; }

        [Column("_prefix")]
        [StringLength(20)]
        public string C_prefix { get; set; }

        [Column("_province_id")]
        public long? C_province_id { get; set; }

        [Column("_district_id")]
        public long? C_district_id { get; set; }
    }
}
