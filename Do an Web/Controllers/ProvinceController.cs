using Do_an_Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Do_an_Web.Controllers
{
    public class ProvinceController : Controller
    {
        // GET: Province
        public ActionResult ListProvince()
        {
            var db = new NhaTroEntities();
            var p = db.posts.ToList();
          
            return View(p);
        }
        
    }
}