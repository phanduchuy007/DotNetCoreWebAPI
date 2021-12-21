using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebAPI.Models
{
    [Table("tblSubject")]
    public class Subjects
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int IDStudent { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        [Required]
        [StringLength(50)]
        public string Teacher { get; set; }
        [Required]
        [StringLength(50)]
        public string Classroom { get; set; }
        [Column(TypeName = "Float")]
        public double? Mark { get; set; }
    }
}
