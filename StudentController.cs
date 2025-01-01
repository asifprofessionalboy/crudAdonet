////using AspNetCore.Reporting;
//using CrudUsingADO.NET.DataAccessLayer;
//using CrudUsingADO.NET.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.IO.Packaging;
//using System.Net.Mime;
////using Microsoft.AspNetCore.Hosting;
//using System.Data;
//using System.IO;
//using System.Net.Mime;
//using Newtonsoft.Json;
//using Microsoft.ReportingServices.ReportViewerControl.WinForms;
//using Microsoft.Reporting.NETCore;

using AspNetCore.Reporting;
using CrudUsingADO.NET.DataAccessLayer;
using CrudUsingADO.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net.Mime;

namespace CrudUsingADO.NET.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDataAccess _studentDataAccess;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(StudentDataAccess studentDataAccess, IWebHostEnvironment webHostEnvironment)
        {
            _studentDataAccess = studentDataAccess;
            _webHostEnvironment = webHostEnvironment;
        }




        public IActionResult RDLCIndex()
        {
          //  Get student list
             var stuReport = _studentDataAccess.GetAllStudents();

          //  Convert list to DataTable
             ViewBag.stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

          //   Pass the data to the view
            //  return View();

          
            return View();

            
        }


        public IActionResult RdlcList()
        {
            try
            {
                // Get student list
                var stuReport = _studentDataAccess.GetAllStudents();

                // Convert list to DataTable
                var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

                // Get the report path
                string wwwRootFolder = _webHostEnvironment.WebRootPath;
                string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

                // Load the RDLC report
                var localReport = new LocalReport(reportPath);

                // Add the DataTable as a data source for the report
                localReport.AddDataSource("DataSet1", stuDataTbl);

                // Render the report to HTML
                var reportResult = localReport.Execute(RenderType.Html, 1, null);

                // Store the report result in ViewBag
                ViewBag.ReportHtml = reportResult.MainStream;

                // Pass the student data to the view
                ViewBag.StudentDataTable = stuDataTbl;

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }




        //view


        //public IActionResult StudentView()
        //{
        //    try
        //    {
        //        // Get student list
        //        var stuReport = _studentDataAccess.GetAllStudents();

        //        // Convert list to DataTable
        //        var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

        //        // Get the report path
        //        string wwwRootFolder = _webHostEnvironment.WebRootPath;
        //        string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

        //        // Load the RDLC report
        //        var localReport = new LocalReport(reportPath);

        //        // Add the DataTable as a data source for the report
        //        localReport.AddDataSource("DataSet1", stuDataTbl);

        //        // Render the report to PDF
        //        var reportResult = localReport.Execute(RenderType.Pdf, 1, null);

        //        // Return the generated PDF file as a stream
        //        return File(reportResult.MainStream, MediaTypeNames.Application.Pdf);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, "An error occurred while generating the report.");
        //    }
        //}




        //Asp .net core reporting
        public IActionResult StudentReport()
        {
            try
            {
                // Get student list
                var stuReport = _studentDataAccess.GetAllStudents();

                // Convert list to DataTable
                var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

                // Get the report path
                string wwwRootFolder = _webHostEnvironment.WebRootPath;
                string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

                // Load the RDLC report
                var localReport = new LocalReport(reportPath);

                // Add the DataTable as a data source for the report
                localReport.AddDataSource("DataSet1", stuDataTbl);

                // Render the report to PDF
                var reportResult = localReport.Execute(RenderType.Pdf, 1, null);

                // Return the generated PDF file
                return File(reportResult.MainStream, MediaTypeNames.Application.Octet, "StudentRpt.pdf");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                // Replace this with your logging mechanism
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }







        //// GenerateExcel

        //public IActionResult StudentReportExcel()
        //{
        //    try
        //    {
        //        // Get student list
        //        var stuReport = _studentDataAccess.GetAllStudents();

        //        // Convert list to DataTable
        //        var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

        //        // Get the report path
        //        string wwwRootFolder = _webHostEnvironment.WebRootPath;
        //        string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

        //        // Load the RDLC report
        //        var localReport = new LocalReport(reportPath);

        //        // Add the DataTable as a data source for the report
        //        localReport.AddDataSource("DataSet1", stuDataTbl);

        //        // Render the report to Excel
        //        var reportResult = localReport.Execute(RenderType.ExcelOpenXml, 1, null);

        //        // Return the generated Excel file
        //        return File(reportResult.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentRpt.xlsx");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return an error response
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, "An error occurred while generating the report.");
        //    }
        //}


        //Microsoft.ReportingServices.ReportViewerControl.WinForms


        //public IActionResult ReportControllerView()
        //{
        //    try
        //    {
        //        // Get student list
        //        var stuReport = _studentDataAccess.GetAllStudents();

        //        // Convert list to DataTable
        //        var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

        //        // Get the report path
        //        string wwwRootFolder = _webHostEnvironment.WebRootPath;
        //        string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

        //        // Store the data and report path in ViewData
        //        ViewData["ReportPath"] = reportPath;
        //        ViewData["DataSource"] = stuDataTbl;

        //        // Return the view that contains the ReportViewer
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return an error response
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, "An error occurred while generating the report.");
        //    }
        //}



        public IActionResult ReportControllerView()
        {
            try
            {
                // Get student list
                var stuReport = _studentDataAccess.GetAllStudents();

                // Convert list to DataTable
                var stuDataTbl = ConvertListToDataTable.ToDataTable(stuReport);

                // Get the report path
                string wwwRootFolder = _webHostEnvironment.WebRootPath;
                string reportPath = Path.Combine(wwwRootFolder, @"Reports\StudentRPT.rdlc");

                // Store the data and report path in ViewData
                ViewData["ReportPath"] = reportPath;
                ViewData["DataSource"] = stuDataTbl;

                // Assume that the Windows Forms app has generated the report as PDF at this path
                string reportPdfPath = Path.Combine(wwwRootFolder, "Reports", "StudentReport.pdf");

                // Pass the PDF path to the view
                ViewData["PdfPath"] = Url.Content("~/Reports/StudentReport.pdf");

                // Return the view that contains the ReportViewer iframe
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while generating the report.");
            }
        }





        public IActionResult ReportViewer()
        {
            return View();
        }











        public async Task<IActionResult> Index()
        {
            var stu =_studentDataAccess.GetAllStudents();
            return View(stu);
        }


         


        // GET: Student/Details/{id}
        public IActionResult Details(Guid id)
        {
            var student = _studentDataAccess.GetAllStudents().Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }




        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                _studentDataAccess.CreateStudent(student);
                TempData["Message"] = "Created successfully";
              //  return RedirectToAction(nameof(Index));
       
            }
            return View(student);
        }




        // GET: Student/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var student = _studentDataAccess.GetAllStudents().Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _studentDataAccess.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var student = _studentDataAccess.GetAllStudents().Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _studentDataAccess.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }




    }
}
