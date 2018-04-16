using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;
using System.Data.Linq;
namespace WebBanGiay.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
       ShoeShopEntities1 data = new ShoeShopEntities1();            
        
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public List<ItemGioHang> LayGioHang()
        {
            // giỏ hàng đả tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                // nếu session giỏ hàng đã tồn tại
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;

            }
            return lstGioHang;
        }
        // thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult ThemGioHang(int IdGiay, string strUrl)
        {
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng 
            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đả tồn tại một sản phẩm trên giỏ hàng
            ItemGioHang giaycheck = lstGioHang.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giaycheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                if (giay.SoLuongTon < giaycheck.SoLuong)
                {
                    return View("ThongBao");
                }
                giaycheck.SoLuong++;
                giaycheck.TongTien = giaycheck.SoLuong * giaycheck.DonGia;
                return Redirect(strUrl);
            }
            ItemGioHang itemGH = new ItemGioHang(IdGiay);
            if (giay.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            data.SaveChanges();
            return Redirect(strUrl);
        }
        // xây dựng phương thức tính số lương
        public double TinhTongSoLuong()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        // phương thức tính tiền
        public decimal TongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.TongTien);
        }
        // xây dựng giỏ hàng partial
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang(int IdGiay)
        {
            // kiểm tra sản phẩm có tồn tại ko
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa
            ItemGioHang giaycheck = lstGioHang.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giaycheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Lấy list giỏ hàng trên giao diện
            ViewBag.GioHang = lstGioHang;
            // nếu sản phẩm đả tồn tại
            return View(giaycheck);
        }
        // cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            // kiểm tra số lượng tồn
            Giay giaycheck = data.Giays.SingleOrDefault(n => n.IdGiay == itemGH.IdGiay);
            if (giaycheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            // cập nhật số lượng trong session giỏ hàng
            List<ItemGioHang> lstGH = LayGioHang();
            // lấy sản phẩm cập nhật từ  trong list<GioHang>
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.IdGiay == itemGH.IdGiay);
            // cập nhật lại số lượng và tổng tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.TongTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XoaGioHang(int IdGiay)
        {
            // kiểm tra sản phẩm có tồn tại ko
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            // kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa
            ItemGioHang giaycheck = lstGioHang.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giaycheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // xóa sản phẩm 
            lstGioHang.Remove(giaycheck);
            return RedirectToAction("XemGioHang");
        }
        // xậy dựng action đặt hàng
        public ActionResult DatHang(KhachHang khachhang)
        {
            ////kiểm tra đăng nhập
            //if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            //{
            //    return RedirectToAction("DangNhap","NguoiDung");
            //}
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang KH = new KhachHang();
            if (Session["user"] == null)
            {
                // thêm khách hàng đối với khách hàng vãng lai(chưa có tài khoản)
                KH = khachhang;
                data.KhachHangs.Add(KH);
                data.SaveChanges();
            }
         
            // thêm đơn hàng
            DonDatHang ddh = new DonDatHang();
            //ddh.IdKH =  int.Parse(KH.IdKH.ToString());
            ddh.IdKH = KH.IdKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            data.DonDatHangs.Add(ddh);
            data.SaveChanges();
            // thêm chi tiết đơn đặt hàng
            List<ItemGioHang> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                ChiTietDonDatHang ctdh = new ChiTietDonDatHang();
                ctdh.IdDDH = ddh.IdDDH;
                ctdh.IdGiay = item.IdGiay;
                ctdh.TenGiay = item.TenGiay;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                data.ChiTietDonDatHangs.Add(ctdh);
            }
        
            Session["GioHang"] = null;
            data.SaveChanges();
            return View("ThongBao1");
        }
        // thêm giỏ hàng ajax
        public ActionResult ThemGioHangAjax(int IdGiay, string strUrl)
        {
            // kiểm tra sản phẩm có tồn tại trong csdl ko
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                // trang đường dẩn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng 

            List<ItemGioHang> lstGioHang = LayGioHang();
            // trường hợp đả tồn tại một sản phẩm trên giỏ hàng
            ItemGioHang giaycheck = lstGioHang.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giaycheck != null)
            {
                // kiểm tra số lượng sản phẩm tồn
                if (giay.SoLuongTon < giaycheck.SoLuong)
                {
                    return Content("<script>alert(\"Sản phẩm đã hết hàng !\")</script>");
                }
                giaycheck.SoLuong++;
                giaycheck.TongTien = giaycheck.SoLuong * giaycheck.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TongTien();
                return PartialView("GioHangPartial");
            }

            ItemGioHang itemGH = new ItemGioHang(IdGiay);
            if (giay.SoLuongTon < itemGH.SoLuong)
            {
                return Content("<script>alert(\"Sản phẩm đã hết hàng !\")</script>");
            }
            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView("GioHangPartial");
        }
        
        //Thông báo cho sản phẩm hết hàng
        public ActionResult ThongBao()
        {
            return View();
        }

        //Thông báo cho đơn đặt hàng thành công
        public ActionResult ThongBao1()
        {
            return View();
        }
	}
}
