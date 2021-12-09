using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Models
{
    public class StudentDataContext:DbContext
    {
        public StudentDataContext(DbContextOptions<StudentDataContext>options):base (options)
        {
        }

        public DbSet<Student> tblStudent { get; set; }
    }
}
