using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project13_web.Models;

namespace Project13_web.Controllers
{
    public class BooksAPIController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/BooksAPI
        public IQueryable<Book> GetBooks()
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            var book = db.Books.ToList();
            foreach (var item in book)
            {
                item.Image = url + "/Content/Book_Image/" + item.Image;

            }
            return db.Books;
        }

       


        // GET: api/BooksAPI/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Uri host = new Uri(Request.RequestUri.ToString());
            string url = host.GetLeftPart(UriPartial.Authority);

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Image = url + "/Content/Book_Image/" + book.Image;
            return Ok(book);
        }

        // PUT: api/BooksAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Book_Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BooksAPI
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Book_Id }, book);
        }

        // DELETE: api/BooksAPI/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Book_Id == id) > 0;
        }
    }
}