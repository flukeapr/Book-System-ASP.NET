using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project13_web.Models
{
    public class CustomerIndexData
    {
        public IEnumerable<Customer> customers { get; set; }
        public IEnumerable<Book> books { get; set; }
        public IEnumerable<Order> orders { get; set; }
    }
}