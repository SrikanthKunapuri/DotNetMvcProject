using MVCDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
namespace MVCDB.Controllers
{
    public class StudentController : Controller
    {

        StudentDAL dal = new StudentDAL();

        #region get the  selected records
        // GET: multiple Students records
        public ViewResult DisplayStudents()
        {
            var Students = dal.GetStudents(null);
            return View(Students);
        }

        // GET: single Students record
        public ViewResult DisplayStudent(int Sid) {
            var student = dal.GetStudent(Sid, null);
            return View(student);
        }

        #endregion

        #region Insert the record
        // GET: Add Student record
        [HttpGet]
        public ViewResult AddStudent() {
            return View();
        }

        // POST: Add Student record

        [HttpPost]
        public RedirectToRouteResult AddStudent(Student student, HttpPostedFileBase selectedFile) {
            if (selectedFile != null) {
                string physicalPath = Server.MapPath("~/images");
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
                selectedFile.SaveAs(physicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            student.Status = true;
            dal.InsertStudent(student);
            return RedirectToAction("DisplayStudents");
        }
        #endregion


        #region Update
        public ViewResult EditStudent(int Sid)
        {
            var student = dal.GetStudent(Sid, true);
            TempData["photo"]=student.Photo;
            return View(student);
        }

        public RedirectToRouteResult UpdateStudent(Student student, HttpPostedFileBase selectedFile) {
            if (selectedFile != null)
            {
                string physicalPath = Server.MapPath("~/images");
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
                selectedFile.SaveAs(physicalPath + selectedFile.FileName);
                student.Photo = selectedFile.FileName;
            }
            else if (TempData["photo"] != null)
                student.Photo = TempData["photo"].ToString();
                student.Status = true;
            dal.UpdateStudent(student); 
            return RedirectToAction("DisplayStudents");
        }

        #endregion

        #region Delete
        public RedirectToRouteResult DeleteStudent(int Sid) {
            dal.DeleteStudent(Sid);
            return RedirectToAction("DisplayStudents");
        }
        #endregion

    };
}
