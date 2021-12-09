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
                id = 0,
                name = "Huy",
                address = "Binh Dinh",
                email = "huy@gmail.com",
                mark = 9.9
            },
            new Student()
            {
                id = 1,
                name = "Hoang",
                address = "Binh Dinh",
                email = "hoang@gmail.com",
                mark = 9.9
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
            return students.SingleOrDefault(x => x.id == id);
        }

        public List<Student> GetStudents()
        {
            return students;
        }
    }
}
