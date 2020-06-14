using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_Web.ViewModel
{
    public class ThongTinMoTa
    {
        private string thongtin;

        public ThongTinMoTa(string thongtin)
        {
            this.Thongtin = thongtin;
        }

        public string Thongtin { get => thongtin; set => thongtin = value; }
    }
}
