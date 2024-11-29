using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXeGanMay.Models;

namespace WebBanXeGanMay.Controllers
{
    public class StoreController : Controller
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        // GET: Store
        private List<XEGANMAY> Layxemoi(int soluong)
        {
            return db.XEGANMAYs.OrderByDescending(a => a.Ngaycapnhat).Take(soluong).ToList();
        }
        public ActionResult Index(int ? page )
        {
            int pageSize = 4;
            int pageNum = ( page ?? 1);
           
            var xemoi = Layxemoi(15);
            return View(xemoi.ToPagedList(pageNum,pageSize));
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
