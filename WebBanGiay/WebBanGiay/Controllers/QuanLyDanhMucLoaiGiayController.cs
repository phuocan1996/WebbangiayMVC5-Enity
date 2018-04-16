using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class QuanLyDanhMucLoaiGiayController : Controller
    {
        //
        // GET: /QuanLyDanhMucLoaiGiay/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        public ActionResult Index()
        {
            return View(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(LoaiGiay loaigiay)
        {
            data.LoaiGiays.Add(loaigiay);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int IdLoaiGiay)
        {
            if (IdLoaiGiay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiGiay nsx = data.LoaiGiays.SingleOrDefault(n => n.IdLoaiGiay == IdLoaiGiay);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChinhSua(LoaiGiay loaigiay)
        {
            data.LoaiGiays.Add(loaigiay);
            data.Entry(loaigiay).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? IdLoaiGiay)
        {
            if (IdLoaiGiay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiGiay nsx = data.LoaiGiays.SingleOrDefault(n => n.IdLoaiGiay == IdLoaiGiay);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost]
        public ActionResult Xoa(int IdLoaiGiay)
        {
            if (IdLoaiGiay == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGiay nsx = data.LoaiGiays.SingleOrDefault(n => n.IdLoaiGiay == IdLoaiGiay); if (nsx == null)
            {
                return HttpNotFound();
            }
            data.LoaiGiays.Remove(nsx);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}