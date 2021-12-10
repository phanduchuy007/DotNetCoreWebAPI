using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Dal.Repository.SubjectjRepository
{
    public interface ISubjectsRepository:IRepository<Subjects>
    {
        IEnumerable<Subjects> GetSubjectByIDStudent(int id);
    }
}
