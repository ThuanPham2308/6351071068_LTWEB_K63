using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanXeGanMay.Models
{
    public class GioHang
    {
        QLBanXeGanMayEntities db = new QLBanXeGanMayEntities();
        public int iMaxe { get; set; }
        public string sTenxe { get; set; }

        public string sAnhbia { get; set; }

        public double dDongia {  get; set; }
        
        public int iSoluong {  get; set; }
        public double dThanhtien { 
            get {  return dDongia*iSoluong; }
        }
        public GioHang( int Maxe)
        {
            iMaxe = Maxe;
            XEGANMAY xe = db.XEGANMAYs.Single(n => n.MaXe == iMaxe);
            sTenxe = xe.TenXe;
            sAnhbia = xe.Anhbia;
            dDongia = double.Parse(xe.Giaban.ToString());
            iSoluong = 1;
        }
    }
}