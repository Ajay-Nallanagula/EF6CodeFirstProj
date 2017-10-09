using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF6CodeFirstApporach.Dal;
using EF6CodeFirstApporach.Models;
using System.Diagnostics;
using System.Collections;
using PagedList;

namespace EF6CodeFirstApporach.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrWhiteSpace(searchString))
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var students = from student in db.Students select student;

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                students = FilterNames(searchString.ToLower(), students);
            }

           

            students = SortStudentList(sortOrder, students);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
            //return View(students.ToList());
        }

        private IQueryable<Student> FilterNames(string searchString, IQueryable<Student> students)
        {
            return students.Where(m => m.FirstName.ToLower().Contains(searchString) || m.LastName.ToLower().Contains(searchString)); 
        }

        private IQueryable<Student> SortStudentList(string sortOrder,IQueryable<Student> students)
        {
           // var students = from student in db.Students select student; //students is of type IQueryable<Students> here we are using Linq To Entites

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(m => m.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(m => m.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(m => m.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(m => m.LastName);
                    break;
            }
            return students;
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EnrollmentDate,EmailAddress")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                Trace.WriteLine(ex.Message);
                ModelState.AddModelError("UNABLE TO SAVE:", "Unable to save the data at this moment, Contact your system Administrator");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Student student = db.Students.Find(id);
            bool isUpdated = TryUpdateModel(student, new string[] { "FirstName", "LastName", "EnrollmentDate", "EmailAddress" });
            try
            {
                if (isUpdated)
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                ModelState.AddModelError("EditError", "Unable to edit the record this time, Contact SystemAdminstrator");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool saveChagesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChagesError)
            {
                ViewBag.ErrorMessage = "Oops delete operation gone wrong , contact sysadmin";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //This will sent an extra query to database i.e find/select query 
                // Student student = db.Students.Find(id);
                // db.Students.Remove(student);

                //As an alternative to above code we can set theEntityState per record to Deleteas shown below 
                Student studentToDelete = new Student() { Id = id };
                db.Entry(studentToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (DataException ex)
            {
                Trace.WriteLine(ex.Message);
                RedirectToAction("Delete", new { Id = id, saveChagesError = true });
            }
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
