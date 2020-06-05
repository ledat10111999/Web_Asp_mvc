using Microsoft.Ajax.Utilities;
using Microsoft.Azure.Management.BatchAI.Fluent.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_an_Web.Models
{
    public partial class uppost
    {
       
       // public int IDimg { get; set; }
       [Required(ErrorMessage ="Chọn thành phố")] 
        public string tenTp { get; set; }
      
        [Required(ErrorMessage = "Chọn Quận")]
        public string tenQuan { get; set; }
        [Required(ErrorMessage = "Chọn đường")]
        public string tenDuong { get; set; }
        [Required(ErrorMessage = "Chọn phường")]
        public string tenPhuong { get; set; }
        [Required(ErrorMessage = "Nhập số nhà")]
        public string soNha { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ chính xác")]
        public string DiaChiChinhXac { get; set; }
        [Required(ErrorMessage = "Chọn thông tin mô tả")]
        public string ThongTinMoTa { get; set; }
        [Required(ErrorMessage = "Chọn thông đối tượng cho thuê")]
        public string DoiTuongChoThue { get; set; }
        [Required(ErrorMessage = "Nhập diện tích")]
        public Nullable<int> DienTich { get; set; }
        [Required(ErrorMessage = "Nhập tiêu đề")]
        public string TieuDe { get; set; }
        [Required(ErrorMessage = "Nhập nội dung")]
        public string NoiDung { get; set; }
        [Required(ErrorMessage = "Nhập giá")]
        public Nullable<int> Gia { get; set; }
        // public int IDusers { get; set; }
        [Required(ErrorMessage = "Nhập hình ảnh")]
        public HttpPostedFileBase [] image { get; set; }
        [Required(ErrorMessage = "Nhập số diện thoại")]
        public Nullable<int> SDT { get; set; }
        public int IDuser { get; set; }
    }
    public enum DoiTuong
    {
        Nam,
        Nữ
    }
}