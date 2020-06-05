namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("img")]
    public partial class img
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        public int? IDpost { get; set; }

        public int? IDimg { get; set; }

        public virtual post post { get; set; }
    }
}
