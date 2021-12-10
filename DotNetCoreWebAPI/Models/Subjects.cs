using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Models
{
    public class Subjects
    {
        [Key]
        public int ID { get; set; }
        public int IDStudent { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Classroom { get; set; }
        public float Mark { get; set; }
    }
}
