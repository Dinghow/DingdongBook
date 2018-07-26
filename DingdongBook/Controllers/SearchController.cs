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
    public class SearchController : Controller
    {
        private Entities1 db = new Entities1();

        [HttpPost]
        public ActionResult Index(SearchSender msg)
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_name = Session["user_account"].ToString();
            }

            List<BOOK> books = new List<BOOK>();
            var book = from m in db.BOOK
                       select m;

            if (!String.IsNullOrEmpty(msg.category))

            {

                book = book.Where(x => x.CATEGORY == msg.category); //根据选择种类筛选图书

            }

            if (!String.IsNullOrEmpty(msg.writer_book))
            {
                if (msg.writer_book == "作者")
                {
                    //如果选择关键字为作者，在作者名中搜索
                    var list = db.ZUOZHE.Where(m => m.AUTHOR_NAME.Contains(msg.keyword)).ToList();//获取zuozhe视图中作者名包含搜索词的项
                    foreach(var item in list)
                    {
                        books.Add(db.BOOK.Where(m => m.ID == item.BOOK_ID).FirstOrDefault());//根据list每一项的book_id在book查找
                    }
                }
                else
                {
                    //如果关键字为书名，直接在book中检索
                    book = book.Where(m => m.NAME.Contains(msg.keyword));
                }
            }
            else
            {
                //如果没有选择搜索关键字，则默认为搜索书名
                book = book.Where(m => m.NAME.Contains(msg.keyword));
            }

            if (msg.writer_book == "作者")
            {
                ViewBag.message = books;
                ViewBag.count = books.LongCount();
            }
            else
            {
                ViewBag.message = book;
                ViewBag.count = book.LongCount();//总数
            }

            return View();
        }

        //List all books by category
        public ActionResult Index(string category)
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
            {
                ViewBag.user_name = Session["user_account"].ToString();
            }

            var book = from m in db.BOOK
                       select m;
            book = book.Where(x => x.CATEGORY == category); //根据选择种类筛选图书
            ViewBag.message = book;
            ViewBag.count = book.LongCount();//总数

            return View();
        }

        //用于表单传递信息
        public class SearchSender
        {
            public string keyword { set; get; }
            public string category { set; get; }
            public string writer_book { set; get;}
        }
    }
}
