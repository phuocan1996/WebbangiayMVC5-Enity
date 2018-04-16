using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Controllers
{
    public class TinTucController : Controller
    {
        //
        // GET: /TinTuc/
        ShoeShopEntities1 data = new ShoeShopEntities1();
        public ActionResult Index()
        {
            return View("TinTuc", data.TinTucs.OrderBy(n => n.IdTinTuc));
        }
        public ActionResult TinTuc(int? id)
        {
            return View(data.TinTucs.Where(n => n.IdTinTuc == id));
        }
        public ActionResult NoiDung(int? id)
        {
            return View(data.TinTucs.SingleOrDefault(n => n.IdTinTuc == id));
        }
    }
}