using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.StudentData
{
    public class MockStudentData : IStudentData
    {
        private List<Student> students = new List<Student>()
        {
            new Student()
            {
                ID = 0,
                Name = "Huy",
                Address = "Binh Dinh",
                Email = "huy@gmail.com"
            },
            new Student()
            {
                ID = 1,
                Name = "Hoang",
                Address = "Binh Dinh",
                Email = "hoang@gmail.com"
            }
        };

        public Student AddStudent(Student student)
        {
            students.Add(student);
            return student;
        }

        public void DeleteStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Student EditStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int id)
        {
            return students.SingleOrDefault(x => x.ID == id);
        }

        public List<Student> GetStudents()
        {
            return students;
        }
    }
}
