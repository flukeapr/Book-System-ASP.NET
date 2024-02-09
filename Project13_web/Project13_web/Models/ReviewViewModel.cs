using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project13_web.Models
{
    public class ReviewViewModel
    {
        public Book books { get; set; }

        public IEnumerable<Review> reviews { get; set; }
    }
}