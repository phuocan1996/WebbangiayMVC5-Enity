using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class ThongKeController : Controller
    {
        //
        // GET: /ThongKe/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        public ActionResult Index()
        {
            //ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();// lấy số người truy cập từ application
            //ViewBag.SoNguoiOnline = HttpContext.Application["SoNguoiOnline"].ToString();
            ViewBag.ThongKeDoanhThu = ThongKeDoanhThu();// thống kê doanh thu
            ViewBag.ThongKeDonHang = ThongKeDonHang(); // thống kê đơn hàng
            ViewBag.ThongKeThanhVien = ThongKeThanhVien(); // thống kê thành viên
            ViewBag.ThongKeGiay = ThongKeGiay(); // thống kê số lượng sản phẩm tồn
            ViewBag.ThongKeMaHangGiay = ThongKeMaHangGiay();// thống kê nhà sản xuất

            ViewBag.ThongKeLoaiGiay = ThongKeLoaiGiay();
            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = data.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value;
            return TongDoanhThu; // thống kê tổng doanh thu
        }
        public double ThongKeDonHang()
        {
            // đếm đơn đặt hàng
            double slddh = data.DonDatHangs.Count();
            return slddh;

            //int ddh = 0;
            //var lstDDH = db.DonDatHangs;
            //if (lstDDH.Count() > 0)
            //{
            //    ddh = lstDDH.Count();
            //}
            //return ddh;
        }
        public double ThongKeThanhVien()
        {
            // đếm đơn đặt hàng
            double sltv = data.ThanhViens.Count();
            return sltv;
        }

        public double ThongKeMaHangGiay()
        {
            // đếm đơn đặt hàng
            double slncc = data.MaHangGiays.Count();
            return slncc;
        }
        public double ThongKeLoaiGiay()
        {
            // đếm đơn đặt hàng
            double slncc = data.LoaiGiays.Count();
            return slncc;
        }
        public int ThongKeGiay()
        {
            // đếm số lượng sản phẩm
            int sanpham = data.Giays.Sum(n => n.SoLuongTon).Value;
            return sanpham;
        }


        public decimal ThongKeDoanhThuTheoThang(int Thang, int Nam)
        {
            //thống kê tất cả doanh thu
            // list ra don dat hang có ngày tháng năm tương ứng
            var lstDDH = data.DonDatHangs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal TongTien = 0;
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }
            return TongTien;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (data != null)
                    data.Dispose();
                data.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}