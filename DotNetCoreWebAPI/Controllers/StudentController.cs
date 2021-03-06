using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.Generic;
using DotNetCoreWebAPI.Dal.Repository.Interface;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Services;
using DotNetCoreWebAPI.StudentData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        // private IStudentData _studentData;
        /*private IStudentRepository _repositoryStudent;
        private ISubjectsRepository _repositorySubjects;*/
        private IUnitOfWork _unitOfWork;
        private Service _service;

        public StudentController(IUnitOfWork unitOfWork, Service service)
        {
            _unitOfWork = unitOfWork;
            _service = service;
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
            _unitOfWork.Submit();

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
                _unitOfWork.Submit();

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
                _unitOfWork.Submit();

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

        [Route("api/add-student-subject")]
        [HttpPost]
        public IActionResult AddDataTable(Operation operation)
        {
            // var dataContext = new StudentDataContext();

            _service.AddDataTable(operation);

            /*_unitOfWork.Student.Add(new Student()
            {
                Name = operation.Name,
                Address = operation.Address,
                Email = operation.Email
            });

            // var student = (from st in _unitOfWork.Student.Gets() where st.Email == operation.Email select st).FirstOrDefault();

            _unitOfWork.Subject.Add(new Subjects()
            {
                IDStudent = student.ID,
                Subject = operation.Subject,
                Teacher = operation.Teacher,
                Classroom = operation.Classroom,
                Mark = operation.Mark
            });

            // SaveChanges
            _unitOfWork.Submit();*/

            return Ok();
        }

        [Route("api/delete-subject/{id}")]
        [HttpGet]
        public IActionResult DeleteSubject(int id)
        {
            var subject = _unitOfWork.Subject.Get(id);
            if (subject != null)
            {
                _unitOfWork.Subject.Delete(subject);
                _unitOfWork.Submit();

                return Ok();
            }
            return NotFound($"Subject with Id:{id} was not found");
        }

        [Route("api/delete-data-student/{id}")]
        [HttpDelete]
        public IActionResult DeleteDataStudent(int id)
        {
            var student = _unitOfWork.Student.Get(id);
            if (student != null)
            {
                _service.DeleteDataStudent(id);

                return Ok();
            }
            return NotFound($"Student with Id:{id} was not found");
        }

        [EnableCors("mypolicy")]
        [Route("api/filter-student-by-mark/{mark}")]
        [HttpGet]
        public IActionResult FilterStudentByMark(double mark)
        {
            var student = _service.FilterStudentByMark(mark);

            if (student != null) return Ok(student);

            return NotFound($"There are no students");
        }
    }
}
