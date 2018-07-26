using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DingdongBook.Models;
using System.Text;
using System.Collections;

namespace DingdongBook.Controllers
{
    public class AdminController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Admin
        public ActionResult Index(string search_book,string search_user)
        {
            //Get login information
            ViewBag.admin_auth = (Convert.ToBoolean(Session["admin_auth"])) ? 1 : 0;
            if (ViewBag.admin_auth == 1)
                ViewBag.admin_name = Session["admin_account"].ToString();
            if (search_book == null && search_user == null)
            {
                var all_user = from u in db.USERS
                               orderby u.ID ascending
                               where u.AUTHORITY == "user"//只展现用户
                               select u;//得到所有用户
                var writer_list = db.Database.SqlQuery<string>("select AUTHOR_NAME from ZUOZHE order by BOOK_ID asc").ToList();//得到作者清单
                ViewBag.writer_list = writer_list;
                ViewBag.num = all_user.Count();//用户数量
                ViewBag.all_user = all_user.ToList();
                var all_book = from u in db.BOOK
                               orderby u.ID
                               select u;//得到所有图书
                ViewBag.booknum = all_book.Count();//图书数量
                ViewBag.all_book = all_book.ToList();
            }
            else if (search_book == null && search_user != null)
            {
                string search = search_user;

                ArrayList result = new ArrayList();
                var list1 = db.USERS.Where(m => m.NAME.Contains(search)).ToList();
                for (int i = 0; i < list1.Count(); i++)
                {
                    result.Add(list1[i]);
                }
                var list2 = db.USERS.Where(m => m.ACCOUNT.Contains(search)).ToList();
                for (int i = 0; i < list2.Count(); i++)
                {
                    result.Add(list2[i]);
                }
                ViewBag.num = list1.Count() + list2.Count();
                ViewBag.all_user = result;
            }
            if (search_book != null && search_user == null)
            {
                var list = db.BOOK.Where(u => u.NAME.Contains(search_book)).ToList();

                ViewBag.booknum = list.Count();
                ViewBag.all_book = list;
            }

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
        public void addBook(string name, string ISBN, string writer, string category, string publisher, string time, string image, string intro)//添加图书
        {
            BOOK newbook = new Models.BOOK();
            int bookmax = db.Database.SqlQuery<int>("select max(ID) from BOOK").FirstOrDefault();
            bookmax++;//新的书的ID
            newbook.ID = bookmax;
            newbook.ISBN = ISBN;
            newbook.NAME = name;
            newbook.PRICE = 20;
            newbook.IMAGE = image;
            newbook.CATEGORY = category;
            newbook.PUBLISHER = publisher;
            newbook.PUBLISHTIME = time;
            newbook.ABSTRACT = intro;
            db.BOOK.Add(newbook);
            db.SaveChanges();
        }
        public void DeleteUser(int userId)//删除用户
        {
            var user = db.USERS.Find(userId);
            db.USERS.Remove(user);
            db.SaveChanges();
        }
        public void deleteBook(int ID)//删除书籍
        {
            var book = db.BOOK.Find(ID);
            db.BOOK.Remove(book);
            db.SaveChanges();
        }
        public void EditBook(int bookId, string bookName, string isbn, string author, string publisher, string publishDate,int unitPrice, string synopsis)//修改书籍
        {
            var book = db.BOOK.Find(bookId);
            book.ISBN = isbn;
            book.NAME = bookName;
            book.PRICE = unitPrice;
            book.PUBLISHER = publisher;
            book.PUBLISHTIME = publishDate;
            book.ABSTRACT = synopsis;
            db.SaveChanges();
        }
        public ActionResult SearchUser(AdminSender msg)//搜索用户
        {
            string search = msg.user_search;

            return Redirect("~/Admin/Index?search_user="+ search);
        }

        public ActionResult SearchBook(AdminSender msg)//搜索用户
        {
            string search = msg.book_search;

            return Redirect("~/Admin/Index?search_user&&search_book=" + search);
        }

        public class AdminSender
        {
            public string user_search { set; get; }
            public string book_search { set; get; }
        }
    }
}