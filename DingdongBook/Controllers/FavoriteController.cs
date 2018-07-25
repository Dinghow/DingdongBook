using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.Collections;

namespace DingdongBook.Controllers
{
    public class FavoriteController : Controller
    {
        private Entities1 db = new Entities1();
        // GET: Favorite
        public ActionResult Index(int ID)
        {
            //Get login information
            ViewBag.user_auth = (Convert.ToBoolean(Session["user_auth"])) ? 1 : 0;
            if (ViewBag.user_auth == 1)
                ViewBag.user_name = Session["user_account"].ToString();

            ViewBag.list = db.Database.SqlQuery<BOOK>("select * from BOOK where BOOK.ID in (select BOOK_ID from FAVORITE where USER_ID=" + ID.ToString() + ")").ToList();//获得收藏书籍清单
            ViewBag.num = db.Database.SqlQuery<int>("select count(*) from FAVORITE where USER_ID=" + ID.ToString()).FirstOrDefault();
            ArrayList writer = new ArrayList();
            for(int i = 0; i < ViewBag.num; i++)
            {
                string a = ViewBag.list[i].ID.ToString();
                var temp=db.Database.SqlQuery<string>("select AUTHOR_NAME from ZUOZHE where BOOK_ID=" + a).FirstOrDefault();//获得作者名称
                writer.Add(temp);
            }
            ViewBag.writer = writer;
            return View();
        }

        //Add book to favorite list
        public ActionResult addFav(int ID)
        {
            int user_id;
            if (Session["user_id"] != null)
            {
                user_id = Convert.ToInt32(Session["user_id"]);
            }
            else
            {
                return Redirect("~/Home/Login");
            }
            int num = db.Database.SqlQuery<int>("select count(*) from FAVORITE where USER_ID=" + user_id.ToString() + " and BOOK_ID=" + ID.ToString()).FirstOrDefault();
            if (num == 0)
            {
                db.Database.ExecuteSqlCommand("insert into FAVORITE values(" + user_id.ToString() + "," + ID.ToString() + ")");
            }

            return Redirect("~/books/index?id="+ID.ToString());
        }

        //Remove book from favorite list
        public ActionResult removeFav(int ID)
        {
            int user_id;
            if (Session["user_id"] != null)
            {
                user_id = Convert.ToInt32(Session["user_id"]);
            }
            else
            {
                return Redirect("~/Home/Login");
            }
            db.Database.ExecuteSqlCommand("delete from FAVORITE where USER_ID=" + user_id.ToString() + " and BOOK_ID=" + ID.ToString());

            return Redirect("~/");
        }
    }
}