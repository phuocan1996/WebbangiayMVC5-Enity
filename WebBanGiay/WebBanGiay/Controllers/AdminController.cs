using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class AdminController : Controller
    {
        ShoeShopEntities1 data = new ShoeShopEntities1();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XemChiTiet()
        {
            return View(data.KhachHangs.OrderBy(n => n.IdKH));
        }

        [HttpPost]
        public ActionResult LoginAdmin(FormCollection collection)
        {
            string TaiKhoan = collection["txtUser"].ToString();
            string MatKhau = collection["txtPass"].ToString();
            ThanhVien thanhvien = data.ThanhViens.SingleOrDefault(n => n.TaiKhoan == TaiKhoan && n.MatKhau == MatKhau);
            if (thanhvien != null)
            {
                Session["user"] = thanhvien;
                return RedirectToAction("XemChiTiet");

            }
            return Content("Tài khoản hoặc mật khẩu không đúng!");
        }
        public ActionResult Logout()
        {
            Session[WebBanGiay.Common.Session.USER_SESSION] = null;
            return RedirectToAction("Index","Admin");
        }
	}
}