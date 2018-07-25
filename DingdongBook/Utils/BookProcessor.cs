using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DingdongBook.Models;
using System.Web.Mvc;

namespace DingdongBook.Controllers
{
    public partial class HomeController : Controller
    {
        public void GetNewestBooks()
        {
            //Get the information of top 5 newest books
            int last = db.BOOK.Count();
            BOOK newest_Book1 = db.BOOK.Find(last);
            BOOK newest_Book2 = db.BOOK.Find(last - 1);
            BOOK newest_Book3 = db.BOOK.Find(last - 2);
            BOOK newest_Book4 = db.BOOK.Find(last - 3);
            BOOK newest_Book5 = db.BOOK.Find(last - 4);

            ViewBag.nBook1_id = newest_Book1.ID;
            ViewBag.nBook1_name = newest_Book1.NAME;
            ViewBag.nBook1_img = newest_Book1.IMAGE;
            ViewBag.nBook1_price = newest_Book1.PRICE;

            ViewBag.nBook2_id = newest_Book2.ID;
            ViewBag.nBook2_name = newest_Book2.NAME;
            ViewBag.nBook2_img = newest_Book2.IMAGE;
            ViewBag.nBook2_price = newest_Book2.PRICE;

            ViewBag.nBook3_id = newest_Book3.ID;
            ViewBag.nBook3_name = newest_Book3.NAME;
            ViewBag.nBook3_img = newest_Book3.IMAGE;
            ViewBag.nBook3_price = newest_Book3.PRICE;

            ViewBag.nBook4_id = newest_Book4.ID;
            ViewBag.nBook4_name = newest_Book4.NAME;
            ViewBag.nBook4_img = newest_Book4.IMAGE;
            ViewBag.nBook4_price = newest_Book4.PRICE;

            ViewBag.nBook5_id = newest_Book5.ID;
            ViewBag.nBook5_name = newest_Book5.NAME;
            ViewBag.nBook5_img = newest_Book5.IMAGE;
            ViewBag.nBook5_price = newest_Book5.PRICE;
        }

        public void GetBestBooks()
        {
            int quantity = db.BOOK.Count();
            //The number of people for each score level
            int num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0;
            //Use dictionary to save the average score of each book
            Dictionary<int, float> scores = new Dictionary<int, float>();
            for (int i = 1; i <= quantity; i++) 
            {
                num1 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=1 and BOOK_ID=" + i.ToString()).FirstOrDefault();
                num2 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=2 and BOOK_ID=" + i.ToString()).FirstOrDefault();
                num3 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=3 and BOOK_ID=" + i.ToString()).FirstOrDefault();
                num4 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=4 and BOOK_ID=" + i.ToString()).FirstOrDefault();
                num5 = db.Database.SqlQuery<int>("select count(*) from COMMENTS where SCORE=5 and BOOK_ID=" + i.ToString()).FirstOrDefault();
                float total_score = num1 * 1 + num2 * 2 + num3 * 3 + num4 * 4 + num5 * 5;
                float total_people = num1 + num2 + num3 + num4 + num5;
                float average = total_score / total_people;
                scores[i] = average;
            }

            //Sort the dictionary by value
            Dictionary<int, float> sorted_scores = scores.OrderByDescending(o => o.Value).ToDictionary(p => p.Key, o => o.Value);
            List<int> index = new List<int>(sorted_scores.Keys);

            
            BOOK bestBook1 = db.BOOK.Find(index[0]);
            BOOK bestBook2 = db.BOOK.Find(index[1]);
            BOOK bestBook3 = db.BOOK.Find(index[2]);
            BOOK bestBook4 = db.BOOK.Find(index[3]);
            BOOK bestBook5 = db.BOOK.Find(index[4]);

            //Use ViewBag to transfer the data
            ViewBag.bBook1_id = bestBook1.ID;
            ViewBag.bBook1_name = bestBook1.NAME;
            ViewBag.bBook1_img = bestBook1.IMAGE;
            ViewBag.bBook1_price = bestBook1.PRICE;

            ViewBag.bBook2_id = bestBook2.ID;
            ViewBag.bBook2_name = bestBook2.NAME;
            ViewBag.bBook2_img = bestBook2.IMAGE;
            ViewBag.bBook2_price = bestBook2.PRICE;

            ViewBag.bBook3_id = bestBook3.ID;
            ViewBag.bBook3_name = bestBook3.NAME;
            ViewBag.bBook3_img = bestBook3.IMAGE;
            ViewBag.bBook3_price = bestBook3.PRICE;

            ViewBag.bBook4_id = bestBook4.ID;
            ViewBag.bBook4_name = bestBook4.NAME;
            ViewBag.bBook4_img = bestBook4.IMAGE;
            ViewBag.bBook4_price = bestBook4.PRICE;

            ViewBag.bBook5_id = bestBook5.ID;
            ViewBag.bBook5_name = bestBook5.NAME;
            ViewBag.bBook5_img = bestBook5.IMAGE;
            ViewBag.bBook5_price = bestBook5.PRICE;
        }
    }
}