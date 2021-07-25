using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion.Models;
using PagedList;

namespace Fashion.Controllers
{
    public class ChucNangController : Controller
    {
        QLBanQuanAoDataContext context = new QLBanQuanAoDataContext();

        //------------------------------ Sản Phẩm ---------------------------------------
        public ActionResult DSsanpham(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            int pagesize = 10;
            int pageNum = (page ?? 1);
            var list = context.SANPHAMs.OrderByDescending(s => s.MaSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        public ActionResult loai(int id)
        {
            var list = context.Loaisanphams.Where(n => n.MaloaiSP == id);
            return View(list.Single());
        }
        public ActionResult NCC(int id)
        {
            var list = context.NCCs.Where(n => n.MaNCC == id);
            return View(list.Single());
        }


        [HttpGet]
        public ActionResult Themsanpham()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            ViewBag.MaNCC = new SelectList(context.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.Loai = new SelectList(context.Loaisanphams.ToList().OrderBy(n => n.MaloaiSP), "MaloaiSP", "Tensanpham");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themsanpham(SANPHAM sp, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            ViewBag.MaNCC = new SelectList(context.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.Loai = new SelectList(context.Loaisanphams.ToList().OrderBy(n => n.MaloaiSP), "MaloaiSP", "Tensanpham");
            var TenSp = collection["Ten"];
            var gia = collection["Gia"];
            var Mota = collection["textarea"];
            var Date = collection["Date"];
            var SoLuong = collection["SoLuong"];
            var loai = collection["Loai"];
            var ncc = collection["MaNCC"];

            var filename = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Sản Phẩm"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
                return View();
            }
            else
            {
                fileUpload.SaveAs(path);
            }

            sp.TenSP = TenSp;
            sp.Giaban = decimal.Parse(gia);
            sp.Mota = Mota;
            sp.Ngaycapnhat = DateTime.Parse(Date);
            sp.Soluongton = Int32.Parse(SoLuong);
            sp.MaloaiSP = Int32.Parse(loai);
            sp.MaNCC = Int32.Parse(ncc);
            sp.Anhbia = filename;
            context.SANPHAMs.InsertOnSubmit(sp);
            context.SubmitChanges();
            return RedirectToAction("DSsanpham", "ChucNang");
        }

        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }
        [HttpPost, ActionName("Xoasanpham")]
        public ActionResult XacNhanXoasanpham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.SANPHAMs.DeleteOnSubmit(sp);
                context.SubmitChanges();
                return RedirectToAction("DSsanpham");
            }
        }


        [HttpGet]
        public ActionResult Suasanpham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaNCC = new SelectList(context.NCCs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaloaiSP = new SelectList(context.Loaisanphams.ToList().OrderBy(n => n.MaloaiSP), "MaloaiSP", "Tensanpham", sp.MaloaiSP);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Suasanpham")]
        public ActionResult XacNhanSuasanpham(FormCollection collection, int id, HttpPostedFileBase fileUpload)
        {
            var img = "";
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            if (fileUpload != null)
            {
                img = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Sản Phẩm"), img);
                if (!System.IO.File.Exists(path))//Sản Phẩm Chưa Tồn Tại
                {
                    fileUpload.SaveAs(path);
                }
            }
            else
            {
                img = collection["Anh"];
            }
            SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            sp.Anhbia = img;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            UpdateModel(sp);
            context.SubmitChanges();
            return RedirectToAction("DSsanpham");

        }


        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }


            SANPHAM ncc = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        //----------------------------------- Loại Sản Phẩm ------------------------------------
        public ActionResult DSLSP(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            int pagesize = 10;
            int pageNum = (page ?? 1);
            var list = context.Loaisanphams.OrderBy(s => s.MaloaiSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        [HttpGet]
        public ActionResult ThemLSP()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemLSP(Loaisanpham lsp)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                context.Loaisanphams.InsertOnSubmit(lsp);
                context.SubmitChanges();
                return RedirectToAction("DSLSP", "ChucNang");
            }
        }

        public ActionResult SuaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                Loaisanpham lsp = context.Loaisanphams.SingleOrDefault(n => n.MaloaiSP == id);
                if (lsp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(lsp);
            }
        }

        [HttpPost, ActionName("SuaLSP")]
        public ActionResult XacNhanSuaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                Loaisanpham ncc = context.Loaisanphams.SingleOrDefault(n => n.MaloaiSP == id);
                ViewBag.MaNCC = ncc.MaloaiSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSLSP");
            }
        }

        [HttpGet]
        public ActionResult XoaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                Loaisanpham ncc = context.Loaisanphams.SingleOrDefault(n => n.MaloaiSP == id);
                ViewBag.MaloaiSP = ncc.MaloaiSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }
        [HttpPost, ActionName("XoaLSP")]
        public ActionResult XacNhanXoaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                Loaisanpham ncc = context.Loaisanphams.SingleOrDefault(n => n.MaloaiSP == id);
                ViewBag.MaloaiSP = ncc.MaloaiSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.Loaisanphams.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC");
            }
        }



        //-----------------------------------Nhà Cung Cấp---------------------------------------
        public ActionResult DSNCC(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                int pagesize = 10;
                int pageNum = (page ?? 1);
                var list = context.NCCs.OrderByDescending(s => s.MaNCC).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        [HttpGet]
        public ActionResult ThemNCC()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemNCC(NCC ncc)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                context.NCCs.InsertOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC", "ChucNang");
            }
        }

        [HttpGet]
        public ActionResult XoaNCC(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NCC ncc = context.NCCs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }
        [HttpPost, ActionName("XoaNCC")]
        public ActionResult XacNhanXoa(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NCC ncc = context.NCCs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.NCCs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC");
            }
        }

        [HttpGet]
        public ActionResult SuaNCC(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NCC ncc = context.NCCs.SingleOrDefault(n => n.MaNCC == id);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }

        [HttpPost, ActionName("SuaNCC")]
        public ActionResult XacNhanSua(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NCC ncc = context.NCCs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC");
            }
        }

        //------------------------------ Góp Ý ------------------------------------
        public ActionResult DSGOPY(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                int pagesize = 10;
                int pageNum = (page ?? 1);
                var list = context.HoTros.OrderByDescending(s => s.MaKH).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }

        public ActionResult GOPY_CHITIET(int id)
        {
            HoTro ht = context.HoTros.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = ht.MaKH;
            if (ht == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ht);
        }

        //--------------------------- Khách Hàng ----------------------------------
        public ActionResult DSKH(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                int pagesize = 10;
                int pageNum = (page ?? 1);
                var list = context.KHACHHANGs.OrderByDescending(s => s.MaKH).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }

        [HttpGet]
        public ActionResult DonDatHang(int? page)
        {
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var GioHienTai = DateTime.Today;
            var list = context.DONDATHANGs.Where(s => s.Ngaydat >= GioHienTai).OrderByDescending(i => i.Ngaydat).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        [HttpPost]
        public ActionResult DonDatHang(string date, string date2, int? page)
        {
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var Date = DateTime.Parse(date);

            if (date2 == "")
            {
                var listdate = context.DONDATHANGs.Where(s => s.Ngaydat >= Date).OrderByDescending(i => i.Ngaydat).ToList();
                return View(listdate.ToPagedList(pageNum, pagesize));
            }
            var Date2 = DateTime.Parse(date2);
            var list = context.DONDATHANGs.Where(s => s.Ngaydat >= Date && s.Ngaydat <= Date2).OrderByDescending(i => i.Ngaydat).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        public ActionResult ChiTietDonDatHang(int? id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            if (id == null)
            {
                return HttpNotFound();
            }
            var list = context.CHITIETDONTHANGs.Where(s => s.MaDonHang == id).OrderByDescending(s => s.MaSP).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult SuaDonDatHang(int id)
        {
            //if (Session["TKAdmin"] == null)
            //{
            //    return RedirectToAction("Index", "Fashion");
            //}

            DONDATHANG ncc = context.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        [HttpPost, ActionName("SuaDonDatHang")]
        public ActionResult XacNhanSuaDonDatHang(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                DONDATHANG ncc = context.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DonDatHang");
            }
        }
    }
}