namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("project")]
    public partial class project
    {
        public long id { get; set; }

        [Column("_name")]
        [StringLength(200)]
        public string C_name { get; set; }

        [Column("_province_id")]
        public long? C_province_id { get; set; }

        [Column("_district_id")]
        public long? C_district_id { get; set; }

        [Column("_lat")]
        public double? C_lat { get; set; }

        [Column("_lng")]
        public double? C_lng { get; set; }
    }
}
