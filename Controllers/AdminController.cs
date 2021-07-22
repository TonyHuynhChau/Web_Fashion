using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion.Models;
using PagedList;
using PagedList.Mvc;

namespace Fashion.Controllers
{
    public class AdminController : Controller
    {
        QLBanQuanAoDataContext context = new QLBanQuanAoDataContext();
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index","Fashion");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            string user = collection["form-username"];
            string pass = collection["form-password"];

            Admin ad = context.Admins.SingleOrDefault(a => a.UserAdmin == user && a.PassAdmin == pass);
            if (ad == null)
            {
                ViewBag.ThongBaoAdmin = "Tài Khoản Hoặc Mật Khẩu Sai";
                return this.Login();
            }
            Session["TKAdmin"] = ad;
            return RedirectToAction("Index", "Admin");
        }     
    }
}