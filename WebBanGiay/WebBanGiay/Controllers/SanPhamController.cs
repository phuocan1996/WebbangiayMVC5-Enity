using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanGiay.Models;
using System.Net;


namespace WebBanGiay.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }
        public ActionResult XemChiTiet(int Id)
        {
            var giay = from g in data.Giays
                       where g.IdGiay == Id
                       select g;
            return View(giay.Single());
        }

        //Giày theo loại
        public ActionResult LoaiGiay()
        {
            var loaigiay = from lg in data.LoaiGiays select lg;
            return PartialView(loaigiay);
        }
        public ActionResult GiayTheoLoai(int Id)
        {
            int? page = 1;
            int pageNum = (page ?? 1);
            var giay = from g in data.Giays where g.IdLoaiGiay == Id select g;
            return View(giay.OrderBy(n => n.IdGiay).ToPagedList(pageNum, 8));                     

        }
        
        //Giày theo hãng
        public ActionResult HangGiay()
        {
            var hanggiay = from hg in data.MaHangGiays select hg;
            return PartialView(hanggiay);
        }
        public ActionResult GiayTheoHang(int Id)
        {
            int? page = 1;
            int pageNum = (page ?? 1);
            var giay = from g in data.Giays
                       where g.IdMHG == Id
                       select g;
            return View(giay.OrderBy(n => n.IdGiay).ToPagedList(pageNum, 8));
            
        }
    }
}