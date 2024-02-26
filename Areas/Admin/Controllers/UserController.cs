using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTWeb.Models;

namespace DoAn_LTWeb.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        CompanyDBContext db = new CompanyDBContext();
        public ActionResult Index()
        {
            List<User> users = db.Users.ToList();
            return View(users);
        }
        public ActionResult Create()
        {
            ViewBag.User = db.Users.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user, string retypePassword)
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

            myUser.Role = "user";
            db.Users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult CreateAdmin()
        {
            ViewBag.User = db.Users.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateAdmin(User user, string retypePassword)
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

            myUser.Role = "admin";
            db.Users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            User users = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(users);
        }
        [HttpPost]
        public ActionResult Delete(int id, User u)
        {
            User users = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            User users = db.Users.Where(row => row.UserId == id).FirstOrDefault();
            return View(users);
        }
        [HttpPost]
        public ActionResult Update(User u)
        {

            User users = db.Users.Where(row => row.UserId == u.UserId).FirstOrDefault();
            // Update
            users.UserName = u.UserName;
            users.Role = u.Role;
            users.EmailAddress = u.EmailAddress;
            db.SaveChanges();
            return RedirectToAction("Update");
        }
    }
}