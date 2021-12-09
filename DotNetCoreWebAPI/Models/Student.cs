using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public double mark { get; set; }
    }
}
