using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXeGanMay.Models;

namespace WebBanXeGanMay.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        public ActionResult Index()
        {
            var xeganmays = db.XEGANMAYs.ToList();
            ViewData["xeganmays"] = xeganmays;


            var xemoinhats = db.XEGANMAYs
                                   .OrderByDescending(x => x.Ngaycapnhat)
                                   .Take(5) 
                                   .ToList();
            ViewData["xemoinhats"] = xemoinhats;

            var hangsanxuats = db.HANGSANXUATs.ToList();
            ViewData["hangsanxuats"] = hangsanxuats;

            var loaixes = db.LOAIXEs.ToList();
            ViewData["loaixes"] = loaixes;

            return View();
        }
        public ActionResult NhaPhanPhoi()
        {
            var npp = from s in db.NHAPHANPHOIs select s;
            return PartialView(npp);
        }
        public ActionResult SPTheoNPP(int id)
        {
            var products = from s in db.XEGANMAYs where s.MaNPP == id select s;
            return View(products);
        }
        public ActionResult LoaiXe()
        {
            var lx = from s in db.LOAIXEs select s;
            return PartialView(lx);
        }
        public ActionResult SPTheoLX(int id)
        {
            var products = from s in db.XEGANMAYs where s.MaLX == id select s;
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var xeGanMay = db.XEGANMAYs.Find(id);  
            if (xeGanMay == null)
            {
                return HttpNotFound();
            }
            return View(xeGanMay);
        }
    }
}