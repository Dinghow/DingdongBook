using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.Text;

namespace DingdongBook.Controllers
{
    public class IndividualController : Controller
    {
        private Entities1 db = new Entities1();
        // GET: Individual
        public ActionResult Index()
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
                ViewBag.user_name = Session["user_account"].ToString();

            //Get present individual information
            USERS present_user = db.USERS.Find(Convert.ToInt16(Session["user_id"]));
            ViewBag.name = present_user.NAME;
            ViewBag.email = present_user.EMAIL;
            ViewBag.gender = present_user.GENDER;

            return View();
        }

        [HttpPost]
        public ActionResult EditInfo(IndividualSender msg)
        {
            //Edit personal information and submit changes
            USERS present_user = db.USERS.Find(Convert.ToInt16(Session["user_id"]));

            if(msg.name != "")
                present_user.NAME = msg.name;
            if (msg.gender == "男")
                present_user.GENDER = "male";
            else if (msg.gender == "女")
                present_user.GENDER = "female";
            if(msg.name != "")
                present_user.EMAIL = msg.email;
            db.SaveChanges();

            return Redirect("~/Individual");
        }

        [HttpPost]
        public ActionResult EditPassword(IndividualSender msg)
        {
            //Edit password with encryption and submit changes
            USERS present_user = db.USERS.Find(Convert.ToInt16(Session["user_id"]));
            byte[] bytes = Convert.FromBase64String(present_user.PASSWORD);
            var decode_password = Encoding.UTF8.GetString(bytes);
            
            if(decode_password == msg.ex_password)
            {
                byte[] new_bytes = Encoding.UTF8.GetBytes(msg.new_password);
                present_user.PASSWORD = Convert.ToBase64String(new_bytes);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Wrong password!";
            }

            return Redirect("~/Individual");
        }

        //A class to transfer message from View to Controller
        public class IndividualSender
        {
            public string name { set; get; }
            public string gender { set; get; }
            public string email { set; get; }
            public string ex_password { set; get; }
            public string new_password { set; get; }
        }
    }
}