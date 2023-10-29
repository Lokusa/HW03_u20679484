using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HW03_u20679484.Models
{
    public class BorrowReport
    {
        public List<StudentMonthlyBorrow> GetStudentMonthlyBorrows(int studentId)
        {
            using (var context = new LibraryEntities())
            {
                var query = from b in context.borrows
                            where b.studentId == studentId
                            group b by new
                            {
                                Month = b.takenDate,
                            } into g
                            select new
                            {
                                g.Key.Month,
                                Count = g.Count()
                            };

                var reportData = query.ToList() // Retrieve the data from the database
                    .Select(item => new StudentMonthlyBorrow
                    {
                        Month = item.Month.HasValue
                            ? $"{item.Month.Value.Month:D2}/{item.Month.Value.Year}"
                            : "N/A",
                        BooksBorrowed = item.Count
                    })
                    .ToList(); // Perform the formatting in memory

                return reportData;
            }
        }

    }

}

