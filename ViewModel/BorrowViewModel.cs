
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lib_Mangement.ViewModel
{
    public class BorrowViewModel
    {
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public SelectList Books { get; set; }
        public SelectList Students { get; set; }
    }
}