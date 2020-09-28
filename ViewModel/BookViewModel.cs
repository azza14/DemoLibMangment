using Lib_Mangement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lib_Mangement.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Book Name is Required")]
        [Display(Name = "Book Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Author  Name is Required")]
        [Display(Name = "Author Name")]
        public string Author_Name { get; set; }
        [Required(ErrorMessage = "Publisher Name is Required")]
        [Display(Name = "Publisher Name")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Available Number is Required")]
        [Display(Name = "Available Number ")]
        public int AvailableNumber { get; set; }
        [Required(ErrorMessage = "OriginalCopiese is Required")]
        [Display(Name = "Original of Copies")]
        public int OriginalCopies { get; set; }
        public bool IsAvailable { get; set; }


        public static explicit operator BookViewModel(Book model)
        {
            BookViewModel viewModel = new BookViewModel();
            viewModel.Id = model.Id;
            viewModel.Code = model.Code;
            viewModel.Name = model.Name;
            viewModel.Publisher = model.Publisher;
            viewModel.Author_Name = model.Author_Name;
            viewModel.OriginalCopies = model.OriginalCopies;
            viewModel.AvailableNumber = model.AvailableNumber;
            return viewModel;
        }
    }
}