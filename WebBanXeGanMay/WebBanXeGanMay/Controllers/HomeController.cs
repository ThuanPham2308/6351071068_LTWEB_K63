using PagedList;
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
        private List<XEGANMAY> Layxemoi(int soluong)
        {
            return db.XEGANMAYs.OrderByDescending(a => a.Ngaycapnhat).Take(soluong).ToList();
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);

            var xemoi = Layxemoi(15);
            return View(xemoi.ToPagedList(pageNum, pageSize));
        }
    }
}