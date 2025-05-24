using MEGATECH.App_Start;
using MEGATECH.Models;
using MEGATECH.Models.EF;
using MEGATECH.Models.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MEGATECH.Controllers
{
    public class KhachHangController : Controller
    {
        private MEGATECHDBContext db;

        public KhachHangController()
        {
             db = new MEGATECHDBContext();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]        
        
        public ActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Thông tin đăng nhập không hợp lệ !";
                return View();
            }

            if (string.IsNullOrEmpty(model.Email)
                || string.IsNullOrEmpty(model.MatKhau))
            {
                TempData["error"] = "Tên đăng nhập hoặc mật khẩu không được để trống!";
                return View();
            }
            var taiKhoan = db.KhachHangs.FirstOrDefault(x => x.Email == model.Email);
            
            if (taiKhoan == null)
            {
                TempData["error"] = "Tài khoản khách hàng này chưa được tạo!";
                return View();
            }
          
            if (taiKhoan.MatKhau != model.MatKhau)
            {
                TempData["error"] = "Mật khẩu đăng nhập không đúng!";
                return View();
            }
          
            // Tài khoản đăng nhập: lưu vào session server
            Session["khach"] = taiKhoan;

            // Lưu cookie
            CookieHelper.Create("email-khach", taiKhoan.Email, DateTime.Now.AddDays(10));
            CookieHelper.Create("matkhau-khach", taiKhoan.MatKhau, DateTime.Now.AddDays(10));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(KhachHang model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Thông tin đăng ký không hợp lệ !";
                return View();
            }

            //Tạo ngẫu nhiên mã hoá đơn
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            var maKhachHang = new string(Enumerable.Repeat(chars, 8)
                           .Select(s => s[random.Next(s.Length)]).ToArray());

            model.ID = maKhachHang;

            db.KhachHangs.Add(model);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex) {
                TempData["error"] = "Lưu thông tin khách hàng không thành công !";
                return View();
            }

            return RedirectToAction("Login", "KhachHang");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            //Xoá session
            Session.Remove("khach");

            //Xoá cookie
            CookieHelper.Remove("email-khach");
            CookieHelper.Remove("matkhau-khach");

            //Xoá session form
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "KhachHang");
        }
    }
}