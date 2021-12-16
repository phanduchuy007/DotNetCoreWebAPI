using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAPI.Dal.Repository;
using DotNetCoreWebAPI.Dal.Repository.SubjectjRepository;
using DotNetCoreWebAPI.Dal.UnitOfWork;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Services
{
    public class Service
    {
        StudentDataContext _studentData;
        IStudentRepository _studentRepository;
        IUnitOfWork _unitOfWork;

        public Service(StudentDataContext studentData, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
           _studentData = studentData;
           _studentRepository = studentRepository;
           _unitOfWork = unitOfWork;
        }

        public void AddTable(Operation operation)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _studentData.tblStudent.ToList() where st.Email == operation.Email select st).FirstOrDefault();

                    if (student == null)
                    {
                        _studentData.tblStudent.Add(new Student()
                        {
                            Name = operation.Name,
                            Address = operation.Address,
                            Email = operation.Email
                        });
                        _studentData.SaveChanges();

                        var checkStudent = (from st in _studentData.tblStudent.ToList() where st.Email == operation.Email select st).FirstOrDefault();

                        _studentData.tblSubject.Add(new Subjects()
                        {
                            IDStudent = checkStudent.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _studentData.SaveChanges();
                    }

                    if (student != null)
                    {
                        _studentData.tblSubject.Add(new Subjects()
                        {
                            IDStudent = student.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _studentData.SaveChanges();
                    }     

                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                }
            }
        }

        public void DeleteDataStudent(int id)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    var student = (from st in _studentData.tblStudent where st.ID == id select st).FirstOrDefault();
                    
                    if (student != null)
                    {
                        var subject = (from sub in _studentData.tblSubject.ToList() where sub.IDStudent == student.ID select sub).ToList();

                        if (subject != null)
                        {
                            foreach (var sub in subject)
                            {
                                _studentData.tblSubject.Remove(sub);
                                /*var a = ((StudentRepository)_studentRepository)._studentContext;
                                var b = _studentData;*/
                            }                         
                            _studentData.SaveChanges();

                            _studentData.tblStudent.Remove(student);
                            _studentData.SaveChanges();
                        }
                        else
                        {
                            _studentData.tblStudent.Remove(student);
                            _studentData.SaveChanges();
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
    }
}
