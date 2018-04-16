using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using WebBanGiay.Models;
namespace WebBanGiay.Controllers
{
    public class QuanLyDanhMucHangGiayController : Controller
    {
        //
        // GET: /QuanLyDanhMucHangGiay/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        public ActionResult Index()
        {
            return View(data.MaHangGiays.OrderBy(n => n.IdMHG));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoMoi(MaHangGiay mahanggiay)
        {
            data.MaHangGiays.Add(mahanggiay);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int IdMHG)
        {
            if (IdMHG == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MaHangGiay nsx = data.MaHangGiays.SingleOrDefault(n => n.IdMHG == IdMHG);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChinhSua(MaHangGiay mahanggiay)
        {
            data.MaHangGiays.Add(mahanggiay);
            data.Entry(mahanggiay).State = System.Data.Entity.EntityState.Modified;
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? IdMHG)
        {
            if (IdMHG == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MaHangGiay nsx = data.MaHangGiays.SingleOrDefault(n => n.IdMHG == IdMHG);
            if (nsx == null)
            {
                return HttpNotFound();
            }
            return View(nsx);
        }
        [HttpPost]
        public ActionResult Xoa(int IdMHG)
        {
            if (IdMHG == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaHangGiay nsx = data.MaHangGiays.SingleOrDefault(n => n.IdMHG == IdMHG); if (nsx == null)
            {
                return HttpNotFound();
            }
            data.MaHangGiays.Remove(nsx);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}