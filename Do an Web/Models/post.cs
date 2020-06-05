namespace Do_an_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            imgs = new HashSet<img>();
        }

        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDimg { get; set; }

        [Required]
        [StringLength(45)]
        public string tenTp { get; set; }

        [Required]
        [StringLength(45)]
        public string tenQuan { get; set; }

        [Required]
        [StringLength(45)]
        public string tenDuong { get; set; }

        [Required]
        [StringLength(45)]
        public string tenPhuong { get; set; }

        [Required]
        [StringLength(45)]
        public string soNha { get; set; }

        [Required]
        [StringLength(45)]
        public string DiaChiChinhXac { get; set; }

        [Required]
        [StringLength(45)]
        public string ThongTinMoTa { get; set; }

        public int? DienTich { get; set; }

        [Required]
        [StringLength(45)]
        public string TieuDe { get; set; }

        [Required]
        [StringLength(500)]
        public string NoiDung { get; set; }

        [Required]
        [StringLength(5)]
        public string DoiTuongChoThue { get; set; }

        public int? Gia { get; set; }

        public int IDusers { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        public int? SDT { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? update_at { get; set; }

        public bool? display { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<img> imgs { get; set; }

        public virtual user user { get; set; }
    }
}
