using Lib_Mangement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lib_Mangement.Services
{
    public class BorrowService
    {
        public static BorrowHistory GetBorrowBook(int id)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                BorrowHistory borrowBook = context.BorrowHistories.Include(e => e.Book).Include(s => s.Student).Where(b => b.Id == id).First();
                return borrowBook;
            }

        }
        public static List<BorrowHistory> GetBorrowBooks()
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
               
                var borrowHistories = context.BorrowHistories.Include(b => b.Book).Include(b => b.Student).ToList();
                return borrowHistories;
            }
        }
        public static void AddNewBorrow(int studentId, int bookId)
        {
            BorrowHistory newBorrowBook = new BorrowHistory();
            newBorrowBook.StudentId = studentId;
            newBorrowBook.BookId = bookId;
            newBorrowBook.BorrowDate = DateTime.Now;

            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                context.BorrowHistories.Add(newBorrowBook);
                context.SaveChanges();
            }

        }
        public static void ReturnBook(int id)
        {
            BorrowHistory returnedBorrowBook = GetBorrowBook(id);
            int originalQuantity = returnedBorrowBook.Book.OriginalCopies;
            int avaliableQuantity = returnedBorrowBook.Book.AvailableNumber + 1;
            if (BookServices.IsUnvalidQuantity(originalQuantity, avaliableQuantity))
            {
                throw new Exception("Avaliable Quantity More Than Original Quantity");
            }
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                BorrowHistory borrowBook = context.BorrowHistories.Find(returnedBorrowBook.Id);
                context.BorrowHistories.Remove(borrowBook);
                context.SaveChanges();
            }
            Book book = BookServices.GetBook(returnedBorrowBook.BookId);
            BookServices.IncreaseBookAvlaibleNumber(book);
        }
    }
}