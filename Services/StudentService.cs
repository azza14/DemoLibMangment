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
    public class StudentService
    {
        public static List<Student> GetALL()
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                return context.Students.ToList();
            }

        }
        public static Student GetStudent(int id)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                return context.Students.Find(id);
            }
        }
        public static void AddNewStudent(StudentViewModel student)
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                Student newStudent = new Student();
                newStudent.FirstName = student.FirstName;
                newStudent.LastName = student.LastName;
               
                context.Students.Add(newStudent);
                context.SaveChanges();
            }

        }

        internal static SelectList GetStudentList()
        {
            using (LibraryDBEntities context = new LibraryDBEntities())
            {
                var users = (from u in context.Students
                             select new { Id = u.Id, FullName = u.FirstName + " " + u.LastName }).ToList();

                return new SelectList(users, "Id", "FullName");
            }
        }

        public static void UpdateStudent(int id, StudentViewModel oldStudent)
        {

            using (LibraryDBEntities contex = new LibraryDBEntities())
            {
                Student stu = contex.Students.Find(id);
                contex.Entry(stu).State = EntityState.Modified;
                stu.FirstName = oldStudent.FirstName;
                stu.LastName = oldStudent.LastName;
               
                contex.SaveChanges();
            }
        }



    }
}