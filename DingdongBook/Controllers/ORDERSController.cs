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
    public class OrdersController : Controller
    {
        private Entities1 db = new Entities1();
        public static int user_id;

        // GET: ORDERS
        public ActionResult Index(int ID)
        {
            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            ViewBag.user_name = Session["user_account"].ToString();
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_id = Session["user_id"].ToString();
                if (ID.ToString() != ViewBag.user_id)
                    return Redirect("~/Home/Login");
            }
            else
                return Redirect("~/Home/Login");

            if (ID == null)
            {
                return HttpNotFound();
            }
            ViewBag.infoYes = db.Database.SqlQuery<ORDERS>("select * from ORDERS where USER_ID= " + ID.ToString() + "and STATUS = 'YES'").ToList();//已签收订单信息
            ViewBag.infoNo = db.Database.SqlQuery<ORDERS>("select * from ORDERS where USER_ID= " + ID.ToString() + "and STATUS = 'NO'").ToList();//未签收订单信息
            ViewBag.BookYes = db.Database.SqlQuery<PURCHASE>("select * from PURCHASE where PURCHASE.ORDER_ID in (select ID from ORDERS where USER_ID=" + ID.ToString() + " and STATUS='YES')").ToList();//已签收订单中的书籍信息
            ViewBag.BookNo = db.Database.SqlQuery<PURCHASE>("select * from PURCHASE where PURCHASE.ORDER_ID in (select ID from ORDERS where USER_ID=" + ID.ToString() + " and STATUS='NO')").ToList();//未签收订单中的书籍信息
            ViewBag.NumYes = db.Database.SqlQuery<int>("select count(*) from ORDERS where  USER_ID= " + ID.ToString() + "and STATUS='YES'").FirstOrDefault();//已签收订单的数量
            ViewBag.NumNo = db.Database.SqlQuery<int>("select count(*) from ORDERS where  USER_ID= " + ID.ToString() + "and STATUS='NO'").FirstOrDefault();//未签收订单的数量
            ViewBag.BookYesNum = db.Database.SqlQuery<int>("select count(*) from PURCHASE where PURCHASE.ORDER_ID in (select ID from ORDERS where USER_ID=" + ID.ToString() + " and STATUS='YES')").FirstOrDefault();//已签收订单中的书籍数量
            ViewBag.BookNoNum = db.Database.SqlQuery<int>("select count(*) from PURCHASE where PURCHASE.ORDER_ID in (select ID from ORDERS where USER_ID=" + ID.ToString() + " and STATUS='NO')").FirstOrDefault();//未签收订单中的书籍数量
            return View();
        }

        public ActionResult Process_CART(int ID)//从购物车购买
        {
            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            ViewBag.user_name = Session["user_account"].ToString();
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_id = Session["user_id"].ToString();
                if (ID.ToString() != ViewBag.user_id)
                    return Redirect("~/Home/Login");
            }
            else
                return Redirect("~/Home/Login");

            int book_numbers = Convert.ToInt32(Session["select_book_number"]);//Get the size of selected book list
            user_id = ID;
            List<CARTLIST> order_list = new List<CARTLIST>();

            System.Web.HttpContext.Current.Session["address"] = 1;
            ViewBag.cart = db.Database.SqlQuery<CART>("select * from CART where USER_ID=" + ID.ToString()).FirstOrDefault();
            //ViewBag.OrderList = db.Database.SqlQuery<CARTLIST>("select * from CARTLIST where USER_ID=" + ID.ToString()).ToList();//订单列表
            //Search 
            for (int i = 0; i < book_numbers; i++)
            {
                order_list.Add(db.Database.SqlQuery<CARTLIST>("select * from CARTLIST where USER_ID=" + ID.ToString() + " and BOOK_ID =" + Session["select_book" + i.ToString()]).FirstOrDefault());
            }
            ViewBag.OrderList = order_list;

            //ViewBag.num = db.Database.SqlQuery<int>("select count(*) from CARTLIST where USER_ID=" + ID.ToString()).FirstOrDefault();
            ViewBag.num = book_numbers;
            ViewBag.address = db.Database.SqlQuery<ADDRESS>("select * from ADDRESS where USER_ID=" + ID.ToString()).ToList();//获得用户地址
            ViewBag.address_count = db.Database.SqlQuery<int>("select count(*) from ADDRESS where USER_ID=" + ID.ToString()).FirstOrDefault();
            int sum=0;
            int amout=0;
            for (int i = 0; i < book_numbers; i++)
            {
                sum += Convert.ToInt32(order_list[i].PRICE*order_list[i].QUANTITY);
                amout += Convert.ToInt32(order_list[i].QUANTITY);
            }
            ViewBag.sum = sum;
            int post_cost = 15 - 3 * amout;
            if (post_cost < 0)
            {
                post_cost = 0;
            }
            ViewBag.POST_COST = post_cost;
            ViewBag.total = sum+post_cost;//总价

            return View();
        }
        public ActionResult Process_Book(int ID,int num)//直接书籍购买
        {
            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            ViewBag.user_name = Session["user_account"].ToString();
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_id = Session["user_id"].ToString();
            }

            ViewBag.address = db.Database.SqlQuery<ADDRESS>("select * from ADDRESS where USER_ID=" + ID.ToString()).ToList();//获得用户地址
            ViewBag.address_count = db.Database.SqlQuery<int>("select count(*) from ADDRESS where USER_ID=" + ID.ToString()).FirstOrDefault();
            ViewBag.OrderList = db.Database.SqlQuery<BOOK>("select * from BOOK where ID=" + ID.ToString()).FirstOrDefault();//购买的书籍信息
            ViewBag.num = num;//购买数量
            ViewBag.total_price = num * ViewBag.OrderList.PRICE;//总价
            ViewBag.total = ViewBag.total_price + 15;//总价（包含运费）
            return View();
        }
       
        //Set the new order information
        public int GetOrder(int[] bookIds)
        {
            Session["select_book_number"] = bookIds.Count();
            for (int i = 0; i < bookIds.Count(); i++)
            {
                Session["select_book" + i.ToString()] = bookIds[i];
            }

            return Convert.ToInt32(Session["user_id"]);
        }

        //Finish order
        public ActionResult Order_Complete()
        {
            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            ViewBag.user_id = Session["user_id"].ToString();
            ViewBag.user_name = Session["user_account"].ToString();

            return View();
        }

        //提交订单
        public void check(int addrId)
        {
            int book_numbers = Convert.ToInt32(Session["select_book_number"]);//Get the size of selected book list
            int user_id = Convert.ToInt32(Session["user_id"]);
            List<CARTLIST> order_list = new List<CARTLIST>();
            for (int i = 0; i < book_numbers; i++)
            {
                order_list.Add(db.Database.SqlQuery<CARTLIST>("select * from CARTLIST where USER_ID=" + user_id.ToString() + " and BOOK_ID =" + Session["select_book" + i.ToString()]).FirstOrDefault());
            }
            CART cart = db.CART.Find(user_id);//购物车项
            //var cartlist = db.CARTLIST.SqlQuery("select * from CARTLIST where USER_ID=" + user_id.ToString()).ToList();//购物车清单
            //int count = db.Database.SqlQuery<int>("select QUANTITY from CARTLIST where USER_ID=" + user_id.ToString()).FirstOrDefault();//项数
            int amout = 0;
            int total_price = 0;
            for (int i = 0; i < book_numbers; i++)
            {
                total_price += Convert.ToInt32(order_list[i].TOTAL_PRICE);
                amout += Convert.ToInt32(order_list[i].QUANTITY);
            }
            ORDERS new2 = new ORDERS();
            int max = db.Database.SqlQuery<int>("select max(ID) from ORDERS").FirstOrDefault();//找到ID的最大值
            max++;//作为新ID
            int post_cost = 15 - 3 * amout;
            if (post_cost < 0)
            {
                post_cost = 0;
            }
            new2.ID = max; new2.USER_ID = user_id; new2.ADDRESS_ID = addrId; new2.QUANTITY = book_numbers; new2.PRICE = total_price;
            new2.TIME_START = DateTime.Now.ToString(); new2.STATUS = "NO"; new2.POST_COST = post_cost;
            db.ORDERS.Add(new2);
            for (int i = 0; i < book_numbers; i++)
            {
                ORDER_INCLUDE temp = new ORDER_INCLUDE();
                temp.ORDER_ID = max;
                temp.BOOK_ID = order_list[i].BOOK_ID;
                temp.QUANTITY = order_list[i].QUANTITY;
                temp.TOTAL_PRICE = order_list[i].TOTAL_PRICE;
                db.ORDER_INCLUDE.Add(temp);
            }
            for(int i = 0; i < book_numbers; i++)
            {
                removeCart(Convert.ToInt32(Session["select_book" + i.ToString()]), user_id);
            }
            db.SaveChanges();
        }
        public void removeCart(int ID,int user_id)//移除购物车
        {
           
            var temp = db.Database.SqlQuery<CART_INCLUDE>("select * from CART_INCLUDE where USER_ID=" + user_id.ToString() + " and BOOK_ID=" + ID.ToString()).FirstOrDefault();//找到购物车中书籍记录
            CART cart = db.CART.Find(user_id);//找到购物车
            int post_cost = Convert.ToInt32(cart.POST_COST + 3 * temp.QUANTITY);//邮费
            if (post_cost > 15)
            {
                post_cost = 15;
            }
            int count = Convert.ToInt32(cart.QUANTITY - temp.QUANTITY);//数量
            int price = Convert.ToInt32(cart.TOTAL_PRICE - temp.TOTAL_PRICE);//价格
            cart.QUANTITY = count;
            cart.TIME_START = DateTime.Now.ToString();
            cart.POST_COST = post_cost;
            cart.TOTAL_PRICE = price;
            db.Database.ExecuteSqlCommand("delete from CART_INCLUDE where USER_ID=" + user_id.ToString() + " and BOOK_ID=" + ID.ToString());//删除记录
            db.SaveChanges();
    
        }


        //Confirm receipt
        public void StatusChange(int orderId)
        {
            ORDERS order = db.ORDERS.Find(orderId);
            order.STATUS = "YES";
            db.SaveChanges();
        }

        //Delete receipt
        public void DeleteOrder(int orderId)
        {
            ORDERS order = db.ORDERS.Find(orderId);
            db.Database.ExecuteSqlCommand("delete from ORDER_INCLUDE WHERE ORDER_ID = "+ orderId.ToString());
            db.ORDERS.Remove(order);
            db.SaveChanges();
        }

        //Comment book
        public void SetComment(int bookId,int grade,string content)
        {
            COMMENTS temp = new COMMENTS();
            int max = db.Database.SqlQuery<int>("select max(ID) from COMMENTS").FirstOrDefault();
            max++;//新评论的ID
            temp.ID = max;
            temp.USER_ID = user_id;
            temp.BOOK_ID = bookId;
            temp.CONTENT = content;
            temp.TIME = DateTime.Now.ToString("u");
            temp.SCORE = grade;
            temp.TOTAL_LIKE = 0.ToString();
            temp.TOTAL_DISLIKE = 0.ToString();
            temp.TOTAL_LIKE = "0";
            db.COMMENTS.Add(temp);

            db.SaveChanges();
        }
    }
}