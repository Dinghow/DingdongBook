using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.Collections;

namespace DingdongBook.Controllers
{
    public class CARTsController : Controller
    {
        private Entities1 db = new Entities1();
        static int row;

        // GET: CARTs
        public ActionResult Index(int ID)
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
                ViewBag.user_name = Session["user_account"].ToString();
            ViewBag.user_id = ID;

            int num = db.Database.SqlQuery<int>("select count(*) from CARTLIST where USER_ID=" + ID.ToString()).FirstOrDefault();//购物车中的项数
            ViewBag.num = num;
            row = num;
            if (num == 0)
            {
                return View();
            }
            var list = db.Database.SqlQuery<CARTLIST>("select * from CARTLIST where USER_ID=" + ID.ToString()).ToList();//购物车清单
            ViewBag.CartList = list;
            double total_price = 0;
            for(int i = 0; i < num; i++)
            {
                total_price = total_price + (double)list[i].TOTAL_PRICE;
            }
            ViewBag.price = total_price;
            return View();
        }

    }
}
