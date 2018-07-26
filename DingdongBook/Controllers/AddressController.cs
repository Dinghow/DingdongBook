using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;

namespace DingdongBook.Controllers
{
    public class ADDRESSController : Controller
    {

        private Entities1 db = new Entities1();

        public ActionResult Index(int ID)
        {

            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_name = Session["user_account"].ToString();
                ViewBag.user_id = Session["user_id"].ToString();
                if (ID.ToString() != ViewBag.user_id)
                    return Redirect("~/Home/Login");
            }
            else
                return Redirect("~/Home/Login");

            var address = from d in db.ADDRESS
                         orderby d.ID
                         where d.USER_ID == ID
                         select d;

            var allUseId = from d in db.ADDRESS
                           orderby d.ID
                           select d.ID;

            ViewBag.user_id = ID;//用户id
            ViewBag.username = address.First().NAME;//用户名字
            ViewBag.mm = address;

            ViewBag.max = allUseId.Max() + 1;//下个订单号为最大订单号+1

            return View();
        }

        //Add address 
        [HttpPost]
        public ActionResult AddAddress(ADDRESS aDDRESS)
        {
            string user_id = Session["user_id"].ToString();
            db.ADDRESS.Add(aDDRESS);
            db.SaveChanges();

            return Redirect("~/Address/index?id="+ user_id);
        }

        //Delete address
        public void DeleteAddress(int[] addrIds)
        {
            foreach(int i in addrIds)
            {
                ADDRESS temp = db.ADDRESS.Find(i);
                db.ADDRESS.Remove(temp);
            }
            db.SaveChanges();
        }
    }
}
