using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Dal.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetListStudentAscending();
        Student UpdateStudent(Student student, Student studentUpdate);
    }
}
