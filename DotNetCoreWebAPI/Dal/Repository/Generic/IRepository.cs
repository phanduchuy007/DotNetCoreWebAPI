using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Dal.Repository.Generic
{
    public interface IRepository<T> where T :class
    {
        T GetStudent(int id);
        List<T> GetStudents();
        T AddStudent(T obj);
        void DeleteStudent(T obj);
    }
}
