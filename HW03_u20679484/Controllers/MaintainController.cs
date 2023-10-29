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
    public class MaintainController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        public async Task<ActionResult> MaintainIndex(int? authorPage, int? typePage, int? borrowPage)
        {
            int authorPageSize = 10;
            int authorPageNumber = authorPage ?? 1;

            var authors = await db.authors.ToListAsync();
            var pagedAuthors = authors.ToPagedList(authorPageNumber, authorPageSize);

            int typePageSize = 10;
            int typePageNumber = typePage ?? 1;

            var types = await db.types.ToListAsync();
            var pagedTypes = types.ToPagedList(typePageNumber, typePageSize);

            int borrowPageSize = 10;
            int borrowPageNumber = borrowPage ?? 1;

            var borrows = await db.borrows.ToListAsync();
            var pagedBorrows = borrows.ToPagedList(borrowPageNumber, borrowPageSize);

            var viewModel = new CombinedViewModel
            {
                Authors = pagedAuthors,
                Types = pagedTypes,
                Borrows = pagedBorrows
            };

            return View("MaintainIndex", viewModel);
        }

        // Action methods for Authors
        public async Task<ActionResult> AuthorIndex(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var authors = await db.authors.ToListAsync();
            var pagedAuthors = authors.ToPagedList(pageNumber, pageSize);

            return View("AuthorIndex", pagedAuthors);
        }

        public async Task<ActionResult> AuthorDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            authors author = await db.authors.FindAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        public ActionResult AuthorCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthorCreate([Bind(Include = "name, surname")] authors author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("AuthorIndex");
            }

            return View(author);
        }

        public async Task<ActionResult> AuthorEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            authors author = await db.authors.FindAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthorEdit([Bind(Include = "authorId, name, surname")] authors author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("AuthorIndex");
            }

            return View(author);
        }

        public async Task<ActionResult> AuthorDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            authors author = await db.authors.FindAsync(id);

            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("AuthorDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthorDeleteConfirmed(int id)
        {
            authors author = await db.authors.FindAsync(id);
            db.authors.Remove(author);
            await db.SaveChangesAsync();
            return RedirectToAction("AuthorIndex");
        }

        // Action methods for Borrows
        public async Task<ActionResult> BorrowIndex(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var borrows = await db.borrows.ToListAsync();
            var pagedBorrows = borrows.ToPagedList(pageNumber, pageSize);

            return View("BorrowIndex", pagedBorrows);
        }

        public async Task<ActionResult> BorrowDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            borrows borrow = await db.borrows.FindAsync(id);

            if (borrow == null)
            {
                return HttpNotFound();
            }

            return View(borrow);
        }

        public ActionResult BorrowCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BorrowCreate([Bind(Include = "studentId, bookId, takenDate, broughtDate")] borrows borrow)
        {
            if (ModelState.IsValid)
            {
                db.borrows.Add(borrow);
                await db.SaveChangesAsync();
                return RedirectToAction("BorrowIndex");
            }

            return View(borrow);
        }

        public async Task<ActionResult> BorrowEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            borrows borrow = await db.borrows.FindAsync(id);

            if (borrow == null)
            {
                return HttpNotFound();
            }

            return View(borrow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BorrowEdit([Bind(Include = "borrowId, studentId, bookId, takenDate, broughtDate")] borrows borrow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrow).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("BorrowIndex");
            }

            return View(borrow);
        }

        public async Task<ActionResult> BorrowDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            borrows borrow = await db.borrows.FindAsync(id);

            if (borrow == null)
            {
                return HttpNotFound();
            }

            return View(borrow);
        }

        [HttpPost, ActionName("BorrowDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BorrowDeleteConfirmed(int id)
        {
            borrows borrow = await db.borrows.FindAsync(id);
            db.borrows.Remove(borrow);
            await db.SaveChangesAsync();
            return RedirectToAction("BorrowIndex");
        }

        // Action methods for Types
        public async Task<ActionResult> TypeIndex(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var types = await db.types.ToListAsync();
            var pagedTypes = types.ToPagedList(pageNumber, pageSize);

            return View("TypeIndex", pagedTypes);
        }

        public async Task<ActionResult> TypeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            types type = await db.types.FindAsync(id);

            if (type == null)
            {
                return HttpNotFound();
            }

            return View(type);
        }

        public ActionResult TypeCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TypeCreate([Bind(Include = "name")] types type)
        {
            if (ModelState.IsValid)
            {
                db.types.Add(type);
                await db.SaveChangesAsync();
                return RedirectToAction("TypeIndex");
            }

            return View(type);
        }

        public async Task<ActionResult> TypeEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            types type = await db.types.FindAsync(id);

            if (type == null)
            {
                return HttpNotFound();
            }

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TypeEdit([Bind(Include = "typeId, name")] types type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("TypeIndex");
            }

            return View(type);
        }

        public async Task<ActionResult> TypeDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            types type = await db.types.FindAsync(id);

            if (type == null)
            {
                return HttpNotFound();
            }

            return View(type);
        }

        [HttpPost, ActionName("TypeDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TypeDeleteConfirmed(int id)
        {
            types type = await db.types.FindAsync(id);
            db.types.Remove(type);
            await db.SaveChangesAsync();
            return RedirectToAction("TypeIndex");
        }

    }
}
