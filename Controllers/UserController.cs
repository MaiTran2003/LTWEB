using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTWeb.Models;
using BCrypt;

namespace DoAn_LTWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null)
            {
                CompanyDBContext db = new CompanyDBContext();
                User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, myUser.Password) && myUser.Role == "admin")
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.UserName);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        return RedirectToAction("Index", "Home", new { area = "admin" });
                    }
                    else if (BCrypt.Net.BCrypt.Verify(user.Password, myUser.Password) && myUser.Role == "user")
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.UserName);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Invalid username or password !!!");
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("auth");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authCookie);

            HttpCookie roleCookie = new HttpCookie("role");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(roleCookie);

            return RedirectToAction("Index", "Products");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string retypePassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (user.Password != retypePassword)
            {
                ModelState.AddModelError("retypePassword", "Passwords do not match.");
                return View();
            }
            CompanyDBContext db = new CompanyDBContext();
            User myUser = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("UserName", "UserName already exist.");
                return View();
            }
            myUser = db.Users.Where(u => u.EmailAddress == user.EmailAddress).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("EmailAddress", "EmailAddress already exist.");
                return View();
            }
            myUser = new User();
            myUser.UserName = user.UserName;
            myUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            myUser.EmailAddress = user.EmailAddress;
            myUser.UserId = user.UserId;
            myUser.Role = "user";
            db.Users.Add(myUser);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}