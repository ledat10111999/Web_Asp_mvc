using Do_an_Web.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_an_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NhaTroEntities db;
        public HomeController()
        {
            db = new NhaTroEntities();
        }
        public ActionResult List()
        {
           
            var p = db.posts.ToList();
            var list = JsonConvert.SerializeObject(p,
    Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

            return Content(list, "application/json");



        }
        public ActionResult Index()
        {
            ViewBag.province = new SelectList(GetProvinces(), "C_name", "C_name");
         
            return View(db.posts.ToList());
        }
        public List<province> GetProvinces()
        {
            
            List<province> province = db.provinces.ToList();
            
            return province;
        }
        [HttpPost]
        public ActionResult GetDistrict(string province)
        {
            
            var query = from b in db.districts
                        join c in db.provinces
                        on b.C_province_id equals c.id where c.C_name == province
                        select new
                        {
                            name =b.C_name,
                            prefix = b.C_prefix,
                            bolt = b.C_prefix + " " + b.C_name ,   
                            ID = b.id

                        };
            var district = query.ToList();
            
            ViewBag.District = new SelectList(district, "name", "bolt");
            return PartialView("District");
        }
        [HttpPost]
        public ActionResult GetWard(getward infor)
        {
            
            var query = from d in db.districts
                        join w in db.wards
                        on d.id equals w.C_district_id
                        join p in db.provinces on w.C_province_id equals p.id
                        join pr in db.provinces on infor.province equals pr.C_name
                        where d.C_name == infor.district
                        select new
                        {
                            name = w.C_name,
                            prefix = w.C_prefix,
                            ID = w.id,
                            full = w.C_prefix + " " + w.C_name,
                            provinceid = w.C_province_id
                        };
            var Ward = query.ToList();
            ViewBag.Ward = new SelectList(Ward,"name","full");
            return PartialView("_WardPartial");
          
        }
        [HttpPost]
        public ActionResult GetStreet(getward info)
        {
            var db = new NhaTroEntities();
            var query = from s in db.streets
                        join p in db.provinces on s.C_province_id equals p.id
                        join d in db.districts on s.C_district_id equals d.id
                        join pr in db.provinces on info.province equals pr.C_name
                        where info.district == d.C_name
                        select new
                        {
                            name = s.C_name,
                            prefix = s.C_prefix,
                            full = s.C_prefix + " " + s.C_name,
                        };
            var street = query.ToList();
            ViewBag.Street = new SelectList(street, "name", "full");
            return PartialView("_StreetPartial");

        }


    }
}