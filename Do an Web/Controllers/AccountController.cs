using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using Do_an_Web.Models;
using System.Threading.Tasks;
using System.Web.Security;
using System.Runtime.CompilerServices;

namespace Do_an_Web.Controllers
{
    public class AccountController : Controller
    {

       
        // GET: Account
        public ActionResult signin (string returnUrl)
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            if (!String.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult signin( login signin)
        {
            var db = new NhaTroEntities();
            if (ModelState.IsValid)
            {
                var account = db.users.FirstOrDefault(x => x.Email == signin.Email.Trim());
                if (account != null)
                {
                    var person = db.users.FirstOrDefault(x => x.Email == signin.Email&& x.Pass == signin.Pass.Trim());
                    if (person != null)
                    {
                        Session["username"] = person.Last_name + " " + person.First_name;
                        Session["ID"] = person.ID;
                        Session["quyenhan"] = person.QuyenHan;
                        Session["Phone"] = person.SDT;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Pass", "Sai mật khẩu");
                        return View(signin);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "tài khoản hoặc mật khẩu không hợp lệ");
                    return View(signin);
                }
            }
            else
            {
               
                return View();
            }
             
           
        }
        //Đăng ký
        public ActionResult signup()
        {
            return View();
        }
        // post đăng ký
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult signup(register signup)
        {
            if (ModelState.IsValid)
            {
                var db = new NhaTroEntities();
                var checkemail = db.users.FirstOrDefault(x => x.Email == signup.Email.Trim());
             
                if (checkemail == null)
                {
                    if (signup.Pass == signup.confirmpass)
                    {
                        var date = DateTime.Now;
                        var us = new user()
                        {

                            Email = signup.Email,
                            Created_at = date,
                            Update_at = date,
                            First_name = signup.First_name,
                            Last_name = signup.Last_name,
                            Pass = signup.Pass,
                            SDT = signup.SDT,
                            QuyenHan = "user",
                            money =100000,

                            

                        };
                        try
                        {
                            db.users.Add(us);
                            db.SaveChanges();
                            var id = db.users.FirstOrDefault(x => x.Email == signup.Email.Trim());
                            Session["username"] = signup.Last_name + " " + signup.First_name;
                            Session["ID"] = id.ID;
                            Session["QuyenHan"] = id.QuyenHan;
                            Session["Phone"] = id.SDT;
                            return RedirectToAction("Index", "Home");
                        }
                        catch (Exception e)
                        {
                            return Content("Khong tao duoc" + e);
                        }
                       


                    }
                    else
                    {
                        ModelState.AddModelError("confirmpass", "Mật khẩu không khớp");
                        return View(signup);
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(signup);
                }        
            }
            else
            {
                return View();
            }
            
            
        }
        [HttpDelete]
        public ActionResult SignOut()
        {
            Session.Remove("ID");
            return Json(200);
        }
    }
}