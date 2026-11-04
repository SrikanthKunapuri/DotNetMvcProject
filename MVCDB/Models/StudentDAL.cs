using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace MVCDB.Models
{
    public class StudentDAL
    {
        MVCDBDataContext dc;

        public StudentDAL() { 
        string ConStr = ConfigurationManager.ConnectionStrings["MVCDBConnectionString"].ConnectionString;
                dc = new MVCDBDataContext(ConStr);
        }

        // get multi student record
        public List<Student> GetStudents(bool? Status) {

            List<Student> students;
            if(Status == null)
                students =(from s in dc.Students select s).ToList();

            else
                students=(from s in dc.Students where  s.Status == Status select s).ToList();
            return students;
        }

        // get single student record
        public Student GetStudent(int sid,bool ? Status) {
            Student student;
            if (Status == null)
                student = (from s in dc.Students where s.Sid == sid select s).Single();
            else
                student = (from s in dc.Students where s.Sid == sid && s.Status == Status select s).Single();
            return student;
           
        }
        //Add the table student record 
        public void InsertStudent(Student student)
        {
            try
            {
                dc.Students.InsertOnSubmit(student);
                dc.SubmitChanges();
            }
            catch (Exception ex) { 
              throw ex;
            }
        }

        //Update the table student record 
        public void UpdateStudent(Student student)
        {
            try
            {
                Student oldStudent = dc.Students.SingleOrDefault(S => S.Sid == student.Sid);
                oldStudent.Name = student.Name;
                oldStudent.Class = student.Class;
                oldStudent.Fees = student.Fees;
                oldStudent.Photo = student.Photo;
                oldStudent.Status = student.Status;
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete the table student record 
        public void DeleteStudent(int sid)
        {
           // SingleOrDefault   or FirstOrDefault  any one we can write both are same 

            try {
                Student oldStudent = dc.Students.FirstOrDefault(S => S.Sid == sid);

               // write the below code for permanent deletion
                dc.Students.DeleteOnSubmit(oldStudent);

                //write the below code for Updating the status
               // oldStudent.Status=false;
                dc.SubmitChanges(); // <-- Required to commit change
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}