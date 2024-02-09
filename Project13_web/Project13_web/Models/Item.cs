using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project13_web.Models
{
    public class Item
    {
        public Book Book { get; set; }

        public int Quanity { get; set; }

        public int Total { get; set; }

        public string PaymentMethod { get; set; }
    }
}