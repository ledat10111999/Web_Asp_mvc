using Do_an_Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_an_Web.Controllers
{
    public class AdminController : Controller
    {


        private readonly NhaTroEntities db;
        public AdminController()
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
        [HttpPut]
        public ActionResult UpPost(int id)
        {
            try
            {
                var post = db.posts.FirstOrDefault(p => p.ID == id);
                post.display = true;
                db.SaveChanges();
                return Json(200);
            }
            catch (Exception e)
            {
                return Json(500);
            }
          
            
        }

        [HttpDelete]
        public ActionResult DeletePost(int id)
        {
            try
            {
                var post = db.posts.FirstOrDefault(p => p.ID == id);
                db.posts.Remove(post);
                db.SaveChanges();
                return Json(200);
            }
            catch (Exception e)
            {
                return Json(500);
            }


        }
        [HttpPost]
        public ActionResult adminList()
        {

            var p = db.posts.Where(m=> m.display == false);
            var list = JsonConvert.SerializeObject(p,
                Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

            return Content(list, "application/json");



        }
        [HttpPost]
        public ActionResult ListUser()
        {

            var p = db.users.ToList();
            var list = JsonConvert.SerializeObject(p,
                Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

            return Content(list, "application/json");



        }
        // GET: Admin

        public ActionResult Dashboard(int? id)
        {
            if (Session["ID"] !=null && Session["QuyenHan"].ToString() == "admin")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
           
        }
        [HttpPost]
        public ActionResult Listprovince()
        {
            var p = db.provinces.ToList();
            var list = JsonConvert.SerializeObject(p,
                Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

            return Content(list, "application/json");


        }
        public ActionResult BrowsePost(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var check = db.posts.FirstOrDefault(p => p.ID == id);
            if (check != null)
            {

                if (Session["ID"] != null)
                {
                    int IdUser = Int32.Parse(Session["ID"].ToString());
                    var savepost = db.saveposts.FirstOrDefault(p => p.IDpost == id && p.IDusers == IdUser);
                    ViewBag.SavePost = savepost;
                }
                return View(check);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
    }
}