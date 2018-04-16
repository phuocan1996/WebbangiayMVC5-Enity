using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Common;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class NguoiDungController : Controller
    {
        //
        // GET: /NguoiDung/
        ShoeShopEntities1 data = new ShoeShopEntities1();
       
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        public ActionResult DangKyPartialView()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection f, KhachHang kh)
        {
            var tenkh = f["TenKH"];
            var taikhoan = f["TaiKhoan"];
            var matkhau = f["MatKhau"];
            var matkhaunhaplai = f["MatKhauNhapLai"];
            var diachi = f["DiaChi"];
            var sodienthoai = f["SoDienThoai"];
            var email = f["Email"];

            if (String.IsNullOrEmpty(tenkh))
                ViewData["Loi1"] = "Vui lòng nhập họ tên";
            else if (String.IsNullOrEmpty(taikhoan))
                ViewData["Loi2"] = "Vui lòng nhập tên tài khoản";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi3"] = "Vui lòng nhập mật khẩu";
            else if (String.IsNullOrEmpty(matkhaunhaplai))
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu";
            else if (String.IsNullOrEmpty(email))
                ViewData["Loi5"] = "Vui lòng nhập địa chỉ";
            else if (String.IsNullOrEmpty(diachi))
                ViewData["Loi6"] = "Vui lòng nhập số điện thoại";
            else if (String.IsNullOrEmpty(sodienthoai))
                ViewData["Loi7"] = "Vui lòng nhập email";

            else
            {
                kh.TenKH = tenkh;
                kh.TaiKhoan = taikhoan;
                kh.MatKhau = matkhau;
                kh.DiaChi = diachi;
                kh.SoDienThoai = sodienthoai;
                kh.Email = email;
                data.KhachHangs.Add(kh);
                data.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            return this.Dangky();
        }

        

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhapPartialView()
        {
            return PartialView();
        }
        public ActionResult DangNhap(FormCollection f)
        {
            var taikhoan = f["TaiKhoan"];
            var matkhau = f["MatKhau"];

            if (String.IsNullOrEmpty(taikhoan))
                ViewData["Loi1"] = "Vui lòng nhập tên tài khoản";
            else
                if (String.IsNullOrEmpty(matkhau))
                    ViewData["Loi2"] = "Vui lòng nhập mật khẩu";

                else
                {
                    KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
                    if (kh != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công!";
                        var session = new UserLogin();
                        session.TaiKhoan = taikhoan;
                        Session.Add(Common.Session.USER_SESSION, session);
                         
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
                }

            return View();
        }
        public ActionResult DangXuat()
        {
            Session[WebBanGiay.Common.Session.USER_SESSION] = null;
            return Redirect("/");
        }
	}
}