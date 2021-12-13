using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;

namespace DotNetCoreWebAPI.Dal.UnitOfWork
{
   public interface IUnitOfWork
    {
        IStudentRepository Student { get; }
        ISubjectsRepository Subject { get; }
        void Submit();
    }
}
