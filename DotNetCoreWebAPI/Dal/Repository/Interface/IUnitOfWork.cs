using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Dal.Repository.Interface
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }
        ISubjectsRepository Subject { get; }
        void Submit();
    }
}
