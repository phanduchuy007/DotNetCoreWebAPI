using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Dal.UnitOfWork;
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
        // private IStudentRepository _repositoryStudent;

        // private ISubjectsRepository _repositorySubjects;

        private IUnitOfWork _unitOfWork;

        /*public StudentController(IStudentRepository repositoryStudent, ISubjectsRepository subjectsRepository)
        {
            _repositoryStudent = repositoryStudent;
            _repositorySubjects = subjectsRepository;
        }*/

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // get/student
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_unitOfWork.Student.Gets());
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var student = _unitOfWork.Student.Get(id);

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
            _unitOfWork.Student.Add(student);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Path + "/" + student.ID, student);
        }

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var student = _unitOfWork.Student.Get(id);
            if (student != null)
            {
                _unitOfWork.Student.Delete(student);

                return Ok();
            }
            return NotFound($"Student with Id:{id} was not found");
        }

        [Route("api/[controller]")]
        [HttpPut]
        public IActionResult UpdateStudent([FromBody]Student st)
        {
            var student = _unitOfWork.Student.Get(st.ID);
            if (student != null)
            {
            var newStudent = _unitOfWork.Student.UpdateStudent(student, st);

                return Ok(newStudent);
            }
            return NotFound($"Student with Id:{st.ID} was not found");
        }

        [Route("api/students-ascending")]
        [HttpGet]
        public IActionResult GetListStudentAscending()
        {
            return Ok(_unitOfWork.Student.GetListStudentAscending());
        }

        [Route("api/subject-by-id-student/{id}")]
        [HttpGet]
        public IActionResult GetSubjectByIDStudent(int id) {
            var student = _unitOfWork.Student.Get(id);
            if (student != null)
            {
                return Ok(_unitOfWork.Subject.GetSubjectByIDStudent(id));
            }
            return NotFound($"Subject with Id:{id} was not found");
        } 
    }
}
