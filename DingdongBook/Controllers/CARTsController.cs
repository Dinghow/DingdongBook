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
            //Get login information and judge user's id
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_id = Session["user_id"].ToString();
                ViewBag.user_name = Session["user_account"].ToString();
                if (ID.ToString() != ViewBag.user_id)
                    return Redirect("~/Home/Login");
            }
            else
                return Redirect("~/Home/Login");

            //Get car information
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
                list[i].TOTAL_PRICE = list[i].PRICE * list[i].QUANTITY;
                total_price = total_price + (double)list[i].TOTAL_PRICE;
            }
            ViewBag.price = total_price;
            db.SaveChanges();

            return View();
        }

        //Remove book
        public void RemoveBook(int bookId)
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            CART_INCLUDE book_temp = db.CART_INCLUDE.Where(u => u.USER_ID == user_id).Where(u => u.BOOK_ID == bookId).FirstOrDefault();
            CART cart_temp = db.CART.Where(u => u.USER_ID == user_id).FirstOrDefault();
            //Judge whether the cart is empty
            if (cart_temp.QUANTITY != book_temp.QUANTITY)
            {
                cart_temp.QUANTITY -= book_temp.QUANTITY;
            }
            else
                db.CART.Remove(cart_temp);
            db.CART_INCLUDE.Remove(book_temp);
            db.SaveChanges();
        }

        //Add book to favorite list
        public void AddFav(int bookId)
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            int num = db.Database.SqlQuery<int>("select count(*) from FAVORITE where USER_ID=" + user_id.ToString() + " and BOOK_ID=" + bookId.ToString()).FirstOrDefault();
            if (num == 0)
            {
                db.Database.ExecuteSqlCommand("insert into FAVORITE values(" + user_id.ToString() + "," + bookId.ToString() + ")");
            }
            RemoveBook(bookId);
        }

        //Edit the amount of book
        public void EditAmount(int bookId,int bookAmount)
        {
            int user_id = Convert.ToInt32(Session["user_id"]);
            CART cart_temp = db.CART.Where(u => u.USER_ID == user_id).FirstOrDefault();
            CART_INCLUDE book_temp = db.CART_INCLUDE.Where(u => u.USER_ID == user_id).Where(u => u.BOOK_ID == bookId).FirstOrDefault();
            BOOK book = db.BOOK.Find(bookId);
            decimal delta = (decimal)bookAmount - (decimal)book_temp.QUANTITY;
            cart_temp.QUANTITY += delta;
            cart_temp.TOTAL_PRICE += delta * book.PRICE;
            book_temp.QUANTITY = bookAmount;
            book_temp.TOTAL_PRICE = book.PRICE * bookAmount;

            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult AddAddress(ADDRESS aDDRESS)
        {
            string user_id = Session["user_id"].ToString();
            db.ADDRESS.Add(aDDRESS);
            db.SaveChanges();

            return Redirect("~/orders/process_cart?id=" + user_id);
        }
    }
}
