using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lib_Mangement.Models;
using Lib_Mangement.Services;
using Lib_Mangement.ViewModel;

namespace Lib_Mangement.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: BorrowHistories
        public ActionResult Index()
        {
             var result= BorrowService.GetBorrowBooks();
            return View(result);
        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int id)
        {
            var borrowBook = (BorrowBookViewModel)BorrowService.GetBorrowBook(id);
            if (borrowBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowBook);
        }

        // GET: BorrowHistories/Create
        public ActionResult Create()
        {
            BorrowViewModel borrowViewModel = new BorrowViewModel();
            borrowViewModel.Students = StudentService.GetStudentList();
            borrowViewModel.Books = BookServices.GetBooksList();

            return View(borrowViewModel);
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(BorrowViewModel borrowViewModel)
        {
            try
            {
                BorrowService.AddNewBorrow(borrowViewModel.StudentId, borrowViewModel.BookId);
                Book book = BookServices.GetBook(borrowViewModel.BookId);
                BookServices.DecreaseBookAvlaible(book);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Code", borrowHistory.BookId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FirstName", borrowHistory.StudentId);
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookId,StudentId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Code", borrowHistory.BookId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "FirstName", borrowHistory.StudentId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            db.BorrowHistories.Remove(borrowHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
