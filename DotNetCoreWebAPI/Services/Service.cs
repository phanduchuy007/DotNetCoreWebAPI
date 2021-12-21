using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.Interface;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Services
{
    public class Service
    {
        StudentDataContext _studentData;
        IUnitOfWork _unitOfWork;

        public Service(StudentDataContext studentData, IUnitOfWork unitOfWork)
        {
           _studentData = studentData;
           _unitOfWork = unitOfWork;
        }

        public void AddDataTable(Operation operation)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _unitOfWork.Student.Gets() where st.Email == operation.Email select st).FirstOrDefault();

                    if (student == null)
                    {
                        _unitOfWork.Student.Add(new Student()
                        {
                            Name = operation.Name,
                            Address = operation.Address,
                            Email = operation.Email
                        });
                        _unitOfWork.Submit();

                        var checkStudent = (from st in _unitOfWork.Student.Gets() where st.Email == operation.Email select st).FirstOrDefault();

                        _unitOfWork.Subject.Add(new Subjects()
                        {
                            IDStudent = checkStudent.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _unitOfWork.Submit();
                    }

                    if (student != null)
                    {
                        _unitOfWork.Subject.Add(new Subjects()
                        {
                            IDStudent = student.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _unitOfWork.Submit();
                    }     

                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteDataStudent(int id)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _unitOfWork.Student.Gets() where st.ID == id select st).FirstOrDefault();
                    
                    if (student != null)
                    {
                        var subject = (from sub in _unitOfWork.Subject.Gets() where sub.IDStudent == student.ID select sub).ToList();

                        if (subject != null)
                        {
                            foreach (var sub in subject)
                            {
                                _unitOfWork.Subject.Delete(sub);
                            }
                            _unitOfWork.Submit();

                            _unitOfWork.Student.Delete(student);
                            _unitOfWork.Submit();
                        }

                        if(subject == null)
                        {
                            _unitOfWork.Student.Delete(student);
                            _unitOfWork.Submit();
                        }
                    }
                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<Operation> FilterStudentByMark(double mark)
        {
            var student = from st in _studentData.tblStudent
                          join sub in _studentData.tblSubject on st.ID equals sub.IDStudent
                          where sub.Mark >= mark
                          select new Operation
                          {
                              Name = st.Name,
                              Address = st.Address,
                              Email = st.Email,
                              Subject = sub.Subject,
                              Teacher = sub.Teacher,
                              Classroom = sub.Classroom,
                              Mark = sub.Mark
                          };

            return student;
        }
    }
}
