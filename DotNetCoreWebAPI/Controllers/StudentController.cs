using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
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
        private IStudentRepository _repositoryStudent;

        private ISubjectsRepository _repositorySubjects;

        public StudentController(IStudentRepository repositoryStudent, ISubjectsRepository subjectsRepository)
        {
            _repositoryStudent = repositoryStudent;
            _repositorySubjects = subjectsRepository;
        }

        // get/student
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_repositoryStudent.Gets());
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var student = _repositoryStudent.Get(id);

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
            _repositoryStudent.Add(student);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Path + "/" + student.ID, student);
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var student = _repositoryStudent.Get(id);
            if (student != null)
            {
                _repositoryStudent.Delete(student);

                return Ok();
            }
            return NotFound($"Student with Id:{id} was not found");
        }

        [Route("api/[controller]")]
        [HttpPut]
        public IActionResult UpdateStudent([FromBody]Student st)
        {
            var student = _repositoryStudent.Get(st.ID);
            if (student != null)
            {
            var newStudent = _repositoryStudent.UpdateStudent(student, st);

                return Ok(newStudent);
            }
            return NotFound($"Student with Id:{st.ID} was not found");
        }

        [Route("api/students-ascending")]
        [HttpGet]
        public IActionResult GetListStudentDescending()
        {
            return Ok(_repositoryStudent.GetListStudentDescending());
        }

        [Route("api/subject-by-id-student/{id}")]
        [HttpGet]
        public IActionResult GetSubjectByIDStudent(int id) {
            var student = _repositoryStudent.Get(id);
            if (student != null)
            {
                return Ok(_repositorySubjects.GetSubjectByIDStudent(id));
            }
            return NotFound($"Subject with Id:{id} was not found");
        } 
    }
}
