namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            posts = new HashSet<post>();
            saveposts = new HashSet<savepost>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(45)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Pass { get; set; }

        [Required]
        [StringLength(45)]
        public string First_name { get; set; }

        [Required]
        [StringLength(45)]
        public string Last_name { get; set; }

        public int SDT { get; set; }

        [StringLength(10)]
        public string QuyenHan { get; set; }

        public decimal? money { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Created_at { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post> posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<savepost> saveposts { get; set; }
    }
}
