using HW03_u20679484.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HW03_u20679484.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult> CombinedIndex(int? studentPage, int? bookPage)
        {
            int studentPageSize = 10;
            int studentPageNumber = (studentPage ?? 1);

            var students = await db.students.ToListAsync();
            var pagedStudents = students.ToPagedList(studentPageNumber, studentPageSize);

            int bookPageSize = 10;
            int bookPageNumber = (bookPage ?? 1);

            var books = await db.books.ToListAsync();
            var pagedBooks = books.ToPagedList(bookPageNumber, bookPageSize);

            var viewModel = new CombinedViewModel
            {
                Students = pagedStudents,
                Books = pagedBooks
            };

            return View(viewModel);
        }


        // Action methods for students
        public async Task<ActionResult> StudentIndex(int? page)
        {
            int pageSize = 10; // Set the number of items to display per page
            int pageNumber = (page ?? 1); // Get the current page number, or default to page 1 if no page is specified

            // Retrieve the list of students from the database
            var students = await db.students.ToListAsync();

            // Paginate the list of students
            var pagedStudents = students.ToPagedList(pageNumber, pageSize);

            return View("StudentIndex", pagedStudents);
        }


        public async Task<ActionResult> StudentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            students student = await db.students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        public ActionResult StudentCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentCreate([Bind(Include = "name, surname, birthdate, gender, class, point")] students student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("StudentIndex");
            }

            return View(student);
        }

        public async Task<ActionResult> StudentEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            students student = await db.students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentEdit([Bind(Include = "studentId, name, surname, birthdate, gender, class, point")] students student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("StudentIndex");
            }

            return View(student);
        }

        public async Task<ActionResult> StudentDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            students student = await db.students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("StudentDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentDeleteConfirmed(int id)
        {
            students student = await db.students.FindAsync(id);
            db.students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("StudentIndex");
        }

        // Action methods for books
        public async Task<ActionResult> BookIndex(int? page)
        {
            int pageSize = 10; // Set the number of items to display per page
            int pageNumber = (page ?? 1); // Get the current page number, or default to page 1 if no page is specified

            // Retrieve the list of books from the database
            var books = await db.books.ToListAsync();

            // Paginate the list of books
            var pagedBooks = books.ToPagedList(pageNumber, pageSize);

            
            return View("BookIndex", pagedBooks);
        }


        public async Task<ActionResult> BookDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            books book = await db.books.FindAsync(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        public ActionResult BookCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookCreate([Bind(Include = "name, pagecount, point, authorId, typeId")] books book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("BookIndex");
            }

            return View(book);
        }

        public async Task<ActionResult> BookEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            books book = await db.books.FindAsync(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookEdit([Bind(Include = "bookId, name, pagecount, point, authorId, typeId")] books book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BookIndex");
            }

            return View(book);
        }

        public async Task<ActionResult> BookDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            books book = await db.books.FindAsync(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("BookDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookDeleteConfirmed(int id)
        {
            books book = await db.books.FindAsync(id);
            db.books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("BookIndex");
        }
        public string GetBookStatus(int bookId)
        {
            var borrowRecord = db.borrows.FirstOrDefault(b => b.bookId == bookId && b.broughtDate == null);

            if (borrowRecord != null)
            {
                return "Out";
            }

            return "Available";
        }
    }
}
