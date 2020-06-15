using Do_an_Web.Models;
using Do_an_Web.ViewModel.Validate;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Do_an_Web.ViewModel
{
    public class UpdatePostViewModel
    {
        public int ID { get; set; }
       
        [Required]
        public string tenTp { get; set; }
        [Required]
        public string tenQuan { get; set; }
        [Required]
        public string tenDuong { get; set; }
        [Required]
        public string tenPhuong { get; set; }
        [Required]
        public string soNha { get; set; }
        [Required]
        public string DiaChiChinhXac { get; set; }
        [Required]
        public string ThongTinMoTa { get; set; }
        [Required]
        public Nullable<int> DienTich { get; set; }
        [Required]
        public string TieuDe { get; set; }
        [Required]
        public string NoiDung { get; set; }
        [Required]
        public string DoiTuongChoThue { get; set; }
        [Required]
        public Nullable<int> Gia { get; set; }
        
        public int IDusers { get; set; }
        [Required]
       
        public Nullable<int> SDT { get; set; }

        public IEnumerable<province> provinces { get; set; }
        public IEnumerable<district> districts { get; set; }
        public IEnumerable<ward> wards { get; set; }
        public IEnumerable<street> streets { get; set; }
        [Required(ErrorMessage = "Nhập hình ảnh")]
        [FileType("JPG,JPEG,PNG")]
        public HttpPostedFileBase[] image { get; set; }
        public enum DoiTuong
        {
            Nam,
            Nữ

        }
    }
}
 