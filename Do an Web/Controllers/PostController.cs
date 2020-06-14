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
using Do_an_Web.ViewModel;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;

namespace Do_an_Web.Controllers
{
    public class PostController : Controller
    {
        private readonly NhaTroEntities db;
        public PostController()
        {
            db = new NhaTroEntities();
        }
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
        // GET: Post
        public ActionResult NewPost()
        {
            if (Session["ID"] != null)
            {

                var Newpost = new NewPostViewModel
                {
                    provinces = db.provinces,
                    districts = db.districts.Where(p => p.id < 0),
                    wards = db.wards.Where(p => p.id < 0),
                    streets = db.streets.Where(p => p.id < 0)

                };
                ViewBag.ThongTinMoTa = new SelectList(listinfor(), "ThongTin", "ThongTin");
                return View(Newpost);
            }
            return RedirectToAction("Index", "Home");
           
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult NewPost(NewPostViewModel param)
        {
            if (!ModelState.IsValid)
            {
                var Newpost = new NewPostViewModel
                {
                    provinces = db.provinces,
                    districts = (from b in db.districts
                                 join c in db.provinces
                                 on b.C_province_id equals c.id
                                 where c.C_name == param.tenTp
                                 select b
                                 ).ToList(),
                    wards = (from d in db.districts
                             join w in db.wards
                             on d.id equals w.C_district_id
                             join p in db.provinces on w.C_province_id equals p.id
                             join pr in db.provinces on param.tenTp equals pr.C_name
                             where d.C_name == param.tenQuan
                             select w).ToList(),
                    streets = (from s in db.streets
                               join p in db.provinces on s.C_province_id equals p.id
                               join d in db.districts on s.C_district_id equals d.id
                               join pr in db.provinces on param.tenTp equals pr.C_name
                               where param.tenQuan == d.C_name
                               select s).ToList()

                };
                ViewBag.ThongTinMoTa = new SelectList(listinfor(), "ThongTin", "ThongTin");
                return View(Newpost);
            }
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
            for (var i = 0; i < param.image.Length; i++)
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
                    
                    var ps = db.posts.FirstOrDefault(o => o.ID == id);
                    if(ps == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ThongTinMoTa = new SelectList(listinfor(), "Thongtin", "Thongtin",ps.ThongTinMoTa);
                        var updatePost = new UpdatePostViewModel
                        {
                            provinces = db.provinces,
                            districts = (from b in db.districts
                                         join c in db.provinces
                                         on b.C_province_id equals c.id
                                         where c.C_name == ps.tenTp
                                         select b
                                 ).ToList(),
                            wards = (from d in db.districts
                                     join w in db.wards
                                     on d.id equals w.C_district_id
                                     join p in db.provinces on w.C_province_id equals p.id
                                     join pr in db.provinces on ps.tenTp equals pr.C_name
                                     where d.C_name == ps.tenQuan
                                     select w).ToList(),
                            streets = (from s in db.streets
                                       join p in db.provinces on s.C_province_id equals p.id
                                       join d in db.districts on s.C_district_id equals d.id
                                       join pr in db.provinces on ps.tenTp equals pr.C_name
                                       where ps.tenQuan == d.C_name
                                       select s).ToList(),
                            DiaChiChinhXac = ps.DiaChiChinhXac,
                            DienTich = ps.DienTich,
                            DoiTuongChoThue = ps.DoiTuongChoThue,
                            Gia = ps.Gia,
                            NoiDung = ps.NoiDung,
                            SDT = ps.SDT,
                            soNha = ps.soNha,
                            TieuDe = ps.TieuDe,
                            tenTp=ps.tenTp,
                            tenQuan = ps.tenQuan,
                            tenDuong = ps.tenDuong,
                            tenPhuong = ps.tenPhuong,
                            ID = ps.ID
                           
                        };
                        return View(updatePost);
                    }
                }
      
            }
               return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePost(UpdatePostViewModel param)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ThongTinMoTa = new SelectList(listinfor(), "Thongtin", "Thongtin", param.ThongTinMoTa);
                var updatePost = new UpdatePostViewModel
                {
                    provinces = db.provinces,
                    districts = (from b in db.districts
                                 join c in db.provinces
                                 on b.C_province_id equals c.id
                                 where c.C_name == param.tenTp
                                 select b
                         ).ToList(),
                    wards = (from d in db.districts
                             join w in db.wards
                             on d.id equals w.C_district_id
                             join p in db.provinces on w.C_province_id equals p.id
                             join pr in db.provinces on param.tenTp equals pr.C_name
                             where d.C_name == param.tenQuan
                             select w).ToList(),
                    streets = (from s in db.streets
                               join p in db.provinces on s.C_province_id equals p.id
                               join d in db.districts on s.C_district_id equals d.id
                               join pr in db.provinces on param.tenTp equals pr.C_name
                               where param.tenQuan == d.C_name
                               select s).ToList(),
                    DiaChiChinhXac = param.DiaChiChinhXac,
                    DienTich = param.DienTich,
                    DoiTuongChoThue = param.DoiTuongChoThue,
                    Gia = param.Gia,
                    NoiDung = param.NoiDung,
                    SDT = param.SDT,
                    soNha = param.soNha,
                    TieuDe = param.TieuDe,
                    tenTp = param.tenTp,

                    tenQuan = param.tenQuan,
                    tenDuong = param.tenDuong,
                    tenPhuong = param.tenPhuong,

                };
                return View(updatePost);
            }
            var date = DateTime.Now;
            post Edit = db.posts.FirstOrDefault(p => p.ID == param.ID);
            Edit.tenTp = param.tenTp;
            Edit.tenQuan = param.tenQuan;
            Edit.tenPhuong = param.tenPhuong;
            Edit.tenDuong = param.tenDuong;
            Edit.soNha = param.soNha;
            Edit.DiaChiChinhXac = param.DiaChiChinhXac;
            Edit.ThongTinMoTa = param.ThongTinMoTa;
            Edit.TieuDe = param.TieuDe;
            Edit.NoiDung = param.NoiDung;
            Edit.SDT = param.SDT;
            Edit.Gia = param.Gia;
            Edit.DienTich = param.DienTich;
            Edit.DoiTuongChoThue = param.DoiTuongChoThue;
            List<string> listimg = uploadImage(param.image);
            Edit.image = listimg[0];
            Edit.update_at = date;
            db.SaveChanges();
            return RedirectToAction( "Post", "DetailPost",param.ID);
        }
          [HttpDelete]
        public ActionResult DeletePost(int id)
        {
            var delete = db.posts.FirstOrDefault(p => p.ID == id);
            try
            {
                foreach (var i in delete.imgs)
                {
                    string fullPath = Request.MapPath("~/UploadedFiles/" + i.image);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                db.posts.Remove(delete);
                db.SaveChanges();
               
                
                return Json(200);
            }catch(Exception e)
            {
                return Json(e);
            }
        }


    }
}