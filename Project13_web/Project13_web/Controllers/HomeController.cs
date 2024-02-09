using Project13_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project13_web.Controllers
{
    public class HomeController : Controller
    {
        private DBEntities db = new DBEntities();
        public ViewResult Index(string Searchstring)
        {
            var book = from p in db.Books
                       select p;
            if (!String.IsNullOrEmpty(Searchstring))
            {
                book = book.Where(p => p.Book_Name.Contains(Searchstring) || p.Category.Contains(Searchstring));
            }

            return View(book);
        }
        public ViewResult Cartoon(string Searchstring)
        {
            var book = from p in db.Books
                       select p;
            if (!String.IsNullOrEmpty(Searchstring))
            {
                book = book.Where(p => p.Book_Name.Contains(Searchstring));
            }

            return View(book.Where(x => x.Category == "การ์ตูน"));
        }
        public ViewResult Novel(string Searchstring)
        {
            var book = from p in db.Books
                       select p;
            if (!String.IsNullOrEmpty(Searchstring))
            {
                book = book.Where(p => p.Book_Name.Contains(Searchstring));
            }

            return View(book.Where(x => x.Category == "นิยาย"));
        }
        public ViewResult Children(string Searchstring)
        {
            var book = from p in db.Books
                       select p;
            if (!String.IsNullOrEmpty(Searchstring))
            {
                book = book.Where(p => p.Book_Name.Contains(Searchstring));
            }

            return View(book.Where(x => x.Category == "หนังสือเด็ก"));
        }
        public ViewResult Study(string Searchstring)
        {
            var book = from p in db.Books
                       select p;
            if (!String.IsNullOrEmpty(Searchstring))
            {
                book = book.Where(p => p.Book_Name.Contains(Searchstring));
            }

            return View(book.Where(x => x.Category == "หนังสือเรียน"));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            List<Review> reviews = db.Reviews.Where(r => r.BookId == id).ToList();

            var viewModel = new ReviewViewModel
            {
                books = book,
                reviews = reviews
            };



            return View(viewModel);
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            var result = from b in db.Books
                         join o in db.Orders on b.Book_Id equals o.book_id
                         select new Project13_web.Models.OrderViewModel
                         {
                             book_img = b.Image,
                             book_name = o.Book_Name,
                             quantity = o.quanity,
                             total = o.total,
                             pay_method = o.pay_select,
                             email = o.userEmail,
                             Name = o.name,
                             Phone = o.phone,
                             Address= o.address
                         };



            return View(result.ToList().Where(x => x.email == User.Identity.Name).ToList());
        }
        [HttpPost]
        public ActionResult SaveReview(int bookId, int rating, string comment)
        {
            // สร้าง instance ของ Review
            var review = new Review
            {
                userName = User.Identity.Name,
                BookId = bookId,
                rating = rating,
                comment = comment,
                createdAt = DateTime.Now
            };

            // บันทึก review ลงในฐานข้อมูล
            db.Reviews.Add(review);
            db.SaveChanges();

            // ในที่นี้คุณอาจต้องทำการอัพเดทคะแนนรีวิวใน Book ด้วย (ตามที่คุณออกแบบฐานข้อมูล)

            return RedirectToAction("Details", new { id = bookId });
        }
    }
}