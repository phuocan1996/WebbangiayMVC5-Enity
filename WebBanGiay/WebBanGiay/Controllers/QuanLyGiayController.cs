using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using WebBanGiay.Models;
using System.IO;

namespace WebBanGiay.Controllers
{
    public class QuanLyGiayController : Controller
    {
        //
        // GET: /QuanLyGiay/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        public ActionResult Index()
        {
            return View(data.Giays.Where(n => n.DaXoa == false).OrderByDescending(n => n.IdGiay));
        }
        [HttpGet]
        public ActionResult TaoMoiGiay()
        {
            //load dropdownlist nhà cung cấp, loại giày, hãng giày
            ViewBag.IdMHG = new SelectList(data.MaHangGiays.OrderBy(n => n.TenHG), "IdMHG", "TenHG");
            ViewBag.IdLoaiGiay = new SelectList(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay), "IdLoaiGiay", "TenLoai");

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoiGiay(Giay giay, HttpPostedFileBase[] fileupload)
        {
            //load dropdownlist nhà cung cấp, loại giày, hãng giày
            ViewBag.IdMHG = new SelectList(data.MaHangGiays.OrderBy(n => n.TenHG), "IdMHG", "TenHG");
            ViewBag.IdLoaiGiay = new SelectList(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay), "IdLoaiGiay", "TenLoai");
            //Nếu thư mục chứa hình ảnh đó rồi thì xuất ra thông báo
            data.Giays.Add(giay);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSuaGiay(int? IdGiay)
        {
            // lấy sản phẩm cần chỉnh dựa vào id
            if (IdGiay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                return HttpNotFound();
            }

            //load dropdownlist nhà cung cấp, loại giày, hãng giày
            ViewBag.IdMHG = new SelectList(data.MaHangGiays.OrderBy(n => n.TenHG), "IdMHG", "TenHG", giay.IdGiay);
            ViewBag.IdLoaiGiay = new SelectList(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay), "IdLoaiGiay", "TenLoai", giay.IdLoaiGiay);

            return View(giay);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSuaGiay(Giay model, HttpPostedFileBase fileupload)
        {

            ViewBag.IdMHG = new SelectList(data.MaHangGiays.OrderBy(n => n.TenHG), "IdMHG", "TenHG", model.IdGiay);
            ViewBag.IdLoaiGiay = new SelectList(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay), "IdLoaiGiay", "TenLoai", model.IdLoaiGiay);
            // nếu dử liệu đầu vào ok
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/CSSHome/HinhGiay"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    model.HinhAnh = fileName;
                    var edg = data.Giays.First(a => a.IdLoaiGiay == model.IdLoaiGiay);
                    edg.TenGiay = model.TenGiay;
                    edg.DonGia = model.DonGia;
                    edg.NgayCapNhat = model.NgayCapNhat;
                    edg.Mota = model.Mota;
                    edg.SoLuongTon = model.SoLuongTon;
                    edg.LuotXem = model.LuotXem;
                    edg.Moi = model.Moi;
                    edg.IdMHG = model.IdMHG;
                    edg.IdLoaiGiay = model.IdLoaiGiay;
                    edg.DaXoa = model.DaXoa;
                    edg.HinhAnh = model.HinhAnh;
                    edg.HinhAnh1 = model.HinhAnh1;
                    edg.HinhAnh2 = model.HinhAnh2;
                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult XoaGiay(int? IdGiay)
        {
            // lấy sản phẩm cần chỉnh dựa vào id
            if (IdGiay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                return HttpNotFound();
            }

            //load dropdownlist nhà cung cấp, loại giày, hãng giày
            ViewBag.IdMHG = new SelectList(data.MaHangGiays.OrderBy(n => n.TenHG), "IdMHG", "TenHG", giay.IdGiay);
            ViewBag.IdLoaiGiay = new SelectList(data.LoaiGiays.OrderBy(n => n.IdLoaiGiay), "IdLoaiGiay", "TenLoai", giay.IdLoaiGiay);

            return View(giay);
        }
        [HttpPost]
        public ActionResult XoaGiay(int IdGiay)
        {
            if (IdGiay == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = data.Giays.SingleOrDefault(n => n.IdGiay == IdGiay);
            if (giay == null)
            {
                return HttpNotFound();
            }
            data.Giays.Remove(giay);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}