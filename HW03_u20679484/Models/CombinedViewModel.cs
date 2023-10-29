using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HW03_u20679484.Models
{
    public class CombinedViewModel
    {
        public IPagedList<authors> Authors { get; set; }
       
        public IPagedList<borrows> Borrows { get; set; }
        public IPagedList<types> Types { get; set; }
       
        public IPagedList<students> Students { get; set; }
   
        public IPagedList<books> Books { get; set; }

        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }
}