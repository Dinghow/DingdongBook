using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.Text;

namespace DingdongBook.Controllers
{
    public class AdminController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Admin
        public ActionResult Index()
        {
            //Get login information
            ViewBag.admin_auth = (Convert.ToBoolean(Session["admin_auth"])) ? 1 : 0;
            if (ViewBag.admin_auth == 1)
                ViewBag.admin_name = Session["admin_account"].ToString();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginSender msg)
        {
            USERS temp = new USERS();
            if (msg == null)
            {
                ViewBag.Error = "Empty account";
            }
            else
            {
                temp = db.USERS.Where(u => u.ACCOUNT == msg.account).Where(u => u.AUTHORITY == "admin").FirstOrDefault();
                if (temp == null)
                    ViewBag.Error = "Account wrong";
                else
                {
                    //Decode the password from base64 and verify 
                    byte[] bytes = Convert.FromBase64String(temp.PASSWORD);
                    var decode_password = Encoding.UTF8.GetString(bytes);
                    if (msg.password == decode_password)
                    {
                        Session["admin_auth"] = true;
                        Session["admin_id"] = temp.ID;
                        Session["admin_account"] = msg.account;
                        return Redirect("~/Admin/Index");
                    }
                    else
                    {
                        ViewBag.Error = "Account or password wrong!";
                    }
                }
            }
            return Redirect("~/Admin/Login");
        }

        public ActionResult Logout()
        {
            Session["admin_auth"] = false;
            Session["admin_id"] = "";
            Session["admin_account"] = "";

            return Redirect("~/");
        }

        //A class to transfer message from View to Controller
        public class LoginSender
        {
            public string account { get; set; }
            public string password { get; set; }
        }

        //A class to transfer message from View to Controller
        public class DataSender
        {
            public string user_search;
        }
    }
}