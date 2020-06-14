using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Do_an_Web.Models
{
    public partial class register
    {
        [Required]
        [DataType(DataType.Text)]
        public string First_name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Last_name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public int SDT { get; set; }
        [Required(ErrorMessage = "Bạn nhập tài khoản")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        public string confirmpass { get; set; }

       
    }
}