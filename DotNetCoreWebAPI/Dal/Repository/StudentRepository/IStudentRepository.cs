using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Dal.Repository
{
    public interface IStudentRepository:IRepository<Student>
    {
        IEnumerable<Student> GetListStudentDescending();
        Student UpdateStudent(Student student, Student studentUpdate);
    }
}
