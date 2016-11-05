using MVC5Application1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Application1.Controllers
{
    public class HomeController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        vw_CustomerRepository repo = RepositoryHelper.Getvw_CustomerRepository();
        
        public ActionResult Index(string search)
        {
            var customer = repo.All().Where(c => c.是否已刪除 != true);

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
                var customer = 客戶資料repo.All().Where(客 => 客.帳號 == login.Email).ToList();

                if (customer.Count == 1)
                {
                    if (customer.First().密碼 == Hash.Encode(login.Password))
                    {
                        FormsAuthentication.RedirectFromLoginPage(login.Email, false);
                        //return Redirect(ReturnUrl ?? "/");
                        return Redirect("/客戶資料/Edit2");
                    }
                }
            }
            return View();
        }
    }
}