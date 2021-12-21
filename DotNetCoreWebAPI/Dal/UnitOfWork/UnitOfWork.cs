using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository.Interface;
using DotNetCoreWebAPI.Dal.Repository.StudentRepository;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        StudentDataContext _dbContext;
        StudentRepository _studentRepository;
        SubjectsRepository _subjectsRepository;

        public UnitOfWork(StudentDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IStudentRepository Student
        {
            get
            {
                return _studentRepository ?? (_studentRepository = new StudentRepository(_dbContext));
            }
        }

        public ISubjectsRepository Subject
        {
            get
            {
                return _subjectsRepository ?? (_subjectsRepository = new SubjectsRepository(_dbContext));
            }
        }

        public void Submit()
        {
            _dbContext.SaveChanges();
        }
    }
}
