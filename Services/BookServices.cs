using Lib_Mangement.Models;
using Lib_Mangement.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lib_Mangement.Services
{
    public class BookServices
    {

        public static List<Book> GetALL()
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                return context.Books.ToList();
            }

        }
        public static Book GetBook(int id)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                return context.Books.Find(id);
            }
        }
        public static void AddNewBook(BookViewModel book)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                Book newBook = new Book();
                newBook.Name = book.Name;
                newBook.Code = book.Code;
                newBook.Author_Name = book.Author_Name;
                newBook.Publisher = book.Publisher;
                newBook.OriginalCopies = book.OriginalCopies;
                newBook.AvailableNumber = (int)book.OriginalCopies;
                context.Books.Add(newBook);
                context.SaveChanges();
            }

        }
        public static List<Book> GetAvaliableBooks()
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                List<Book> avaliableBooks = (from b in context.Books
                                             where b.AvailableNumber > 0
                                             select b).ToList();
                return avaliableBooks;
            }
        }
        internal static SelectList GetBooksList()
        {
            List<Book> avaliableBooks = GetAvaliableBooks();
            return new SelectList(avaliableBooks, "Id", "Name");
        }

        internal static void DecreaseBookAvlaible(Book book)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                context.Entry(book).State = EntityState.Modified;
                book.AvailableNumber = book.AvailableNumber - 1;
                context.SaveChanges();
            }
        }

        public static void UpdateBook(int id, BookViewModel oldbbook)
        {
            if (IsNegativeValue(oldbbook ))
            {
                throw new Exception(" Please check number of book");
            }
           
                using(LibraryDBEntities contex= new LibraryDBEntities())
                {
                     Book book = contex.Books.Find(id);
                    contex.Entry(book).State = EntityState.Modified;
                    book.Id = oldbbook.Id;
                    book.Code = oldbbook.Code;
                    book.Name = oldbbook.Name;
                    book.Author_Name = oldbbook.Author_Name;
                    book.Publisher = oldbbook.Publisher;
                    book.OriginalCopies = oldbbook.OriginalCopies;
                    book.AvailableNumber = oldbbook.AvailableNumber;
                    contex.SaveChanges();
                }
            }
        public static void DeleteBook(int id)
        {
            using(LibraryDBEntities context = new LibraryDBEntities())
            {
                var deletedBook = context.Books.Where(x => x.Id == id).FirstOrDefault();
                context.Books.Remove(deletedBook);
                context.SaveChanges();
            }
        }
        public static bool IsUnvalidQuantity(int originalQuantity, int avaliableQuantity)
        {
            return (originalQuantity < avaliableQuantity);
        }
        public static void IncreaseBookAvlaibleNumber(Book book)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                context.Entry(book).State = EntityState.Modified;
                book.AvailableNumber = book.AvailableNumber + 1;
                context.SaveChanges();
            }
        }
        public static bool IsNegativeValue(BookViewModel book)
        {
            return (book.OriginalCopies < 0 || book.AvailableNumber < 0);
        }

    }
}