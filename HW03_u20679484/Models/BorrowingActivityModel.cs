using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Xml.Linq;

public class BorrowingActivityModel
{
    public int BorrowId { get; set; }

    public int StudentId { get; set; }

    public int BookId { get; set; }

    [Display(Name = "Taken Date")]
    [DataType(DataType.Date)]
    public DateTime TakenDate { get; set; }

    [Display(Name = "Brought Date")]
    [DataType(DataType.Date)]
    public DateTime BroughtDate { get; set; }

    public string StudentName { get; set; } // You may want to include student's name

    public string BookName { get; set; } // You may want to include the book's name


}

