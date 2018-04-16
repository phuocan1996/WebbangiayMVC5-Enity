using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models
{
    public class DangKyModel
    {
        [Display(Name="Họ và tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string TenKH { set; get; }
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage="Yêu cầu nhập tên đăng nhập")]
        public string TaiKhoan { set; get; }
        [Display(Name = "Mật khẩu")]
        [StringLength(50,MinimumLength=6, ErrorMessage="Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string MatKhau { set; get; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("MatKhau",ErrorMessage="Nhập lại mật khẩu không đúng.")]
        public string MatKhauNhapLai { set; get; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { set; get; }
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        public string Email { set; get; }

    }
}