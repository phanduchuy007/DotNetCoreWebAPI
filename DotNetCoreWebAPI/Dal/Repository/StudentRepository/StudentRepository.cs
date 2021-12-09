using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Dal.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        StudentDataContext _studentContext;
        public StudentRepository(StudentDataContext studentContext):base(studentContext)
        {
            _studentContext = studentContext;
        }

        public IEnumerable<Student> GetListStudentDescending()
        {
            var ListStudent = _studentContext.Set<Student>().ToList();
            var student = ListStudent.OrderByDescending(st => st.mark);
            return student;
        }

        public Student UpdateStudent(Student student, Student studentUpdate)
        {
            student.name = studentUpdate.name;
            student.email = studentUpdate.email;
            student.address = studentUpdate.address;
            student.mark = studentUpdate.mark;

            _studentContext.Set<Student>().Update(student);
            _studentContext.SaveChanges();

            return studentUpdate;
        }
    }

}
