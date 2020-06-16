using Do_an_Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_an_Web.Controllers
{
    public class UserController : Controller
    {
        public readonly NhaTroEntities db;
        // GET: User
        public UserController()
        {
            db = new NhaTroEntities();
        }
        public ActionResult AccountInfor()
        {
            if(Session["ID"] != null)
            {
                var id = Convert.ToInt32(Session["ID"].ToString());

                var user = db.users.FirstOrDefault(p => p.ID ==id );
            return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Posted()
        {
            if (Session["ID"] != null)
            {
                var id = Convert.ToInt32(Session["ID"].ToString());
                var post = db.posts.Where(p => p.IDusers == id).ToList();
                return View(post);

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Saved()
        {
            if (Session["ID"] != null)
            {
                var id = Convert.ToInt32(Session["ID"].ToString());
                var save = (from p in db.posts
                           join s in db.saveposts on p.ID equals s.IDpost
                           where s.IDusers == id
                           select p).ToList();
                return View(save);

            }
            return RedirectToAction("Index", "Home");
        }

    
    }
}