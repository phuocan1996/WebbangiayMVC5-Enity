using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;

namespace WebBanGiay.Controllers
{
    public class HomeController : Controller
    {
        ShoeShopEntities1 data = new ShoeShopEntities1();
        private List<Giay> Laygiaymoi (int count)
        {
            //Lấy sắp xếp theo ngày cập nhật
            return data.Giays.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            //Tạo biến qui định số sản phẩm trên mỗi trang
            int pageSize = 8;
            //Tạo biến số trang
            int pageNum = (page ?? 1);

            //Lấy top 5 sách bán chạy nhất
            var giaymoi = Laygiaymoi(28);
            return View(giaymoi.ToPagedList(pageNum, pageSize));
             
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        
        public ActionResult MenuPartial()
        {
            // truy vấn list giày
            var lstGiay = data.Giays;
            return PartialView(lstGiay);
        }
        public ActionResult LienHe()
        {
            ViewBag.Success = false;
            return View(new LienHe());
        }
        [HttpPost]
        public ActionResult LienHe(LienHe contact)
        {
            ViewBag.Success = false;
            if (ModelState.IsValid)
            {
                // Collect additional data
                contact.SentDate = DateTime.Now;
                contact.IP = Request.UserHostAddress;


                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = true;
                MailMessage m = new MailMessage(
                    "phuocanblog@gmail.com", // From
                    "phuocan1996@gmail.com", // To
                    "Someone is contacting you through your website!", // Subject
                    contact.BuildMessage()); // Body
                ViewBag.Success = true;
                smtpClient.Send(m);
            }

            return View();        }
        
	}
}