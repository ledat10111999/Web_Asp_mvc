namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("savepost")]
    public partial class savepost
    {
        public int ID { get; set; }

        public int IDusers { get; set; }

        public int IDpost { get; set; }

        public virtual user user { get; set; }
    }
}
