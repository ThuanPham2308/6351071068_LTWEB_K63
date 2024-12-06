using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXeGanMay.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Data.Entity.Migrations;

namespace WebBanXeGanMay.Controllers
{
    public class AdminController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);

                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }
        public ActionResult Xe(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.XEGANMAYs.ToList().OrderBy(n => n.MaXe).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Themmoixe()
        {
            ViewBag.MaNPP = new SelectList(db.NHAPHANPHOIs.ToList().OrderBy(n => n.TenNPP), "MaNPP", "TenNPP");

            ViewBag.MaLX = new SelectList(db.LOAIXEs.ToList().OrderBy(n => n.TenLoaiXe), "MaLX", "TenLoaiXe");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoixe(XEGANMAY Xe, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images/XE"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    Xe.Anhbia = "images/XE/" + fileName;

                    db.XEGANMAYs.Add(Xe);
                    db.SaveChanges();
                }

                return RedirectToAction("Xe");
            }
        }

        public ActionResult Chitietxe(int id)
        {
            XEGANMAY Xe = db.XEGANMAYs.Find(id);
            ViewBag.MaXe = Xe.MaXe;
            return View(Xe);
        }
        [HttpGet]
        public ActionResult Xoaxe(int id)
        {
            XEGANMAY Xe = db.XEGANMAYs.Find(id);
            ViewBag.MaXe = Xe.MaXe;
            return View(Xe);
        }
        [HttpPost, ActionName("XoaXe")]
        public ActionResult Xacnhanxoa(int id)
        {
            XEGANMAY Xe = db.XEGANMAYs.Find(id);
            ViewBag.MaXe = Xe.MaXe;
            db.XEGANMAYs.Remove(Xe);
            db.SaveChanges();
            return RedirectToAction("Xe");
        }
        [HttpGet]
        public ActionResult Suaxe(int id)
        {
            XEGANMAY Xe = db.XEGANMAYs.SingleOrDefault(n => n.MaXe == id);
            if (Xe == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNPP = new SelectList(db.NHAPHANPHOIs.ToList().OrderBy(n => n.TenNPP), "MaNPP", "TenNPP");
            ViewBag.MaLX = new SelectList(db.LOAIXEs.ToList().OrderBy(n => n.TenLoaiXe), "MaLX", "TenLoaiXe");
            return View(Xe);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suaxe(XEGANMAY Xe, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaNPP = new SelectList(db.NHAPHANPHOIs.ToList().OrderBy(n => n.TenNPP), "MaNPP", "TenNPP");
            ViewBag.MaLX = new SelectList(db.LOAIXEs.ToList().OrderBy(n => n.TenLoaiXe), "MaLX", "TenLoaiXe");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);

                    var path = Path.Combine(Server.MapPath("~/images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    Xe.Anhbia = "images/XE/" + fileName;
                    db.XEGANMAYs.AddOrUpdate(Xe);
                    db.SaveChanges();
                }

                return RedirectToAction("Xe");
            }
        }
        public ActionResult Thongke()
        {
            var ymhs = db.NHAPHANPHOIs
                .Where(ymh => ymh.TenNPP == "Công ty Yamaha")
                .Select(ymh => ymh.XEGANMAYs.Count)
                .FirstOrDefault();  
            ViewBag.ymhs = ymhs;

            var szks = db.NHAPHANPHOIs
                .Where(szk => szk.TenNPP == "Công ty Suzuki")
                .Select(szk => szk.XEGANMAYs.Count)
                .FirstOrDefault();
            ViewBag.szks = szks;

            var hds = db.NHAPHANPHOIs
                .Where(hd => hd.TenNPP == "Công ty Honda")
                .Select(hd => hd.XEGANMAYs.Count)
                .FirstOrDefault();
            ViewBag.hds = hds;

            return View();
        }

    }
}