using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebBanXeGanMay.Models;

namespace WebBanXeGanMay.Controllers
{
    public class UserController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: User
        public ActionResult Index()
        {
            ViewBag.TenDN = "Chưa đăng nhập"; 
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var email = collection["Email"];
            var sdt = collection["SDT"];
            var diachi = collection["DiaChi"];
            var ngaysinh = collection["Ngaysinh"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var xacnhanmatkhau = collection["XacNhanMatKhau"];
            bool flag = true;

            if (String.IsNullOrEmpty(hoten))
            {
                flag = false;
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                flag = false;
                ViewData["Loi5"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(sdt))
            {
                flag = false;
                ViewData["Loi6"] = "Số điện thoại không được để trống";
            }
            if (string.IsNullOrEmpty(tendn))
            {
                flag = false;
                ViewData["Loi2"] = "Tên đăng nhập không được để trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                flag = false;
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }
            if (String.IsNullOrEmpty(xacnhanmatkhau))
            {
                flag = false;
                ViewData["Loi4"] = "Xác nhận mật khẩu không được để trống";
            }
            if (flag == true)
            {
                kh.HoTen = hoten;
                kh.Email = email;
                kh.DienthoaiKH = sdt;
                kh.DiachiKH = diachi;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;

                db.KHACHHANGs.Add(kh);
                db.SaveChanges();

                return RedirectToAction("DangNhap");
            }

            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhap(FormCollection collection)
        {
            ViewBag.Thongbao = "Đăng nhập tài khoản";
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];

            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.TenDN = tendn;
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("index", "Store");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
    }
}