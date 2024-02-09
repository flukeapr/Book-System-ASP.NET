using Project13_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project13_web.Controllers
{
    public class CartController : Controller
    {
        private DBEntities db = new DBEntities();
        // GET: Cart
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult AddToCart(int bookID)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();

                cart.Add(new Item() { Book = db.Books.Find(bookID), Quanity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = IsInCart(bookID);
                if (index != -1)
                {
                    cart[index].Quanity++;
                }
                else
                {
                    cart.Add(new Item() { Book = db.Books.Find(bookID), Quanity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Payment");
        }
        public int IsInCart(int bookID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Book.Book_Id == bookID)
                {
                    return i;
                }
            }
            return -1;
        }
        public ActionResult RemoveFromCart(int bookID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = IsInCart(bookID);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Payment()
        {
            if (Session["cart"] == null)
            {

                return RedirectToAction("Payment");
            }
            List<Item> cart = (List<Item>)Session["cart"];
            ViewBag.Cart = cart;

            return View();
        }
        [HttpPost]
        public ActionResult Payment(Order order, string recive, string tel, string address)
        {

            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                string paymentMethod = Request.Form["payment-method"];
                List<Item> cart = (List<Item>)Session["cart"];
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < cart.Count; i++)
                    {
                        var item = cart[i];
                        order.book_id = item.Book.Book_Id;
                        order.userEmail = User.Identity.Name;
                        order.Book_Cat = item.Book.Category;
                        order.total = item.Quanity * item.Book.Price;
                        order.quanity = item.Quanity;
                        order.pay_select = paymentMethod;
                        order.Book_Name = item.Book.Book_Name;
                        order.name = recive;
                        order.phone = tel;
                        order.address = address;
                        db.Orders.Add(order);
                        db.SaveChanges();
                        int index = IsInCart(item.Book.Book_Id) ;
                        cart.RemoveAt(index);
                        Session["cart"] = cart;
                    };

                    
                    

                    return RedirectToAction("OrderList", "Home");
                }
            }




            return View();
        }


    }
}
