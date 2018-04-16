using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiay.Models
{
    public class ItemGioHang
    {
        public int IdGiay { get; set; }
        public string TenGiay { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal TongTien { get; set; }
        public string HinhAnh { get; set; }
        public ItemGioHang(int IdGiay)
        {
            using (ShoeShopEntities1 data = new ShoeShopEntities1())
            {
                this.IdGiay = IdGiay;
                Giay giay = data.Giays.Single(n => n.IdGiay == IdGiay);
                this.TenGiay = giay.TenGiay;
                this.HinhAnh = giay.HinhAnh;
                this.DonGia = giay.DonGia.Value;
                this.SoLuong = 1;
                this.TongTien = DonGia * SoLuong;
            }
        }
        public ItemGioHang(int IdGiay, int SL)
        {
            using (ShoeShopEntities1 data = new ShoeShopEntities1())
            {
                this.IdGiay = IdGiay;
                Giay giay = data.Giays.Single(n => n.IdGiay == IdGiay);
                this.TenGiay = giay.TenGiay;
                this.HinhAnh = giay.HinhAnh;
                this.DonGia = giay.DonGia.Value;
                this.SoLuong = SL;
                this.TongTien = DonGia * SoLuong;
            }
        }        
    }
}