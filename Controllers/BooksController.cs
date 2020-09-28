using Lib_Mangement.Models;
using Lib_Mangement.Services;
using Lib_Mangement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace Lib_Mangement.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View(BookServices.GetALL());
        }

        // GET: Books/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            BookViewModel book = new BookViewModel();
            return View(book);
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            try
            {
                // TODO: Add insert logic here
                BookServices.AddNewBook(book);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = ex.Message;
                return View();
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            BookViewModel bookView = (BookViewModel)BookServices.GetBook(id);
            return View(bookView);
        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookViewModel bookViewModel)
        {
            try
            {
                // TODO: Add update logic here
                BookServices.UpdateBook(id, bookViewModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = ex.Message;
                return View();
            }
        }
        //public JsonResult DeleteBook(int BookId)
        //{

        //    try
        //    {
        //        BookServices.DeleteBook(BookId);
        //        return Json(new { redirectToUrl = Url.Action("Index", "Books") });
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { message = e.Message });
        //    }
        //}
        public JsonResult Deletebook(int BookId)
        {
            using (LibraryDBEntities db = new LibraryDBEntities())
            {
                bool result = false;
                var book = db.Books.Where(x => x.Id == BookId).SingleOrDefault();
                if(book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    result = true;
                }
                return Json(result,JsonRequestBehavior.AllowGet);

            }



        }
    }
}
    

