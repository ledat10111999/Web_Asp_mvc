﻿using Do_an_Web.Models;
using Do_an_Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_an_Web.Controllers
{
    public class DetailPostController : Controller
    {
        private readonly NhaTroEntities db;
        public DetailPostController()
        {
            db = new NhaTroEntities();
        }
        // GET: DetailPost
        public ActionResult Post(int? id)
        {
            if(id== null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            var check=  db.posts.FirstOrDefault(p => p.ID == id);
            if(check != null) {
              
                if (Session["ID"] !=null)
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
        [HttpPost]
        public ActionResult SavePost(SavePost param)
        {
            try
            {
                var save = new savepost
                {
                    IDpost = param.Idpost,
                    IDusers = param.Iduser

                };
                db.saveposts.Add(save);
                db.SaveChanges();
                return Json(200);
            }catch(Exception e)
            {
                return Json(500);
            }
        }
        [HttpPost]
        public ActionResult DeleteSavePost(SavePost param)
        {
            try
            {

                var delete= db.saveposts.FirstOrDefault(p => p.IDpost == param.Idpost && p.IDusers == param.Iduser);
                db.saveposts.Remove(delete);
                db.SaveChanges();
                return Json(200);
            }
            catch (Exception e)
            {
                return Json(500);
            }
        }

    }
}