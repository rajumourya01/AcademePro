using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;

namespace MVCWithLinq_Temp.Models
{
    public class StudentDAL
    {
        MVCDB_2DataContext dc = new MVCDB_2DataContext(ConfigurationManager.ConnectionStrings["MVCDB_2ConnectionString"].ConnectionString);

        #region GetStudent
        public List<Student> GetStudents(bool ? status )
        {
            List<Student> students = new List<Student>();

            try
            {
                
                if (status == null)
                {
                    students = (from s in dc.Students  select s).ToList();
                }
                else
                {
                    students = (from s in dc.Students where s.Status == status select s).ToList();

                }
                return students;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return students;
        }
        #endregion


        #region GetSingleStd
        public Student GetStudent(int Sid, bool? status)
        {
            try
            {
                Student s;
                if (status == null)
                {
                    s = (from s1 in dc.Students where s1.Sid == Sid select s1).Single();
                }
                else
                {
                    s = (from s1 in dc.Students where s1.Sid == Sid && s1.Status == status select s1).Single();

                }
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            #endregion
        

        #region InsertStudent
        public void InsertStudent(Student student)
        {
            try
            {
                dc.Students.InsertOnSubmit(student);
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region   UpdateStdData

        public void UpdateStudent (Student NewStudent)
        {
            try
            {
            Student oldstudent = dc.Students.FirstOrDefault(s=>s.Sid == NewStudent.Sid);
                oldstudent.Name= NewStudent.Name;
                oldstudent.Class = NewStudent.Class;
                oldstudent.Fees = NewStudent.Fees;
                oldstudent.Photo = NewStudent.Photo;

                dc.SubmitChanges();

            }

            catch (Exception ex) 
            {
                throw ex;
            }
        }

        #endregion

        #region DeleteStudent

        public void Delete(int Sid)
        {
            try
            {
                Student oldstudent = dc.Students.FirstOrDefault(S => S.Sid == Sid);
                oldstudent.Status = false;

                dc.SubmitChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion 



    }

}