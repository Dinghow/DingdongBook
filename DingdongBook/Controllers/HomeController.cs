using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using DingdongBook.Models;

namespace DingdongBook.Controllers
{
    public  partial class HomeController : Controller
    {
        private Entities1 db = new Entities1();

        public ActionResult Index()
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_name = Session["user_account"].ToString();
                ViewBag.user_id = Convert.ToInt16(Session["user_id"]);
            }

            //Load the newest books and best books
            GetNewestBooks();
            GetBestBooks();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Sender msg)
        {
            USERS temp = new USERS();
            if (msg == null)
            {
                ViewBag.Error = "Empty account";
            }
            else
            { 
               temp = db.USERS.Where(u => u.ACCOUNT == msg.account).Where(u=>u.AUTHORITY == "user").FirstOrDefault();
                if (temp == null)
                    ViewBag.Error = "Account wrong";
                else
                {
                    //Decode the password from base64 and verify 
                    byte[] bytes = Convert.FromBase64String(temp.PASSWORD);
                    var decode_password = Encoding.UTF8.GetString(bytes);
                    if (msg.password == decode_password)
                    {
                        Session["user_auth"] = true;
                        Session["user_id"] = temp.ID;
                        Session["user_account"] = msg.account;
                        return Redirect("~/");
                    }
                    else
                    {
                        ViewBag.Error = "Account or password wrong!";
                    }
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user_auth"] = false;
            Session["user_id"] = "";
            Session["user_account"] = "";

            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult Register(Sender msg)
        {
            //Check whether the account is already existed
            var temp = db.USERS.Where(u => u.ACCOUNT == msg.account).Where(u => u.AUTHORITY == "user").FirstOrDefault();
            if(temp != null)
            {
                ViewBag.Error = "Account already exists";
            }
            else
            {
                USERS new_user = new USERS();
                new_user.ACCOUNT = msg.account;
                if (msg.password == msg.re_password)
                {
                    //Encrypt the password with base64
                    byte[] bytes = Encoding.UTF8.GetBytes(msg.password);
                    new_user.ID = db.USERS.Count();
                    new_user.PASSWORD = Convert.ToBase64String(bytes);
                    new_user.EMAIL = msg.email;
                    new_user.AUTHORITY = "user";
                    db.USERS.Add(new_user);
                    db.SaveChanges();
                }
                else
                    return View();

                return Redirect("~/Home/Login");
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //A class to transfer message from View to Controller
        public class Sender
        {
            public string account { get; set; }
            public string password { get; set; }
            public string re_password { get; set; }
            public string email { get; set; }
        }
    }
}