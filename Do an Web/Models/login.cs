using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Do_an_Web.Models
{
    public partial class login
    {
        [Required (ErrorMessage ="Nhập email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nhập password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
        public bool save { get; set; }
    }
}