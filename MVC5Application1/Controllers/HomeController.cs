using MVC5Application1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Application1.Controllers
{
    public class HomeController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index(string search)
        {
            var customer = db.vw_Customer.Where(c => c.是否已刪除 != true);

            if (!string.IsNullOrEmpty(search))
            {
                customer = customer.Where(c => c.客戶名稱.Contains(search));
            }

            return View(customer.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}