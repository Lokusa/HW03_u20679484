using HW03_u20679484.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using OfficeOpenXml;


namespace HW03_u20679484.Controllers
{
    public class BorrowReportController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        private BorrowReport borrowReport = new BorrowReport();

        // GET: BorrowReport
        public ActionResult Index()
        {
            var students = db.students.ToList();
            ViewBag.StudentsList = new SelectList(students, "studentId", "name");
            return View("Index");
        }


        [HttpPost]
        public ActionResult GenerateReport(int studentId)
        {
            // Get the student's monthly borrow data
            List<StudentMonthlyBorrow> reportData = borrowReport.GetStudentMonthlyBorrows(studentId);

            return Json(reportData, JsonRequestBehavior.AllowGet);
        }
       

    }
}
