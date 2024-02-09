using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project13_web.Models
{
    public class OrderViewModel
    {

        public string book_img { get; set; }

        public string book_name { get; set; }

        public int? quantity { get; set; }

        public decimal? total { get; set; }

        public string pay_method { get; set; }

        public string email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}