using System;
using System.Collections.Generic;
using System.Text;

namespace Project13_mobile.Models
{
    class Book
    {
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }
        public string Category { get; set; }
        public string Short_Story { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Image { get; set; }
    }
}