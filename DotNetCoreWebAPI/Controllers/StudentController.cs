using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.StudentData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        // private IStudentData _studentData;
        private IStudentRepository _repository;
        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // get/student
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_repository.GetStudents());
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var student = _repository.GetStudent(id);

            if (student != null)
            {
                return Ok(student);
            }

            return NotFound($"Student with Id: {id} was not found");
        }

        [Route("api/[controller]")]
        [HttpPost]
        public IActionResult GetStudent(Student student)
        {
            _repository.AddStudent(student);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Path + "/" + student.id, student);
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var student = _repository.GetStudent(id);
            if (student != null)
            {
                _repository.DeleteStudent(student);

                return Ok();
            }
            return NotFound($"Student with Id:{id} was not found");
        }

        [Route("api/[controller]")]
        [HttpPut]
        public IActionResult UpdateStudent([FromBody]Student st)
        {
            var student = _repository.GetStudent(st.id);
            if (student != null)
            {
            var newStudent = _repository.UpdateStudent(student, st);

                return Ok(newStudent);
            }
            return NotFound($"Student with Id:{st.id} was not found");
        }

        [Route("api/students-descending")]
        [HttpGet]
        public IActionResult GetListStudentDescending()
        {
            return Ok(_repository.GetListStudentDescending());
        }
    }
}
