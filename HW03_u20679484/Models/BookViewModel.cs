using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW03_u20679484.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int? pageCount { get; set; }
        public int ?point { get; set; }
        public string Status { get; set; } // Add a property for the book status
    }

}