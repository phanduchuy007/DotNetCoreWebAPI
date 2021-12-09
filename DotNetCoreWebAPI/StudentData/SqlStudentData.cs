using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.StudentData
{
    public class SqlStudentData : IStudentData
    {
        private StudentDataContext _studentContext;

        public SqlStudentData(StudentDataContext studentContext)
        {
            _studentContext = studentContext;
        }

        public Student AddStudent(Student student)
        {        
            _studentContext.tblStudent.Add(student);
            _studentContext.SaveChanges();
            return student;
        }

        public void DeleteStudent(Student student)
        {
            _studentContext.tblStudent.Remove(student);
            _studentContext.SaveChanges();
        }

        public Student EditStudent(Student student)
        {
            var existingStudent = _studentContext.tblStudent.Find(student.id);
            if (existingStudent != null)
            {
                existingStudent.name = student.name;
                existingStudent.email = student.email;
                existingStudent.address = student.address;
                existingStudent.mark = student.mark;
                _studentContext.tblStudent.Update(existingStudent);
                _studentContext.SaveChanges();
            }
            return student;
        }

        public Student GetStudent(int id)
        {
            var student = _studentContext.tblStudent.Find(id);
            return student;
        }

        public List<Student> GetStudents()
        {
            return _studentContext.tblStudent.ToList();
        }
    }
}
