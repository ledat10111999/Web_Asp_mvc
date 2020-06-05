using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebSockets;
using Do_an_Web.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;

namespace Do_an_Web.Controllers
{
    public class PostController : Controller
    {
        public List<district> listdt( string tenTp)
        {
            var db = new NhaTroEntities();
            var query = (from b in db.districts
                        join c in db.provinces
                        on b.C_province_id equals c.id
                        where c.C_name == tenTp
                        select b).ToList();

            return query;
        }
        public List<ward> listw(string tenTp, string tenQuan)
        {
            var db = new NhaTroEntities();
            var query = (from d in db.districts
                         join w in db.wards
                         on d.id equals w.C_district_id
                         join p in db.provinces on w.C_province_id equals p.id
                         join pr in db.provinces on tenTp equals pr.C_name
                         where d.C_name == tenQuan
                         select w).ToList();
            return query;
        }

        public List<street> listst(string tenTp, string tenQuan)
        {
            var db = new NhaTroEntities();
            var query = (from s in db.streets
                         join p in db.provinces on s.C_province_id equals p.id
                         join d in db.districts on s.C_district_id equals d.id
                         join pr in db.provinces on tenTp equals pr.C_name
                         where tenQuan == d.C_name
                         select s).ToList();
            return query;
        }
        public List<ThongTinMoTa> listinfor()
        {
            List<ThongTinMoTa> infor = new List<ThongTinMoTa>();
            infor.Add(new ThongTinMoTa("Phòng trọ"));
            infor.Add(new ThongTinMoTa("Nhà nguyên căn"));
            infor.Add(new ThongTinMoTa("Tìm người ở ghép"));
            infor.Add(new ThongTinMoTa("Cho thuê căn hộ"));
            return infor;
        }
        // GET: Post
        public ActionResult NewPost()
        {
            if (Session["ID"] != null)
            {
                var db = new NhaTroEntities();
               
                ViewBag.province = new SelectList(GetProvinces(), "C_name", "C_name");
                ViewBag.ThongTinMoTa = new SelectList(listinfor(), "ThongTin", "ThongTin");
                return View();
            }
            return RedirectToAction("Index", "Home");
           
        }
        public List<province> GetProvinces()
        {
            var db = new NhaTroEntities();
            List<province> province = db.provinces.ToList();
            return province;
        }
        public List<string> uploadImage(HttpPostedFileBase[] files)
        {
            List<string> listImg = new List<string>();
            foreach (HttpPostedFileBase img in files)
            {
                var InputFileName = DateTime.Now.Millisecond.ToString() + Path.GetFileName(img.FileName);
                var serverSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                img.SaveAs(serverSavePath);
                listImg.Add(InputFileName);
            }
            return listImg;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult NewPost(uppost param)
        {
            if (ModelState.IsValid)
            {
               List<string> listimg = uploadImage(param.image);
                var haha = new NhaTroEntities();
                var date = DateTime.Now;
                var newpost = new post()
                {
                     created_at = date,
                     update_at = date,
                     DiaChiChinhXac = param.DiaChiChinhXac,
                     DienTich = param.DienTich,
                     display = true,
                     DoiTuongChoThue = param.DoiTuongChoThue,
                     Gia = param.Gia,
                     IDimg = param.IDuser,
                     IDusers = param.IDuser,
                     image = listimg[0],
                     NoiDung = param.NoiDung,
                     SDT = param.SDT,
                     soNha = param.soNha,
                     tenDuong = param.tenDuong,
                     tenPhuong = param.tenPhuong,
                     tenQuan = param.tenQuan,
                     tenTp = param.tenTp,
                     ThongTinMoTa = param.ThongTinMoTa,
                     TieuDe = param.TieuDe,
                };
                haha.posts.Add(newpost);
                haha.SaveChanges();
                int id = newpost.ID;
              for (var i = 0; i< param.image.Length; i++)
                {
                    var imge = new img()
                    {
                        IDimg = param.IDuser,
                        image = listimg[i],
                        IDpost = id

                    };
                    haha.imgs.Add(imge);
                    haha.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            var db = new NhaTroEntities();
            ViewBag.province = new SelectList(GetProvinces(), "C_name", "C_name");
            ViewBag.ThongTinMoTa = new SelectList(listinfor(), "Thongtin", "Thongtin");

           if(param.tenTp != null )
            {
                var query = from b in db.districts
                            join c in db.provinces
                            on b.C_province_id equals c.id
                            where c.C_name == param.tenTp
                            select new
                            {
                                name = b.C_name,
                                prefix = b.C_prefix,
                                bolt = b.C_prefix + " " + b.C_name,
                                ID = b.id

                            };
                var district = query.ToList();
                ViewBag.District = new SelectList(district, "name", "bolt");
                return View();
            }
           if(param.tenTp != null && param.tenQuan != null && param.tenPhuong == null)
            {
                var query = from d in db.districts
                            join w in db.wards
                            on d.id equals w.C_district_id
                            join p in db.provinces on w.C_province_id equals p.id
                            join pr in db.provinces on param.tenTp equals pr.C_name
                            where d.C_name == param.tenQuan
                            select new
                            {
                                name = w.C_name,
                                prefix = w.C_prefix,
                                ID = w.id,
                                full = w.C_prefix + " " + w.C_name,
                                provinceid = w.C_province_id
                            };
                var Ward = query.ToList();
                ViewBag.Ward = new SelectList(Ward, "name", "full");
                return View();
            }
            if (param.tenTp != null && param.tenQuan != null && param.tenDuong ==null)
            {
                var query = from s in db.streets
                            join p in db.provinces on s.C_province_id equals p.id
                            join d in db.districts on s.C_district_id equals d.id
                            join pr in db.provinces on param.tenTp equals pr.C_name
                            where param.tenQuan == d.C_name
                            select new
                            {
                                name = s.C_name,
                                prefix = s.C_prefix,
                                full = s.C_prefix + " " + s.C_name,
                            };
                var street = query.ToList();
                ViewBag.Street = new SelectList(street, "name", "full");
                return View();
            }
          return View();
        }
        public ActionResult UpdatePost(int? id)
        {
            if (Session["ID"] != null)
            {
                if(id == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var db = new NhaTroEntities();
                    var ps = db.posts.Where(x => x.ID == id).FirstOrDefault();
                    if(ps == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ThongTinMoTa = new SelectList(listinfor(), "Thongtin", "Thongtin", ps.ThongTinMoTa);
                        ViewBag.province = new SelectList(GetProvinces(), "C_name", "C_name",ps.tenTp);
                        ViewBag.District = new SelectList(listdt(ps.tenTp), "C_name", "C_name",ps.tenQuan);
                        ViewBag.Ward = new SelectList(listw(ps.tenTp,ps.tenQuan), "C_name", "C_name",ps.tenPhuong);
                        ViewBag.Street = new SelectList(listst(ps.tenTp, ps.tenQuan), "C_name", "C_name",ps.tenDuong);
                        return View(ps);
                    }
                }
      
            }
               return RedirectToAction("Index", "Home");
        }




    }
}