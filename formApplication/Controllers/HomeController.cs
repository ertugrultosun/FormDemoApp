using formApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace formApplication.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterAction(User user)
        {
            var newuser = new User
            {
                email = user.email,
                secret = user.secret,
                first_name = user.first_name,
                last_name = user.last_name,
                registered_at = DateTime.Now,
            };
            db.Users.Add(newuser);
            db.SaveChanges();
            LoginAction(newuser);
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginAction(User user)
        {
            try
            {
                var buffer = db.Users.FirstOrDefault(m => m.email == user.email);
                if (user.secret == buffer.secret)
                {
                    Session.Add("CurrentUser", user);
                    Session["UserId"] = user.id;
                    Session["UserMail"] = user.email;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public ActionResult Logout()
        {
            Session.Remove("CurrentUser");
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            try
            {
                string name = Session["UserMail"].ToString();
                var user = db.Users.FirstOrDefault(m => m.email == name);
                var buffer = db.Forms.Where(m => m.users_id == user.id).ToList();
                ViewBag.Model = buffer;
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult AddForm(string json)
        {
            var success = false;
            if (json == null)
                success = false;
            else
            {
                string name = Session["UserMail"].ToString();
                var buffer = db.Users.FirstOrDefault(m => m.email == name);
                var newform = new Form
                {
                    users_id = buffer.id,
                    context = json,
                    created_at = DateTime.Now,
                };
                db.Forms.Add(newform);
                
                db.SaveChanges();
                success = true;
            }
                
            return Json(new { success });
        }

        public ActionResult FormList()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FormDetail(int id)
        {
            try
            {
                var buffer = db.Forms.FirstOrDefault(m => m.id == id);
                ViewBag.Context = buffer.context;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}