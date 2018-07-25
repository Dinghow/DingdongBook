using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.ComponentModel;

namespace DingdongBook.Controllers
{
    
    public class BOOKsController : Controller
    {   
        [DisplayName("购物车")]
        public class cartSender
        {
            public int amount { get; set; }
            public int book_id { get; set; }
        }
        private Entities1 db = new Entities1();
        static public int id;
        // GET: BOOKs
     

        public ActionResult Index(int ID)
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
                ViewBag.user_name = Session["user_account"].ToString();

            id = ID;
            ViewBag.id = ID;
            if (ID == null)
            {
                return HttpNotFound();
            }
            ViewBag.info = db.BOOK.SqlQuery("select * from BOOK where ID=" + ID.ToString()).ToList();//书籍信息
            ViewBag.writer = db.Database.SqlQuery<string>("select AUTHOR_NAME from AUTHOR,WRITE where AUTHOR.ID=WRITE.AUTHOR_ID and BOOK_ID=" + ID.ToString()).FirstOrDefault();//书籍的作者
            ViewBag.num = db.Database.SqlQuery<int>("select count(*) from COMMENTS where BOOK_ID=" + ID.ToString()).FirstOrDefault();//评论人次
            if (ViewBag.num == 0)//无评论
            {
                ViewBag.rate1 = 0;ViewBag.rate2 = 0;ViewBag.rate3 = 0;ViewBag.rate4 = 0;ViewBag.rate5 = 0;
                ViewBag.total = "N/A";
                return View();
            }
            int num1 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=1 and BOOK_ID=" + ID.ToString()).FirstOrDefault();//1星评论人次
            int num2 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=2 and BOOK_ID=" + ID.ToString()).FirstOrDefault();//2星评论人次
            int num3 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=3 and BOOK_ID=" + ID.ToString()).FirstOrDefault();//3星评论人次
            int num4 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=4 and BOOK_ID=" + ID.ToString()).FirstOrDefault();//4星评论人次
            int num5 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=5 and BOOK_ID=" + ID.ToString()).FirstOrDefault();//5星评论人次
            ViewBag.rate1 = Math.Round((double)num1 / ViewBag.num, 2) * 100;//比例
            ViewBag.rate2 = Math.Round((double)num2 / ViewBag.num, 2) * 100;
            ViewBag.rate3 = Math.Round((double)num3 / ViewBag.num, 2) * 100;
            ViewBag.rate4 = Math.Round((double)num4 / ViewBag.num, 2) * 100;
            ViewBag.rate5 = Math.Round((double)num5 / ViewBag.num, 2) * 100;
            ViewBag.total = Math.Round((double)(1 * num1 + 2 * num2 + 3 * num3 + 4 * num4 + 5 * num5) / ViewBag.num,1);
            ViewBag.comment = db.Database.SqlQuery<COMMENTS>("select * from COMMENTS where BOOK_ID=" + ID.ToString()+" order by TOTAL desc").ToList();
            ViewBag.name = db.Database.SqlQuery<string>("select NAME from USERS natural join COMMENTS order by TOTAL desc").ToList();
            ViewBag.comment_score = db.Database.SqlQuery<int>("select SCORE from USERS natural join COMMENTS order by TOTAL desc").ToList();
         
            return View();
        }

        public ActionResult addCart(int ID)
        { 
            int times = 1;
            int price = db.Database.SqlQuery<int>("select PRICE from BOOK where ID=" + ID.ToString()).FirstOrDefault();
            int user_id;
            if(Session["user_id"] != null)
            {
                user_id = Convert.ToInt32(Session["user_id"]);
            }
            else
            {
                return Redirect("~/Home/Login");
            }
            int num = db.Database.SqlQuery<int>("select QUANTITY from CART_INCLUDE where BOOK_ID=" + ID.ToString() + " and USER_ID=" + user_id.ToString()).FirstOrDefault();//当前购买该书数量
            if (num == 0)//目前
            {
                CART_INCLUDE temp1 = new CART_INCLUDE();
                temp1.USER_ID = user_id;
                temp1.BOOK_ID = ID;
                temp1.QUANTITY = times;
                temp1.TOTAL_PRICE = price * times;
                db.CART_INCLUDE.Add(temp1);
            }
            else
            {
                var temp = from u in db.CART_INCLUDE
                           where u.BOOK_ID == ID && u.USER_ID == user_id
                           select u;
                temp.FirstOrDefault().QUANTITY += times;
                temp.FirstOrDefault().TOTAL_PRICE += price * times;
            }
            int cart_num = db.Database.SqlQuery<int>("select count(*) from CART where USER_ID=" + user_id.ToString()).FirstOrDefault();//当前用户购物车记录
            if (cart_num == 0)//购物车为空
            {
                CART temp2 = new CART();
                temp2.USER_ID = user_id;
                temp2.QUANTITY = times;
                temp2.TIME_START = DateTime.Now.ToString();
                int cost = 15 - 3 * times;
                if (cost < 0) { cost = 0; }//计算邮费
                temp2.POST_COST = cost;
                temp2.TOTAL_PRICE = times * price;
                db.CART.Add(temp2);
            }
            else//购物车不为空
            {
                var result = from u in db.CART
                             where u.USER_ID == user_id
                             select u;

                result.FirstOrDefault().QUANTITY += times;
                result.FirstOrDefault().TIME_START = DateTime.Now.ToString();
                int cost = 15 - 3 * Convert.ToInt32(result.FirstOrDefault().QUANTITY);
                if (cost < 0) { cost = 0; }
                result.FirstOrDefault().POST_COST = cost;
            }
            db.SaveChanges();
            return Redirect("Index?ID=" + ID.ToString());
        }

        // GET: BOOKs/Details/5
        [HttpPost]
        public ActionResult addCart(cartSender cs)
        {
            int ID = id;
            int user_id;
            if (Session["user_id"] != null)
            {
                user_id = Convert.ToInt32(Session["user_id"]);
            }
            else
            {
                return Redirect("~/Home/Login");
            }
            int times = cs.amount;
            int price = db.Database.SqlQuery<int>("select PRICE from BOOK where ID=" + ID.ToString()).FirstOrDefault();
            int num = db.Database.SqlQuery<int>("select QUANTITY from CART_INCLUDE where BOOK_ID=" + ID.ToString()+" and USER_ID="+user_id.ToString()).FirstOrDefault();//当前购买该书数量
            if (num == 0)//目前
            {
                CART_INCLUDE temp1 = new CART_INCLUDE();
                temp1.USER_ID = user_id;
                temp1.BOOK_ID = ID;
                temp1.QUANTITY = times;
                temp1.TOTAL_PRICE = price*times;
                db.CART_INCLUDE.Add(temp1);                
            }
            else
            {
                var temp = from u in db.CART_INCLUDE
                           where u.BOOK_ID == ID && u.USER_ID == user_id
                           select u;
                temp.FirstOrDefault().QUANTITY += times;
                temp.FirstOrDefault().TOTAL_PRICE += price * times;
            }
            int cart_num = db.Database.SqlQuery<int>("select count(*) from CART where USER_ID=" + user_id.ToString()).FirstOrDefault();//当前用户购物车记录
            if (cart_num == 0)//购物车为空
            {
                CART temp2 = new CART();
                temp2.USER_ID = user_id;
                temp2.QUANTITY = times;
                temp2.TIME_START = DateTime.Now.ToString();
                int cost = 15 - 3 * times;
                if (cost < 0) { cost = 0; }//计算邮费
                temp2.POST_COST = cost;
                temp2.TOTAL_PRICE = times * price;
                db.CART.Add(temp2);
            }
            else//购物车不为空
            {
                var result = from u in db.CART
                             where u.USER_ID == user_id
                             select u;
                
                result.FirstOrDefault().QUANTITY += times;
                result.FirstOrDefault().TIME_START = DateTime.Now.ToString(); 
                int cost=15-3*Convert.ToInt32(result.FirstOrDefault().QUANTITY);
                if (cost < 0) { cost = 0; }
                result.FirstOrDefault().POST_COST = cost;
            }       
            db.SaveChanges();
            return Redirect("Index?ID="+ID.ToString());
        }

    }
}
