using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Repository;

namespace DotNetCoreWebAPI.Dal.Repository.SubjectjRepository
{
    public class SubjectsRepository : GenericRepository<Subjects>, ISubjectsRepository
    {
        StudentDataContext _studentDataContext;
        public SubjectsRepository(StudentDataContext studentDataContext):base(studentDataContext)
        {
            _studentDataContext = studentDataContext;
        }

        public IEnumerable<Subjects> GetSubjectByIDStudent(int id)
        {
            var ListSubject = from subjects in _studentDataContext.Set<Subjects>() where subjects.IDStudent == id select subjects;

            return ListSubject;
        }
    }
}
