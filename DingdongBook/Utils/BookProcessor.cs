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
            BOOK[] newest_Books = new BOOK[5];
 
            for(int i = 0; i < 5; i++)
            {
                newest_Books[i] = db.BOOK.Find(last - i);
            }

            ViewBag.nBooks = newest_Books;
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
            BOOK[] best_Books = new BOOK[5];

            for(int i  = 0;i < 5; i++)
            {
                best_Books[i] = db.BOOK.Find(index[i]);
            }

            //Use ViewBag to transfer the data
            ViewBag.bBooks = best_Books;
        }
    }
}