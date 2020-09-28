using Lib_Mangement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lib_Mangement.ViewModel
{
    public class BorrowBookViewModel
    {
        
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }

        public static explicit operator BorrowBookViewModel(BorrowHistory model)
        {
            BorrowBookViewModel bor = new BorrowBookViewModel();
            bor.Id = model.Id;
            bor.StudentId = model.StudentId;
            bor.BookId = model.BookId;
            bor.BorrowDate = model.BorrowDate;
            bor.Student = model.Student; 
            bor.Book = model.Book;

            return bor;
        }
    }
}