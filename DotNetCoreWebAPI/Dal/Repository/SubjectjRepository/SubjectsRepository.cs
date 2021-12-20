using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Models;

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
            var ListSubject = (from subjects in _studentDataContext.Set<Subjects>() where subjects.IDStudent == id select subjects).ToList();
            /*List<Subjects> newListSubject = new List<Subjects>();
            Subjects sub;
            foreach (var item in ListSubject)
            {
                if (item.Mark == null)
                {
                    sub = new Subjects()
                    {
                        ID = item.ID,
                        IDStudent = item.IDStudent,
                        Subject = item.Subject,
                        Teacher = item.Teacher,
                        Classroom = item.Classroom,
                        Mark = 0
                    };
                    newListSubject.Add(sub);
                }
                else
                {
                    newListSubject.Add(item);
                }

            }*/
            return ListSubject;
        }
    }
}
