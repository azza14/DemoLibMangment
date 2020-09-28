using Lib_Mangement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lib_Mangement.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public static explicit operator StudentViewModel(Student model)
        {
            StudentViewModel viewmodel = new StudentViewModel();
            viewmodel.Id = model.Id;
            viewmodel.FirstName = model.FirstName;
            viewmodel.LastName = model.LastName;
            return viewmodel;
         
        }

       
    }
}