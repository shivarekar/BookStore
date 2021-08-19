using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class Categories
    {
        public string Category { get; set; }

        public double Discount { get; set; }
        public IList<Book> Books { get; set; }
    }
}
