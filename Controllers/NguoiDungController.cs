using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Fashion.Models;
using NPOI.POIFS.Crypt;

namespace Fashion.Controllers
{
    public class NguoiDungController : Controller
    {
        QLBanQuanAoDataContext data = new QLBanQuanAoDataContext();
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection, KHACHHANG kh)
        {
            //
            var hoten = collection["HOTEN"];
            var TK = collection["TaiKhoan"];
            var MK = collection["Pass"];
            var ConfirmMK = collection["ConfirmPass"];

            string Email = collection["Email"];
            string Address = collection["Address"];
            string SDT = collection["SDT"];
            string Date = String.Format("{0:MM/dd/yyyy}", collection["Date"]);
            if (MK.Equals(ConfirmMK))
            {
                if (hoten == null || TK == null || MK == null || Email == null || hoten == null || hoten == null || hoten == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    MD5 md5 = new MD5CryptoServiceProvider();

                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(ConfirmMK));

                    byte[] bytedata = md5.Hash;

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytedata.Length; i++)
                    {

                        builder.Append(bytedata[i].ToString("x2"));
                    }

                    string MaHoa = builder.ToString();

                    kh.HoTen = hoten;
                    kh.Taikhoan = TK;
                    kh.Matkhau = MaHoa;
                    kh.Email = Email;
                    kh.DiachiKH = Address;
                    kh.DienthoaiKH = SDT;
                    kh.Ngaysinh = DateTime.Parse(Date);
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("Login", "NguoiDung");
                }
            }
            else
            {
                @ViewData["error"] = "Mật Khẩu Không Trùng Khớp";
                return this.SignUp();
            }

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            string TK = collection["TaiKhoan"];
            string MK = collection["Password"];

            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(MK));

            byte[] bytedata = md5.Hash;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytedata.Length; i++)
            {

                builder.Append(bytedata[i].ToString("x2"));
            }

            string MaHoa = builder.ToString();

            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(a => a.Taikhoan == TK && a.Matkhau == MaHoa);
            if (kh != null)
            {
                Session["User"] = kh.HoTen;
                Session["Taikhoan"] = kh;
                return RedirectToAction("Index","Fashion");
            }
            else
            {
                ViewBag.Thongbao = "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng";
            }

            return View();
        }
        
        public ActionResult LogOut()
        {
            Session["User"] = null;
            Session["Taikhoan"] = null;
            return RedirectToAction("Index","Fashion");
        }


    
    }
}