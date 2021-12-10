using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Repository
{
    public class GenericRepository<T> : IRepository<T> where T:class
    {
        StudentDataContext _studentContext;

        public GenericRepository(StudentDataContext studentContext)
        {
            _studentContext = studentContext;
        }

        public T Add(T obj)
        {
            _studentContext.Set<T>().Add(obj);
            _studentContext.SaveChanges();
            return obj;
        }

        public void Delete(T obj)
        {
            _studentContext.Set<T>().Remove(obj);
            _studentContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _studentContext.Set<T>().Find(id);
        }

        public List<T> Gets()
        {
            return _studentContext.Set<T>().ToList();          
        }
    }
}
