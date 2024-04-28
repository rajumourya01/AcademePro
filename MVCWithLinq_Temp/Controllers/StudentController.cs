using MVCWithLinq_Temp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace MVCWithLinq_Temp.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL dal = new StudentDAL();

        #region DisplayData
        public ViewResult DisplayStudents()
        {
            return View(dal.GetStudents(true));
        }

        public ViewResult DisplayStudent(int Sid)
        {
            return View(dal.GetStudent(Sid, true));
        }
        #endregion

        #region AddSStdDATA
        [HttpGet ]
        public ViewResult AddStudent()
        {
            return View();
        }
        [HttpPost ]

        public RedirectToRouteResult AddStudent(Student student, HttpPostedFileBase SelectedFile)
        {

            if (SelectedFile != null)
            {
                string FolderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                SelectedFile.SaveAs(FolderPath + SelectedFile.FileName);
                student.Photo = SelectedFile.FileName;
            }
            student.Status = true;
            dal.InsertStudent(student);

            return RedirectToAction("DisplayStudents");
        }

        #endregion

        #region EDIT&UPDATE
        public ViewResult Edit(int Sid)
        {
            return View(dal.GetStudent(Sid,true));
        }

        [HttpPost ]

        public RedirectToRouteResult UpdateStudent(Student student, HttpPostedFileBase SelectedFile)
        {
            int sid = student.Sid;
            if (SelectedFile != null)
            {
                string FolderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                SelectedFile.SaveAs(FolderPath + SelectedFile.FileName);
                student.Photo = SelectedFile.FileName;
            }
            else
            {
                string Photo = dal.GetStudent(sid, true).Photo;
                student.Photo = Photo;
            }
            student.Status = true;

            dal.UpdateStudent(student);

            return RedirectToAction("DisplayStudents");
        }
        #endregion

        #region DELETE
        
        public RedirectToRouteResult DeleteStudnet(int Sid)
        {
            dal.Delete(Sid);
            return RedirectToAction("DisplayStudents");
        }

        #endregion 



    }
}